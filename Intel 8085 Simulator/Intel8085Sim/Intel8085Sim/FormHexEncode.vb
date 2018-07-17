Imports System.Text
Public Class FormHexEncode

    Private Sub FormHexEncode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tbStart.Text = "0000"
        tbSrc.Focus()
    End Sub

    Private Sub tbSrc_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tbSrc.KeyUp
        If e.KeyCode = Keys.Enter Then
            Try
                Dim sa As Integer
                sa = Integer.Parse(tbStart.Text, Globalization.NumberStyles.HexNumber)
                sa = sa And &HFFFF
                Dim src As String
                src = tbSrc.Text
                EncodeHex(sa, src)
            Catch ex As Exception
                wbDst.DocumentText += String.Format("<br><br><hr><br><b><tt>Exception: <font color=blue>{0}</font></tt></b><br><br><hr>", ex.Message)
            End Try
        End If
    End Sub


    Private Sub EncodeHex(ByVal sa As Integer, ByVal src As String)
        Dim db As New StringBuilder()
        db.Append("<html><head><title>HEX Encoder</title></head><body>")
        db.Append("<table cellspacing=0 cellpadding=5 border=2 width='90%' align=center>")
        db.Append("<tr><th>HEX Data</th></tr>")
        db.Append("<tr><td><font color=blue family='Consolas'>")
        Dim i As Integer
        i = 0
        Dim ll As New List(Of Integer)
        While i < src.Length
            If Not (src.Substring(i, 1).Trim.Length < 1) Then
                Dim dataByte As Integer
                Try
                    dataByte = Integer.Parse(src.Substring(i, Math.Min(2, src.Length - i)), Globalization.NumberStyles.HexNumber)
                    i += Math.Min(2, src.Length - i)
                    ll.Add(dataByte)
                    Continue While
                Catch ex As Exception

                End Try
            End If
            i += 1
        End While

        Dim arr() As Integer = ll.ToArray()
        Dim mkr As Integer = 0
        While mkr < arr.Length
            db.Append(":")

            Dim sdt As New StringBuilder()
            Dim startaddress As Integer = sa
            Dim ck As Integer = 0
            For mm As Integer = 0 To &HF
                sdt.Append(String.Format("{0:X2}", arr(mkr)))
                ck += arr(mkr)
                sa += 1
                mkr += 1
                If Not (mkr < arr.Length) Then
                    Exit For
                End If
            Next
            Dim sdt_len As Integer
            sdt_len = sdt.Length
            sdt_len = sdt_len / 2
            ck += sdt_len
            ck += startaddress And &HFF
            ck += (startaddress >> 8) And &HFF
            ck = Not (ck)
            ck += 1
            ck = ck And &HFF
            db.Append(String.Format("{0:X2}{1:X4}00{2}{3:X2}", sdt_len, startaddress, sdt.ToString(), ck))
            db.Append("<br>")
        End While
        db.Append("</td></tr>")
        db.Append("<tr><th>End of HEX File Marker</th></tr>")
        db.Append("<tr><td><font color=blue family='Consolas'>:00000001FF</font></td></tr>")
        db.Append("</table>")
        db.Append("</body></html>")

        wbDst.DocumentText = db.ToString()
    End Sub
End Class