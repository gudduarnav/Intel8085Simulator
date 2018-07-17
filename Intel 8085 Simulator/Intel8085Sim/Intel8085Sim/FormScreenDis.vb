Public Class FormScreenDis
    Private Shared bmp As Bitmap = Nothing
    Private fnt As New Font("Consolas", 8)

    Private Sub FormScreenDis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim g As Graphics
            If bmp Is Nothing Then
                bmp = New Bitmap(&HFF, &HFF)
                g = Graphics.FromImage(bmp)
                g.FillRectangle(Brushes.Black, New Rectangle(0, 0, bmp.Width, bmp.Height))


            End If
            g = Graphics.FromImage(bmp)
            Dim sz As SizeF = g.MeasureString("X", fnt)
            Dim w, h As Integer
            w = bmp.Width / sz.Width
            h = bmp.Height / sz.Height
            Dim str As String
            str = String.Format("Text [{0} x {1}] and Graphics Display [{2} x {3}]", w, h, bmp.Width, bmp.Height)
            Me.Text = str

            Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                         ControlStyles.UserPaint Or _
                         ControlStyles.DoubleBuffer, True)

        Catch ex As Exception

        End Try

    End Sub

    Private Shared pt As New GPoint()

    Structure GPoint
        Public x As Integer
        Public y As Integer
        Public clr As Color
        Public command As Integer
    End Structure

    Private Sub ExecuteCommand(ByVal cmd As Integer)
        pt.command = cmd
    End Sub

    Private Sub ShowData(ByVal val As Integer)
        Try

            Dim cmd As Integer = pt.command

            If (cmd And 1) <> 1 Then
                Exit Sub
            End If

            If ((cmd >> 1) And 1) = 1 Then
                Dim g As Graphics = Graphics.FromImage(bmp)
                Dim br As New SolidBrush(Me.GetMyColor(val))
                Dim rect As New Rectangle(0, 0, bmp.Width, bmp.Height)
                g.FillRectangle(br, rect)
                Me.Invalidate()
                FormMain.MakeMeFirst(Me)
                Exit Sub
            End If

            If ((cmd >> 4) And 1) = 1 Then
                pt.x = val
            End If
            If ((cmd >> 5) And 1) = 1 Then
                pt.y = val
            End If

            If ((cmd >> 6) And 1) = 1 Then
                pt.clr = GetMyColor(val)
            End If

            If ((cmd >> 2) And 1) = 1 Then
                ' text mode : val is ASCII
                Dim g As Graphics = Graphics.FromImage(bmp)
                Dim str As String = Chr(val And &HFF)
                Dim clr As New SolidBrush(pt.clr)
                Dim sz As SizeF = g.MeasureString(str, fnt)

                Dim pointa As Point
                pointa.X = pt.x * sz.Width
                pointa.Y = pt.y * sz.Height
                g.DrawString(str, fnt, clr, pointa)
                Me.Invalidate()
                FormMain.MakeMeFirst(Me)
                Exit Sub
            End If

            If ((cmd >> 3) And 1) = 1 Then
                ' gfx
                ' If (pt.x < 1) Or (pt.x > (bmp.Width - 1)) Or (pt.y < 1) Or (pt.y > (bmp.Height - 1)) Then
                'Exit Sub
                ' End If

                bmp.SetPixel(pt.x, pt.y, pt.clr)
                Me.Invalidate()
                FormMain.MakeMeFirst(Me)
                Exit Sub
            End If

            If ((cmd >> 7) And 1) = 1 Then
                Me.Invalidate()
            End If
        Catch ex As Exception
            ' FormReport.WriteLine("Exception in Screen Display: " + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Function GetMyColor(ByVal clr As Integer) As Color
        Dim rr, gg, bb, aa As Integer
        bb = clr And 3
        gg = (clr >> 2) And 3
        rr = (clr >> 4) And 3
        aa = (clr >> 6) And 3

        rr = rr * 255 / 3
        gg = gg * 255 / 3
        bb = bb * 255 / 3
        aa = aa * 255 / 3

        Return Color.FromArgb(aa, rr, gg, bb)
    End Function

    Private Shared kd As FormScreenDis = Nothing
    Public Shared Sub LoadMe(ByVal p As FormMain)
        If kd Is Nothing Then
            kd = New FormScreenDis()
            kd.MdiParent = p
            kd.Show()
        End If
    End Sub

    Public Shared Sub OutPort(ByVal p As FormMain, ByVal ch As Integer)
        Try
            LoadMe(p)
            kd.ShowData(ch)
        Catch ex As Exception
            FormReport.WriteLine("Exception in FormScreenDis.OutPort(...): " + ex.Message)
        End Try

    End Sub

    Public Shared Sub OutCommandPort(ByVal p As FormMain, ByVal ch As Integer)
        Try
            LoadMe(p)
            kd.ExecuteCommand(ch)
        Catch ex As Exception
            FormReport.WriteLine("Exception in FormScreenDis.OutPort(...): " + ex.Message)
        End Try

    End Sub


    Private Sub FormScreenDis_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        kd = Nothing
    End Sub

    Private Sub FormScreenDis_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        e.Graphics.DrawImage(bmp, New Rectangle(0, 0, Me.Width, Me.Height))
    End Sub
End Class