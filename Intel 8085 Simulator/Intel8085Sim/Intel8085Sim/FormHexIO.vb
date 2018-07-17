Imports System.Threading
Public Class FormHexIO
    Private ev As New ManualResetEvent(False)

    Private Sub FormHexIO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tbInput.Text = ""
        kd.ev.Reset()
    End Sub

    Private Sub SetInteger(ByVal ival As Integer)
        FormMain.MakeMeFirst(Me)
        lbOutput.Text = String.Format("{0:X2}", ival)
    End Sub

    Private Function GetInteger() As Integer
        FormMain.MakeMeFirst(Me)
        Dim i As Integer = 0
        Try
            i = Integer.Parse(tbInput.Text, Globalization.NumberStyles.HexNumber)
        Catch ex As Exception

        End Try
        tbInput.Clear()
        Return i
    End Function


    Public Shared Function PortInWait() As Boolean
        If kd Is Nothing Then
            Return False
        End If
        While kd.ev.WaitOne() <> True
            Thread.Sleep(50)
        End While
        kd.ev.Reset()
        Return True
    End Function



    Private Shared kd As FormHexIO = Nothing
    Public Shared Sub LoadMe(ByVal p As FormMain)
        If kd Is Nothing Then
            kd = New FormHexIO()
            kd.MdiParent = p
            kd.Show()
        End If
    End Sub

    Public Shared Sub OutPort(ByVal p As FormMain, ByVal ch As Integer)
        Try
            LoadMe(p)
            kd.SetInteger(ch)
        Catch ex As Exception
            FormReport.WriteLine("Exception in FormKeyDisplay.OutPort(...): " + ex.Message)
        End Try

    End Sub
    Public Shared Function InPort(ByVal p As FormMain) As Integer
        Try
            LoadMe(p)
            Return kd.GetInteger()
        Catch ex As Exception

            Return 0
        End Try
    End Function


    Private Sub FormHexIO_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        kd = Nothing
    End Sub

    Private Sub tbInput_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tbInput.KeyUp
        If e.KeyCode = Keys.Enter Then
            Dim i As Integer = GetInteger()
            i = i And &HFF
            tbInput.Text = String.Format("{0:X2}", i)
            ev.Set()
        End If
    End Sub
End Class