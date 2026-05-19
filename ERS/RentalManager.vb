Imports MySql.Data.MySqlClient

Public NotInheritable Class RentalManager
    Private Sub New()
    End Sub

    ''' <summary>Load all active equipment, optionally filtered by category.</summary>
    Public Shared Function LoadEquipment(Optional category As String = Nothing) As List(Of EquipmentItem)
        Dim items As New List(Of EquipmentItem)()
        Dim sql As String = "SELECT equipment_id, name, category, daily_rate, total_stock, avail_stock, icon_tag, is_active FROM equipment WHERE is_active = 1"
        If Not String.IsNullOrEmpty(category) Then
            sql &= " AND category = @cat"
        End If
        sql &= " ORDER BY name"

        Using conn = DBConnection.GetConnection(), cmd As New MySqlCommand(sql, conn)
            If Not String.IsNullOrEmpty(category) Then
                cmd.Parameters.AddWithValue("@cat", category)
            End If
            conn.Open()
            Using rdr = cmd.ExecuteReader()
                While rdr.Read()
                    items.Add(New EquipmentItem With {
                        .EquipmentId = rdr.GetInt32("equipment_id"),
                        .Name = rdr.GetString("name"),
                        .Category = rdr.GetString("category"),
                        .DailyRate = rdr.GetDecimal("daily_rate"),
                        .TotalStock = rdr.GetInt32("total_stock"),
                        .AvailStock = rdr.GetInt32("avail_stock"),
                        .IconTag = rdr.GetString("icon_tag"),
                        .IsActive = rdr.GetBoolean("is_active")
                    })
                End While
            End Using
        End Using
        Return items
    End Function

    ''' <summary>
    ''' Atomic booking: insert customer → rental → details → deduct stock.
    ''' Returns the booking code on success, or throws on failure.
    ''' </summary>
    Public Shared Function CreateBooking(
            fullName As String,
            contactNo As String,
            rentalStart As Date,
            rentalEnd As Date,
            cart As List(Of CartItem)) As String

        Dim days As Integer = (rentalEnd - rentalStart).Days
        If days <= 0 Then Throw New ArgumentException("End date must be after start date.")
        If cart.Count = 0 Then Throw New ArgumentException("Cart is empty.")

        Dim subtotal As Decimal = 0D
        For Each ci In cart
            subtotal += ci.Equipment.DailyRate * ci.Quantity * days
        Next
        Dim securityDep As Decimal = 500D
        Dim total As Decimal = subtotal + securityDep

        Dim bookingCode As String = GenerateBookingCode()

        Using conn = DBConnection.GetConnection()
            conn.Open()
            Using trx = conn.BeginTransaction()
                Try
                    ' 1. Insert customer
                    Dim custId As Long
                    Using cmd As New MySqlCommand(
                        "INSERT INTO customers (full_name, contact_no) VALUES (@fn, @cn); SELECT LAST_INSERT_ID();", conn, trx)
                        cmd.Parameters.AddWithValue("@fn", fullName)
                        cmd.Parameters.AddWithValue("@cn", contactNo)
                        custId = Convert.ToInt64(cmd.ExecuteScalar())
                    End Using

                    ' 2. Insert rental header
                    Dim rentalId As Long
                    Using cmd As New MySqlCommand(
                        "INSERT INTO rentals (booking_code, customer_id, rental_start, rental_end, security_dep, subtotal, total_amount, status) " &
                        "VALUES (@bc, @cid, @rs, @re, @sd, @sub, @tot, 'Active'); SELECT LAST_INSERT_ID();", conn, trx)
                        cmd.Parameters.AddWithValue("@bc", bookingCode)
                        cmd.Parameters.AddWithValue("@cid", custId)
                        cmd.Parameters.AddWithValue("@rs", rentalStart)
                        cmd.Parameters.AddWithValue("@re", rentalEnd)
                        cmd.Parameters.AddWithValue("@sd", securityDep)
                        cmd.Parameters.AddWithValue("@sub", subtotal)
                        cmd.Parameters.AddWithValue("@tot", total)
                        rentalId = Convert.ToInt64(cmd.ExecuteScalar())
                    End Using

                    ' 3. Insert each line item & deduct stock
                    For Each ci In cart
                        Dim lineTotal As Decimal = ci.Equipment.DailyRate * ci.Quantity * days

                        Using cmd As New MySqlCommand(
                            "INSERT INTO rental_details (rental_id, equipment_id, quantity, daily_rate, days_rented, line_total) " &
                            "VALUES (@rid, @eid, @qty, @rate, @days, @lt)", conn, trx)
                            cmd.Parameters.AddWithValue("@rid", rentalId)
                            cmd.Parameters.AddWithValue("@eid", ci.Equipment.EquipmentId)
                            cmd.Parameters.AddWithValue("@qty", ci.Quantity)
                            cmd.Parameters.AddWithValue("@rate", ci.Equipment.DailyRate)
                            cmd.Parameters.AddWithValue("@days", days)
                            cmd.Parameters.AddWithValue("@lt", lineTotal)
                            cmd.ExecuteNonQuery()
                        End Using

                        ' Deduct available stock – abort if insufficient
                        Using cmd As New MySqlCommand(
                            "UPDATE equipment SET avail_stock = avail_stock - @qty WHERE equipment_id = @eid AND avail_stock >= @qty", conn, trx)
                            cmd.Parameters.AddWithValue("@qty", ci.Quantity)
                            cmd.Parameters.AddWithValue("@eid", ci.Equipment.EquipmentId)
                            Dim affected = cmd.ExecuteNonQuery()
                            If affected = 0 Then
                                Throw New InvalidOperationException($"Insufficient stock for {ci.Equipment.Name}.")
                            End If
                        End Using
                    Next

                    trx.Commit()
                    Return bookingCode

                Catch
                    trx.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Function

    Private Shared Function GenerateBookingCode() As String
        Dim datePart As String = DateTime.Now.ToString("yyyyMMdd")
        Dim seq As Integer = 1

        Using conn = DBConnection.GetConnection(), cmd As New MySqlCommand(
            "SELECT COUNT(*) FROM rentals WHERE booking_code LIKE @pat", conn)
            cmd.Parameters.AddWithValue("@pat", $"BK-{datePart}-%")
            conn.Open()
            seq = Convert.ToInt32(cmd.ExecuteScalar()) + 1
        End Using

        Return $"BK-{datePart}-{seq:D4}"
    End Function
End Class
