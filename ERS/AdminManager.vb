Imports MySql.Data.MySqlClient

Public NotInheritable Class AdminManager
    Private Sub New()
    End Sub

    ''' <summary>Validate admin credentials; returns full_name on success, Nothing on failure.</summary>
    Public Shared Function ValidateLogin(username As String, password As String) As String
        Dim hash As String = HashHelper.ComputeSHA256(password)

        Using conn = DBConnection.GetConnection(), cmd As New MySqlCommand(
            "SELECT full_name FROM admins WHERE username = @u AND password_hash = @h", conn)
            cmd.Parameters.AddWithValue("@u", username)
            cmd.Parameters.AddWithValue("@h", hash)
            conn.Open()
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing Then Return result.ToString()
            Return Nothing
        End Using
    End Function

    ''' <summary>Auto-flag overdue rentals.</summary>
    Public Shared Sub UpdateOverdueRentals()
        Using conn = DBConnection.GetConnection(), cmd As New MySqlCommand(
            "UPDATE rentals SET status = 'Overdue' WHERE rental_end < CURDATE() AND status = 'Active'", conn)
            conn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ''' <summary>Dashboard statistics.</summary>
    Public Shared Function GetStats() As (Active As Integer, Overdue As Integer, TodayBookings As Integer)
        Dim a, o, t As Integer
        Using conn = DBConnection.GetConnection()
            conn.Open()
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM rentals WHERE status='Active'", conn)
                a = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM rentals WHERE status='Overdue'", conn)
                o = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM rentals WHERE DATE(created_at) = CURDATE()", conn)
                t = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
        Return (a, o, t)
    End Function

    ''' <summary>Load rentals, optionally filtered by status.</summary>
    Public Shared Function LoadRentals(Optional statusFilter As String = Nothing) As DataTable
        Dim sql = "SELECT r.rental_id, r.booking_code, c.full_name, c.contact_no, " &
                  "r.rental_start, r.rental_end, r.subtotal, r.total_amount, r.status " &
                  "FROM rentals r INNER JOIN customers c ON r.customer_id = c.customer_id"
        If Not String.IsNullOrEmpty(statusFilter) Then
            sql &= " WHERE r.status = @st"
        End If
        sql &= " ORDER BY r.created_at DESC"

        Dim dt As New DataTable()
        Using conn = DBConnection.GetConnection(), cmd As New MySqlCommand(sql, conn)
            If Not String.IsNullOrEmpty(statusFilter) Then
                cmd.Parameters.AddWithValue("@st", statusFilter)
            End If
            conn.Open()
            Using da As New MySqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    ''' <summary>Mark rental as Returned and restore stock – both in one transaction.</summary>
    Public Shared Sub ReturnRental(rentalId As Integer)
        Using conn = DBConnection.GetConnection()
            conn.Open()
            Using trx = conn.BeginTransaction()
                Try
                    ' Restore stock for each line item
                    Using cmd As New MySqlCommand(
                        "UPDATE equipment e INNER JOIN rental_details d ON e.equipment_id = d.equipment_id " &
                        "SET e.avail_stock = e.avail_stock + d.quantity WHERE d.rental_id = @rid", conn, trx)
                        cmd.Parameters.AddWithValue("@rid", rentalId)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' Set status
                    Using cmd As New MySqlCommand(
                        "UPDATE rentals SET status = 'Returned' WHERE rental_id = @rid", conn, trx)
                        cmd.Parameters.AddWithValue("@rid", rentalId)
                        cmd.ExecuteNonQuery()
                    End Using

                    trx.Commit()
                Catch
                    trx.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Sub

    ''' <summary>Cancel an active/overdue rental and restore stock – both in one transaction.</summary>
    Public Shared Sub CancelRental(rentalId As Integer)
        Using conn = DBConnection.GetConnection()
            conn.Open()
            Using trx = conn.BeginTransaction()
                Try
                    ' Restore stock for each line item
                    Using cmd As New MySqlCommand(
                        "UPDATE equipment e INNER JOIN rental_details d ON e.equipment_id = d.equipment_id " &
                        "SET e.avail_stock = e.avail_stock + d.quantity WHERE d.rental_id = @rid", conn, trx)
                        cmd.Parameters.AddWithValue("@rid", rentalId)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' Set status to Cancelled
                    Using cmd As New MySqlCommand(
                        "UPDATE rentals SET status = 'Cancelled' WHERE rental_id = @rid", conn, trx)
                        cmd.Parameters.AddWithValue("@rid", rentalId)
                        cmd.ExecuteNonQuery()
                    End Using

                    trx.Commit()
                Catch
                    trx.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Sub

    ' ==================== EQUIPMENT CRUD ====================

    Public Shared Function LoadAllEquipment() As DataTable
        Dim dt As New DataTable()
        Using conn = DBConnection.GetConnection(), cmd As New MySqlCommand(
            "SELECT equipment_id, name, category, daily_rate, total_stock, avail_stock, icon_tag, is_active FROM equipment ORDER BY name", conn)
            conn.Open()
            Using da As New MySqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    Public Shared Sub AddEquipment(name As String, category As String, dailyRate As Decimal, stock As Integer, icon As String)
        Using conn = DBConnection.GetConnection(), cmd As New MySqlCommand(
            "INSERT INTO equipment (name, category, daily_rate, total_stock, avail_stock, icon_tag) VALUES (@n,@c,@r,@s,@s,@i)", conn)
            cmd.Parameters.AddWithValue("@n", name)
            cmd.Parameters.AddWithValue("@c", category)
            cmd.Parameters.AddWithValue("@r", dailyRate)
            cmd.Parameters.AddWithValue("@s", stock)
            cmd.Parameters.AddWithValue("@i", icon)
            conn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Shared Sub UpdateEquipment(eqId As Integer, name As String, category As String, dailyRate As Decimal, totalStock As Integer, icon As String)
        Using conn = DBConnection.GetConnection(), cmd As New MySqlCommand(
            "UPDATE equipment SET name=@n, category=@c, daily_rate=@r, total_stock=@ts, " &
            "avail_stock = avail_stock + (@ts - total_stock), icon_tag=@i WHERE equipment_id=@id", conn)
            cmd.Parameters.AddWithValue("@n", name)
            cmd.Parameters.AddWithValue("@c", category)
            cmd.Parameters.AddWithValue("@r", dailyRate)
            cmd.Parameters.AddWithValue("@ts", totalStock)
            cmd.Parameters.AddWithValue("@i", icon)
            cmd.Parameters.AddWithValue("@id", eqId)
            conn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Shared Sub DeleteEquipment(eqId As Integer)
        Using conn = DBConnection.GetConnection(), cmd As New MySqlCommand(
            "UPDATE equipment SET is_active = 0 WHERE equipment_id = @id", conn)
            cmd.Parameters.AddWithValue("@id", eqId)
            conn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
End Class
