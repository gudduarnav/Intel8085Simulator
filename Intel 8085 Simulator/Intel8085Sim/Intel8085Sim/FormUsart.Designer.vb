<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormUsart
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
        Me.cbComList = New System.Windows.Forms.ComboBox()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbSelectedCom = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "COM Port:"
        '
        'cbComList
        '
        Me.cbComList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbComList.FormattingEnabled = True
        Me.cbComList.Location = New System.Drawing.Point(87, 20)
        Me.cbComList.Name = "cbComList"
        Me.cbComList.Size = New System.Drawing.Size(121, 21)
        Me.cbComList.TabIndex = 1
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(214, 18)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(169, 23)
        Me.btnSelect.TabIndex = 2
        Me.btnSelect.Text = "Activate"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Selected COM Port:"
        '
        'lbSelectedCom
        '
        Me.lbSelectedCom.AutoSize = True
        Me.lbSelectedCom.Location = New System.Drawing.Point(165, 72)
        Me.lbSelectedCom.Name = "lbSelectedCom"
        Me.lbSelectedCom.Size = New System.Drawing.Size(33, 13)
        Me.lbSelectedCom.TabIndex = 4
        Me.lbSelectedCom.Text = "None"
        '
        'FormUsart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(407, 157)
        Me.Controls.Add(Me.lbSelectedCom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.cbComList)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FormUsart"
        Me.Text = "COM Port: [40h: Data, 41h: Command/Status] "
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbComList As System.Windows.Forms.ComboBox
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbSelectedCom As System.Windows.Forms.Label
End Class
