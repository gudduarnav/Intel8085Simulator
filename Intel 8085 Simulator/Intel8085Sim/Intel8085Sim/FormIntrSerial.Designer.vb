<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormIntrSerial
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.tbSod = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnRst55 = New System.Windows.Forms.Button()
        Me.btnRst65 = New System.Windows.Forms.Button()
        Me.btnRst75 = New System.Windows.Forms.Button()
        Me.btnTrap = New System.Windows.Forms.Button()
        Me.btnSOD = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbIntrFlag = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.tbIntrFlag)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnSOD)
        Me.GroupBox1.Controls.Add(Me.tbSod)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 14)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(405, 47)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Serial Transmission:"
        '
        'tbSod
        '
        Me.tbSod.Location = New System.Drawing.Point(49, 19)
        Me.tbSod.MaxLength = 1
        Me.tbSod.Name = "tbSod"
        Me.tbSod.ReadOnly = True
        Me.tbSod.Size = New System.Drawing.Size(38, 20)
        Me.tbSod.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "SOD:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnRst55)
        Me.GroupBox2.Controls.Add(Me.btnRst65)
        Me.GroupBox2.Controls.Add(Me.btnRst75)
        Me.GroupBox2.Controls.Add(Me.btnTrap)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 82)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(391, 182)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Hardware Interrupts:"
        '
        'btnRst55
        '
        Me.btnRst55.Location = New System.Drawing.Point(95, 137)
        Me.btnRst55.Name = "btnRst55"
        Me.btnRst55.Size = New System.Drawing.Size(209, 27)
        Me.btnRst55.TabIndex = 3
        Me.btnRst55.Text = "RST 5.5"
        Me.btnRst55.UseVisualStyleBackColor = True
        '
        'btnRst65
        '
        Me.btnRst65.Location = New System.Drawing.Point(95, 104)
        Me.btnRst65.Name = "btnRst65"
        Me.btnRst65.Size = New System.Drawing.Size(209, 27)
        Me.btnRst65.TabIndex = 2
        Me.btnRst65.Text = "RST 6.5"
        Me.btnRst65.UseVisualStyleBackColor = True
        '
        'btnRst75
        '
        Me.btnRst75.Location = New System.Drawing.Point(95, 71)
        Me.btnRst75.Name = "btnRst75"
        Me.btnRst75.Size = New System.Drawing.Size(209, 27)
        Me.btnRst75.TabIndex = 1
        Me.btnRst75.Text = "RST 7.5"
        Me.btnRst75.UseVisualStyleBackColor = True
        '
        'btnTrap
        '
        Me.btnTrap.Location = New System.Drawing.Point(95, 28)
        Me.btnTrap.Name = "btnTrap"
        Me.btnTrap.Size = New System.Drawing.Size(209, 27)
        Me.btnTrap.TabIndex = 0
        Me.btnTrap.Text = "TRAP"
        Me.btnTrap.UseVisualStyleBackColor = True
        '
        'btnSOD
        '
        Me.btnSOD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSOD.Location = New System.Drawing.Point(128, 18)
        Me.btnSOD.Name = "btnSOD"
        Me.btnSOD.Size = New System.Drawing.Size(75, 23)
        Me.btnSOD.TabIndex = 3
        Me.btnSOD.Text = "SID = 1"
        Me.btnSOD.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(232, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Interrupt Flag:"
        '
        'tbIntrFlag
        '
        Me.tbIntrFlag.Location = New System.Drawing.Point(325, 19)
        Me.tbIntrFlag.MaxLength = 1
        Me.tbIntrFlag.Name = "tbIntrFlag"
        Me.tbIntrFlag.ReadOnly = True
        Me.tbIntrFlag.Size = New System.Drawing.Size(38, 20)
        Me.tbIntrFlag.TabIndex = 5
        '
        'FormIntrSerial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(424, 280)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "FormIntrSerial"
        Me.Text = "Interrupt Mask and Serial IO"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tbSod As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRst55 As System.Windows.Forms.Button
    Friend WithEvents btnRst65 As System.Windows.Forms.Button
    Friend WithEvents btnRst75 As System.Windows.Forms.Button
    Friend WithEvents btnTrap As System.Windows.Forms.Button
    Friend WithEvents btnSOD As System.Windows.Forms.Button
    Friend WithEvents tbIntrFlag As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
