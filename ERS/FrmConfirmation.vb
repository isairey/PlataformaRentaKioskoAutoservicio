Public Class FrmConfirmation
    Inherits Form

    Private ReadOnly NavyColor As Color = ColorTranslator.FromHtml("#1E3A5F")
    Private ReadOnly GreenColor As Color = ColorTranslator.FromHtml("#1A5C2A")

    Public Sub New(bookingCode As String, customerName As String, grandTotal As Decimal)
        Text = "Booking Confirmed — 2C Rentals"
        Size = New Size(480, 400)
        StartPosition = FormStartPosition.CenterParent
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        BackColor = Color.White
        Font = New Font("Segoe UI", 10)

        Dim y = 30

        Dim lblCheck As New Label With {
            .Text = "✅", .Font = New Font("Segoe UI Emoji", 40),
            .Size = New Size(ClientSize.Width, 60),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(0, y)}
        Controls.Add(lblCheck)
        y += 70

        Dim lblMsg As New Label With {
            .Text = "Booking Confirmed!", .AutoSize = False,
            .Size = New Size(ClientSize.Width, 32),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Font = New Font("Segoe UI", 16, FontStyle.Bold),
            .ForeColor = GreenColor, .Location = New Point(0, y)}
        Controls.Add(lblMsg)
        y += 44

        Dim lblCode As New Label With {
            .Text = bookingCode, .AutoSize = False,
            .Size = New Size(ClientSize.Width, 36),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Font = New Font("Consolas", 18, FontStyle.Bold),
            .ForeColor = NavyColor, .Location = New Point(0, y)}
        Controls.Add(lblCode)
        y += 44

        Dim lblName As New Label With {
            .Text = $"Customer: {customerName}", .AutoSize = False,
            .Size = New Size(ClientSize.Width, 24),
            .TextAlign = ContentAlignment.MiddleCenter,
            .ForeColor = Color.DimGray, .Location = New Point(0, y)}
        Controls.Add(lblName)
        y += 28

        Dim lblTot As New Label With {
            .Text = $"Total Amount: ₱{grandTotal:N2}", .AutoSize = False,
            .Size = New Size(ClientSize.Width, 28),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = NavyColor, .Location = New Point(0, y)}
        Controls.Add(lblTot)
        y += 44

        Dim lblCashier As New Label With {
            .Text = "Please proceed to the cashier for payment.",
            .AutoSize = False, .Size = New Size(ClientSize.Width, 24),
            .TextAlign = ContentAlignment.MiddleCenter,
            .ForeColor = Color.Gray, .Font = New Font("Segoe UI", 10, FontStyle.Italic),
            .Location = New Point(0, y)}
        Controls.Add(lblCashier)
        y += 36

        Dim btnDone As New Button With {
            .Text = "DONE", .Size = New Size(200, 44),
            .Location = New Point((ClientSize.Width - 200) \ 2, y),
            .FlatStyle = FlatStyle.Flat, .BackColor = NavyColor,
            .ForeColor = Color.White, .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .Cursor = Cursors.Hand}
        btnDone.FlatAppearance.BorderSize = 0
        AddHandler btnDone.Click, Sub() Me.Close()
        Controls.Add(btnDone)
    End Sub
End Class
