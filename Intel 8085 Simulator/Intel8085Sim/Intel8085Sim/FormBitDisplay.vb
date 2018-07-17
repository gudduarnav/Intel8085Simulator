Public Class FormBitDisplay

    Private Sub FormBitDisplay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub CheckBoxState(ByVal i As Integer, ByVal bs As Integer, ByVal cks As CheckBox)
        Dim ib As Integer
        ib = i >> bs
        ib = ib And &H1
        If ib = 1 Then
            cks.Checked = True
            cks.ForeColor = Color.LightYellow
        Else
            cks.Checked = False
            cks.ForeColor = Color.LimeGreen
        End If
    End Sub
    Private Sub SetInteger(ByVal i As Integer)
        FormMain.MakeMeFirst(Me)
        CheckBoxState(i, 0, ck0)
        CheckBoxState(i, 1, ck1)
        CheckBoxState(i, 2, ck2)
        CheckBoxState(i, 3, ck3)
        CheckBoxState(i, 4, ck4)
        CheckBoxState(i, 5, ck5)
        CheckBoxState(i, 6, ck6)
        CheckBoxState(i, 7, ck7)
    End Sub

    Private Sub IntFromCheck(ByRef i As Integer, ByVal bs As Integer, ByVal cks As CheckBox)
        If cks.Checked = True Then
            i = i Or (1 << bs)
        End If
    End Sub
    Private Function GetInteger() As Integer
        FormMain.MakeMeFirst(Me)
        Dim i As Integer = 0
        IntFromCheck(i, 0, ck0)
        IntFromCheck(i, 1, ck1)
        IntFromCheck(i, 2, ck2)
        IntFromCheck(i, 3, ck3)
        IntFromCheck(i, 4, ck4)
        IntFromCheck(i, 5, ck5)
        IntFromCheck(i, 6, ck6)
        IntFromCheck(i, 7, ck7)
        Return i
    End Function


    Private Shared kd As FormBitDisplay = Nothing
    Public Shared Sub LoadMe(ByVal p As FormMain)
        If kd Is Nothing Then
            kd = New FormBitDisplay()
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

    Private Sub FormBitDisplay_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        kd = Nothing
    End Sub
End Class