Public Class FrmManageEquipment
    Inherits Form

    Private ReadOnly BgColor As Color = ColorTranslator.FromHtml("#F0F2F5")
    Private ReadOnly NavyColor As Color = ColorTranslator.FromHtml("#1E3A5F")
    Private ReadOnly OrangeColor As Color = ColorTranslator.FromHtml("#F47C20")

    Private dgv As DataGridView
    Private txtName As TextBox
    Private cmbCategory As ComboBox
    Private txtRate As TextBox
    Private txtStock As TextBox
    Private txtIcon As TextBox
    Private btnAdd As Button
    Private btnUpdate As Button
    Private btnDelete As Button

    Public Sub New()
        Text = "Manage Equipment — 2C Rentals"
        Size = New Size(900, 600)
        StartPosition = FormStartPosition.CenterParent
        BackColor = BgColor
        Font = New Font("Segoe UI", 10)
        MinimumSize = New Size(750, 500)
        InitUI()
    End Sub

    Private Sub InitUI()
        ' ── Header ──
        Dim pnlHead As New Panel With {.Dock = DockStyle.Top, .Height = 48, .BackColor = NavyColor}
        pnlHead.Controls.Add(New Label With {
            .Text = "🛠  Equipment Inventory", .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 13, FontStyle.Bold),
            .AutoSize = True, .Location = New Point(14, 10)})
        Controls.Add(pnlHead)

        ' ── Form panel (bottom) ──
        Dim pnlForm As New Panel With {.Dock = DockStyle.Bottom, .Height = 140, .BackColor = Color.White,
            .Padding = New Padding(14)}
        Dim y = 10

        pnlForm.Controls.Add(New Label With {.Text = "Name", .Location = New Point(12, y), .AutoSize = True,
            .Font = New Font("Segoe UI", 8, FontStyle.Bold), .ForeColor = NavyColor})
        txtName = New TextBox With {.Location = New Point(12, y + 18), .Size = New Size(200, 28)}
        pnlForm.Controls.Add(txtName)

        pnlForm.Controls.Add(New Label With {.Text = "Category", .Location = New Point(222, y), .AutoSize = True,
            .Font = New Font("Segoe UI", 8, FontStyle.Bold), .ForeColor = NavyColor})
        cmbCategory = New ComboBox With {.Location = New Point(222, y + 18), .Size = New Size(130, 28),
            .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbCategory.Items.AddRange({"Seating", "Tables", "Audio/Visual"})
        cmbCategory.SelectedIndex = 0
        pnlForm.Controls.Add(cmbCategory)

        pnlForm.Controls.Add(New Label With {.Text = "Rate/Day", .Location = New Point(362, y), .AutoSize = True,
            .Font = New Font("Segoe UI", 8, FontStyle.Bold), .ForeColor = NavyColor})
        txtRate = New TextBox With {.Location = New Point(362, y + 18), .Size = New Size(90, 28)}
        pnlForm.Controls.Add(txtRate)

        pnlForm.Controls.Add(New Label With {.Text = "Stock", .Location = New Point(462, y), .AutoSize = True,
            .Font = New Font("Segoe UI", 8, FontStyle.Bold), .ForeColor = NavyColor})
        txtStock = New TextBox With {.Location = New Point(462, y + 18), .Size = New Size(70, 28)}
        pnlForm.Controls.Add(txtStock)

        pnlForm.Controls.Add(New Label With {.Text = "Icon", .Location = New Point(542, y), .AutoSize = True,
            .Font = New Font("Segoe UI", 8, FontStyle.Bold), .ForeColor = NavyColor})
        txtIcon = New TextBox With {.Location = New Point(542, y + 18), .Size = New Size(50, 28)}
        pnlForm.Controls.Add(txtIcon)

        y += 56

        btnAdd = MakeBtn("Add", 12, y, NavyColor)
        AddHandler btnAdd.Click, AddressOf BtnAdd_Click
        pnlForm.Controls.Add(btnAdd)

        btnUpdate = MakeBtn("Update", 122, y, OrangeColor)
        AddHandler btnUpdate.Click, AddressOf BtnUpdate_Click
        pnlForm.Controls.Add(btnUpdate)

        btnDelete = MakeBtn("Delete", 232, y, Color.Crimson)
        AddHandler btnDelete.Click, AddressOf BtnDelete_Click
        pnlForm.Controls.Add(btnDelete)

        Controls.Add(pnlForm)

        ' ── Grid ──
        dgv = New DataGridView With {
            .Dock = DockStyle.Fill, .ReadOnly = True,
            .AllowUserToAddRows = False, .AllowUserToDeleteRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            .BackgroundColor = Color.White, .BorderStyle = BorderStyle.None,
            .RowHeadersVisible = False, .Font = New Font("Segoe UI", 9)}
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 230, 245)
        dgv.DefaultCellStyle.SelectionForeColor = NavyColor
        dgv.ColumnHeadersDefaultCellStyle.BackColor = NavyColor
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        dgv.EnableHeadersVisualStyles = False
        AddHandler dgv.SelectionChanged, AddressOf Dgv_SelectionChanged
        Controls.Add(dgv)
        dgv.BringToFront()
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        RefreshGrid()
    End Sub

    Private Sub RefreshGrid()
        Try
            dgv.DataSource = AdminManager.LoadAllEquipment()
        Catch ex As Exception
            MessageBox.Show("Error loading equipment: " & ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Dgv_SelectionChanged(sender As Object, e As EventArgs)
        If dgv.CurrentRow Is Nothing Then Return
        txtName.Text = dgv.CurrentRow.Cells("name").Value.ToString()
        Dim cat = dgv.CurrentRow.Cells("category").Value.ToString()
        Dim idx = cmbCategory.Items.IndexOf(cat)
        If idx >= 0 Then cmbCategory.SelectedIndex = idx
        txtRate.Text = dgv.CurrentRow.Cells("daily_rate").Value.ToString()
        txtStock.Text = dgv.CurrentRow.Cells("total_stock").Value.ToString()
        txtIcon.Text = dgv.CurrentRow.Cells("icon_tag").Value.ToString()
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs)
        If Not ValidateInputs() Then Return
        Try
            AdminManager.AddEquipment(txtName.Text.Trim(), cmbCategory.SelectedItem.ToString(),
                CDec(txtRate.Text), CInt(txtStock.Text), If(txtIcon.Text.Trim() = "", "📦", txtIcon.Text.Trim()))
            RefreshGrid()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs)
        If dgv.CurrentRow Is Nothing Then Return
        If Not ValidateInputs() Then Return
        Try
            Dim eqId = Convert.ToInt32(dgv.CurrentRow.Cells("equipment_id").Value)
            AdminManager.UpdateEquipment(eqId, txtName.Text.Trim(), cmbCategory.SelectedItem.ToString(),
                CDec(txtRate.Text), CInt(txtStock.Text), If(txtIcon.Text.Trim() = "", "📦", txtIcon.Text.Trim()))
            RefreshGrid()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs)
        If dgv.CurrentRow Is Nothing Then Return
        Dim eqId = Convert.ToInt32(dgv.CurrentRow.Cells("equipment_id").Value)
        Dim nm = dgv.CurrentRow.Cells("name").Value.ToString()
        If MessageBox.Show($"Deactivate '{nm}'?", "Confirm",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                AdminManager.DeleteEquipment(eqId)
                RefreshGrid()
                ClearFields()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Function ValidateInputs() As Boolean
        If String.IsNullOrWhiteSpace(txtName.Text) Then
            MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Dim rate As Decimal
        If Not Decimal.TryParse(txtRate.Text, rate) OrElse rate <= 0 Then
            MessageBox.Show("Enter a valid daily rate.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Dim stock As Integer
        If Not Integer.TryParse(txtStock.Text, stock) OrElse stock < 0 Then
            MessageBox.Show("Enter a valid stock quantity.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    Private Sub ClearFields()
        txtName.Clear()
        txtRate.Clear()
        txtStock.Clear()
        txtIcon.Clear()
        cmbCategory.SelectedIndex = 0
    End Sub

    Private Function MakeBtn(txt As String, x As Integer, y As Integer, bg As Color) As Button
        Return New Button With {
            .Text = txt, .Size = New Size(100, 36), .Location = New Point(x, y),
            .FlatStyle = FlatStyle.Flat, .BackColor = bg, .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold), .Cursor = Cursors.Hand}
    End Function
End Class
