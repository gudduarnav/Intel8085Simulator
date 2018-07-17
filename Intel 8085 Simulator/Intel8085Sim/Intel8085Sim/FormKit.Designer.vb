<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormKit
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbAddress = New System.Windows.Forms.TextBox()
        Me.tbByte = New System.Windows.Forms.TextBox()
        Me.btnSetAddr = New System.Windows.Forms.Button()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnGo = New System.Windows.Forms.Button()
        Me.btnExec = New System.Windows.Forms.Button()
        Me.btnRes = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Address:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(245, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 22)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Byte:"
        '
        'tbAddress
        '
        Me.tbAddress.BackColor = System.Drawing.Color.Black
        Me.tbAddress.ForeColor = System.Drawing.Color.Lime
        Me.tbAddress.Location = New System.Drawing.Point(108, 18)
        Me.tbAddress.MaxLength = 4
        Me.tbAddress.Name = "tbAddress"
        Me.tbAddress.ReadOnly = True
        Me.tbAddress.Size = New System.Drawing.Size(100, 30)
        Me.tbAddress.TabIndex = 2
        '
        'tbByte
        '
        Me.tbByte.BackColor = System.Drawing.Color.Black
        Me.tbByte.ForeColor = System.Drawing.Color.Lime
        Me.tbByte.Location = New System.Drawing.Point(311, 18)
        Me.tbByte.MaxLength = 2
        Me.tbByte.Name = "tbByte"
        Me.tbByte.ReadOnly = True
        Me.tbByte.Size = New System.Drawing.Size(64, 30)
        Me.tbByte.TabIndex = 3
        '
        'btnSetAddr
        '
        Me.btnSetAddr.Location = New System.Drawing.Point(16, 68)
        Me.btnSetAddr.Name = "btnSetAddr"
        Me.btnSetAddr.Size = New System.Drawing.Size(146, 38)
        Me.btnSetAddr.TabIndex = 4
        Me.btnSetAddr.Text = "Set Address"
        Me.btnSetAddr.UseVisualStyleBackColor = True
        '
        'btnPrev
        '
        Me.btnPrev.Location = New System.Drawing.Point(168, 68)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(104, 38)
        Me.btnPrev.TabIndex = 5
        Me.btnPrev.Text = "Prev"
        Me.btnPrev.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(278, 68)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(97, 38)
        Me.btnNext.TabIndex = 6
        Me.btnNext.Text = "Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(16, 112)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(146, 38)
        Me.btnGo.TabIndex = 7
        Me.btnGo.Text = "Go"
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'btnExec
        '
        Me.btnExec.Location = New System.Drawing.Point(168, 112)
        Me.btnExec.Name = "btnExec"
        Me.btnExec.Size = New System.Drawing.Size(126, 38)
        Me.btnExec.TabIndex = 8
        Me.btnExec.Text = "Exec"
        Me.btnExec.UseVisualStyleBackColor = True
        '
        'btnRes
        '
        Me.btnRes.Location = New System.Drawing.Point(300, 112)
        Me.btnRes.Name = "btnRes"
        Me.btnRes.Size = New System.Drawing.Size(75, 38)
        Me.btnRes.TabIndex = 9
        Me.btnRes.Text = "Res"
        Me.btnRes.UseVisualStyleBackColor = True
        '
        'FormKit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(389, 174)
        Me.Controls.Add(Me.btnRes)
        Me.Controls.Add(Me.btnExec)
        Me.Controls.Add(Me.btnGo)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrev)
        Me.Controls.Add(Me.btnSetAddr)
        Me.Controls.Add(Me.tbByte)
        Me.Controls.Add(Me.tbAddress)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Consolas", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.Name = "FormKit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "8085 Program Kit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbAddress As System.Windows.Forms.TextBox
    Friend WithEvents tbByte As System.Windows.Forms.TextBox
    Friend WithEvents btnSetAddr As System.Windows.Forms.Button
    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents btnExec As System.Windows.Forms.Button
    Friend WithEvents btnRes As System.Windows.Forms.Button
End Class
