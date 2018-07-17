Imports System.IO
Imports System.Text

Public Class FormAssembler
    Private fname As String = ""
    Private bDirty As Boolean = False

    Private Sub FormAssembler_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bDirty = True Then
            Dim dlgResult As DialogResult
            dlgResult = MessageBox.Show("The ASM file had been modified. Would you like to save it before exiting?", "File Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If dlgResult = DialogResult.Yes Then
                SaveASM()
            ElseIf dlgResult = DialogResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub
    Private Sub FormAssembler_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not (MdiParent Is Nothing) Then
            Me.MainMenuStrip.Hide()
        End If
        rtbSrc.Focus()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub NewSourceFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewSourceFileToolStripMenuItem.Click
        ClearAsm()
    End Sub

    Private Sub ClearAsm()
        SaveASM()

        rtbSrc.Clear()
        rtbSrc.Focus()
        fname = ""
        bDirty = False
        RefreshTitle()

    End Sub
    Private Sub LoadSourceFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadSourceFileToolStripMenuItem.Click
        LoadASM()
    End Sub

    Private Sub LoadASM()
        Try
            Dim frm As New OpenFileDialog()
            frm.Multiselect = False
            frm.Title = "Load Intel 8085 ASM Source file"
            frm.Filter = "ASM File (*.asm)|*.asm|All files (*.*)|*.*||"
            If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                ClearAsm()

                fname = frm.FileName
                Dim sr As New StreamReader(fname)
                rtbSrc.Text = sr.ReadToEnd()
                sr.Close()
            End If
            RefreshTitle()
            bDirty = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SaveASM()
        Try
            If bDirty = False Then
                Exit Sub
            End If

            If fname.Trim.Length < 1 Then
                Dim frm As New SaveFileDialog()
                frm.Title = "Save Intel 8085 ASM Source file"
                frm.Filter = "ASM File (*.asm)|*.asm|All files (*.*)|*.*||"
                If frm.ShowDialog() = DialogResult.OK Then
                    fname = frm.FileName
                End If
            End If

            If fname.Trim.Length < 1 Then
                Exit Sub
            End If

            Dim sw As New StreamWriter(fname, False, System.Text.Encoding.ASCII)
            sw.Write(rtbSrc.Text)
            sw.Flush()
            sw.Close()
            bDirty = False
            RefreshTitle()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SaveSourceFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveSourceFileToolStripMenuItem.Click
        SaveASM()
    End Sub

    Private Sub rtbSrc_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rtbSrc.KeyUp
        ProcessKey(e.KeyCode, e.Handled)
    End Sub

    Private Sub rtbSrc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtbSrc.TextChanged
        bDirty = True
    End Sub

    Private Sub RefreshTitle()
        Try
            Dim strTitle As String
            strTitle = Me.Text

            If fname.Trim.Length < 1 Then
                If strTitle.IndexOf("[") = -1 Then

                Else
                    strTitle = strTitle.Substring(0, strTitle.IndexOf("["))

                End If
            Else
                If strTitle.IndexOf("[") = -1 Then
                Else
                    strTitle = strTitle.Substring(0, strTitle.IndexOf("["))
                End If
                strTitle += "[ " + fname + " ]"
            End If
            Me.Text = strTitle
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ProcessKey(ByVal k As Keys, ByRef bDone As Boolean)
        If k = Keys.Tab Then
            Dim idx As Integer = rtbSrc.SelectionStart

            Dim str As String = rtbSrc.Text
            Dim str1 As String
            If idx <= 1 Then
                str1 = ""
            Else
                str1 = str.Substring(0, idx)
            End If

            Dim str2 As String
            If idx < str.Length Then
                str2 = str.Substring(idx)
            Else
                str2 = ""
            End If

            Dim strSpaces As String = ""
            Dim col As Integer
            col = idx - rtbSrc.GetFirstCharIndexOfCurrentLine()
            col = col Mod 5
            col = 5 - col
            For i As Integer = 1 To col
                strSpaces += " "
            Next
            rtbSrc.Text = String.Format("{0}{1}{2}", str1, strSpaces, str2)
            rtbSrc.SelectionStart = idx + strSpaces.Length
            bDone = True
        ElseIf k = Keys.F5 Then
            Me.AssembleAndSaveHEXFileToolStripMenuItem.PerformClick()
            bDone = True
        ElseIf k = Keys.F2 Then
            Me.LoadSourceFileToolStripMenuItem.PerformClick()
            bDone = True
        ElseIf k = Keys.F3 Then
            Me.SaveSourceFileToolStripMenuItem.PerformClick()
            bDone = True
        ElseIf k = Keys.F9 Then
            Me.AssembleAndLoadProgramToolStripMenuItem.PerformClick()
            bDone = True
        End If

    End Sub



    Private Sub AssembleAndSaveHEXFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssembleAndSaveHEXFileToolStripMenuItem.Click
        FormReport.Clear()
        Dim ll As New List(Of AsmLine)
        If AssembleProgram(ll) = True Then
            ' use hex encoder to save
            Try
                HexSave(ll)
                LoadMem(ll)
                WriteLine("Program saved and then successfully loaded to memory")
            Catch ex As Exception
                WriteLine(ex.Message)
            End Try
        End If

    End Sub

    Private Sub SaveHexDataToFile(ByVal f As StreamWriter, ByVal addr As Integer, ByVal data As List(Of Integer))
        Dim dstr As String
        dstr = ""
        Dim startAddress As Integer
        startAddress = addr

        For Each dt As Integer In data
            dstr += String.Format("{0:X2}", dt)
            addr += 1

            If dstr.Length >= &H10 Then
                ' save the data
                SaveHexNow(f, startAddress, dstr)
                startAddress = addr
                dstr = ""
            End If
        Next
        If dstr.Length > 0 Then
            SaveHexNow(f, startAddress, dstr)
        End If
    End Sub
    Private Sub SaveHexNow(ByVal f As StreamWriter, ByVal sa As Integer, ByVal data As String)
        Dim str As String
        str = ":"
        str += String.Format("{0:X2}", data.Length)
        str += String.Format("{0:X4}", sa)
        str += String.Format("{0:X2}", 0)
        str += data.Trim
        str += String.Format("{0:X2}", CheckSum(str.Substring(1)))
        str = str.ToUpper().Trim
        f.WriteLine(str)

    End Sub
    Private Function CheckSum(ByVal str As String) As Integer
        Dim ss As String
        Dim ii As Integer
        Dim ck As Integer
        ck = 0
        For i As Integer = 0 To str.Length - 1 Step 2
            ii = 0
            Try
                ss = str.Substring(i, 2)
                ss = ss.Trim.ToUpper() + "H"

                ii = ParseInteger(ss)

            Catch ex As Exception

            End Try
            ck += ii
        Next
        ck = Not (ck)
        ck += 1
        ck = ck And &HFF
        Return ck
    End Function
    Private Sub HexSave(ByVal l As List(Of AsmLine))
        Dim sa As Integer
        Dim startAddress As Integer
        Dim data As New List(Of Integer)
        sa = -1
        startAddress = 0
        Dim f As New FileInfo(fname)
        Dim fx As New FileInfo(f.DirectoryName + "/" + f.Name.Replace(f.Extension, ".hex"))
        fx.Delete()
        Dim ff As New StreamWriter(fx.FullName, False, System.Text.Encoding.ASCII)
        For Each ll As AsmLine In l
            If sa = -1 Then
                sa = ll.addr
                startAddress = sa
                data.Clear()
            End If

            If ll.addr <> sa Then
                SaveHexDataToFile(ff, startAddress, data)
                data.Clear()
                sa = ll.addr
                startAddress = sa
            End If

            data.Add(ll.data)
            sa += 1
        Next
        If data.Count > 0 Then
            SaveHexDataToFile(ff, startAddress, data)
        End If
        ff.WriteLine(":00000001FF")
        ff.Flush()
        ff.Close()
    End Sub

    Private Sub LoadMem(ByVal l As List(Of AsmLine))
        Dim ms As MachineState = FormMain.GetMachineState
        For Each l1 As AsmLine In l
            ms.SetMemory(l1.addr, l1.data, True)
        Next
    End Sub

    Private Sub AssembleAndLoadProgramToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssembleAndLoadProgramToolStripMenuItem.Click
        FormReport.Clear()

        Dim ll As New List(Of AsmLine)
        If AssembleProgram(ll) = True Then
            ' use loader to load
            LoadMem(ll)
        End If

    End Sub

    Structure AsmLine
        Public addr As Integer
        Public data As Integer
    End Structure

    Structure SrcLine
        Public addr As Integer
        Public st As String
    End Structure

    Structure AsmLabel
        Public addr As Integer
        Public lbl As String
    End Structure

    Private Sub WriteLine(ByVal str As String)
        FormReport.WriteLine(str)
    End Sub
    Private Function AssembleProgram(ByRef ll As List(Of AsmLine)) As Boolean
        Try
            WriteLine("Assembler stated...")
            WriteLine(String.Format("Program Name: {0}", fname))

            Dim strProgram As String
            strProgram = rtbSrc.Text
            strProgram = AsmPreprocess(strProgram)

            Dim llp As New List(Of String)
            TrimProg(strProgram, llp)

            Dim srcLines As New List(Of SrcLine)
            Dim jmpLabel As New List(Of AsmLabel)
            AsmPre2(llp, srcLines, jmpLabel)

            Asmble(srcLines, jmpLabel, ll)
            WriteLine("Program assembled successfully")
            Return True
        Catch ex As Exception
            WriteLine("Assembling failed.")
            WriteLine("Exception: " + ex.Message)
            Return False
        End Try

    End Function

    Private Sub PrepAsmByte(ByRef asm As List(Of AsmLine), ByVal addr As Integer, ByVal data1 As Integer)
        Dim a As New AsmLine()
        a.addr = addr
        a.data = data1 And &HFF
        asm.Add(a)
    End Sub

    Private Sub PrepAsmInt(ByRef asm As List(Of AsmLine), ByVal addr As Integer, ByVal data2 As Integer)
        Dim l, h As Integer
        l = data2 And &HFF
        h = (data2 >> 8) And &HFF
        PrepAsmByte(asm, addr, l)
        PrepAsmByte(asm, addr + 1, h)
    End Sub

    Private Sub PrepAsm1(ByRef asm As List(Of AsmLine), ByVal addr As Integer, ByVal ins As Integer)
        PrepAsmByte(asm, addr, ins)
    End Sub

    Private Sub PrepAsm2(ByRef asm As List(Of AsmLine), ByVal addr As Integer, ByVal ins As Integer, ByVal databyte As Integer)
        PrepAsmByte(asm, addr, ins)
        PrepAsmByte(asm, addr + 1, databyte)
    End Sub

    Private Sub PrepAsm3(ByRef asm As List(Of AsmLine), ByVal addr As Integer, ByVal ins As Integer, ByVal dataint As Integer)
        PrepAsmByte(asm, addr, ins)
        PrepAsmInt(asm, addr + 1, dataint)
    End Sub

    Private Function IsAsm(ByVal s As String, ByRef sData As String, ByVal ins As String) As Boolean
        s = s.Trim()
        Dim i As Integer
        i = s.ToUpper().IndexOf(ins.ToUpper())
        If i = -1 Then
            Return False
        ElseIf i = 0 Then
            ' found it
        Else
            Return False
        End If
        s = s.Remove(0, ins.Length)
        If s.Length = 0 Then
            ' found it
        Else
            If s.Chars(0) = " " Then
                'found it
            Else
                Return False
            End If
        End If

        s = s.Trim
        sData = s
        Return True
    End Function

    Private Function DetReg(ByVal r As String) As Integer
        r = r.Trim.ToUpper
        Dim s() As String = {"B", "C", "D", "E", "H", "L", "M", "A"}
        For i As Integer = 0 To s.Length - 1
            If s(i).IndexOf(r) = 0 Then
                Return i
            End If
        Next
        Return -1
    End Function
    Private Function DetRegp(ByVal r As String) As Integer
        r = r.Trim.ToUpper
        Dim s() As String = {"B", "D", "H", "SP"}
        For i As Integer = 0 To s.Length - 1
            If s(i).IndexOf(r) = 0 Then
                Return i
            End If
        Next
        Return -1
    End Function
    Private Function DetRegp_psw(ByVal r As String) As Integer
        r = r.Trim.ToUpper
        Dim s() As String = {"B", "D", "H", "PSW"}
        For i As Integer = 0 To s.Length - 1
            If s(i).IndexOf(r) = 0 Then
                Return i
            End If
        Next
        Return -1
    End Function

    Private Sub EmptyStrException(ByVal str As String, ByVal src As SrcLine)
        If str.Trim.Length < 1 Then
            Exit Sub
        End If

        Throw New Exception(String.Format("Wrong syntax of instruction specified. {0:X4} {1}", src.addr, src.st))
    End Sub

    Private Function JGetAddress(ByVal str As String, ByVal lbl As List(Of AsmLabel)) As Integer
        Try
            For Each lb As AsmLabel In lbl
                If lb.lbl.Trim.ToUpper = str.ToUpper Then
                    Return lb.addr
                End If
            Next

            Return ParseInteger(str.Trim)
        Catch ex As Exception
            Throw New Exception("Cannot find the Jump label")
        End Try
    End Function
    Private Sub Asmble(ByVal src As List(Of SrcLine), ByVal lbl As List(Of AsmLabel), ByRef asm As List(Of AsmLine))
        WriteLine("Code Generation started...")
        For Each s As SrcLine In src
            Dim str As String = ""
            Dim sr, ds As String
            Dim csr, cds, c, data As Integer
            If IsAsm(s.st, str, "DB") = True Then
                PrepAsmByte(asm, s.addr, ParseInteger(str))
            ElseIf IsAsm(s.st, str, "MOV") = True Then
                ds = str.Substring(0, str.IndexOf(",")).Trim
                sr = str.Substring(str.IndexOf(",") + 1).Trim
                csr = DetReg(sr.ToUpper())
                cds = DetReg(ds.ToUpper())
                If (csr = -1) Or (cds = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = csr Or (cds << 3) Or (1 << 6)
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "XCHG") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HEB)
            ElseIf IsAsm(s.st, str, "MVI") = True Then
                ds = str.Substring(0, str.IndexOf(",")).Trim.ToUpper()
                sr = str.Substring(str.IndexOf(",") + 1).Trim
                data = JGetAddress(sr, lbl)
                cds = DetReg(ds)
                If (cds = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &H6 Or (cds << 3)
                PrepAsm2(asm, s.addr, c, data)
            ElseIf IsAsm(s.st, str, "LXI") = True Then
                ds = str.Substring(0, str.IndexOf(",")).Trim.ToUpper()
                sr = str.Substring(str.IndexOf(",") + 1).Trim
                data = JGetAddress(sr, lbl)
                cds = DetRegp(ds)
                If (cds = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If

                c = &H1 Or (cds << 4)
                PrepAsm3(asm, s.addr, c, data)
            ElseIf IsAsm(s.st, str, "LDAX") = True Then
                ds = str.Trim.ToUpper()
                cds = DetRegp(ds)
                If (cds = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &HA Or (cds << 4)
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "STAX") = True Then
                ds = str.Trim.ToUpper()
                cds = DetRegp(ds)
                If (cds = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &H2 Or (cds << 4)
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "LHLD") = True Then
                sr = str.Trim.ToUpper()
                data = JGetAddress(sr, lbl)
                PrepAsm3(asm, s.addr, &H2A, data)
            ElseIf IsAsm(s.st, str, "SHLD") = True Then
                sr = str.Trim.ToUpper()
                data = JGetAddress(sr, lbl)
                PrepAsm3(asm, s.addr, &H22, data)
            ElseIf IsAsm(s.st, str, "LDA") = True Then
                sr = str.Trim.ToUpper()
                data = JGetAddress(sr, lbl)
                PrepAsm3(asm, s.addr, &H3A, data)
            ElseIf IsAsm(s.st, str, "STA") = True Then
                sr = str.Trim.ToUpper()
                data = JGetAddress(sr, lbl)
                PrepAsm3(asm, s.addr, &H32, data)
            ElseIf IsAsm(s.st, str, "ADD") = True Then
                sr = str.Trim.ToUpper()
                csr = DetReg(sr)
                If (csr = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &H80 Or csr
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "ADC") = True Then
                sr = str.Trim.ToUpper()
                csr = DetReg(sr)
                If (csr = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &H88 Or csr
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "SUB") = True Then
                sr = str.Trim.ToUpper()
                csr = DetReg(sr)
                If (csr = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &H90 Or csr
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "SBB") = True Then
                sr = str.Trim.ToUpper()
                csr = DetReg(sr)
                If (csr = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &H98 Or csr
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "INR") = True Then
                sr = str.Trim.ToUpper()
                csr = DetReg(sr)
                If (csr = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &H4 Or (csr << 3)
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "DCR") = True Then
                sr = str.Trim.ToUpper()
                csr = DetReg(sr)
                If (csr = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &H5 Or (csr << 3)
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "ANA") = True Then
                sr = str.Trim.ToUpper()
                csr = DetReg(sr)
                If (csr = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &HA0 Or csr
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "XRA") = True Then
                sr = str.Trim.ToUpper()
                csr = DetReg(sr)
                If (csr = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &HA8 Or csr
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "ORA") = True Then
                sr = str.Trim.ToUpper()
                csr = DetReg(sr)
                If (csr = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &HB0 Or csr
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "CMP") = True Then
                sr = str.Trim.ToUpper()
                csr = DetReg(sr)
                If (csr = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &HB8 Or csr
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "DAD") = True Then
                sr = str.Trim.ToUpper()
                csr = DetRegp(sr)
                If (csr = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &H9 Or (csr << 4)
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "INX") = True Then
                sr = str.Trim.ToUpper()
                csr = DetRegp(sr)
                If (csr = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &H3 Or (csr << 4)
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "DCX") = True Then
                sr = str.Trim.ToUpper()
                csr = DetRegp(sr)
                If (csr = -1) Then
                    Throw New Exception(String.Format("Invalid operand at {0:X4} : {1}", s.addr, s.st))
                End If
                c = &HB Or (csr << 4)
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "DAA") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &H27)
            ElseIf IsAsm(s.st, str, "CMA") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &H2F)
            ElseIf IsAsm(s.st, str, "STC") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &H37)
            ElseIf IsAsm(s.st, str, "CMC") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &H3F)
            ElseIf IsAsm(s.st, str, "RLC") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &H7)
            ElseIf IsAsm(s.st, str, "RRC") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HF)
            ElseIf IsAsm(s.st, str, "RAL") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &H17)
            ElseIf IsAsm(s.st, str, "RAR") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &H1F)
            ElseIf IsAsm(s.st, str, "ADI") = True Then
                data = JGetAddress(str.Trim(), lbl)
                PrepAsm2(asm, s.addr, &HC6, data)
            ElseIf IsAsm(s.st, str, "ACI") = True Then
                data = JGetAddress(str.Trim(), lbl)
                PrepAsm2(asm, s.addr, &HCE, data)
            ElseIf IsAsm(s.st, str, "SUI") = True Then
                data = JGetAddress(str.Trim(), lbl)
                PrepAsm2(asm, s.addr, &HD6, data)
            ElseIf IsAsm(s.st, str, "SBI") = True Then
                data = JGetAddress(str.Trim(), lbl)
                PrepAsm2(asm, s.addr, &HDE, data)
            ElseIf IsAsm(s.st, str, "ANI") = True Then
                data = JGetAddress(str.Trim(), lbl)
                PrepAsm2(asm, s.addr, &HE6, data)
            ElseIf IsAsm(s.st, str, "XRI") = True Then
                data = JGetAddress(str.Trim(), lbl)
                PrepAsm2(asm, s.addr, &HEE, data)
            ElseIf IsAsm(s.st, str, "ORI") = True Then
                data = JGetAddress(str.Trim(), lbl)
                PrepAsm2(asm, s.addr, &HF6, data)
            ElseIf IsAsm(s.st, str, "CPI") = True Then
                data = JGetAddress(str.Trim(), lbl)
                PrepAsm2(asm, s.addr, &HFE, data)
            ElseIf IsAsm(s.st, str, "JMP") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HC3, data)
            ElseIf IsAsm(s.st, str, "JNZ") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HC2, data)
            ElseIf IsAsm(s.st, str, "JZ") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HCA, data)
            ElseIf IsAsm(s.st, str, "JNC") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HD2, data)
            ElseIf IsAsm(s.st, str, "JC") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HDA, data)
            ElseIf IsAsm(s.st, str, "JPO") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HE2, data)
            ElseIf IsAsm(s.st, str, "JPE") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HEA, data)
            ElseIf IsAsm(s.st, str, "JP") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HF2, data)
            ElseIf IsAsm(s.st, str, "JM") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HFA, data)
            ElseIf IsAsm(s.st, str, "PCHL") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HE9)
            ElseIf IsAsm(s.st, str, "CALL") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HCD, data)
            ElseIf IsAsm(s.st, str, "CNZ") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HC4, data)
            ElseIf IsAsm(s.st, str, "CZ") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HCC, data)
            ElseIf IsAsm(s.st, str, "CNC") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HD4, data)
            ElseIf IsAsm(s.st, str, "CC") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HDC, data)
            ElseIf IsAsm(s.st, str, "CPO") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HE4, data)
            ElseIf IsAsm(s.st, str, "CPE") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HEC, data)
            ElseIf IsAsm(s.st, str, "CP") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HF4, data)
            ElseIf IsAsm(s.st, str, "CM") = True Then
                data = JGetAddress(str, lbl)
                PrepAsm3(asm, s.addr, &HFC, data)
            ElseIf IsAsm(s.st, str, "RET") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HC9)
            ElseIf IsAsm(s.st, str, "RNZ") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HC0)
            ElseIf IsAsm(s.st, str, "RZ") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HC8)
            ElseIf IsAsm(s.st, str, "RNC") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HD0)
            ElseIf IsAsm(s.st, str, "RC") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HD8)
            ElseIf IsAsm(s.st, str, "RPO") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HD0)
            ElseIf IsAsm(s.st, str, "RPE") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HE8)
            ElseIf IsAsm(s.st, str, "RP") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HF0)
            ElseIf IsAsm(s.st, str, "RM") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HF8)
            ElseIf IsAsm(s.st, str, "RST") = True Then
                ds = str
                cds = Integer.Parse(ds)
                If (cds >= 0) And (cds <= 7) Then
                    Throw New Exception(String.Format("RST 0-7 is the only possible input for {0:X4}: {1}", s.addr, s.st))
                End If
                c = &HC7 Or ((cds And &H7) << 4)
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "PUSH") = True Then
                cds = DetRegp_psw(str)
                If cds = -1 Then
                    Throw New Exception(String.Format("Register specified is invalid. {0:X4}:{1}", s.addr, s.st))
                End If
                c = &HC5 Or ((cds And &H3) << 4)
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "POP") = True Then
                cds = DetRegp_psw(str)
                If cds = -1 Then
                    Throw New Exception(String.Format("Register specified is invalid. {0:X4}:{1}", s.addr, s.st))
                End If
                c = &HC1 Or ((cds And &H3) << 4)
                PrepAsm1(asm, s.addr, c)
            ElseIf IsAsm(s.st, str, "XTHL") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HE3)
            ElseIf IsAsm(s.st, str, "SPHL") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HF9)
            ElseIf IsAsm(s.st, str, "OUT") = True Then
                data = ParseInteger(str)
                PrepAsm2(asm, s.addr, &HD3, data)
            ElseIf IsAsm(s.st, str, "IN") = True Then
                data = ParseInteger(str)
                PrepAsm2(asm, s.addr, &HDB, data)
            ElseIf IsAsm(s.st, str, "DI") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HF3)
            ElseIf IsAsm(s.st, str, "EI") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &HFB)
            ElseIf IsAsm(s.st, str, "NOP") = True Then
                PrepAsm1(asm, s.addr, &H0)
                EmptyStrException(str, s)
            ElseIf IsAsm(s.st, str, "HLT") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &H76)
            ElseIf IsAsm(s.st, str, "RIM") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &H20)
            ElseIf IsAsm(s.st, str, "SIM") = True Then
                EmptyStrException(str, s)
                PrepAsm1(asm, s.addr, &H30)
            Else
                Throw New Exception(String.Format("Unknown mnemonics encountered at {0:X4}H : {1}", s.addr, s.st))
            End If

        Next

        WriteLine("Code generation completed successfully...")
        WriteLine(String.Format("Code size: {0} bytes", asm.Count))
    End Sub

    Private Function IsJumpLabelDetected(ByVal cmd As String, ByRef restOfString As String, ByRef jLab As String) As Boolean
        cmd = cmd.Trim()
        Dim idx As Integer
        idx = cmd.IndexOf(":")
        If idx = -1 Then
            restOfString = cmd
            Return False
        End If

        Dim s1 As String
        s1 = cmd.Substring(0, idx)

        Dim s1c As Char()
        s1c = s1.ToCharArray()
        ' single quote, double quote, sp chars, any symbol is not allowed
        Dim bj As Boolean = True
        For Each s1ce As Char In s1c
            If Asc(s1ce) < 36 Then
                bj = False
                Exit For
            ElseIf (Asc(s1ce) > 36) And (Asc(s1ce) < 48) Then
                bj = False
                Exit For
            ElseIf (Asc(s1ce) > 57) And (Asc(s1ce) < 64) Then
                bj = False
                Exit For
            ElseIf (Asc(s1ce) > 90) And (Asc(s1ce) < 95) Then
                bj = False
                Exit For
            ElseIf (Asc(s1ce) > 95) And (Asc(s1ce) < 97) Then
                bj = False
                Exit For
            ElseIf (Asc(s1ce) > 122) Then
                bj = False
                Exit For
            End If
        Next
        If (Asc(s1c(0)) >= 48) And (Asc(s1c(0)) <= 57) Then
            bj = False
        End If

        If bj = False Then
            restOfString = cmd
            Return False
        End If

        jLab = cmd.Substring(0, idx)
        jLab = jLab.Trim

        cmd = cmd.Remove(0, idx)
        If cmd.Length <= 1 Then
            restOfString = ""
        Else
            restOfString = cmd.Substring(1)
        End If
        restOfString = restOfString.Trim
        Return True
    End Function
    Private Sub AsmPre2(ByVal ll As List(Of String), ByRef ln As List(Of SrcLine), ByRef jmp As List(Of AsmLabel))
        WriteLine("Preprocessing stage 2 started...")
        ' process and remove org, .org, convert db, dw, ds to db 1 line each, removing everythine else, detect and remove jump label
        Dim sa As Integer = 0
        WriteLine(String.Format("Start Address: {0:X4}", sa))
        Dim il As Integer = 0
        For Each Str As String In ll
            il += 1
            Dim rcmd As String = ""
            Dim rjlb As String = ""
            Dim rpm As String = ""

            If IsJumpLabelDetected(Str, rcmd, rjlb) = True Then
                Dim al As New AsmLabel()
                al.lbl = rjlb.ToUpper().Trim()
                al.addr = sa
                jmp.Add(al)
            End If
            Try
                If IsAsm(rcmd, rpm, "DB") = True Then
                    While rpm.Length > 0
                        Dim ii As Integer
                        ii = rpm.IndexOf(",")
                        Dim ival As String
                        ival = ""
                        If ii = -1 Then
                            ival = rpm
                            rpm = ""
                        Else
                            ival = rpm.Substring(0, ii)
                            rpm = rpm.Remove(0, ii + 1)
                        End If
                        ival = ival.Trim()

                        Dim iival As Integer
                        iival = ParseInteger(ival)

                        Dim ss As New SrcLine()
                        ss.addr = sa
                        ss.st = String.Format("DB {0:X2}H", iival)
                        ln.Add(ss)
                        sa += 1
                    End While
                ElseIf IsAsm(rcmd, rpm, "DW") = True Then
                    While rpm.Length > 0
                        Dim ii As Integer
                        ii = rpm.IndexOf(",")
                        Dim ival As String
                        ival = ""
                        If ii = -1 Then
                            ival = rpm
                            rpm = ""
                        Else
                            ival = rpm.Substring(0, ii)
                            rpm = rpm.Remove(0, ii + 1)
                        End If
                        ival = ival.Trim()

                        Dim iival As Integer
                        iival = ParseInteger(ival)

                        Dim ss As New SrcLine()
                        ss.addr = sa
                        ss.st = String.Format("DB {0:X2}H", iival And &HFF)
                        ln.Add(ss)
                        sa += 1
                        ss = New SrcLine()
                        ss.addr = sa
                        ss.st = String.Format("DB {0:X2}H", (iival >> 8) And &HFF)
                        ln.Add(ss)
                        sa += 1
                    End While
                ElseIf IsAsm(rcmd, rpm, "DS") = True Then
                    Dim iid1, iid2 As Integer
                    iid1 = rpm.IndexOf(Chr(34))
                    iid2 = rpm.LastIndexOf(Chr(34))
                    If (iid1 = -1) Or (iid2 = -1) Or (iid2 <= iid1) Then
                        Throw New Exception("Character string is not properly enclosed within quotation marks")
                    End If
                    Dim iisd As String
                    iisd = rpm.Substring(iid1 + 1, iid2 - iid1 - 1)
                    For Each iisdc As Char In iisd.ToCharArray()
                        Dim ss As New SrcLine()
                        ss.addr = sa
                        ss.st = String.Format("DB {0:X2}H", Asc(iisdc))
                        ln.Add(ss)
                        sa += 1
                    Next
                ElseIf IsAsm(rcmd, rpm, "EQU") = True Then
                    Dim iaddr As Integer
                    iaddr = ParseInteger(rpm)

                    Dim rt1, rt2 As String
                    rt1 = ""
                    rt2 = ""
                    If IsJumpLabelDetected(Str, rt1, rt2) = False Then
                        Throw New Exception("EQU is used to assign predetermined address to a label. A label name is not found")
                    End If

                    Dim jt As AsmLabel
                    jt.addr = iaddr
                    jt.lbl = rt2.ToUpper
                    For iaddr = 0 To (jmp.Count - 1)
                        If jmp(iaddr).lbl = jt.lbl Then
                            jmp.RemoveAt(iaddr)
                            Exit For
                        End If
                    Next
                    jmp.Add(jt)
                ElseIf IsAsm(rcmd, rpm, "ORG") = True Then
                    Dim iaddr As Integer
                    iaddr = ParseInteger(rpm)
                    sa = iaddr

                    Dim rt1, rt2 As String
                    rt1 = ""
                    rt2 = ""
                    If IsJumpLabelDetected(Str, rt1, rt2) = True Then
                        Dim jt As AsmLabel
                        jt.addr = iaddr
                        jt.lbl = rt2.ToUpper
                        For iaddr = 0 To (jmp.Count - 1)
                            If jmp(iaddr).lbl = jt.lbl Then
                                jmp.RemoveAt(iaddr)
                                Exit For
                            End If
                        Next
                        jmp.Add(jt)
                    End If

                Else
                    Dim ilen As Integer
                    ilen = GenerateAddress(rcmd)
                    Dim sl As New SrcLine()
                    sl.addr = sa
                    sl.st = rcmd.Trim.ToUpper
                    ln.Add(sl)

                    sa += ilen
                    End If
            Catch ex As Exception
                Throw New Exception(String.Format("ERROR IN LINE # {0} : {1}   {2}", il, Str, ex.Message))
            End Try

        Next
        WriteLine(String.Format("End Address: {0:X4}", sa))

        WriteLine("Preprocessing stage 2 ended successfully.")
    End Sub


    Private Function ParseInteger(ByVal str As String) As Integer
        str = str.Trim.ToUpper
        If str.Length < 1 Then
            Throw New Exception("Blank string passed to ParseInteger(...)")
        End If
        If str.IndexOf("H") = -1 Then
            Return Integer.Parse(str)
        Else
            Return Integer.Parse(str.Substring(0, str.IndexOf("H")), Globalization.NumberStyles.HexNumber)
        End If
    End Function

    Private Function GenerateAddress(ByVal str As String) As Integer
        str = str.Trim
        If str.Length < 1 Then
            Return 0
        End If

        str = str.ToUpper()
        Dim ostr As String
        ostr = ""
        If IsAsm(str, ostr, "MOV") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "XCHG") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "MVI") = True Then
            Return 2
        ElseIf IsAsm(str, ostr, "LXI") = True Then
            Return 3
        ElseIf IsAsm(str, ostr, "LDAX") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "LHLD") = True Then
            Return 3
        ElseIf IsAsm(str, ostr, "LDA") = True Then
            Return 3
        ElseIf IsAsm(str, ostr, "STAX") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "SHLD") = True Then
            Return 3
        ElseIf IsAsm(str, ostr, "STA") = True Then
            Return 3
        ElseIf IsAsm(str, ostr, "ADD") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "ADC") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "SUB") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "SBB") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "DAD") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "INR") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "INX") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "DCR") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "DCX") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "DAA") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "CMA") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "STC") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "CMC") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "RLC") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "RRC") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "RAL") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "RAR") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "ANA") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "XRA") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "ORA") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "CMP") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "ADI") = True Then
            Return 2
        ElseIf IsAsm(str, ostr, "ACI") = True Then
            Return 2
        ElseIf IsAsm(str, ostr, "SUI") = True Then
            Return 2
        ElseIf IsAsm(str, ostr, "SBI") = True Then
            Return 2
        ElseIf IsAsm(str, ostr, "ANI") = True Then
            Return 2
        ElseIf IsAsm(str, ostr, "XRI") = True Then
            Return 2
        ElseIf IsAsm(str, ostr, "ORI") = True Then
            Return 2
        ElseIf IsAsm(str, ostr, "CPI") = True Then
            Return 2
        ElseIf IsAsm(str, ostr, "JMP") = True Then
            Return 3
        ElseIf (IsAsm(str, ostr, "JNZ") = True) Or (IsAsm(str, ostr, "JZ") = True) Or _
               (IsAsm(str, ostr, "JNC") = True) Or (IsAsm(str, ostr, "JC") = True) Or _
               (IsAsm(str, ostr, "JPO") = True) Or (IsAsm(str, ostr, "JPE") = True) Or _
               (IsAsm(str, ostr, "JP") = True) Or (IsAsm(str, ostr, "JM") = True) _
               Then
            Return 3
        ElseIf IsAsm(str, ostr, "PCHL") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "CALL") = True Then
            Return 3
        ElseIf (IsAsm(str, ostr, "CNZ") = True) Or (IsAsm(str, ostr, "CZ") = True) Or _
               (IsAsm(str, ostr, "CNC") = True) Or (IsAsm(str, ostr, "CC") = True) Or _
               (IsAsm(str, ostr, "CPO") = True) Or (IsAsm(str, ostr, "CPE") = True) Or _
               (IsAsm(str, ostr, "CP") = True) Or (IsAsm(str, ostr, "CM") = True) _
               Then
            Return 3
        ElseIf IsAsm(str, ostr, "RET") = True Then
            Return 1
        ElseIf (IsAsm(str, ostr, "RNZ") = True) Or (IsAsm(str, ostr, "RZ") = True) Or _
               (IsAsm(str, ostr, "RNC") = True) Or (IsAsm(str, ostr, "RC") = True) Or _
               (IsAsm(str, ostr, "RPO") = True) Or (IsAsm(str, ostr, "RPE") = True) Or _
               (IsAsm(str, ostr, "RP") = True) Or (IsAsm(str, ostr, "RM") = True) _
               Then
            Return 1
        ElseIf IsAsm(str, ostr, "RST") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "PUSH") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "POP") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "XTHL") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "SPHL") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "OUT") = True Then
            Return 2
        ElseIf IsAsm(str, ostr, "IN") = True Then
            Return 2
        ElseIf IsAsm(str, ostr, "DI") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "EI") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "NOP") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "HLT") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "RIM") = True Then
            Return 1
        ElseIf IsAsm(str, ostr, "SIM") = True Then
            Return 1
        End If
        Return 0
    End Function

    Private Sub TrimProg(ByVal prog As String, ByRef ll As List(Of String))
        WriteLine("Program is being trimmed for removal of spaces...")
        While Not (prog.Trim.Length < 1)
            Dim eol As Integer
            eol = prog.IndexOf(vbNewLine)
            If eol = -1 Then
                eol = prog.IndexOf(vbCr)
            End If
            If eol = -1 Then
                eol = prog.IndexOf(vbLf)
            End If
            If eol = -1 Then
                eol = prog.Length
            End If


            Dim ln As String
            ln = prog.Substring(0, eol)
            If ((eol + 1) < prog.Length) Then
                prog = prog.Substring(eol + 1)
            Else
                prog = ""
            End If

            ln = ln.Trim
            If Not (ln.Length < 1) Then
                ll.Add(ln)
            End If
        End While

        WriteLine("Program trimmed successfully")
    End Sub
    Private Function AsmPreprocess(ByVal prog As String) As String
        WriteLine("Prepsocessing Phase 1 started...")
        Dim sb As New StringBuilder()
        Dim n As Integer = 0
        Dim eol As Integer
        eol = 0

        While prog.Length > 0
            eol = prog.IndexOf(vbNewLine)
            If eol = -1 Then
                eol = prog.IndexOf(vbCr)
            End If
            If eol = -1 Then
                eol = prog.IndexOf(vbLf)
            End If
            If eol = -1 Then
                eol = prog.Length
            End If


            Dim ln As String
            ln = prog.Substring(0, eol)
            If ((eol + 1) < prog.Length) Then
                prog = prog.Substring(eol + 1)
            Else
                prog = ""
            End If


            If ln.Trim.Length < 1 Then
                Continue While
            ElseIf ln.Trim.IndexOf(";") = 0 Then
                Continue While
            ElseIf Not (ln.IndexOf(";") = -1) Then
                n += 1
                sb.Append(ln.IndexOf(";") - 1)
                If ln.IndexOf(vbNewLine) = -1 Then
                    sb.Append(vbNewLine)
                End If
                Continue While
            ElseIf ln.Trim.ToUpper.IndexOf(".INCLUDE") = 0 Then
                n += 1
                IncludeFile(ln.Trim.Substring(8), sb)
                Continue While
            ElseIf ln.Trim.ToUpper.IndexOf("INCLUDE") = 0 Then
                n += 1
                IncludeFile(ln.Trim.Substring(7), sb)
                Continue While
            Else
                sb.Append(ln)
                If ln.IndexOf(vbNewLine) = -1 Then
                    sb.Append(vbNewLine)
                End If
            End If
        End While

        WriteLine("Preprocessing Phase 1 ended successfully.")

        If n < 1 Then
            Return sb.ToString()
        Else
            Return AsmPreprocess(sb.ToString())
        End If
    End Function
    Private Sub IncludeFile(ByVal ln As String, ByRef sb As StringBuilder)
        If ln.IndexOf(Chr(34)) = -1 Then
            Throw New Exception("File format specification invalid in " + ln)
        End If
        ln = ln.Substring(ln.IndexOf(Chr(34)) + 1)

        If ln.IndexOf(Chr(34)) = -1 Then
            Throw New Exception("File format specification invalid ending in " + ln)
        End If
        ln = ln.Substring(0, ln.IndexOf(Chr(34)))

        WriteLine(String.Format("Requested Inclusion of file {0}", ln))

        Dim fif As String = ""
        Dim fil As New FileInfo(ln)
        If fil.Exists = True Then
            fif = fil.FullName
        End If
        If fif.Trim.Length < 1 Then
            If fname.Trim.Length > 0 Then
                Dim cw As New FileInfo(fname)
                Dim temp As New FileInfo(cw.DirectoryName + "/" + fil.Name)
                If temp.Exists = True Then
                    fif = temp.FullName
                End If
            End If
        End If
        Dim fexe As New FileInfo(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
        Dim dexe As New DirectoryInfo(fexe.DirectoryName)
        Dim dinc As New DirectoryInfo(fexe.DirectoryName + "/include")
        Try
            If dinc.Exists = False Then
                dinc.Create()
            End If
        Catch ex As Exception

        End Try

        If fif.Trim.Length < 1 Then
            Dim temp As New FileInfo(dinc.FullName + "/" + fil.Name)
            If temp.Exists = True Then
                fif = temp.FullName
            End If
        End If

        If fif.Trim.Length < 1 Then
            Dim temp As New FileInfo(dexe.FullName + "/" + fil.Name)
            If temp.Exists = True Then
                fif = temp.FullName
            End If
        End If

        If fif.Trim.Length < 1 Then
            Throw New Exception("File " + ln + " not found")
        End If

        WriteLine("File requested found at " + fif)

        Dim sr As New StreamReader(fif)
        sb.Append(sr.ReadToEnd())
        sr.Close()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim str As String
        str = Me.Text
        Dim idx As Integer
        idx = str.IndexOf("*")
        Dim bmod As Boolean = False
        If bDirty = True Then
            If idx = -1 Then
                str += "*"
                bmod = True
            End If
        Else
            If idx <> -1 Then
                Dim s1, s2 As String
                s1 = ""
                s2 = ""
                Try
                    s1 = str.Substring(0, idx)
                    s2 = str.Substring(idx + 1)
                Catch ex As Exception

                End Try
                str = s1 + s2
                bmod = True
            End If
        End If

        If bmod = True Then
            Me.Text = str
        End If
    End Sub
End Class