Public Class FrmAdminDashboard
    Inherits Form

    Private ReadOnly BgColor As Color = ColorTranslator.FromHtml("#F0F2F5")
    Private ReadOnly NavyColor As Color = ColorTranslator.FromHtml("#1E3A5F")
    Private ReadOnly OrangeColor As Color = ColorTranslator.FromHtml("#F47C20")
    Private ReadOnly GreenColor As Color = ColorTranslator.FromHtml("#1A5C2A")

    Private lblActive As Label
    Private lblOverdue As Label
    Private lblToday As Label
    Private dgvRentals As DataGridView
    Private cmbFilter As ComboBox
    Private btnReturn As Button
    Private btnCancel As Button
    Private btnEquipment As Button

    Public Sub New()
        Text = "Admin Dashboard — 2C Rentals"
        Size = New Size(1100, 700)
        StartPosition = FormStartPosition.CenterParent
        BackColor = BgColor
        Font = New Font("Segoe UI", 10)
        MinimumSize = New Size(900, 550)
        InitUI()
    End Sub

    Private Sub InitUI()
        ' ── Header ──
        Dim pnlHead As New Panel With {.Dock = DockStyle.Top, .Height = 52, .BackColor = NavyColor}
        Dim lblT As New Label With {
            .Text = "📊  Admin Dashboard", .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 14, FontStyle.Bold),
            .AutoSize = True, .Location = New Point(16, 12)}
        pnlHead.Controls.Add(lblT)

        btnEquipment = New Button With {
            .Text = "Manage Equipment", .Size = New Size(170, 34),
            .Location = New Point(880, 9), .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
            .FlatStyle = FlatStyle.Flat, .BackColor = OrangeColor, .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold), .Cursor = Cursors.Hand}
        btnEquipment.FlatAppearance.BorderSize = 0
        AddHandler btnEquipment.Click, Sub()
                                           Dim frm As New FrmManageEquipment()
                                           frm.ShowDialog(Me)
                                       End Sub
        pnlHead.Controls.Add(btnEquipment)
        Controls.Add(pnlHead)

        ' ── Stat cards ──
        Dim pnlStats As New FlowLayoutPanel With {
            .Dock = DockStyle.Top, .Height = 90, .Padding = New Padding(12, 10, 12, 4),
            .BackColor = BgColor, .WrapContents = False}
        lblActive = MakeStatCard(pnlStats, "Active Rentals", "0", NavyColor)
        lblOverdue = MakeStatCard(pnlStats, "Overdue", "0", Color.Crimson)
        lblToday = MakeStatCard(pnlStats, "Today's Bookings", "0", GreenColor)
        Controls.Add(pnlStats)

        ' ── Toolbar ──
        Dim pnlTool As New Panel With {.Dock = DockStyle.Top, .Height = 44, .BackColor = BgColor,
            .Padding = New Padding(14, 6, 14, 6)}
        Dim lblFilt As New Label With {.Text = "Filter:", .AutoSize = True,
            .Location = New Point(14, 12), .ForeColor = NavyColor,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)}
        pnlTool.Controls.Add(lblFilt)

        cmbFilter = New ComboBox With {
            .DropDownStyle = ComboBoxStyle.DropDownList,
            .Location = New Point(70, 8), .Size = New Size(150, 28)}
        cmbFilter.Items.AddRange({"All", "Active", "Overdue", "Returned", "Cancelled"})
        cmbFilter.SelectedIndex = 0
        AddHandler cmbFilter.SelectedIndexChanged, AddressOf CmbFilter_Changed
        pnlTool.Controls.Add(cmbFilter)

        btnReturn = New Button With {
            .Text = "Mark as Returned", .Size = New Size(160, 30),
            .Location = New Point(240, 7), .FlatStyle = FlatStyle.Flat,
            .BackColor = GreenColor, .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold), .Cursor = Cursors.Hand}
        btnReturn.FlatAppearance.BorderSize = 0
        AddHandler btnReturn.Click, AddressOf BtnReturn_Click
        pnlTool.Controls.Add(btnReturn)

        btnCancel = New Button With {
            .Text = "Cancel Rental", .Size = New Size(140, 30),
            .Location = New Point(410, 7), .FlatStyle = FlatStyle.Flat,
            .BackColor = Color.Crimson, .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold), .Cursor = Cursors.Hand}
        btnCancel.FlatAppearance.BorderSize = 0
        AddHandler btnCancel.Click, AddressOf BtnCancel_Click
        pnlTool.Controls.Add(btnCancel)
        Controls.Add(pnlTool)

        ' ── Data grid ──
        dgvRentals = New DataGridView With {
            .Dock = DockStyle.Fill, .ReadOnly = True,
            .AllowUserToAddRows = False, .AllowUserToDeleteRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            .BackgroundColor = Color.White, .BorderStyle = BorderStyle.None,
            .RowHeadersVisible = False, .Font = New Font("Segoe UI", 9)}
        dgvRentals.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 230, 245)
        dgvRentals.DefaultCellStyle.SelectionForeColor = NavyColor
        dgvRentals.ColumnHeadersDefaultCellStyle.BackColor = NavyColor
        dgvRentals.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvRentals.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        dgvRentals.EnableHeadersVisualStyles = False
        Controls.Add(dgvRentals)
        dgvRentals.BringToFront()
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        RefreshData()
    End Sub

    Private Sub RefreshData()
        Try
            AdminManager.UpdateOverdueRentals()
            Dim stats = AdminManager.GetStats()
            lblActive.Text = stats.Active.ToString()
            lblOverdue.Text = stats.Overdue.ToString()
            lblToday.Text = stats.TodayBookings.ToString()

            Dim filter As String = Nothing
            If cmbFilter.SelectedItem IsNot Nothing AndAlso cmbFilter.SelectedItem.ToString() <> "All" Then
                filter = cmbFilter.SelectedItem.ToString()
            End If
            dgvRentals.DataSource = AdminManager.LoadRentals(filter)
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmbFilter_Changed(sender As Object, e As EventArgs)
        RefreshData()
    End Sub

    Private Sub BtnReturn_Click(sender As Object, e As EventArgs)
        If dgvRentals.CurrentRow Is Nothing Then
            MessageBox.Show("Select a rental first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim status = dgvRentals.CurrentRow.Cells("status").Value.ToString()
        If status = "Returned" OrElse status = "Cancelled" Then
            MessageBox.Show($"This rental is already {status}.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim rentalId = Convert.ToInt32(dgvRentals.CurrentRow.Cells("rental_id").Value)
        Dim code = dgvRentals.CurrentRow.Cells("booking_code").Value.ToString()

        If MessageBox.Show($"Mark {code} as Returned? Stock will be restored.",
                           "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                AdminManager.ReturnRental(rentalId)
                MessageBox.Show("Rental marked as Returned.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                RefreshData()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs)
        If dgvRentals.CurrentRow Is Nothing Then
            MessageBox.Show("Select a rental first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim status = dgvRentals.CurrentRow.Cells("status").Value.ToString()
        If status = "Returned" Then
            MessageBox.Show("Cannot cancel a rental that has already been returned.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If status = "Cancelled" Then
            MessageBox.Show("This rental is already cancelled.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim rentalId = Convert.ToInt32(dgvRentals.CurrentRow.Cells("rental_id").Value)
        Dim code = dgvRentals.CurrentRow.Cells("booking_code").Value.ToString()

        If MessageBox.Show($"Cancel booking {code}? Stock will be restored and this cannot be undone.",
                           "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                AdminManager.CancelRental(rentalId)
                MessageBox.Show($"Booking {code} has been cancelled and stock restored.", "Cancelled",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                RefreshData()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Function MakeStatCard(host As FlowLayoutPanel, title As String, value As String, accent As Color) As Label
        Dim card As New Panel With {
            .Size = New Size(210, 68), .BackColor = Color.White,
            .Margin = New Padding(6, 4, 6, 4)}
        Dim lblT As New Label With {
            .Text = title, .Location = New Point(12, 8), .AutoSize = True,
            .Font = New Font("Segoe UI", 8), .ForeColor = Color.Gray}
        Dim lblV As New Label With {
            .Text = value, .Location = New Point(12, 30), .AutoSize = True,
            .Font = New Font("Segoe UI", 20, FontStyle.Bold), .ForeColor = accent}
        card.Controls.AddRange({lblT, lblV})
        host.Controls.Add(card)
        Return lblV
    End Function
End Class
