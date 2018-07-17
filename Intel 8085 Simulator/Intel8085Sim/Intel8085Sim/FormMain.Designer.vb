<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewASMFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenASMFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveASMFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RegisterStateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MemoryViewerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PortViewerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InterruptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MachineStateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResetClearMemoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartExecutionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StopExecutionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeviceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowAlphanumericKeyboardDisplayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HexadecimalDisplayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DecimalDisplayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BitDisplayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextConsoleDisplayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SerialPortToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProgrammerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AssemblerEditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HexFileDecoderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HexFileEncoderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Httparnavguddu6tenetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IOPortDeviceListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ViewToolStripMenuItem, Me.MachineStateToolStripMenuItem, Me.DeviceToolStripMenuItem, Me.ProgrammerToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(641, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewASMFileToolStripMenuItem, Me.OpenASMFileToolStripMenuItem, Me.SaveASMFileToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
        Me.FileToolStripMenuItem.Text = "Loader"
        '
        'NewASMFileToolStripMenuItem
        '
        Me.NewASMFileToolStripMenuItem.Name = "NewASMFileToolStripMenuItem"
        Me.NewASMFileToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.NewASMFileToolStripMenuItem.Text = "Load Program"
        '
        'OpenASMFileToolStripMenuItem
        '
        Me.OpenASMFileToolStripMenuItem.Name = "OpenASMFileToolStripMenuItem"
        Me.OpenASMFileToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.OpenASMFileToolStripMenuItem.Text = "Load Memory File"
        '
        'SaveASMFileToolStripMenuItem
        '
        Me.SaveASMFileToolStripMenuItem.Name = "SaveASMFileToolStripMenuItem"
        Me.SaveASMFileToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.SaveASMFileToolStripMenuItem.Text = "Save Memory File"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RegisterStateToolStripMenuItem, Me.MemoryViewerToolStripMenuItem, Me.PortViewerToolStripMenuItem, Me.InterruptToolStripMenuItem, Me.KitToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(87, 20)
        Me.ViewToolStripMenuItem.Text = "Intel 8085"
        '
        'RegisterStateToolStripMenuItem
        '
        Me.RegisterStateToolStripMenuItem.Name = "RegisterStateToolStripMenuItem"
        Me.RegisterStateToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.RegisterStateToolStripMenuItem.Text = "Register State"
        '
        'MemoryViewerToolStripMenuItem
        '
        Me.MemoryViewerToolStripMenuItem.Name = "MemoryViewerToolStripMenuItem"
        Me.MemoryViewerToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.MemoryViewerToolStripMenuItem.Text = "Memory Viewer"
        '
        'PortViewerToolStripMenuItem
        '
        Me.PortViewerToolStripMenuItem.Name = "PortViewerToolStripMenuItem"
        Me.PortViewerToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.PortViewerToolStripMenuItem.Text = "Port Viewer"
        '
        'InterruptToolStripMenuItem
        '
        Me.InterruptToolStripMenuItem.Name = "InterruptToolStripMenuItem"
        Me.InterruptToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.InterruptToolStripMenuItem.Text = "Interrupt and Serial IO"
        '
        'KitToolStripMenuItem
        '
        Me.KitToolStripMenuItem.Name = "KitToolStripMenuItem"
        Me.KitToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.KitToolStripMenuItem.Text = "8085 Kit"
        '
        'MachineStateToolStripMenuItem
        '
        Me.MachineStateToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ResetClearMemoryToolStripMenuItem, Me.ResetToolStripMenuItem, Me.StartExecutionToolStripMenuItem, Me.StopExecutionToolStripMenuItem})
        Me.MachineStateToolStripMenuItem.Name = "MachineStateToolStripMenuItem"
        Me.MachineStateToolStripMenuItem.Size = New System.Drawing.Size(108, 20)
        Me.MachineStateToolStripMenuItem.Text = "Machine State"
        '
        'ResetClearMemoryToolStripMenuItem
        '
        Me.ResetClearMemoryToolStripMenuItem.Name = "ResetClearMemoryToolStripMenuItem"
        Me.ResetClearMemoryToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
        Me.ResetClearMemoryToolStripMenuItem.Text = "Reset And Clear Memory"
        '
        'ResetToolStripMenuItem
        '
        Me.ResetToolStripMenuItem.Name = "ResetToolStripMenuItem"
        Me.ResetToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
        Me.ResetToolStripMenuItem.Text = "Reset"
        '
        'StartExecutionToolStripMenuItem
        '
        Me.StartExecutionToolStripMenuItem.Name = "StartExecutionToolStripMenuItem"
        Me.StartExecutionToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
        Me.StartExecutionToolStripMenuItem.Text = "Start Execution"
        '
        'StopExecutionToolStripMenuItem
        '
        Me.StopExecutionToolStripMenuItem.Name = "StopExecutionToolStripMenuItem"
        Me.StopExecutionToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
        Me.StopExecutionToolStripMenuItem.Text = "Stop Execution"
        '
        'DeviceToolStripMenuItem
        '
        Me.DeviceToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowAlphanumericKeyboardDisplayToolStripMenuItem, Me.HexadecimalDisplayToolStripMenuItem, Me.DecimalDisplayToolStripMenuItem, Me.BitDisplayToolStripMenuItem, Me.TextConsoleDisplayToolStripMenuItem, Me.SerialPortToolStripMenuItem})
        Me.DeviceToolStripMenuItem.Name = "DeviceToolStripMenuItem"
        Me.DeviceToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
        Me.DeviceToolStripMenuItem.Text = "Device"
        '
        'ShowAlphanumericKeyboardDisplayToolStripMenuItem
        '
        Me.ShowAlphanumericKeyboardDisplayToolStripMenuItem.Name = "ShowAlphanumericKeyboardDisplayToolStripMenuItem"
        Me.ShowAlphanumericKeyboardDisplayToolStripMenuItem.Size = New System.Drawing.Size(336, 22)
        Me.ShowAlphanumericKeyboardDisplayToolStripMenuItem.Text = "Show Alphanumeric Keyboard and Display"
        '
        'HexadecimalDisplayToolStripMenuItem
        '
        Me.HexadecimalDisplayToolStripMenuItem.Name = "HexadecimalDisplayToolStripMenuItem"
        Me.HexadecimalDisplayToolStripMenuItem.Size = New System.Drawing.Size(336, 22)
        Me.HexadecimalDisplayToolStripMenuItem.Text = "Hexadecimal Display"
        '
        'DecimalDisplayToolStripMenuItem
        '
        Me.DecimalDisplayToolStripMenuItem.Name = "DecimalDisplayToolStripMenuItem"
        Me.DecimalDisplayToolStripMenuItem.Size = New System.Drawing.Size(336, 22)
        Me.DecimalDisplayToolStripMenuItem.Text = "Decimal Display"
        '
        'BitDisplayToolStripMenuItem
        '
        Me.BitDisplayToolStripMenuItem.Name = "BitDisplayToolStripMenuItem"
        Me.BitDisplayToolStripMenuItem.Size = New System.Drawing.Size(336, 22)
        Me.BitDisplayToolStripMenuItem.Text = "Bit Display"
        '
        'TextConsoleDisplayToolStripMenuItem
        '
        Me.TextConsoleDisplayToolStripMenuItem.Name = "TextConsoleDisplayToolStripMenuItem"
        Me.TextConsoleDisplayToolStripMenuItem.Size = New System.Drawing.Size(336, 22)
        Me.TextConsoleDisplayToolStripMenuItem.Text = "Text and Graphics  Console Display"
        '
        'SerialPortToolStripMenuItem
        '
        Me.SerialPortToolStripMenuItem.Name = "SerialPortToolStripMenuItem"
        Me.SerialPortToolStripMenuItem.Size = New System.Drawing.Size(336, 22)
        Me.SerialPortToolStripMenuItem.Text = "Serial Port"
        '
        'ProgrammerToolStripMenuItem
        '
        Me.ProgrammerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AssemblerEditorToolStripMenuItem, Me.HexFileDecoderToolStripMenuItem, Me.HexFileEncoderToolStripMenuItem})
        Me.ProgrammerToolStripMenuItem.Name = "ProgrammerToolStripMenuItem"
        Me.ProgrammerToolStripMenuItem.Size = New System.Drawing.Size(87, 20)
        Me.ProgrammerToolStripMenuItem.Text = "Programmer"
        '
        'AssemblerEditorToolStripMenuItem
        '
        Me.AssemblerEditorToolStripMenuItem.Name = "AssemblerEditorToolStripMenuItem"
        Me.AssemblerEditorToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.AssemblerEditorToolStripMenuItem.Text = "Assembler and Editor"
        '
        'HexFileDecoderToolStripMenuItem
        '
        Me.HexFileDecoderToolStripMenuItem.Name = "HexFileDecoderToolStripMenuItem"
        Me.HexFileDecoderToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.HexFileDecoderToolStripMenuItem.Text = "Hex Decoder"
        '
        'HexFileEncoderToolStripMenuItem
        '
        Me.HexFileEncoderToolStripMenuItem.Name = "HexFileEncoderToolStripMenuItem"
        Me.HexFileEncoderToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.HexFileEncoderToolStripMenuItem.Text = "Hex Encoder"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem, Me.Httparnavguddu6tenetToolStripMenuItem, Me.IOPortDeviceListToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(45, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(350, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'Httparnavguddu6tenetToolStripMenuItem
        '
        Me.Httparnavguddu6tenetToolStripMenuItem.Name = "Httparnavguddu6tenetToolStripMenuItem"
        Me.Httparnavguddu6tenetToolStripMenuItem.Size = New System.Drawing.Size(350, 22)
        Me.Httparnavguddu6tenetToolStripMenuItem.Text = "Homepage URL: http://gudduarnav.eu5.org/"
        '
        'IOPortDeviceListToolStripMenuItem
        '
        Me.IOPortDeviceListToolStripMenuItem.Name = "IOPortDeviceListToolStripMenuItem"
        Me.IOPortDeviceListToolStripMenuItem.Size = New System.Drawing.Size(350, 22)
        Me.IOPortDeviceListToolStripMenuItem.Text = "IO Port Device List"
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(641, 312)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Intel 8085 Simulator"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewASMFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenASMFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveASMFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RegisterStateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MemoryViewerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PortViewerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InterruptToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MachineStateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResetClearMemoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StartExecutionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StopExecutionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProgrammerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AssemblerEditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HexFileDecoderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HexFileEncoderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Httparnavguddu6tenetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IOPortDeviceListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeviceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowAlphanumericKeyboardDisplayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HexadecimalDisplayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DecimalDisplayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BitDisplayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TextConsoleDisplayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SerialPortToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
