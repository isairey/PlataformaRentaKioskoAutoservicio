<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmKiosk
    Inherits System.Windows.Forms.Form

    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblTagLine = New System.Windows.Forms.Label()
        Me.MainSplitter = New System.Windows.Forms.SplitContainer()
        Me.pnlFilterBar = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlGrid = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblCartTitle = New System.Windows.Forms.Label()
        Me.pnlCartItems = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlCartBottom = New System.Windows.Forms.Panel()
        Me.lblSubtotal = New System.Windows.Forms.Label()
        Me.lblDeposit = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.btnCheckout = New System.Windows.Forms.Button()
        Me.btnClearCart = New System.Windows.Forms.Button()
        Me.lblDaysLabel = New System.Windows.Forms.Label()
        Me.btnDaysDown = New System.Windows.Forms.Button()
        Me.lblDaysValue = New System.Windows.Forms.Label()
        Me.btnDaysUp = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        CType(Me.MainSplitter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainSplitter.Panel1.SuspendLayout()
        Me.MainSplitter.Panel2.SuspendLayout()
        Me.MainSplitter.SuspendLayout()
        Me.pnlCartBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        ' lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(16, 12)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(127, 30)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "2C Rentals"
        '
        ' lblTagLine
        '
        Me.lblTagLine.AutoSize = True
        Me.lblTagLine.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTagLine.ForeColor = System.Drawing.Color.FromArgb(180, 200, 220)
        Me.lblTagLine.Location = New System.Drawing.Point(180, 18)
        Me.lblTagLine.Name = "lblTagLine"
        Me.lblTagLine.Size = New System.Drawing.Size(340, 15)
        Me.lblTagLine.TabIndex = 1
        Me.lblTagLine.Text = "Equipment Rental System  |  Press F12 for Admin"
        '
        ' pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.ColorTranslator.FromHtml("#1E3A5F")
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.lblTagLine)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1264, 56)
        Me.pnlHeader.TabIndex = 0
        '
        ' pnlFilterBar  (DockStyle.Top inside Panel1)
        '
        Me.pnlFilterBar.AutoSize = False
        Me.pnlFilterBar.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F2F5")
        Me.pnlFilterBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilterBar.Location = New System.Drawing.Point(16, 10)
        Me.pnlFilterBar.Name = "pnlFilterBar"
        Me.pnlFilterBar.Size = New System.Drawing.Size(796, 44)
        Me.pnlFilterBar.TabIndex = 1
        Me.pnlFilterBar.WrapContents = False
        '
        ' pnlGrid  (DockStyle.Fill inside Panel1 — added first so FilterBar docks on top)
        '
        Me.pnlGrid.AutoScroll = True
        Me.pnlGrid.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F2F5")
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(16, 54)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.pnlGrid.Size = New System.Drawing.Size(796, 641)
        Me.pnlGrid.TabIndex = 0
        Me.pnlGrid.WrapContents = True
        '
        ' MainSplitter.Panel1
        '
        Me.MainSplitter.Panel1.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F2F5")
        Me.MainSplitter.Panel1.Controls.Add(Me.pnlGrid)
        Me.MainSplitter.Panel1.Controls.Add(Me.pnlFilterBar)
        Me.MainSplitter.Panel1.Padding = New System.Windows.Forms.Padding(16, 10, 8, 10)
        '
        ' lblCartTitle  (DockStyle.Top inside Panel2)
        '
        Me.lblCartTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblCartTitle.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblCartTitle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1E3A5F")
        Me.lblCartTitle.Location = New System.Drawing.Point(12, 12)
        Me.lblCartTitle.Name = "lblCartTitle"
        Me.lblCartTitle.Size = New System.Drawing.Size(418, 38)
        Me.lblCartTitle.TabIndex = 0
        Me.lblCartTitle.Text = "🛒  Your Cart"
        '
        ' pnlCartItems  (DockStyle.Fill inside Panel2 — added first)
        '
        Me.pnlCartItems.AutoScroll = True
        Me.pnlCartItems.BackColor = System.Drawing.Color.White
        Me.pnlCartItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCartItems.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.pnlCartItems.Location = New System.Drawing.Point(12, 50)
        Me.pnlCartItems.Name = "pnlCartItems"
        Me.pnlCartItems.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.pnlCartItems.Size = New System.Drawing.Size(418, 483)
        Me.pnlCartItems.TabIndex = 1
        Me.pnlCartItems.WrapContents = False
        '
        ' lblSubtotal
        '
        Me.lblSubtotal.AutoSize = True
        Me.lblSubtotal.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblSubtotal.ForeColor = System.Drawing.Color.Gray
        Me.lblSubtotal.Location = New System.Drawing.Point(8, 44)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(100, 17)
        Me.lblSubtotal.TabIndex = 0
        Me.lblSubtotal.Text = "Subtotal: ₱0.00"
        '
        ' lblDeposit
        '
        Me.lblDeposit.AutoSize = True
        Me.lblDeposit.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblDeposit.ForeColor = System.Drawing.Color.Gray
        Me.lblDeposit.Location = New System.Drawing.Point(8, 68)
        Me.lblDeposit.Name = "lblDeposit"
        Me.lblDeposit.Size = New System.Drawing.Size(170, 17)
        Me.lblDeposit.TabIndex = 1
        Me.lblDeposit.Text = "Security Deposit: ₱500.00"
        '
        ' lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotal.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1E3A5F")
        Me.lblTotal.Location = New System.Drawing.Point(8, 96)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(140, 22)
        Me.lblTotal.TabIndex = 2
        Me.lblTotal.Text = "Total: ₱500.00"
        '
        ' btnCheckout
        '
        Me.btnCheckout.BackColor = System.Drawing.ColorTranslator.FromHtml("#F47C20")
        Me.btnCheckout.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCheckout.FlatAppearance.BorderSize = 0
        Me.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCheckout.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnCheckout.ForeColor = System.Drawing.Color.White
        Me.btnCheckout.Location = New System.Drawing.Point(8, 136)
        Me.btnCheckout.Name = "btnCheckout"
        Me.btnCheckout.Size = New System.Drawing.Size(280, 48)
        Me.btnCheckout.TabIndex = 3
        Me.btnCheckout.Text = "PROCEED TO CHECKOUT →"
        Me.btnCheckout.UseVisualStyleBackColor = False
        '
        ' btnClearCart
        '
        Me.btnClearCart.BackColor = System.Drawing.Color.FromArgb(180, 30, 30)
        Me.btnClearCart.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearCart.FlatAppearance.BorderSize = 0
        Me.btnClearCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearCart.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnClearCart.ForeColor = System.Drawing.Color.White
        Me.btnClearCart.Location = New System.Drawing.Point(296, 136)
        Me.btnClearCart.Name = "btnClearCart"
        Me.btnClearCart.Size = New System.Drawing.Size(114, 48)
        Me.btnClearCart.TabIndex = 4
        Me.btnClearCart.Text = "🗑 Clear Cart"
        Me.btnClearCart.UseVisualStyleBackColor = False
        '
        ' pnlCartBottom  (DockStyle.Bottom inside Panel2)
        '
        '
        ' lblDaysLabel
        '
        Me.lblDaysLabel.AutoSize = True
        Me.lblDaysLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblDaysLabel.ForeColor = System.Drawing.Color.Gray
        Me.lblDaysLabel.Location = New System.Drawing.Point(8, 14)
        Me.lblDaysLabel.Name = "lblDaysLabel"
        Me.lblDaysLabel.TabIndex = 5
        Me.lblDaysLabel.Text = "Rental Days:"
        '
        ' btnDaysDown
        '
        Me.btnDaysDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDaysDown.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDaysDown.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1E3A5F")
        Me.btnDaysDown.Location = New System.Drawing.Point(108, 6)
        Me.btnDaysDown.Name = "btnDaysDown"
        Me.btnDaysDown.Size = New System.Drawing.Size(28, 28)
        Me.btnDaysDown.TabIndex = 6
        Me.btnDaysDown.Text = "−"
        Me.btnDaysDown.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        '
        ' lblDaysValue
        '
        Me.lblDaysValue.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblDaysValue.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1E3A5F")
        Me.lblDaysValue.Location = New System.Drawing.Point(138, 6)
        Me.lblDaysValue.Name = "lblDaysValue"
        Me.lblDaysValue.Size = New System.Drawing.Size(34, 28)
        Me.lblDaysValue.TabIndex = 7
        Me.lblDaysValue.Text = "1"
        Me.lblDaysValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        ' btnDaysUp
        '
        Me.btnDaysUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDaysUp.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDaysUp.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1E3A5F")
        Me.btnDaysUp.Location = New System.Drawing.Point(174, 6)
        Me.btnDaysUp.Name = "btnDaysUp"
        Me.btnDaysUp.Size = New System.Drawing.Size(28, 28)
        Me.btnDaysUp.TabIndex = 8
        Me.btnDaysUp.Text = "+"
        Me.btnDaysUp.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        '
        ' pnlCartBottom
        '
        Me.pnlCartBottom.BackColor = System.Drawing.Color.White
        Me.pnlCartBottom.Controls.Add(Me.lblSubtotal)
        Me.pnlCartBottom.Controls.Add(Me.lblDeposit)
        Me.pnlCartBottom.Controls.Add(Me.lblTotal)
        Me.pnlCartBottom.Controls.Add(Me.lblDaysLabel)
        Me.pnlCartBottom.Controls.Add(Me.btnDaysDown)
        Me.pnlCartBottom.Controls.Add(Me.lblDaysValue)
        Me.pnlCartBottom.Controls.Add(Me.btnDaysUp)
        Me.pnlCartBottom.Controls.Add(Me.btnCheckout)
        Me.pnlCartBottom.Controls.Add(Me.btnClearCart)
        Me.pnlCartBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlCartBottom.Location = New System.Drawing.Point(12, 497)
        Me.pnlCartBottom.Name = "pnlCartBottom"
        Me.pnlCartBottom.Size = New System.Drawing.Size(418, 196)
        Me.pnlCartBottom.TabIndex = 2
        '
        ' MainSplitter.Panel2
        '
        Me.MainSplitter.Panel2.BackColor = System.Drawing.Color.White
        Me.MainSplitter.Panel2.Controls.Add(Me.pnlCartItems)
        Me.MainSplitter.Panel2.Controls.Add(Me.lblCartTitle)
        Me.MainSplitter.Panel2.Controls.Add(Me.pnlCartBottom)
        Me.MainSplitter.Panel2.Padding = New System.Windows.Forms.Padding(12)
        '
        ' MainSplitter
        '
        Me.MainSplitter.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F2F5")
        Me.MainSplitter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.MainSplitter.Location = New System.Drawing.Point(0, 56)
        Me.MainSplitter.Name = "MainSplitter"
        Me.MainSplitter.Size = New System.Drawing.Size(1264, 705)
        Me.MainSplitter.SplitterDistance = 820
        Me.MainSplitter.SplitterWidth = 2
        Me.MainSplitter.TabIndex = 1
        '
        ' FrmKiosk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F2F5")
        Me.ClientSize = New System.Drawing.Size(1264, 761)
        Me.Controls.Add(Me.MainSplitter)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(1024, 640)
        Me.Name = "FrmKiosk"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "2C Rentals — Equipment Rental Kiosk"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.MainSplitter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainSplitter.Panel1.ResumeLayout(False)
        Me.MainSplitter.Panel2.ResumeLayout(False)
        Me.MainSplitter.ResumeLayout(False)
        Me.pnlCartBottom.ResumeLayout(False)
        Me.pnlCartBottom.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblTagLine As System.Windows.Forms.Label
    Friend WithEvents MainSplitter As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlFilterBar As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlGrid As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblCartTitle As System.Windows.Forms.Label
    Friend WithEvents pnlCartItems As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlCartBottom As System.Windows.Forms.Panel
    Friend WithEvents lblSubtotal As System.Windows.Forms.Label
    Friend WithEvents lblDeposit As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents btnCheckout As System.Windows.Forms.Button
    Friend WithEvents btnClearCart As System.Windows.Forms.Button
    Friend WithEvents lblDaysLabel As System.Windows.Forms.Label
    Friend WithEvents btnDaysDown As System.Windows.Forms.Button
    Friend WithEvents lblDaysValue As System.Windows.Forms.Label
    Friend WithEvents btnDaysUp As System.Windows.Forms.Button

End Class
