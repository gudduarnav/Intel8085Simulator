<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAssembler
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAssembler))
        Me.rtbSrc = New System.Windows.Forms.RichTextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewSourceFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadSourceFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveSourceFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AssemblerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AssembleAndSaveHEXFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AssembleAndLoadProgramToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'rtbSrc
        '
        Me.rtbSrc.BackColor = System.Drawing.Color.Black
        Me.rtbSrc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbSrc.Font = New System.Drawing.Font("Consolas", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbSrc.ForeColor = System.Drawing.Color.Yellow
        Me.rtbSrc.Location = New System.Drawing.Point(0, 24)
        Me.rtbSrc.Name = "rtbSrc"
        Me.rtbSrc.Size = New System.Drawing.Size(784, 522)
        Me.rtbSrc.TabIndex = 0
        Me.rtbSrc.Text = ""
        Me.rtbSrc.WordWrap = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.AssemblerToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(784, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewSourceFileToolStripMenuItem, Me.LoadSourceFileToolStripMenuItem, Me.SaveSourceFileToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(123, 20)
        Me.FileToolStripMenuItem.Text = "Assembler Source File"
        '
        'NewSourceFileToolStripMenuItem
        '
        Me.NewSourceFileToolStripMenuItem.Name = "NewSourceFileToolStripMenuItem"
        Me.NewSourceFileToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.NewSourceFileToolStripMenuItem.Text = "New Source File"
        '
        'LoadSourceFileToolStripMenuItem
        '
        Me.LoadSourceFileToolStripMenuItem.Name = "LoadSourceFileToolStripMenuItem"
        Me.LoadSourceFileToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.LoadSourceFileToolStripMenuItem.Text = "Load Source File"
        '
        'SaveSourceFileToolStripMenuItem
        '
        Me.SaveSourceFileToolStripMenuItem.Name = "SaveSourceFileToolStripMenuItem"
        Me.SaveSourceFileToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.SaveSourceFileToolStripMenuItem.Text = "Save Source File"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'AssemblerToolStripMenuItem
        '
        Me.AssemblerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AssembleAndSaveHEXFileToolStripMenuItem, Me.AssembleAndLoadProgramToolStripMenuItem})
        Me.AssemblerToolStripMenuItem.Name = "AssemblerToolStripMenuItem"
        Me.AssemblerToolStripMenuItem.Size = New System.Drawing.Size(68, 20)
        Me.AssemblerToolStripMenuItem.Text = "Assembler"
        '
        'AssembleAndSaveHEXFileToolStripMenuItem
        '
        Me.AssembleAndSaveHEXFileToolStripMenuItem.Name = "AssembleAndSaveHEXFileToolStripMenuItem"
        Me.AssembleAndSaveHEXFileToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.AssembleAndSaveHEXFileToolStripMenuItem.Text = "Assemble and Save HEX file"
        '
        'AssembleAndLoadProgramToolStripMenuItem
        '
        Me.AssembleAndLoadProgramToolStripMenuItem.Name = "AssembleAndLoadProgramToolStripMenuItem"
        Me.AssembleAndLoadProgramToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.AssembleAndLoadProgramToolStripMenuItem.Text = "Assemble and Load Program"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 500
        '
        'FormAssembler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 546)
        Me.Controls.Add(Me.rtbSrc)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FormAssembler"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Assembler"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rtbSrc As System.Windows.Forms.RichTextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewSourceFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadSourceFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveSourceFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AssemblerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AssembleAndSaveHEXFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AssembleAndLoadProgramToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
