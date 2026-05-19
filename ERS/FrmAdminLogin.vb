Public Class FrmAdminLogin
    Inherits Form

    Private ReadOnly NavyColor As Color = ColorTranslator.FromHtml("#1E3A5F")
    Private ReadOnly OrangeColor As Color = ColorTranslator.FromHtml("#F47C20")

    Private txtUser As TextBox
    Private txtPass As TextBox

    Public Sub New()
        Text = "Admin Login — 2C Rentals"
        Size = New Size(380, 300)
        StartPosition = FormStartPosition.CenterParent
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        BackColor = Color.White
        Font = New Font("Segoe UI", 10)

        Dim y = 20

        Dim lblH As New Label With {
            .Text = "🔒  Admin Login", .Location = New Point(20, y),
            .AutoSize = True, .Font = New Font("Segoe UI", 14, FontStyle.Bold),
            .ForeColor = NavyColor}
        Controls.Add(lblH)
        y += 48

        Controls.Add(New Label With {
            .Text = "Username", .Location = New Point(20, y),
            .AutoSize = True, .ForeColor = NavyColor,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)})
        y += 24
        txtUser = New TextBox With {.Location = New Point(20, y), .Size = New Size(320, 30)}
        Controls.Add(txtUser)
        y += 38

        Controls.Add(New Label With {
            .Text = "Password", .Location = New Point(20, y),
            .AutoSize = True, .ForeColor = NavyColor,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)})
        y += 24
        txtPass = New TextBox With {.Location = New Point(20, y), .Size = New Size(320, 30),
            .UseSystemPasswordChar = True}
        Controls.Add(txtPass)
        y += 46

        Dim btnLogin As New Button With {
            .Text = "LOGIN", .Size = New Size(320, 44),
            .Location = New Point(20, y), .FlatStyle = FlatStyle.Flat,
            .BackColor = NavyColor, .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .Cursor = Cursors.Hand}
        btnLogin.FlatAppearance.BorderSize = 0
        AddHandler btnLogin.Click, AddressOf BtnLogin_Click
        Controls.Add(btnLogin)

        AcceptButton = btnLogin
    End Sub

    Private Sub BtnLogin_Click(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(txtUser.Text) OrElse String.IsNullOrWhiteSpace(txtPass.Text) Then
            MessageBox.Show("Please enter username and password.", "Validation",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim fullName = AdminManager.ValidateLogin(txtUser.Text.Trim(), txtPass.Text)
            If fullName IsNot Nothing Then
                DialogResult = DialogResult.OK
                Close()
            Else
                MessageBox.Show("Invalid credentials.", "Login Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Login error: " & ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
