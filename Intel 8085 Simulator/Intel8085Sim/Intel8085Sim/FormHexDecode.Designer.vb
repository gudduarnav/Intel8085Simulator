<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormHexDecode
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
        Me.rtbSrc = New System.Windows.Forms.RichTextBox()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.wbDsc = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        '
        'rtbSrc
        '
        Me.rtbSrc.Location = New System.Drawing.Point(14, 13)
        Me.rtbSrc.Name = "rtbSrc"
        Me.rtbSrc.Size = New System.Drawing.Size(564, 116)
        Me.rtbSrc.TabIndex = 0
        Me.rtbSrc.Text = ""
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(588, 14)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(72, 114)
        Me.btnLoad.TabIndex = 1
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'wbDsc
        '
        Me.wbDsc.IsWebBrowserContextMenuEnabled = False
        Me.wbDsc.Location = New System.Drawing.Point(16, 137)
        Me.wbDsc.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbDsc.Name = "wbDsc"
        Me.wbDsc.Size = New System.Drawing.Size(643, 263)
        Me.wbDsc.TabIndex = 2
        '
        'FormHexDecode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(667, 412)
        Me.Controls.Add(Me.wbDsc)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.rtbSrc)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "FormHexDecode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Intel HEX Decoder"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rtbSrc As System.Windows.Forms.RichTextBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents wbDsc As System.Windows.Forms.WebBrowser
End Class
