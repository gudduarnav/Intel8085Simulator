<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormReport))
        Me.rtbOut = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'rtbOut
        '
        Me.rtbOut.BackColor = System.Drawing.Color.Navy
        Me.rtbOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbOut.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbOut.ForeColor = System.Drawing.Color.White
        Me.rtbOut.Location = New System.Drawing.Point(0, 0)
        Me.rtbOut.Name = "rtbOut"
        Me.rtbOut.ReadOnly = True
        Me.rtbOut.Size = New System.Drawing.Size(736, 210)
        Me.rtbOut.TabIndex = 0
        Me.rtbOut.Text = ""
        '
        'FormReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(736, 210)
        Me.Controls.Add(Me.rtbOut)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormReport"
        Me.Text = "Output Console"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rtbOut As System.Windows.Forms.RichTextBox
End Class
