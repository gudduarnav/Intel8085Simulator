Public Class FormRegisterState

    Private Sub FormRegisterState_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        RemoveHandler FormMain.GetMachineState.OnEventRegisterUpdate, AddressOf OnEventRegisterUpdate
        RemoveHandler FormMain.GetMachineState.OnEventFlagsUpdate, AddressOf OnEventFlagsUpdate
        RemoveHandler FormMain.GetMachineState.OnEventPCUpdate, AddressOf OnEventPCUpdate
        RemoveHandler FormMain.GetMachineState.OnEventSPUpdate, AddressOf OnEventSPUpdate

    End Sub

    Private Sub FormRegisterState_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '        tbClock.Text = ClockString(FormMain.GetMachineState.DynamicClock)
        '       lbOriginal.Text = ClockString(FormMain.GetMachineState.GetClockHz())
        tckBarClock.Minimum = MachineState.MinClock
        tckBarClock.Maximum = MachineState.MaxClock
        tckBarClock.TickStyle = TickStyle.None
        tckBarClock.Value = FormMain.GetMachineState.GetClockHz()

        OnEventClockUpdate(FormMain.GetMachineState())
        OnEventFlagsUpdate(FormMain.GetMachineState())
        OnEventPCUpdate(FormMain.GetMachineState().GetPC(), FormMain.GetMachineState())
        OnEventRegisterUpdate(FormMain.GetMachineState())
        OnEventSPUpdate(FormMain.GetMachineState())

        AddHandler FormMain.GetMachineState.OnEventRegisterUpdate, AddressOf OnEventRegisterUpdate
        AddHandler FormMain.GetMachineState.OnEventFlagsUpdate, AddressOf OnEventFlagsUpdate
        AddHandler FormMain.GetMachineState.OnEventPCUpdate, AddressOf OnEventPCUpdate
        AddHandler FormMain.GetMachineState.OnEventSPUpdate, AddressOf OnEventSPUpdate

    End Sub

    Private Function ClockString(ByVal freq As Integer) As String
        If freq < 1000 Then
            Return String.Format("{0} Hz", freq)
        ElseIf freq < 1000000 Then
            Return String.Format("{0} KHz", freq / 1000)
        Else
            Return String.Format("{0} MHz", freq / 1000000)
        End If
    End Function
    Public Sub OnEventPCUpdate(ByVal pc As UInt16, ByRef macState As MachineState)
        Dim ir, s As Integer
        ir = macState.GetIR()
        s = 0

        tbPC.Text = String.Format("{0:X4}", pc)
        tbIR.Text = String.Format("{0:X2}", ir)
        Dim str As String = MachineState.Diassemble(ir, macState.GetMemory(pc + 1), macState.GetMemory(pc + 2), s)
        tbDiasm.Text = String.Format("{0}", str)
        OnEventClockUpdate(macState)
    End Sub

    Public Sub OnEventRegisterUpdate(ByRef macState As MachineState)
        tbA.Text = String.Format("{0:X2}", macState.GetRegister(MachineState.Regs.A))
        tbB.Text = String.Format("{0:X2}", macState.GetRegister(MachineState.Regs.B))
        tbC.Text = String.Format("{0:X2}", macState.GetRegister(MachineState.Regs.C))
        tbD.Text = String.Format("{0:X2}", macState.GetRegister(MachineState.Regs.D))
        tbE.Text = String.Format("{0:X2}", macState.GetRegister(MachineState.Regs.E))
        tbH.Text = String.Format("{0:X2}", macState.GetRegister(MachineState.Regs.H))
        tbL.Text = String.Format("{0:X2}", macState.GetRegister(MachineState.Regs.L))
        tbM.Text = String.Format("{0:X2}", macState.GetRegister(MachineState.Regs.M))
        tbBC.Text = String.Format("{0:X2}{1:X2}", macState.GetRegister(MachineState.Regs.B), macState.GetRegister(MachineState.Regs.C))
        tbDE.Text = String.Format("{0:X2}{1:X2}", macState.GetRegister(MachineState.Regs.D), macState.GetRegister(MachineState.Regs.E))
        tbHL.Text = String.Format("{0:X2}{1:X2}", macState.GetRegister(MachineState.Regs.H), macState.GetRegister(MachineState.Regs.L))
    End Sub
    Public Sub OnEventSPUpdate(ByRef macState As MachineState)
        tbSP.Text = String.Format("{0:X4}", macState.GetSP())
    End Sub

    Public Sub OnEventFlagsUpdate(ByRef macState As MachineState)
        tbFlag.Text = String.Format("{0:X2}", macState.GetFlagRegs())
        tbAC.Text = macState.FlagAC
        tbCY.Text = macState.FlagCY
        tbP.Text = macState.FlagP
        tbS.Text = macState.FlagS
        tbZ.Text = macState.FlagZ
    End Sub



    Public Sub OnEventClockUpdate(ByRef macState As MachineState)
        lbOriginal.Text = ClockString(macState.GetClockHz())
        If Not (tckBarClock.Value = macState.GetClockHz()) And FormMain.GetMachineState().IsRunning() = True Then
            tckBarClock.Value = macState.GetClockHz
        End If

        tbClock.Text = ClockString(macState.DynamicClock)
    End Sub



    Private Sub tckBarClock_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tckBarClock.ValueChanged
        Dim bState As Boolean = FormMain.GetMachineState().IsRunning
        FormMain.GetMachineState().SetClockHz(tckBarClock.Value)
        lbOriginal.Text = ClockString(FormMain.GetMachineState().GetClockHz())
        If bState Then
            FormMain.GetMachineState().StartExecution()
        End If
    End Sub
End Class