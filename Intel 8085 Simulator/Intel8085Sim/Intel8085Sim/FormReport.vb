Public Class FormReport

    Private Sub FormReport_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        outform = Nothing
    End Sub

    Private Sub FormReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        outform = Me
    End Sub
    Public Shared MainForm As Form = Nothing
    Private Shared outform As FormReport = Nothing

    Private Sub LoadOutput()
        If outform Is Nothing Then
            outform = New FormReport
            outform.MdiParent = MainForm
            outform.Show()
        End If

    End Sub

    Private Delegate Sub dLoadOutput()

    Private Sub Invoke_LoadOutput()
        Try
            FormMain.Invoke(New dLoadOutput(AddressOf LoadOutput), Nothing)
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub Write(ByVal str As String)
        If outform Is Nothing Then
            Dim ff As New FormReport()
            ff.Invoke_LoadOutput()
            While outform Is Nothing
                ' spin and wait
            End While
        End If

        outform.WriteOut(str)
    End Sub
    Public Shared Sub WriteLine(ByVal str As String)
        Write(str + vbCrLf)
    End Sub

    Public Shared Sub Clear()
        Write("")
        outform.ClearOut()
    End Sub

    Private Sub WriteOut(ByVal str As String)
        FormMain.MakeMeFirst(Me)
        rtbOut.Text += str
    End Sub

    Private Sub ClearOut()
        rtbOut.Clear()
    End Sub
End Class