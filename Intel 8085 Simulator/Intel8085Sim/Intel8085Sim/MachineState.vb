Imports System.Xml.Serialization
Imports System.Threading
Imports System.Runtime.InteropServices

<Serializable()> <XmlRoot("MachineState")> Public Class MachineState
    'Registers

    Enum Regs As Integer
        B = &H0
        C = &H1
        D = &H2
        E = &H3
        H = &H4
        L = &H5
        M = &H6
        A = &H7
    End Enum

    Private Regs_String() As String = {"B", "C", "D", "E", "H", "L", "M", "A"}

    Public Function GetRegsNameByIndex(ByVal idx As Regs) As String
        Return Regs_String(idx)
    End Function

    Public Function GetRegsIndexFromName(ByVal nm As String) As Regs
        For i = 0 To Regs_String.Length
            If Regs_String(i).Trim.ToUpper = nm.Trim.ToUpper Then
                Return i
            End If
        Next
        Return -1
    End Function


    Private regs_state(8) As Byte
    Public Function GetRegister(ByVal idx As Regs) As Integer
        If idx = Regs.M Then
            Dim addr As Integer = GetRegister(Regs.L)
            addr = addr Or (GetRegister(Regs.H) << 8)
            regs_state(idx) = GetMemory(addr)
        End If
        Return regs_state(idx)
    End Function
    Public Sub SetRegister(ByVal idx As Regs, ByVal val As Integer, ByVal bRaise As Boolean)
        SyncLock (Me)
            val = val And &HFF
            regs_state(idx) = val
            If idx = Regs.M Then
                Dim addr As Integer = GetRegister(Regs.L)
                addr = addr Or (GetRegister(Regs.H) << 8)
                SetMemory(addr, regs_state(idx), True)
            End If
            Try
                If bRaise = True Then
                    RaiseEvent OnEventRegisterUpdate(Me)
                End If
            Catch ex As Exception

            End Try
        End SyncLock
    End Sub

    ' Clock
    Public Const MinClock As Integer = 1
    Public Const MaxClock As Integer = 2000000

    Private clock_speed As Integer = MaxClock     ' in Hz
    Public Function GetClockHz() As Integer
            Return clock_speed
    End Function
    Public Sub SetClockHz(ByVal sp As Integer)
        SyncLock (Me)
            If sp < MinClock Then
                sp = MinClock
            End If
            If sp > MaxClock Then
                sp = MaxClock
            End If
            clock_speed = sp
        End SyncLock
    End Sub

    Public Function GetTimePeriodInNanoSec() As Double
        Dim t As Double = 1 / GetClockHz()

        Return t
    End Function


    ' Memory
    Private mem(64 * 1024) As Byte
    Public Function GetMemory(ByVal loc As Integer) As Integer
            loc = loc And &HFFFF
            Return mem(loc)
    End Function

    Public Sub SetMemory(ByVal loc As Integer, ByVal val As Integer, ByVal bEv As Boolean)
        SyncLock (Me)
            val = val And &HFF
            loc = loc And &HFFFF
            mem(loc) = val

            Try
                If bEv = True Then
                    RaiseEvent OnEventMemoryUpdate(loc, Me)
                    If loc = GetPC() Then
                        RaiseEvent OnEventPCUpdate(GetPC(), Me)
                    End If
                End If
            Catch ex As Exception

            End Try
        End SyncLock
    End Sub

    ' Flag Registers
    Private flagsRegs As Byte
    Public Function GetFlagRegs() As Integer
        Return flagsRegs
    End Function
    Public Sub SetFlagRegs(ByVal val As Integer, ByVal bEv As Boolean)
        SyncLock (Me)
            val = val And &HFF
            flagsRegs = val
            Try
                If bEv = True Then
                    RaiseEvent OnEventFlagsUpdate(Me)
                End If
            Catch ex As Exception

            End Try
        End SyncLock
    End Sub

    Public Function GetFlagBit(ByVal bitVal As Integer) As Integer
        bitVal = bitVal And &H7
        Dim f, a, d As Integer
        f = GetFlagRegs()
        a = 1 << bitVal
        f = f And a
        d = f >> bitVal
        d = d And &H1
        Return d
    End Function
    Public Sub SetFlagBit(ByVal bitVal As Integer, ByVal b As Integer)
        bitVal = bitVal And &H7
        b = b And &H1
        Dim f, r, d As Integer
        f = GetFlagRegs()
        r = 1 << bitVal
        d = b << bitVal
        d = d And r

        f = f And Not (r)
        f = f Or d
        SetFlagRegs(f, True)
    End Sub
    ' 1 bit flag set
    <XmlIgnore()> Public Property FlagS As Byte
        Get
            Return GetFlagBit(7)
        End Get
        Set(ByVal value As Byte)
            SetFlagBit(7, value)
        End Set
    End Property

    <XmlIgnore()> Public Property FlagZ As Byte
        Get
            Return GetFlagBit(6)
        End Get
        Set(ByVal value As Byte)
            SetFlagBit(6, value)
        End Set
    End Property

    <XmlIgnore()> Public Property FlagAC As Byte
        Get
            Return GetFlagBit(4)
        End Get
        Set(ByVal value As Byte)
            SetFlagBit(4, value)
        End Set
    End Property

    <XmlIgnore()> Public Property FlagP As Byte
        Get
            Return GetFlagBit(2)
        End Get
        Set(ByVal value As Byte)
            SetFlagBit(2, value)
        End Set
    End Property

    <XmlIgnore()> Public Property FlagCY As Byte
        Get
            Return GetFlagBit(0)
        End Get
        Set(ByVal value As Byte)
            SetFlagBit(0, value)
        End Set
    End Property

    ' Stack Pointer
    Private sp As UInt16 = &HFFFF
    Public Function GetSP() As Integer
        Return sp
    End Function

    Public Sub SetSP(ByVal val As Integer, ByVal bEv As Boolean)
        val = val And &HFFFF
        sp = val
        Try
            If bEv = True Then
                RaiseEvent OnEventSPUpdate(Me)
            End If
        Catch ex As Exception

        End Try
    End Sub

    ' Program Counter
    <XmlElement("ProgramCounter")> Private pc As UInt16 = 0
    Public Function GetPC() As Integer
        Return pc
    End Function

    Public Sub SetPC(ByVal val As Integer, ByVal bEv As Boolean)
        SyncLock (Me)
            Try
                If bEv = True Then
                    RaiseEvent OnEventPCUpdate(GetPC(), Me)
                End If
            Catch ex As Exception

            End Try
            val = val And &HFFFF
            pc = val

        End SyncLock
    End Sub


    Public Function NextPC(ByVal rel As Integer) As Integer
        Dim val As Integer
        val = GetPC()
        val += rel
        If val < UInt16.MinValue Then
            val += UInt16.MaxValue
        End If
        If val > UInt16.MaxValue Then
            val -= UInt16.MaxValue
        End If
        If val < UInt16.MinValue Then
            val = UInt16.MinValue
        End If
        SetPC(val, True)
        Return GetPC()
    End Function

    Public Function NextPC() As Integer
        Return NextPC(1)
    End Function

    ' Instruction register
    Public Function GetIR() As Byte
        Return GetMemory(GetPC())
    End Function

    ' Port
    Public Sub InPort(ByVal port As Integer, ByVal bEv As Boolean)
        port = port And &HFF
        Dim data As Byte = 0
        Dim prio As Integer = 0
        Try
            If bEv = True Then
                RaiseEvent OnEventInPortUpdate(data, port, Me, prio)
            End If
        Catch ex As Exception

        End Try
        SetRegister(Regs.A, data, bEv)
    End Sub

    Public Sub OutPort(ByVal port As Integer, ByVal bEv As Boolean)
        port = port And &HFF
        Dim data As Byte
        data = GetRegister(Regs.A)
        Try
            If bEv = True Then
                RaiseEvent OnEventOutPortUpdate(data, port, Me)
            End If
        Catch ex As Exception

        End Try
    End Sub

    ' Event Management
    Public Event OnEventRegisterUpdate(ByRef macState As MachineState)
    Public Event OnEventMemoryUpdate(ByVal addr As UInt16, ByRef macState As MachineState)
    Public Event OnEventFlagsUpdate(ByRef macState As MachineState)
    Public Event OnEventOutPortUpdate(ByVal data As Byte, ByVal port As Byte, ByRef macState As MachineState)
    Public Event OnEventInPortUpdate(ByRef data As Byte, ByVal port As Byte, ByRef macState As MachineState, ByRef prio As Integer) ' higher prio will be able to push in data
    Public Event OnEventSPUpdate(ByRef macState As MachineState)
    Public Event OnEventPCUpdate(ByVal pc As UInt16, ByRef macState As MachineState)
    Public Event OnStateUpdate(ByVal bRunning As Boolean, ByRef macState As MachineState)

    Private Sub SendEventStatus(ByVal bRunning As Boolean)
        SyncLock (Me)
            Try
                RaiseEvent OnStateUpdate(bRunning, Me)
            Catch ex As Exception

            End Try
        End SyncLock
    End Sub

    'XML Serialization functions
    <XmlElement("PC")> Public Property Xml_PC As Integer
        Get
            Return GetPC()
        End Get
        Set(ByVal value As Integer)
            SetPC(value, True)
        End Set
    End Property

    <XmlElement("SP")> Public Property Xml_SP As Integer
        Get
            Return GetSP()
        End Get
        Set(ByVal value As Integer)
            SetSP(value, True)
        End Set
    End Property

    <XmlElement("CLOCK")> Public Property Xml_Clock As UInt64
        Get
            Return GetClockHz()
        End Get
        Set(ByVal value As UInt64)
            SetClockHz(value)
        End Set
    End Property

    <XmlElement("FLAGS")> Public Property Xml_Flags As Integer
        Get
            Return GetFlagRegs()
        End Get
        Set(ByVal value As Integer)
            SetFlagRegs(value, True)
        End Set
    End Property

    <XmlArray("REGS")> Public Property Xml_Regs As ArrayList
        Get
            Return New ArrayList(regs_state)
        End Get
        Set(ByVal value As ArrayList)
            value.CopyTo(regs_state)
        End Set
    End Property

    <XmlArray("MEMORY")> Public Property Xml_Memory As ArrayList
        Get
            Return New ArrayList(mem)
        End Get
        Set(ByVal value As ArrayList)
            value.CopyTo(mem)
        End Set
    End Property

    ' Internal Construction
    Public Sub New()
        SetRegister(Regs.A, 0, False)
        FlagAC = 0
        FlagCY = 0
        FlagP = 0
        FlagS = 0
        FlagZ = 1
    End Sub

    ' Misc 
    Public Sub ClearMemory()
        For i As Integer = 0 To &HFFFF
            If Not (GetMemory(i) = 0) Then
                SetMemory(i, 0, True)
            End If
        Next
    End Sub

    Dim th As Thread = Nothing
    Public Function IsRunning() As Boolean
        If th Is Nothing Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Sub StartExecution()
        If Not (th Is Nothing) Then
            StopExecution()
        End If
        Try
            dynaClock = GetClockHz()
            th = New Thread(AddressOf ExecutingMachine)
            th.Start()
            SendEventStatus(True)
            NextPC(0)
        Catch ex As Exception
            SendEventStatus(False)

        End Try
    End Sub
    Public Sub StopExecution()
        If th Is Nothing Then
            Exit Sub
        End If

        Try
            th.Abort()
        Catch ex As Exception

        End Try
        Try
            th.Join()
        Catch ex As Exception

        End Try
        th = Nothing
        SendEventStatus(False)
        NextPC(0)
    End Sub


    Private hiresTimer As New HiResTimer()
    Private dynaClock As Integer = 0
    Public ReadOnly Property DynamicClock As Integer
        Get
            Return dynaClock
        End Get
    End Property



    Private Sub ProcessInstruction()
        hiresTimer.StartTimer()
        Dim tStates As Integer = 1
        ExecuteCU(tStates)
        Dim tWait As Double = tStates * GetTimePeriodInNanoSec()
        hiresTimer.StopTimer()
        If hiresTimer.Duration > tWait Then
            Exit Sub
        Else
            While hiresTimer.Duration < tWait
                hiresTimer.StopTimer()
            End While
        End If

        Dim freq As Double = 1 / (hiresTimer.Duration / tStates)
        dynaClock = freq
    End Sub

    <DllImport("kernel32.dll")> _
    Private Shared Function GetCurrentThread() As IntPtr

    End Function

    <DllImport("kernel32.dll")> _
    Private Shared Function SetThreadAffinityMask(ByVal hThread As IntPtr, ByVal affMash As UIntPtr) As UIntPtr

    End Function

    Private Sub ExecutingMachine()
        Try
            If Environment.ProcessorCount > 1 Then
                Dim c_th As IntPtr
                c_th = GetCurrentThread()
                If SetThreadAffinityMask(c_th, &H1) = 0 Then
                    Debug.Print(String.Format("ERROR: Thread {0} cannot be concised to 1 processor", c_th))
                End If
            End If

            While True
                ProcessInstruction()
            End While
        Catch ex As Exception

        End Try
    End Sub


    ' IRQ
    Private bIntEnable As Integer = 0
    Private iIntMask As Integer = 0

    Public Property IsInterruptEnabled As Integer
        Get
            Return bIntEnable
        End Get
        Set(ByVal value As Integer)
            bIntEnable = value And &H1
        End Set
    End Property

    Public ReadOnly Property InterruptMask As Integer
        Get
            Return iIntMask
        End Get
    End Property


    Private Sub IntrJump(ByVal addr As Integer, ByVal bSize As Integer, ByVal bForce As Boolean) ' if bForce = true then bypass EI/DI
        If IsRunning() = False Then
            Exit Sub
        End If
        If IsInterruptEnabled = False Then
            If bForce = False Then
                Exit Sub
            End If
        End If

        StopExecution()

        SavePCToStack(bSize)
        SetPC(addr, True)
        StartExecution()
    End Sub

    Private Function GetIntMaskBit(ByVal bt As Integer) As Integer
        bt = bt And &H7
        Dim btMask, btVal As Integer
        btMask = 1 << bt
        btVal = InterruptMask And btMask
        btVal = btVal >> bt
        btVal = btVal And &H1
        Return btVal
    End Function


    Public Sub Interrupt_TRAP()
        IntrJump(&H24, 0, True)
    End Sub

    Public ReadOnly Property IsRST7_5Enabled As Integer
        Get
            Return (GetIntMaskBit(3) And (Not (GetIntMaskBit(4)) And GetIntMaskBit(2))) And &H1
        End Get
    End Property

    Public ReadOnly Property IsRST6_5Enabled As Integer
        Get
            Return (GetIntMaskBit(3) And GetIntMaskBit(1)) And &H1
        End Get
    End Property

    Public ReadOnly Property IsRST5_5Enabled As Integer
        Get
            Return (GetIntMaskBit(3) And GetIntMaskBit(0)) And &H1
        End Get
    End Property

    Public Sub Interrupt_RST7_5()
        If IsRST7_5Enabled = 1 Then
            IntrJump(&H3C, 0, False)
        End If
    End Sub
    Public Sub Interrupt_RST6_5()
        If IsRST6_5Enabled = 1 Then
            IntrJump(&H34, 0, False)
        End If
    End Sub
    Public Sub Interrupt_RST5_5()
        If IsRST5_5Enabled = 1 Then
            IntrJump(&H2C, 0, False)
        End If
    End Sub

    Public Sub Interrupt_RST0()
        IntrJump(&H0, 1, False)
    End Sub
    Public Sub Interrupt_RST1()
        IntrJump(&H8, 1, False)
    End Sub
    Public Sub Interrupt_RST2()
        IntrJump(&H10, 1, False)
    End Sub
    Public Sub Interrupt_RST3()
        IntrJump(&H18, 1, False)
    End Sub
    Public Sub Interrupt_RST4()
        IntrJump(&H20, 1, False)
    End Sub
    Public Sub Interrupt_RST5()
        IntrJump(&H28, 1, False)
    End Sub
    Public Sub Interrupt_RST6()
        IntrJump(&H30, 1, False)
    End Sub
    Public Sub Interrupt_RST7()
        IntrJump(&H38, 1, False)
    End Sub

    Public Sub Interrupt_RST(ByVal r_id As Integer)
        r_id = r_id And &H7

        If r_id = 0 Then
            Interrupt_RST0()
        ElseIf r_id = 1 Then
            Interrupt_RST1()
        ElseIf r_id = 2 Then
            Interrupt_RST2()
        ElseIf r_id = 3 Then
            Interrupt_RST3()
        ElseIf r_id = 4 Then
            Interrupt_RST4()
        ElseIf r_id = 5 Then
            Interrupt_RST5()
        ElseIf r_id = 6 Then
            Interrupt_RST6()
        ElseIf r_id = 7 Then
            Interrupt_RST7()
        End If
    End Sub

    Public Event OnEventSID(ByVal bEnable As Boolean, ByRef val As Integer, ByRef prio As Integer, ByVal macState As MachineState)
    Public Event OnEventSOD(ByVal bEnable As Boolean, ByRef val As Integer, ByVal macState As MachineState)


    Public Sub ReadInterruptMaskToA()
        AsmSid()
        iIntMask = iIntMask And &HFF
        SetRegister(Regs.A, iIntMask, True)
    End Sub
    Public Sub ReadInterruptMaskFromA()
        iIntMask = GetRegister(Regs.A)
        AsmSod()
        iIntMask = iIntMask And &HFF
    End Sub

    Private Sub AsmSid()
        Try
            Dim bEnable As Integer
            bEnable = iIntMask >> 6
            bEnable = bEnable And &H1
            Dim bVal, prio As Integer
            bVal = 0
            prio = 0
            RaiseEvent OnEventSID(CType(bEnable, Boolean), bVal, prio, Me)

            bVal = bVal And bEnable

            If bEnable = 1 Then
                Dim a, b, c As Integer
                a = 1 << 7
                b = iIntMask
                c = Not (a) And b
                a = bVal << 7
                c = c Or a
                c = c And &HFF
                iIntMask = c
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AsmSod()
        Try
            Dim bEnable, bS As Integer
            bEnable = iIntMask >> 6
            bS = iIntMask >> 7
            bEnable = bEnable And &H1
            bS = bS And bEnable
            RaiseEvent OnEventSOD(CType(bEnable, Boolean), bS, Me)
        Catch ex As Exception

        End Try
    End Sub

    Public Function GetIntMask() As Integer
        Return (iIntMask And &HFF)
    End Function

    Public Function IsSerialEnabled() As Boolean
        Dim bE As Integer
        bE = iIntMask
        bE = bE >> 6
        bE = bE And &H1
        Return CBool(bE)
    End Function
End Class
