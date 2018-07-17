Partial Class MachineState

    Private Shared s_reg() As String = {"B", "C", "D", "E", "H", "L", "M", "A"}
    Private Shared s_regp() As String = {"B", "D", "H", "SP"}
    Private Shared s_regp1() As String = {"B", "D", "H", "PSW"}

    Private Shared Function DiassembleH3(ByVal h As Byte, ByVal l As Byte, ByVal op1 As Byte, ByVal op2 As Byte, ByRef iSize As Integer) As String
        Dim str As String
        If l = &H0 Then
            Dim s_t() As String = {"RNZ", "RZ", "RNC", "RC", "RPO", "RPE", "RP", "RM"}
            iSize = 1
            str = String.Format("{0}", s_t(h))
            Return str
        ElseIf (h And &H1) = 0 And l = &H1 Then
            iSize = 1
            str = String.Format("POP {0}", s_regp1((h >> 1) And &H3))
            Return str
        ElseIf h = &H1 And l = &H1 Then
            iSize = 1
            Return "RET"
        ElseIf h = &H5 And l = &H1 Then
            iSize = 1
            Return "PCHL"
        ElseIf h = &H7 And l = &H1 Then
            iSize = 1
            Return "SPHL"
        ElseIf l = &H2 Then
            Dim s_t() As String = {"JNZ", "JZ", "JNC", "JC", "JPO", "JPE", "JP", "JM"}
            iSize = 3
            str = String.Format("{0} {1:X2}{2:X2}", s_t(h), op2, op1)
            Return str
        ElseIf h = &H0 And l = &H3 Then
            iSize = 3
            str = String.Format("JMP {1:X2}{2:X2}", op2, op1)
            Return str
        ElseIf h = &H2 And l = &H3 Then
            iSize = 2
            str = String.Format("OUT {0:X2}", op1)
            Return str
        ElseIf h = &H3 And l = &H3 Then
            iSize = 2
            str = String.Format("IN {0:X2}", op1)
            Return str
        ElseIf h = &H4 And l = &H3 Then
            iSize = 1
            Return "XCHG"
        ElseIf h = &H5 And l = &H3 Then
            iSize = 1
            Return "XTHL"
        ElseIf h = &H6 And l = &H3 Then
            iSize = 1
            Return "DI"
        ElseIf h = &H7 And l = &H3 Then
            iSize = 1
            Return "EI"
        ElseIf l = &H4 Then
            Dim s_t() As String = {"CNZ", "CZ", "CNC", "CC", "CPO", "CPE", "CP", "CM"}
            iSize = 3
            str = String.Format("{0} {1:X2}{2:X2}", s_t(h), op2, op1)
            Return str
        ElseIf (h And &H1) = 0 And l = &H5 Then
            iSize = 1
            str = String.Format("PUSH {0}", s_regp1((h >> 1) And &H3))
            Return str
        ElseIf h = &H1 And l = &H5 Then
            iSize = 3
            str = String.Format("CALL {0:X2}{1:X2}", op2, op1)
            Return str
        ElseIf h = &H0 And l = &H6 Then
            iSize = 2
            str = String.Format("ADI {0:X2}", op1)
            Return str
        ElseIf h = &H1 And l = &H6 Then
            iSize = 2
            str = String.Format("ACI {0:X2}", op1)
            Return str
        ElseIf h = &H2 And l = &H6 Then
            iSize = 2
            str = String.Format("SUI {0:X2}", op1)
            Return str
        ElseIf h = &H3 And l = &H6 Then
            iSize = 2
            str = String.Format("SBI {0:X2}", op1)
            Return str
        ElseIf h = &H4 And l = &H6 Then
            iSize = 2
            str = String.Format("ANI {0:X2}", op1)
            Return str
        ElseIf h = &H5 And l = &H6 Then
            iSize = 2
            str = String.Format("XRI {0:X2}", op1)
            Return str
        ElseIf h = &H6 And l = &H6 Then
            iSize = 2
            str = String.Format("ORI {0:X2}", op1)
            Return str
        ElseIf h = &H7 And l = &H6 Then
            iSize = 2
            str = String.Format("CPI {0:X2}", op1)
            Return str
        ElseIf l = &H7 Then
            iSize = 1
            str = String.Format("RST {0:X1}", h)
        End If
        Return "UNKNOWN"
    End Function

    Private Shared Function DiassembleH2(ByVal h As Byte, ByVal l As Byte, ByVal op1 As Byte, ByVal op2 As Byte, ByRef iSize As Integer) As String
        Dim str As String
        If h = &H0 Then
            iSize = 1
            str = String.Format("ADD {0}", s_reg(l))
            Return str
        ElseIf h = &H1 Then
            iSize = 1
            str = String.Format("ADC {0}", s_reg(l))
            Return str
        ElseIf h = &H2 Then
            iSize = 1
            str = String.Format("SUB {0}", s_reg(l))
            Return str
        ElseIf h = &H3 Then
            iSize = 1
            str = String.Format("SBB {0}", s_reg(l))
            Return str
        ElseIf h = &H4 Then
            iSize = 1
            str = String.Format("ANA {0}", s_reg(l))
            Return str
        ElseIf h = &H5 Then
            iSize = 1
            str = String.Format("XRA {0}", s_reg(l))
            Return str
        ElseIf h = &H6 Then
            iSize = 1
            str = String.Format("ORA {0}", s_reg(l))
            Return str
        ElseIf h = &H7 Then
            iSize = 1
            str = String.Format("CMP {0}", s_reg(l))
            Return str
        End If
        Return "UNKNOWN"
    End Function



    Private Shared Function DiassembleH1(ByVal h As Byte, ByVal l As Byte, ByVal op1 As Byte, ByVal op2 As Byte, ByRef iSize As Integer) As String
        Dim str As String
        If h = &H6 And l = &H6 Then
            iSize = 1
            Return "HLT"
        Else
            iSize = 1
            str = String.Format("MOV {0}, {1}", s_reg(h), s_reg(l))
            Return str
        End If

    End Function



    Private Shared Function DiassembleH0(ByVal h As Byte, ByVal l As Byte, ByVal op1 As Byte, ByVal op2 As Byte, ByRef iSize As Integer) As String
        Dim str As String
        If h = &H0 And l = &H0 Then
            iSize = 1
            Return "NOP"
        ElseIf h = &H4 And l = &H0 Then
            iSize = 1
            Return "RIM"
        ElseIf h = &H6 And l = &H0 Then
            iSize = 1
            Return "SIM"
        ElseIf (h And &H1) = 0 And l = &H1 Then
            iSize = 3
            str = String.Format("LXI {0}, {1:X2}{2:X2}", s_regp((h >> 1) And &H3), op2, op1)
            Return str
        ElseIf (h And &H1) = 1 And l = &H1 Then
            iSize = 1
            str = String.Format("DAD {0}", s_regp((h >> 1) And &H3))
            Return str
        ElseIf (h And &H5) = 0 And l = &H2 Then
            iSize = 1
            str = String.Format("STAX {0}", s_regp((h >> 1) And &H1))
            Return str
        ElseIf (h And &H5) = 1 And l = &H2 Then
            iSize = 1
            str = String.Format("LDAX {0}", s_regp((h >> 1) And &H1))
            Return str
        ElseIf (h And &H6) = 4 And l = &H2 Then
            iSize = 3
            If (h And &H1) = 1 Then
                str = "LHLD"
            Else
                str = "SHLD"
            End If
            Return String.Format("{0} {1:X2}{2:X2}", str, op2, op1)
        ElseIf (h And &H6) = 6 And l = &H2 Then
            iSize = 3
            If (h And &H1) = 1 Then
                str = "LDA"
            Else
                str = "STA"
            End If
            Return String.Format("{0} {1:X2}{2:X2}", str, op2, op1)
        ElseIf (h And &H1) = 0 And l = &H3 Then
            iSize = 1
            str = String.Format("INX {0}", s_regp((h >> 1) And &H3))
            Return str
        ElseIf (h And &H1) = 1 And l = &H3 Then
            iSize = 1
            str = String.Format("DCX {0}", s_regp((h >> 1) And &H3))
            Return str
        ElseIf l = &H4 Then
            iSize = 1
            str = String.Format("INR {0}", s_reg(h))
            Return str
        ElseIf l = &H5 Then
            iSize = 1
            str = String.Format("DCR {0}", s_reg(h))
            Return str
        ElseIf l = &H6 Then
            iSize = 2
            str = String.Format("MVI {0}, {1:X2}", s_reg(h), op1)
            Return str
        ElseIf h = &H0 And l = &H7 Then
            iSize = 1
            Return "RLC"
        ElseIf h = &H1 And l = &H7 Then
            iSize = 1
            Return "RRC"
        ElseIf h = &H2 And l = &H7 Then
            iSize = 1
            Return "RAL"
        ElseIf h = &H3 And l = &H7 Then
            iSize = 1
            Return "RAR"
        ElseIf h = &H4 And l = &H7 Then
            iSize = 1
            Return "DAA"
        ElseIf h = &H5 And l = &H7 Then
            iSize = 1
            Return "CMA"
        ElseIf h = &H6 And l = &H7 Then
            iSize = 1
            Return "STC"
        ElseIf h = &H7 And l = &H7 Then
            iSize = 1
            Return "CMC"
        Else
            Return "UNKNOWN"
        End If
    End Function

    Public Shared Function Diassemble(ByVal id As Byte, ByVal op1 As Byte, ByVal op2 As Byte, ByRef iSize As Integer) As String
        Dim op, h, l As Integer
        l = id And &H7
        h = (id And &H38) >> 3
        op = (id And &HC0) >> 6


        If op = &H0 Then
            Return DiassembleH0(h, l, op1, op2, iSize)
        ElseIf op = &H1 Then
            Return DiassembleH1(h, l, op1, op2, iSize)
        ElseIf op = &H2 Then
            Return DiassembleH2(h, l, op1, op2, iSize)
        ElseIf op = &H3 Then
            Return DiassembleH3(h, l, op1, op2, iSize)
        End If

        Return "UNKNOWN"
    End Function

End Class
