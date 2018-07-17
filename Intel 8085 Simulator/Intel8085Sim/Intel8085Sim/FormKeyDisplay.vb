Imports System.Threading

Public Class FormKeyDisplay

    Private Sub FormKeyDisplay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tbOutput.Text = ""
    End Sub

    Private Sub SerialText(ByVal ch As String)
        FormMain.MakeMeFirst(Me)
        tbOutput.Text += ch
        If tbOutput.Text.Length > 25 Then
            tbOutput.Text = ch
        End If
        If (ch = vbCr) Or (ch = vbCrLf) Then
            tbOutput.Text = ""
        End If
    End Sub

    Public Shared Function PortInWait() As Boolean
        If kd Is Nothing Then
            Return False
        End If
        While kd.tbInput.Text.Length < 1
            Thread.Sleep(50)
        End While
        Return True
    End Function

    Private Function GetAByte() As String
        FormMain.MakeMeFirst(Me)
        Dim str As String
        str = tbInput.Text
        If str.Length < 1 Then
            Return ""
        Else
            Try
                tbInput.Text = str.Substring(1)
            Catch ex As Exception
                tbInput.Text = ""
            End Try
            Try
                Return str.Substring(0, 1)
            Catch ex As Exception
                Return ""
            End Try
        End If

    End Function

    Private Shared kd As FormKeyDisplay = Nothing
    Public Shared Sub LoadMe(ByVal p As FormMain)
        If kd Is Nothing Then
            kd = New FormKeyDisplay()
            kd.MdiParent = p
            kd.Show()
        End If
    End Sub

    Public Shared Sub OutPort(ByVal p As FormMain, ByVal ch As Integer)
        Try
            LoadMe(p)
            kd.SerialText(Chr(ch And &HFF))
        Catch ex As Exception
            FormReport.WriteLine("Exception in FormKeyDisplay.OutPort(...): " + ex.Message)
        End Try

    End Sub
    Public Shared Function InPort(ByVal p As FormMain) As Integer
        Try
            LoadMe(p)
            Dim str As String
            str = kd.GetAByte()
            If str = "" Then
                Return 0
            Else
                Return Asc(str)
            End If

        Catch ex As Exception

            Return 0
        End Try
    End Function

End Class