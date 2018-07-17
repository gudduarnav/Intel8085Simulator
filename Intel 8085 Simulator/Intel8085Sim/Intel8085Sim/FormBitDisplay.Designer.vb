<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormBitDisplay
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
        Me.ck7 = New System.Windows.Forms.CheckBox()
        Me.ck6 = New System.Windows.Forms.CheckBox()
        Me.ck5 = New System.Windows.Forms.CheckBox()
        Me.ck4 = New System.Windows.Forms.CheckBox()
        Me.ck3 = New System.Windows.Forms.CheckBox()
        Me.ck2 = New System.Windows.Forms.CheckBox()
        Me.ck1 = New System.Windows.Forms.CheckBox()
        Me.ck0 = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'ck7
        '
        Me.ck7.AutoSize = True
        Me.ck7.Location = New System.Drawing.Point(12, 12)
        Me.ck7.Name = "ck7"
        Me.ck7.Size = New System.Drawing.Size(79, 26)
        Me.ck7.TabIndex = 0
        Me.ck7.Text = "BIT 7"
        Me.ck7.UseVisualStyleBackColor = True
        '
        'ck6
        '
        Me.ck6.AutoSize = True
        Me.ck6.Location = New System.Drawing.Point(97, 12)
        Me.ck6.Name = "ck6"
        Me.ck6.Size = New System.Drawing.Size(79, 26)
        Me.ck6.TabIndex = 1
        Me.ck6.Text = "BIT 6"
        Me.ck6.UseVisualStyleBackColor = True
        '
        'ck5
        '
        Me.ck5.AutoSize = True
        Me.ck5.Location = New System.Drawing.Point(182, 12)
        Me.ck5.Name = "ck5"
        Me.ck5.Size = New System.Drawing.Size(79, 26)
        Me.ck5.TabIndex = 2
        Me.ck5.Text = "BIT 5"
        Me.ck5.UseVisualStyleBackColor = True
        '
        'ck4
        '
        Me.ck4.AutoSize = True
        Me.ck4.Location = New System.Drawing.Point(267, 12)
        Me.ck4.Name = "ck4"
        Me.ck4.Size = New System.Drawing.Size(79, 26)
        Me.ck4.TabIndex = 3
        Me.ck4.Text = "BIT 4"
        Me.ck4.UseVisualStyleBackColor = True
        '
        'ck3
        '
        Me.ck3.AutoSize = True
        Me.ck3.Location = New System.Drawing.Point(352, 12)
        Me.ck3.Name = "ck3"
        Me.ck3.Size = New System.Drawing.Size(79, 26)
        Me.ck3.TabIndex = 4
        Me.ck3.Text = "BIT 3"
        Me.ck3.UseVisualStyleBackColor = True
        '
        'ck2
        '
        Me.ck2.AutoSize = True
        Me.ck2.Location = New System.Drawing.Point(437, 12)
        Me.ck2.Name = "ck2"
        Me.ck2.Size = New System.Drawing.Size(79, 26)
        Me.ck2.TabIndex = 5
        Me.ck2.Text = "BIT 2"
        Me.ck2.UseVisualStyleBackColor = True
        '
        'ck1
        '
        Me.ck1.AutoSize = True
        Me.ck1.Location = New System.Drawing.Point(522, 12)
        Me.ck1.Name = "ck1"
        Me.ck1.Size = New System.Drawing.Size(79, 26)
        Me.ck1.TabIndex = 6
        Me.ck1.Text = "BIT 1"
        Me.ck1.UseVisualStyleBackColor = True
        '
        'ck0
        '
        Me.ck0.AutoSize = True
        Me.ck0.Location = New System.Drawing.Point(607, 12)
        Me.ck0.Name = "ck0"
        Me.ck0.Size = New System.Drawing.Size(79, 26)
        Me.ck0.TabIndex = 7
        Me.ck0.Text = "BIT 0"
        Me.ck0.UseVisualStyleBackColor = True
        '
        'FormBitDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Navy
        Me.ClientSize = New System.Drawing.Size(689, 51)
        Me.Controls.Add(Me.ck0)
        Me.Controls.Add(Me.ck1)
        Me.Controls.Add(Me.ck2)
        Me.Controls.Add(Me.ck3)
        Me.Controls.Add(Me.ck4)
        Me.Controls.Add(Me.ck5)
        Me.Controls.Add(Me.ck6)
        Me.Controls.Add(Me.ck7)
        Me.Font = New System.Drawing.Font("Consolas", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.Name = "FormBitDisplay"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bit Display [IN=03H, OUT=03H PORT]"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ck7 As System.Windows.Forms.CheckBox
    Friend WithEvents ck6 As System.Windows.Forms.CheckBox
    Friend WithEvents ck5 As System.Windows.Forms.CheckBox
    Friend WithEvents ck4 As System.Windows.Forms.CheckBox
    Friend WithEvents ck3 As System.Windows.Forms.CheckBox
    Friend WithEvents ck2 As System.Windows.Forms.CheckBox
    Friend WithEvents ck1 As System.Windows.Forms.CheckBox
    Friend WithEvents ck0 As System.Windows.Forms.CheckBox
End Class
