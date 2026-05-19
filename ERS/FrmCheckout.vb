Public Class FrmCheckout
    Inherits Form

    Private ReadOnly BgColor As Color = ColorTranslator.FromHtml("#F0F2F5")
    Private ReadOnly NavyColor As Color = ColorTranslator.FromHtml("#1E3A5F")
    Private ReadOnly OrangeColor As Color = ColorTranslator.FromHtml("#F47C20")

    Private _cart As List(Of CartItem)
    Private _initialDays As Integer = 1
    Private txtName As TextBox
    Private txtContact As TextBox
    Private dtpStart As DateTimePicker
    Private dtpEnd As DateTimePicker
    Private lblSummary As Label
    Private lblGrandTotal As Label
    Private btnConfirm As Button

    Public Sub New(cart As List(Of CartItem), days As Integer)
        _cart = cart
        _initialDays = days
        InitUI()
    End Sub

    Private Sub InitUI()
        Text = "Checkout — 2C Rentals"
        Size = New Size(560, 620)
        StartPosition = FormStartPosition.CenterParent
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        BackColor = BgColor
        Font = New Font("Segoe UI", 10)

        Dim y = 16

        ' Header
        Dim lblH As New Label With {
            .Text = "📋  Checkout", .Location = New Point(20, y),
            .AutoSize = True, .Font = New Font("Segoe UI", 16, FontStyle.Bold),
            .ForeColor = NavyColor}
        Controls.Add(lblH)
        y += 48

        ' Name
        Controls.Add(MakeLabel("Full Name", 20, y))
        y += 24
        txtName = New TextBox With {.Location = New Point(20, y), .Size = New Size(500, 30)}
        Controls.Add(txtName)
        y += 38

        ' Contact
        Controls.Add(MakeLabel("Contact No.", 20, y))
        y += 24
        txtContact = New TextBox With {.Location = New Point(20, y), .Size = New Size(500, 30)}
        Controls.Add(txtContact)
        y += 38

        ' Dates
        Controls.Add(MakeLabel("Rental Start Date", 20, y))
        y += 24
        dtpStart = New DateTimePicker With {
            .Location = New Point(20, y), .Size = New Size(240, 30),
            .Format = DateTimePickerFormat.Short, .MinDate = Date.Today}
        Controls.Add(dtpStart)
        AddHandler dtpStart.ValueChanged, AddressOf Dates_Changed

        Controls.Add(MakeLabel("Rental End Date", 270, y - 24))
        dtpEnd = New DateTimePicker With {
            .Location = New Point(270, y), .Size = New Size(250, 30),
            .Format = DateTimePickerFormat.Short, .Value = Date.Today.AddDays(_initialDays),
            .MinDate = Date.Today.AddDays(1)}
        Controls.Add(dtpEnd)
        AddHandler dtpEnd.ValueChanged, AddressOf Dates_Changed
        y += 44

        ' Separator
        Dim sep As New Label With {.BorderStyle = BorderStyle.Fixed3D, .Height = 2,
            .Width = 500, .Location = New Point(20, y)}
        Controls.Add(sep)
        y += 12

        ' Summary
        Controls.Add(MakeLabel("Booking Summary", 20, y))
        y += 24
        lblSummary = New Label With {
            .Location = New Point(20, y), .Size = New Size(500, 120),
            .Font = New Font("Segoe UI", 9), .ForeColor = Color.DimGray,
            .BackColor = Color.White}
        Controls.Add(lblSummary)
        y += 128

        ' Grand total
        lblGrandTotal = New Label With {
            .Location = New Point(20, y), .AutoSize = True,
            .Font = New Font("Segoe UI", 14, FontStyle.Bold),
            .ForeColor = NavyColor}
        Controls.Add(lblGrandTotal)
        y += 44

        ' Confirm
        btnConfirm = New Button With {
            .Text = "CONFIRM BOOKING", .Size = New Size(500, 48),
            .Location = New Point(20, y), .FlatStyle = FlatStyle.Flat,
            .BackColor = OrangeColor, .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .Cursor = Cursors.Hand}
        btnConfirm.FlatAppearance.BorderSize = 0
        AddHandler btnConfirm.Click, AddressOf BtnConfirm_Click
        Controls.Add(btnConfirm)

        UpdateSummary()
    End Sub

    Private Sub Dates_Changed(sender As Object, e As EventArgs)
        If dtpEnd.Value <= dtpStart.Value Then
            dtpEnd.Value = dtpStart.Value.AddDays(1)
        End If
        UpdateSummary()
    End Sub

    Private Sub UpdateSummary()
        Dim days As Integer = (dtpEnd.Value.Date - dtpStart.Value.Date).Days
        If days <= 0 Then days = 1

        Dim sb As New System.Text.StringBuilder()
        Dim subtotal As Decimal = 0D
        For Each ci In _cart
            Dim lt = ci.Equipment.DailyRate * ci.Quantity * days
            subtotal += lt
            sb.AppendLine($"  {ci.Equipment.Name}  ×{ci.Quantity}  ({days}d)  =  ₱{lt:N2}")
        Next
        lblSummary.Text = sb.ToString()
        lblGrandTotal.Text = $"Grand Total: ₱{subtotal + 500D:N2}  (incl. ₱500 deposit)"
    End Sub

    Private Sub BtnConfirm_Click(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(txtName.Text) Then
            MessageBox.Show("Please enter your name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If String.IsNullOrWhiteSpace(txtContact.Text) Then
            MessageBox.Show("Please enter your contact number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If dtpEnd.Value.Date <= dtpStart.Value.Date Then
            MessageBox.Show("End date must be after start date.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim bookingCode = RentalManager.CreateBooking(
                txtName.Text.Trim(), txtContact.Text.Trim(),
                dtpStart.Value.Date, dtpEnd.Value.Date, _cart)

            Dim days = (dtpEnd.Value.Date - dtpStart.Value.Date).Days
            Dim subtotal As Decimal = _cart.Sum(Function(ci) ci.Equipment.DailyRate * ci.Quantity * days)

            Dim confirm As New FrmConfirmation(bookingCode, txtName.Text.Trim(), subtotal + 500D)
            Me.DialogResult = DialogResult.OK
            confirm.ShowDialog(Me.Owner)
            Me.Close()

        Catch ex As InvalidOperationException
            MessageBox.Show(ex.Message, "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Booking failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function MakeLabel(txt As String, x As Integer, y As Integer) As Label
        Return New Label With {
            .Text = txt, .Location = New Point(x, y), .AutoSize = True,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold), .ForeColor = NavyColor}
    End Function
End Class
