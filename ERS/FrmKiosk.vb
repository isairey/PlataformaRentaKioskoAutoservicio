Imports System.Drawing.Drawing2D

Partial Class FrmKiosk

    ' ── Theme colours ──
    Private ReadOnly BgColor As Color = ColorTranslator.FromHtml("#F0F2F5")
    Private ReadOnly CardColor As Color = Color.White
    Private ReadOnly NavyColor As Color = ColorTranslator.FromHtml("#1E3A5F")
    Private ReadOnly OrangeColor As Color = ColorTranslator.FromHtml("#F47C20")
    Private ReadOnly GreenColor As Color = ColorTranslator.FromHtml("#1A5C2A")

    ' ── State ──
    Private _allEquipment As List(Of EquipmentItem)
    Private _cart As New List(Of CartItem)()
    Private _currentCategory As String = Nothing
    Private _toastTimer As Timer
    Private _rentalDays As Integer = 1

    ' ============================================================
    '  CONSTRUCTOR
    ' ============================================================
    Public Sub New()
        InitializeComponent()
        AddFilterPills()
    End Sub

    ' ── Build the four category filter pills into pnlFilterBar ──
    Private Sub AddFilterPills()
        Dim cats = {"All Gear", "Seating", "Audio/Visual", "Tables"}
        For Each c In cats
            Dim pill As New Button With {
                .Text = c, .AutoSize = True,
                .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand,
                .BackColor = CardColor, .ForeColor = NavyColor,
                .Font = New Font("Segoe UI", 9, FontStyle.Bold),
                .Margin = New Padding(0, 4, 8, 4),
                .Padding = New Padding(12, 4, 12, 4),
                .Tag = c}
            pill.FlatAppearance.BorderColor = NavyColor
            pill.FlatAppearance.BorderSize = 1
            AddHandler pill.Click, AddressOf FilterPill_Click
            pnlFilterBar.Controls.Add(pill)
        Next
    End Sub

    ' ============================================================
    '  LOAD / REFRESH
    ' ============================================================
    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        RefreshCatalog()
    End Sub

    Private Sub RefreshCatalog()
        Try
            _allEquipment = RentalManager.LoadEquipment(_currentCategory)
        Catch ex As Exception
            MessageBox.Show("Could not load equipment: " & ex.Message, "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            _allEquipment = New List(Of EquipmentItem)()
        End Try
        RenderGrid()
        HighlightActiveFilter()
    End Sub

    Private Sub RenderGrid()
        pnlGrid.SuspendLayout()
        pnlGrid.Controls.Clear()

        Dim cardW = 230
        Dim cardH = 190

        For Each eq In _allEquipment
            Dim card As New Panel With {
                .Size = New Size(cardW, cardH), .BackColor = CardColor,
                .Margin = New Padding(8), .Padding = New Padding(10),
                .Tag = eq}
            card.Region = CreateRoundRegion(cardW, cardH, 10)

            ' Shadow-ish border
            AddHandler card.Paint, Sub(s, pe)
                                       ControlPaint.DrawBorder(pe.Graphics, CType(s, Panel).ClientRectangle,
                                           Color.FromArgb(210, 210, 210), ButtonBorderStyle.Solid)
                                   End Sub

            Dim lblIcon As New Label With {
                .Text = eq.IconTag, .Font = New Font("Segoe UI Emoji", 28),
                .Size = New Size(cardW - 20, 50), .TextAlign = ContentAlignment.MiddleCenter,
                .Location = New Point(10, 4)}
            card.Controls.Add(lblIcon)

            Dim lblName As New Label With {
                .Text = eq.Name, .Font = New Font("Segoe UI", 10, FontStyle.Bold),
                .ForeColor = NavyColor, .AutoSize = False,
                .Size = New Size(cardW - 20, 22), .TextAlign = ContentAlignment.MiddleCenter,
                .Location = New Point(10, 56)}
            card.Controls.Add(lblName)

            Dim lblRate As New Label With {
                .Text = $"₱{eq.DailyRate:N0}/day", .Font = New Font("Segoe UI", 10),
                .ForeColor = OrangeColor, .AutoSize = False,
                .Size = New Size(cardW - 20, 22), .TextAlign = ContentAlignment.MiddleCenter,
                .Location = New Point(10, 80)}
            card.Controls.Add(lblRate)

            Dim lblStock As New Label With {
                .Text = $"Stock: {eq.AvailStock}", .Font = New Font("Segoe UI", 8),
                .ForeColor = Color.Gray, .AutoSize = False,
                .Size = New Size(cardW - 20, 18), .TextAlign = ContentAlignment.MiddleCenter,
                .Location = New Point(10, 104)}
            card.Controls.Add(lblStock)

            If eq.IsAvailable Then
                Dim btnAdd As New Button With {
                    .Text = "Add to Cart", .Size = New Size(cardW - 30, 36),
                    .Location = New Point(15, 128), .FlatStyle = FlatStyle.Flat,
                    .BackColor = NavyColor, .ForeColor = Color.White,
                    .Font = New Font("Segoe UI", 9, FontStyle.Bold),
                    .Cursor = Cursors.Hand, .Tag = eq}
                btnAdd.FlatAppearance.BorderSize = 0
                AddHandler btnAdd.Click, AddressOf BtnAdd_Click
                card.Controls.Add(btnAdd)
            Else
                Dim lblOOS As New Label With {
                    .Text = "Out of Stock", .Size = New Size(cardW - 30, 36),
                    .Location = New Point(15, 128),
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .ForeColor = Color.White, .BackColor = Color.Gray,
                    .Font = New Font("Segoe UI", 9, FontStyle.Bold)}
                card.Controls.Add(lblOOS)
                card.BackColor = Color.FromArgb(235, 235, 235)
            End If

            pnlGrid.Controls.Add(card)
        Next
        pnlGrid.ResumeLayout()
    End Sub

    ' ============================================================
    '  FILTER PILLS
    ' ============================================================
    Private Sub FilterPill_Click(sender As Object, e As EventArgs)
        Dim pill = DirectCast(sender, Button)
        Dim cat = pill.Tag.ToString()
        _currentCategory = If(cat = "All Gear", Nothing, cat)
        RefreshCatalog()
    End Sub

    Private Sub HighlightActiveFilter()
        For Each ctl In pnlFilterBar.Controls.OfType(Of Button)()
            Dim isActive = (ctl.Tag.ToString() = If(_currentCategory, "All Gear"))
            ctl.BackColor = If(isActive, NavyColor, CardColor)
            ctl.ForeColor = If(isActive, Color.White, NavyColor)
        Next
    End Sub

    ' ============================================================
    '  ADD TO CART
    ' ============================================================
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs)
        Dim eq = DirectCast(DirectCast(sender, Button).Tag, EquipmentItem)

        Dim existing = _cart.FirstOrDefault(Function(ci) ci.Equipment.EquipmentId = eq.EquipmentId)
        If existing IsNot Nothing Then
            If existing.Quantity < eq.AvailStock Then
                existing.Quantity += 1
            End If
        Else
            _cart.Add(New CartItem With {.Equipment = eq, .Quantity = 1})
        End If

        RefreshCartUI()
        ShowToast($"✔  {eq.Name} added to cart")
    End Sub

    ' ============================================================
    '  CART UI
    ' ============================================================
    Private Sub RefreshCartUI()
        pnlCartItems.SuspendLayout()
        pnlCartItems.Controls.Clear()

        Dim containerWidth = pnlCartItems.ClientSize.Width - 24

        For Each ci In _cart
            Dim row As New Panel With {
                .Size = New Size(containerWidth, 56),
                .BackColor = BgColor, .Margin = New Padding(2, 2, 2, 4),
                .Padding = New Padding(6)}

            Dim lblN As New Label With {
                .Text = $"{ci.Equipment.IconTag} {ci.Equipment.Name}",
                .AutoSize = True, .Location = New Point(6, 4),
                .Font = New Font("Segoe UI", 9, FontStyle.Bold), .ForeColor = NavyColor}
            row.Controls.Add(lblN)

            Dim lblP As New Label With {
                .Text = $"₱{ci.Equipment.DailyRate:N0}/day",
                .AutoSize = True, .Location = New Point(6, 26),
                .Font = New Font("Segoe UI", 8), .ForeColor = Color.Gray}
            row.Controls.Add(lblP)

            ' Minus button
            Dim btnMinus As New Button With {
                .Text = "−", .Size = New Size(28, 28),
                .Location = New Point(containerWidth - 120, 14),
                .FlatStyle = FlatStyle.Flat, .Font = New Font("Segoe UI", 10, FontStyle.Bold),
                .BackColor = CardColor, .Tag = ci}
            btnMinus.FlatAppearance.BorderColor = Color.LightGray
            AddHandler btnMinus.Click, AddressOf BtnMinus_Click
            row.Controls.Add(btnMinus)

            ' Qty label
            Dim lblQty As New Label With {
                .Text = ci.Quantity.ToString(), .Size = New Size(30, 28),
                .Location = New Point(containerWidth - 88, 14),
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Segoe UI", 10, FontStyle.Bold)}
            row.Controls.Add(lblQty)

            ' Plus button
            Dim btnPlus As New Button With {
                .Text = "+", .Size = New Size(28, 28),
                .Location = New Point(containerWidth - 56, 14),
                .FlatStyle = FlatStyle.Flat, .Font = New Font("Segoe UI", 10, FontStyle.Bold),
                .BackColor = CardColor, .Tag = ci}
            btnPlus.FlatAppearance.BorderColor = Color.LightGray
            AddHandler btnPlus.Click, AddressOf BtnPlus_Click
            row.Controls.Add(btnPlus)

            pnlCartItems.Controls.Add(row)
        Next
        pnlCartItems.ResumeLayout()
        RecalcTotals()
    End Sub

    Private Sub BtnMinus_Click(sender As Object, e As EventArgs)
        Dim ci = DirectCast(DirectCast(sender, Button).Tag, CartItem)
        ci.Quantity -= 1
        If ci.Quantity <= 0 Then _cart.Remove(ci)
        RefreshCartUI()
    End Sub

    Private Sub BtnPlus_Click(sender As Object, e As EventArgs)
        Dim ci = DirectCast(DirectCast(sender, Button).Tag, CartItem)
        If ci.Quantity < ci.Equipment.AvailStock Then ci.Quantity += 1
        RefreshCartUI()
    End Sub

    Private Sub RecalcTotals()
        Dim dailySub As Decimal = _cart.Sum(Function(ci) ci.Equipment.DailyRate * ci.Quantity)
        Dim sub1 As Decimal = dailySub * _rentalDays
        lblSubtotal.Text = $"Subtotal: ₱{sub1:N2}  ({_rentalDays}d)"
        lblDeposit.Text = "Security Deposit: ₱500.00"
        lblTotal.Text = $"Total: ₱{sub1 + 500D:N2}"
    End Sub

    ' ============================================================
    '  GREEN TOAST
    ' ============================================================
    Private Sub ShowToast(msg As String)
        Dim toast As New Label With {
            .Text = msg, .AutoSize = False, .Size = New Size(300, 36),
            .TextAlign = ContentAlignment.MiddleCenter,
            .BackColor = GreenColor, .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold)}
        toast.Location = New Point((ClientSize.Width - toast.Width) \ 2, ClientSize.Height - 80)
        Controls.Add(toast)
        toast.BringToFront()

        If _toastTimer IsNot Nothing Then _toastTimer.Dispose()
        _toastTimer = New Timer With {.Interval = 2000}
        AddHandler _toastTimer.Tick, Sub()
                                         Controls.Remove(toast)
                                         toast.Dispose()
                                         _toastTimer.Stop()
                                     End Sub
        _toastTimer.Start()
    End Sub

    ' ============================================================
    '  RENTAL DAYS STEPPER
    ' ============================================================
    Private Sub BtnDaysDown_Click(sender As Object, e As EventArgs) Handles btnDaysDown.Click
        If _rentalDays > 1 Then
            _rentalDays -= 1
            lblDaysValue.Text = _rentalDays.ToString()
            RecalcTotals()
        End If
    End Sub

    Private Sub BtnDaysUp_Click(sender As Object, e As EventArgs) Handles btnDaysUp.Click
        If _rentalDays < 365 Then
            _rentalDays += 1
            lblDaysValue.Text = _rentalDays.ToString()
            RecalcTotals()
        End If
    End Sub

    ' ============================================================
    '  CLEAR CART
    ' ============================================================
    Private Sub BtnClearCart_Click(sender As Object, e As EventArgs) Handles btnClearCart.Click
        If _cart.Count = 0 Then Return
        Dim confirm = MessageBox.Show("Remove all items from your cart?", "Clear Cart",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.Yes Then
            _cart.Clear()
            RefreshCartUI()
            ShowToast("🗑  Cart cleared")
        End If
    End Sub

    ' ============================================================
    '  CHECKOUT
    ' ============================================================
    Private Sub BtnCheckout_Click(sender As Object, e As EventArgs) Handles btnCheckout.Click
        If _cart.Count = 0 Then
            MessageBox.Show("Your cart is empty.", "Cart Empty", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim frm As New FrmCheckout(_cart, _rentalDays)
        If frm.ShowDialog(Me) = DialogResult.OK Then
            _cart.Clear()
            RefreshCartUI()
            RefreshCatalog()
        End If
    End Sub

    ' ============================================================
    '  F12 → ADMIN
    ' ============================================================
    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.KeyCode = Keys.F12 Then
            Dim login As New FrmAdminLogin()
            If login.ShowDialog(Me) = DialogResult.OK Then
                Dim dash As New FrmAdminDashboard()
                dash.ShowDialog(Me)
                RefreshCatalog()
            End If
        End If
    End Sub

    ' ============================================================
    '  HELPERS
    ' ============================================================
    Private Shared Function CreateRoundRegion(w As Integer, h As Integer, r As Integer) As Region
        Dim gp As New GraphicsPath()
        gp.AddArc(0, 0, r, r, 180, 90)
        gp.AddArc(w - r, 0, r, r, 270, 90)
        gp.AddArc(w - r, h - r, r, r, 0, 90)
        gp.AddArc(0, h - r, r, r, 90, 90)
        gp.CloseAllFigures()
        Return New Region(gp)
    End Function
End Class
