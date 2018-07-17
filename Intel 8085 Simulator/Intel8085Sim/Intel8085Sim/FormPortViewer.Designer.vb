<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPortViewer
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
        Me.dgvw = New System.Windows.Forms.DataGridView()
        CType(Me.dgvw, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvw
        '
        Me.dgvw.AllowUserToAddRows = False
        Me.dgvw.AllowUserToDeleteRows = False
        Me.dgvw.AllowUserToResizeColumns = False
        Me.dgvw.AllowUserToResizeRows = False
        Me.dgvw.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvw.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvw.Location = New System.Drawing.Point(0, 0)
        Me.dgvw.MultiSelect = False
        Me.dgvw.Name = "dgvw"
        Me.dgvw.Size = New System.Drawing.Size(679, 382)
        Me.dgvw.TabIndex = 0
        '
        'FormPortViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(679, 382)
        Me.Controls.Add(Me.dgvw)
        Me.Name = "FormPortViewer"
        Me.Text = "FormPortViewer"
        CType(Me.dgvw, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvw As System.Windows.Forms.DataGridView
End Class
