Public Class FormKit

    Private Sub btnRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRes.Click
        FormMain.GetMachineState().StopExecution()
        tbAddress.Text = "KIT"
        tbByte.Clear()
        tbAddress.ReadOnly = True
        tbByte.ReadOnly = True
    End Sub

    Private Sub FormKit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
        btnRes.PerformClick()
    End Sub

    Private Sub btnSetAddr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetAddr.Click
        tbAddress.ReadOnly = False
        tbAddress.Clear()
        tbAddress.Focus()
        tbByte.Clear()
        tbByte.ReadOnly = True
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        ShowByte(1)
    End Sub

    Private Sub ShowByte(ByVal prog As Integer)
        Try
            If tbAddress.ReadOnly = True Then
                ' increment address only

                tbByte.ReadOnly = False
                Dim addr As Integer
                addr = Integer.Parse(tbAddress.Text, Globalization.NumberStyles.HexNumber)
                addr = addr And &HFFFF

                Dim bt As Integer
                bt = Integer.Parse(tbByte.Text, Globalization.NumberStyles.HexNumber)
                bt = bt And &HFF

                FormMain.GetMachineState().SetMemory(addr, bt, True)

                addr += prog
                addr = addr And &HFFFF
                tbAddress.Text = String.Format("{0:X4}", addr)
                tbByte.Text = String.Format("{0:X2}", FormMain.GetMachineState().GetMemory(addr))
                tbByte.Focus()
            Else
                ' this is a new address
                tbAddress.ReadOnly = True
                tbByte.ReadOnly = False

                Dim addr As Integer
                addr = Integer.Parse(tbAddress.Text, Globalization.NumberStyles.HexNumber)
                addr = addr And &HFFFF
                tbAddress.Text = String.Format("{0:X4}", addr)
                tbByte.Text = String.Format("{0:X2}", FormMain.GetMachineState().GetMemory(addr))
                tbByte.Focus()
            End If

        Catch ex As Exception

        End Try


    End Sub

    Private Sub btnPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        ShowByte(-1)
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        tbAddress.ReadOnly = False
        tbAddress.Clear()
        tbAddress.Focus()
        tbByte.Clear()
        tbByte.ReadOnly = True
    End Sub

    Private Sub btnExec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExec.Click
        Try
            FormMain.GetMachineState().StopExecution()

            tbAddress.ReadOnly = True
            tbByte.ReadOnly = True

            Dim addr As Integer
            addr = Integer.Parse(tbAddress.Text, Globalization.NumberStyles.HexNumber)
            addr = addr And &HFFFF
            FormMain.GetMachineState().SetPC(addr, True)
            FormMain.GetMachineState().StartExecution()

            tbAddress.ReadOnly = True
            tbByte.ReadOnly = True

            tbAddress.Clear()
            tbByte.Clear()
        Catch ex As Exception

        End Try
    End Sub

End Class