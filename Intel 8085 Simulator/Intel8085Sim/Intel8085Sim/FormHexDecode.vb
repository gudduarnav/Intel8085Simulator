Imports System.Text
Public Class FormHexDecode

    Private Sub FormHexDecode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        rtbSrc.Focus()
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            Dim frm As New OpenFileDialog()
            frm.Filter = "Intel HEX file (*.hex)|*.hex|All files(*.*)|*.*||"
            frm.Multiselect = False
            frm.Title = "Load an Intel HEX file to display content"
            If frm.ShowDialog() = DialogResult.OK Then
                Dim fl As New System.IO.StreamReader(frm.FileName)
                rtbSrc.Text = fl.ReadToEnd()
                fl.Close()
            End If
            rtbSrc.Focus()
        Catch ex As Exception
            Me.Dispose()
        End Try

    End Sub

    Private Sub rtbSrc_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rtbSrc.KeyUp
        If e.KeyCode = Keys.Enter Then
            Try
                DecodeHex()
            Catch ex As Exception
                Dim s As String
                s = wbDsc.DocumentText
                s += String.Format("<hr><br><tt><font color=blue>Exception: {0}</font></tt><br><br><hr>", ex.Message)
                wbDsc.DocumentText = s
            End Try
        End If
    End Sub

    Private Sub DecodeHex()
        Dim str As String
        Dim s As New StringBuilder()
        str = rtbSrc.Text
        s.Append("<html>")
        s.Append("<head><title>HEX Decoder</title></head>")
        s.Append("<body>")

        s.Append("<table cellpadding=5 cellspacing=0 border=1 align=center width='90%'>")
        s.Append("<tr>")
        s.Append("<th>Address</th>")
        s.Append("<th>Byte Value</th>")
        s.Append("</tr>")

        Dim i As Integer = 0
        Dim addr As Integer = 0
        Dim bt As Integer = 0

        While i < str.Length
            If str.Substring(i, 1) = ":" Then
                i += 1
                Dim recLen As Integer
                recLen = Integer.Parse(str.Substring(i, 2), Globalization.NumberStyles.HexNumber) And &HFF
                i += 2

                Dim offset As Integer
                offset = Integer.Parse(str.Substring(i, 4), Globalization.NumberStyles.HexNumber) And &HFFFF
                i += 4

                Dim recType As Integer
                recType = Integer.Parse(str.Substring(i, 2), Globalization.NumberStyles.HexNumber) And &HFF
                i += 2

                addr = offset

                If recType = 1 Then
                    Exit While
                End If

                For j As Integer = 0 To recLen
                    bt = Integer.Parse(str.Substring(i, 2), Globalization.NumberStyles.HexNumber)
                    i += 2
                    If (str.Substring(i, 1) = vbCr) Or (str.Substring(i, 1) = vbLf) Then
                        Exit For
                    End If

                    s.Append("<tr>")
                    s.Append(String.Format("<td align=center><font color=blue family='Consolas'>{0:X4}</font></td><td align=center><font color=borwn family='Consolas'>{1:X2}</font></td>", addr, bt))
                    s.Append("</tr>")
                    addr += 1
                Next
            End If
            i += 1
        End While

        s.Append("</table>")
        s.Append("</body></html>")
        wbDsc.DocumentText = s.ToString()
    End Sub
End Class