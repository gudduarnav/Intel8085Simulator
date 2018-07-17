Imports System.IO

Public Class FormMain
    Private Shared machineState As New MachineState()

    Public Shared Function GetMachineState() As MachineState
        Return machineState
    End Function

    Private Sub FormMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        GetMachineState().StopExecution()
        RemoveHandler GetMachineState().OnStateUpdate, AddressOf OnStateUpdate
        RemoveHandler GetMachineState().OnEventInPortUpdate, AddressOf OnEventInPortUpdate
        RemoveHandler GetMachineState().OnEventOutPortUpdate, AddressOf OnEventOutPortUpdate
    End Sub

    Private Sub FormMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        Me.Text += " "

        FormReport.MainForm = Me
        AddHandler GetMachineState().OnStateUpdate, AddressOf OnStateUpdate
        AddHandler GetMachineState().OnEventInPortUpdate, AddressOf OnEventInPortUpdate
        AddHandler GetMachineState().OnEventOutPortUpdate, AddressOf OnEventOutPortUpdate
        LoadBios()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Dispose()
    End Sub

    Private Sub SaveASMFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveASMFileToolStripMenuItem.Click
        Try
            Dim frm As New SaveFileDialog()
            frm.Filter = "Memory Dump File (*.mem) |*.mem|All Files(*.*)|*.*||"
            frm.Title = "Dump Machine and Memory State"

            If frm.ShowDialog() = DialogResult.OK Then
                Dim sw As New StreamWriter(frm.FileName, False)
                Dim dta As Integer
                For i As Integer = 0 To &HFFFF
                    dta = FormMain.GetMachineState.GetMemory(i)
                    sw.Write(String.Format("{0:X2}", dta))
                Next
                sw.Flush()
                sw.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub OpenASMFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenASMFileToolStripMenuItem.Click
        Try
            Dim frm As New OpenFileDialog()
            frm.Filter = "Memory Dump File (*.mem) |*.mem|All Files(*.*)|*.*||"
            frm.Title = "Load Machine and Memory State"

            If frm.ShowDialog() = DialogResult.OK Then
                LoadMemoryFile(frm.FileName)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub NewASMFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewASMFileToolStripMenuItem.Click
        Try
            Dim frm As New OpenFileDialog()
            frm.Filter = "Intel Hex File(*.hex)|*.hex|Memory Dump File (*.mem) |*.mem|All Files(*.*)|*.*||"
            frm.Title = "Dump Machine and Memory State"

            If frm.ShowDialog() = DialogResult.OK Then
                LoadProgram(frm.FileName)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadBiosFrom(ByVal fpath As String)
        Try
            Dim pth As New DirectoryInfo(fpath)
            Dim fi() As FileInfo = pth.GetFiles("*.*")
            For Each ff As FileInfo In fi
                LoadProgram(ff.FullName)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadBios()
        Try
            Dim bios As New DirectoryInfo(Application.StartupPath + "\\" + "bios")
            If bios.Exists = True Then
                LoadBiosFrom(bios.FullName)
            Else
                bios.Create()
                LoadBios()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadProgram(ByVal fname As String)
        Try
            Dim fi As New FileInfo(fname)
            If fi.Exists = True Then
                If fi.Extension.ToLower().Trim() = ".mem" Then
                    LoadMemoryFile(fi.FullName)
                ElseIf fi.Extension.ToLower().Trim() = ".hex" Then
                    LoadHexFile(fi.FullName)
                ElseIf fi.Extension.ToLower().Trim() = ".com" Then
                    LoadComFile(fi.FullName)
                ElseIf fi.Extension.ToLower().Trim() = ".bin" Then
                    LoadBinFile(fi.FullName)
                ElseIf fi.Extension.ToLower().Trim() = ".exe" Then
                    LoadExeFile(fi.FullName)
                Else
                    Throw New Exception("Cannot load file " + fi.FullName)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error loading program file", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub
    Private Sub LoadBinFile(ByVal fname As String)
        Throw New Exception("Feature not implemented")
    End Sub
    Private Sub LoadComFile(ByVal fname As String)
        Throw New Exception("Feature not implemented")
    End Sub
    Private Sub LoadExeFile(ByVal fname As String)
        Throw New Exception("Feature not implemented")
    End Sub
    Private Sub LoadHexFile(ByVal fname As String)
        Try
            Dim fl As New StreamReader(fname)

            While Not (fl.Peek() = -1)
                Dim chr As Char = ChrW(fl.Read())
                If chr = ":" Then
                    Dim str As String
                    Dim numStyle As Globalization.NumberStyles = Globalization.NumberStyles.HexNumber
                    Dim ifmtprov As IFormatProvider = Globalization.CultureInfo.CurrentCulture

                    Dim ln As Integer = 0
                    str = ChrW(fl.Read)
                    str += ChrW(fl.Read)
                    Integer.TryParse(str, numStyle, ifmtprov, ln)

                    str = ChrW(fl.Read)
                    str += ChrW(fl.Read)
                    str += ChrW(fl.Read)
                    str += ChrW(fl.Read)
                    Dim s_addr As Integer = 0
                    Integer.TryParse(str, numStyle, ifmtprov, s_addr)

                    str = ChrW(fl.Read)
                    str += ChrW(fl.Read)
                    Dim tt As Integer = 0
                    Integer.TryParse(str, numStyle, ifmtprov, tt)

                    If tt = &H0 Then 'Data
                        While True
                            Dim ch As Integer = fl.Peek
                            If ch = -1 Then
                                Exit While
                            ElseIf ChrW(ch) = ":" Then
                                Exit While
                            End If

                            str = ChrW(fl.Read())
                            str += ChrW(fl.Read())
                            Dim instr As Integer = 0
                            Integer.TryParse(str, numStyle, ifmtprov, instr)

                            ch = fl.Peek()
                            If ch = -1 Or ChrW(ch) = ":" Or ChrW(ch) = vbCr Then
                                Exit While
                            Else
                                GetMachineState().SetMemory(s_addr, instr, True)
                                s_addr += 1
                            End If
                        End While
                    ElseIf tt = &H0 Then 'EOF
                        Exit While
                    End If
                End If
            End While
            fl.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try

    End Sub

    Private Sub LoadMemoryFile(ByVal fname As String)
        Try
            Dim sr As New StreamReader(fname)
            Dim str, str1 As String
            Dim len As Integer
            len = 0
            str = sr.ReadToEnd()
            Dim dta As Integer
            For i As Integer = 0 To &HFFFF
                dta = 0
                Try
                    str1 = str.Substring(len, 2)
                    len += 2
                    dta = Integer.Parse(str1, Globalization.NumberStyles.HexNumber)
                Catch ex1 As Exception

                End Try
                If Not (FormMain.GetMachineState.GetMemory(i) = dta) Then
                    FormMain.GetMachineState.SetMemory(i, dta, True)
                End If
            Next
            sr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub

    Private Sub RegisterStateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegisterStateToolStripMenuItem.Click
        Dim frm As New FormRegisterState()
        frm.MdiParent = Me
        frm.Show()
    End Sub


    Private Sub ResetClearMemoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetClearMemoryToolStripMenuItem.Click
        GetMachineState().StopExecution()
        GetMachineState().ClearMemory()
    End Sub

    Private Sub ResetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetToolStripMenuItem.Click
        GetMachineState().StopExecution()
        GetMachineState().SetPC(&H0, True)
    End Sub

    Private Sub StartExecutionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartExecutionToolStripMenuItem.Click
        GetMachineState().StartExecution()
    End Sub

    Private Sub StopExecutionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopExecutionToolStripMenuItem.Click
        GetMachineState().StopExecution()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        MessageBox.Show("Intel 8085 Simulator, Assembler and IDE" + vbCrLf + _
                        "Designed by Arnav Mukhopadhyay", _
                        "Intel 8085 Simulator, Assembler, IDE",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub MemoryViewerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MemoryViewerToolStripMenuItem.Click
        Dim frm As New FormMemoryViewer()
        frm.MdiParent = Me
        frm.Show()

    End Sub

    Public Sub OnStateUpdate(ByVal bRunning As Boolean, ByRef macState As MachineState)
        Dim str As String
        str = Me.Text
        Try
            Dim status As String = ""
            If bRunning = True Then
                status = "RUNNING"
            Else
                status = "STOPPED"
            End If

            If str.IndexOf("[") = -1 Then
                str = String.Format("{0} [{1}]", str, status)
            Else
                str = String.Format("{0}[{1}]", str.Substring(0, str.IndexOf("[")), status)
            End If
        Catch ex As Exception

        End Try

        Me.Text = str
    End Sub

    Private Sub InterruptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InterruptToolStripMenuItem.Click
        Dim frm As New FormIntrSerial()
        frm.MdiParent = Me
        frm.Show()

    End Sub

    Private Sub PortViewerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PortViewerToolStripMenuItem.Click
        Dim frm As New FormPortViewer()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub HexFileDecoderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HexFileDecoderToolStripMenuItem.Click
        Dim frm As New FormHexDecode()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub HexFileEncoderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HexFileEncoderToolStripMenuItem.Click
        Dim frm As New FormHexEncode()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub AssemblerEditorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssemblerEditorToolStripMenuItem.Click
        Dim frm As New FormAssembler()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub Httparnavguddu6tenetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Httparnavguddu6tenetToolStripMenuItem.Click
        Try
            System.Diagnostics.Process.Start("http://gudduarnav.eu5.org/")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub IOPortDeviceListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IOPortDeviceListToolStripMenuItem.Click
        Dim sb As String
        sb = "IO Device Port list" + vbCrLf
        sb += "IN PORT = 00H  KEYBOARD INPUT THROUGH TEXT BOX" + vbCrLf
        sb += "OUT PORT = 00H MESSAGE DISPLAY THROUGH TEXT DISPLAY" + vbCrLf
        sb += "IN PORT = 01H  HEX INPUT THROUGH TEXT BOX" + vbCrLf
        sb += "OUT PORT = 01H HEX DISPLAY" + vbCrLf
        sb += "IN PORT = 02H  DECIMAL INPUT THROUGH TEXT BOX" + vbCrLf
        sb += "OUT PORT = 02H DECIMAL DISPLAY" + vbCrLf
        sb += "IN PORT = 03H  BIT INPUT THROUGH BIT DISPLAY" + vbCrLf
        sb += "OUT PORT = 03H BIT OUTPUT THROUGH BIT DISPLAY" + vbCrLf

        sb += "OUT PORT = 0EH COMMAND OUTPUT PORT FOR TEXT AND GRAPHICS CONSOLE DISPLAY " + vbCrLf
        sb += vbTab + "Command bit 0: always 1 to enable" + vbCrLf
        sb += vbTab + "Command bit 1: clear" + vbCrLf
        sb += vbTab + "Command bit 2: 1 for ASCII text display" + vbCrLf
        sb += vbTab + "Command bit 3: 1 for graphics display" + vbCrLf
        sb += vbTab + "Command bit 4: data is x location or row" + vbCrLf
        sb += vbTab + "Command bit 5: data is y location or column" + vbCrLf
        sb += vbTab + "Command bit 6: data is AARRGGBB color" + vbCrLf
        sb += vbTab + "Command bit 7: 1 to repaint" + vbCrLf


        sb += "OUT PORT = 0FH DATA OUTPUT PORT FOR TEXT AND GRAPHICS CONSOLE DISPLAY" + vbCrLf
        sb += "IN PORT = 0FH DATA INPUT PORT FOR TEXT AND GRAPHICS CONSOLE DISPLAY" + vbCrLf
        sb += "IN/OUT 40H = Serial Data IO Port" + vbCrLf
        sb += "OUT 40H = Control Register for Serial Port" + vbCrLf + "IN 40H = Status Register for Serial Port" + vbCrLf

        MessageBox.Show(sb, "IO Device Port List", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub


    Private Delegate Sub dOutPort(ByVal p As FormMain, ByVal data As Integer)
    Private Delegate Function dInPort(ByVal p As FormMain) As Integer
    Public Sub OnEventOutPortUpdate(ByVal data As Byte, ByVal port As Byte, ByRef macState As MachineState)
        Try
            If port = 0 Then
                Invoke(New dOutPort(AddressOf FormKeyDisplay.OutPort), New Object() {Me, data})
            ElseIf port = 1 Then
                Invoke(New dOutPort(AddressOf FormHexIO.OutPort), New Object() {Me, data})
            ElseIf port = 2 Then
                Invoke(New dOutPort(AddressOf FormDecDisplay.OutPort), New Object() {Me, data})
            ElseIf port = 3 Then
                Invoke(New dOutPort(AddressOf FormBitDisplay.OutPort), New Object() {Me, data})
            ElseIf port = &HE Then
                Invoke(New dOutPort(AddressOf FormScreenDis.OutCommandPort), New Object() {Me, data})
            ElseIf port = &HF Then
                Invoke(New dOutPort(AddressOf FormScreenDis.OutPort), New Object() {Me, data})
            ElseIf port = &H40 Then
                If FormUsart.PortInWait() = False Then
                    Invoke(New dLoadMe(AddressOf FormUsart.LoadMe), New Object() {Me})
                    FormDecDisplay.PortInWait()
                End If
                Invoke(New dOutPort(AddressOf FormUsart.OutPort), New Object() {Me, data})
            ElseIf port = &H41 Then
                If FormUsart.PortInWait() = False Then
                    Invoke(New dLoadMe(AddressOf FormUsart.LoadMe), New Object() {Me})
                    FormDecDisplay.PortInWait()
                End If
                Invoke(New dOutPort(AddressOf FormUsart.OutPortCommand), New Object() {Me, data})
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Delegate Sub dLoadMe(ByVal frm As FormMain)
    Public Sub OnEventInPortUpdate(ByRef data As Byte, ByVal port As Byte, ByRef macState As MachineState, ByRef prio As Integer) ' higher prio will be able to push in data

        Dim bHandled As Boolean
        bHandled = False
        Try
            If port = 0 Then
                bHandled = True
                If FormKeyDisplay.PortInWait() = False Then
                    Invoke(New dLoadMe(AddressOf FormKeyDisplay.LoadMe), New Object() {Me})
                    FormKeyDisplay.PortInWait()
                End If
                data = Invoke(New dInPort(AddressOf FormKeyDisplay.InPort), New Object() {Me})
            ElseIf port = 1 Then
                bHandled = True
                If FormHexIO.PortInWait() = False Then
                    Invoke(New dLoadMe(AddressOf FormHexIO.LoadMe), New Object() {Me})
                    FormHexIO.PortInWait()
                End If
                data = Invoke(New dInPort(AddressOf FormHexIO.InPort), New Object() {Me})
            ElseIf port = 2 Then
                bHandled = True
                If FormDecDisplay.PortInWait() = False Then
                    Invoke(New dLoadMe(AddressOf FormDecDisplay.LoadMe), New Object() {Me})
                    FormDecDisplay.PortInWait()
                End If
                data = Invoke(New dInPort(AddressOf FormDecDisplay.InPort), New Object() {Me})
            ElseIf port = 3 Then
                bHandled = True
                data = Invoke(New dInPort(AddressOf FormBitDisplay.InPort), New Object() {Me})
            ElseIf port = &H40 Then
                bHandled = True
                If FormUsart.PortInWait() = False Then
                    Invoke(New dLoadMe(AddressOf FormUsart.LoadMe), New Object() {Me})
                    FormDecDisplay.PortInWait()
                End If
                data = Invoke(New dInPort(AddressOf FormUsart.InPort), New Object() {Me})
            ElseIf port = &H41 Then
                bHandled = True
                If FormUsart.PortInWait() = False Then
                    Invoke(New dLoadMe(AddressOf FormUsart.LoadMe), New Object() {Me})
                    FormDecDisplay.PortInWait()
                End If
                data = Invoke(New dInPort(AddressOf FormUsart.InPortCommand), New Object() {Me})
            End If
        Catch ex As Exception
            bHandled = False

        End Try

        If bHandled = True Then
            prio = 10000
        End If
    End Sub

    Private Sub ShowAlphanumericKeyboardDisplayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowAlphanumericKeyboardDisplayToolStripMenuItem.Click
        FormKeyDisplay.LoadMe(Me)
    End Sub

    Private Sub HexadecimalDisplayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HexadecimalDisplayToolStripMenuItem.Click
        FormHexIO.LoadMe(Me)
    End Sub

    Private Sub DecimalDisplayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecimalDisplayToolStripMenuItem.Click
        FormDecDisplay.LoadMe(Me)
    End Sub

    Private Sub BitDisplayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BitDisplayToolStripMenuItem.Click
        FormBitDisplay.LoadMe(Me)
    End Sub

    Private Sub TextConsoleDisplayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextConsoleDisplayToolStripMenuItem.Click
        FormScreenDis.LoadMe(Me)
    End Sub

    Public Shared Sub MakeMeFirst(ByVal frm As Form)
        If frm Is Nothing Then
            Exit Sub
        End If

        frm.BringToFront()
    End Sub

    Private Sub SerialPortToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SerialPortToolStripMenuItem.Click
        FormUsart.LoadMe(Me)
    End Sub

    Private Sub KitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KitToolStripMenuItem.Click
        Dim frm As New FormKit()
        frm.MdiParent = Me
        frm.Show()
    End Sub
End Class
