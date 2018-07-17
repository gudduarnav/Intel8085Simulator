Imports System.Threading
Public Class FormDecDisplay
    Private ev As New ManualResetEvent(False)
    Private Sub FormDecDisplay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ev.Reset()
    End Sub



    Private Sub SetInteger(ByVal i As Integer)
        FormMain.MakeMeFirst(Me)

        Dim sign As Integer = i >> 7
        sign = sign And &H1
        If sign = 1 Then
            i = Not (i)
            i = i And &H7F
            i = i + 1
            i = i And &HFF
            i = i * -1
        End If
        lbOutput.Text = i.ToString()
    End Sub

    Private Function GetInteger() As Integer
        FormMain.MakeMeFirst(Me)
        Dim i As Integer = 0
        Try
            i = Integer.Parse(tbInput.Text, Globalization.NumberStyles.Integer)
            If i < 0 Then
                i = i And &HFF
            Else
                i = i And &H7F
            End If

        Catch ex As Exception

        End Try
        tbInput.Clear()
        Return i
    End Function

    Private Shared kd As FormDecDisplay = Nothing
    Public Shared Sub LoadMe(ByVal p As FormMain)
        If kd Is Nothing Then
            kd = New FormDecDisplay()
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


    Private Sub FormDecDisplay_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        kd = Nothing
    End Sub


    Private Sub tbInput_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tbInput.KeyUp
        If e.KeyCode = Keys.Enter Then
            Try
                Dim i As Integer = Integer.Parse(tbInput.Text, Globalization.NumberStyles.Integer)
                If i < 0 Then
                    i = i And &HFF
                Else
                    i = i And &H7F
                End If

                Dim sign As Integer = i >> 7
                sign = sign And &H1
                If sign = 1 Then
                    i = Not (i)
                    i = i And &H7F
                    i = i + 1
                    i = i And &HFF
                    i = i * -1
                End If
                tbInput.Text = i.ToString()
                ev.Set()
            Catch ex As Exception
                tbInput.Text = ""
            End Try
        End If
    End Sub


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

End Class