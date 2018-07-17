Public Class FormIntrSerial
    Private clickStack As New Stack(Of Integer)
    Private Shared SerialPrio As Integer = 1

    Private Sub FormIntrSerial_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        RemoveHandler FormMain.GetMachineState.OnEventPCUpdate, AddressOf OnEventPCUpdate
        RemoveHandler FormMain.GetMachineState.OnEventSID, AddressOf OnEventSID
        RemoveHandler FormMain.GetMachineState.OnEventSOD, AddressOf OnEventSOD
    End Sub

    Private Sub FormIntrSerial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RefreshSerial(FormMain.GetMachineState)
        RefreshIntr(FormMain.GetMachineState)
        AddHandler FormMain.GetMachineState.OnEventPCUpdate, AddressOf OnEventPCUpdate
        AddHandler FormMain.GetMachineState.OnEventSID, AddressOf OnEventSID
        AddHandler FormMain.GetMachineState.OnEventSOD, AddressOf OnEventSOD
    End Sub

    Private Sub RefreshSerial(ByVal m As MachineState)
        If m.IsSerialEnabled = True Then
            btnSOD.Enabled = True
        Else
            btnSOD.Enabled = False
        End If
    End Sub
    Private Sub RefreshIntr(ByVal m As MachineState)
        tbIntrFlag.Text = String.Format("{0:X2}", m.GetIntMask())
        If m.IsRST7_5Enabled = 1 Then
            btnRst75.Enabled = True
        Else
            btnRst75.Enabled = False
        End If

        If m.IsRST6_5Enabled = 1 Then
            btnRst65.Enabled = True
        Else
            btnRst65.Enabled = False
        End If

        If m.IsRST5_5Enabled = 1 Then
            btnRst55.Enabled = True
        Else
            btnRst55.Enabled = False
        End If
    End Sub

    Public Sub OnEventPCUpdate(ByVal pc As UInt16, ByRef macState As MachineState)
        RefreshIntrFlag(macState)
        RefreshIntr(macState)
    End Sub

    Private Sub btnTrap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrap.Click
        RefreshIntrFlag(FormMain.GetMachineState())
        FormMain.GetMachineState.Interrupt_TRAP()
    End Sub

    Private Sub btnRst75_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRst75.Click
        RefreshIntrFlag(FormMain.GetMachineState())
        FormMain.GetMachineState.Interrupt_RST7_5()
    End Sub

    Private Sub btnRst65_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRst65.Click
        RefreshIntrFlag(FormMain.GetMachineState())
        FormMain.GetMachineState.Interrupt_RST6_5()
    End Sub

    Private Sub btnRst55_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRst55.Click
        RefreshIntrFlag(FormMain.GetMachineState())
        FormMain.GetMachineState.Interrupt_RST5_5()
    End Sub

    Private Sub btnSOD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSOD.Click
        RefreshIntrFlag(FormMain.GetMachineState())
        If FormMain.GetMachineState().IsRunning = True Then
            clickStack.Push(1)
        Else
            clickStack.Clear()
        End If
    End Sub

    Public Sub OnEventSID(ByVal bEnable As Boolean, ByRef val As Integer, ByRef prio As Integer, ByVal macState As MachineState)
        RefreshIntrFlag(macState)
        btnSOD.Enabled = bEnable
        If SerialPrio > prio Then
            prio = SerialPrio
            Try
                If clickStack.Count < 1 Then
                    val = 0
                Else
                    val = clickStack.Pop() And &H1
                End If
            Catch ex As Exception
                val = 0
            End Try
        End If

    End Sub
    Public Sub OnEventSOD(ByVal bEnable As Boolean, ByRef val As Integer, ByVal macState As MachineState)
        RefreshIntrFlag(macState)
        If bEnable = False Then
            Exit Sub
        End If

        tbSod.Text = String.Format("{0:X1}", val)
    End Sub

    Public Sub RefreshIntrFlag(ByVal m As MachineState)
        Dim str As String
        str = String.Format("{0:X2}", m.GetIntMask())
        If tbIntrFlag.Text = str Then
            Exit Sub
        Else
            tbIntrFlag.Text = str
        End If
    End Sub
End Class