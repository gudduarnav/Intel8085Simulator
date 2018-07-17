Imports System.IO.Ports
Imports System.Threading
Public Class FormUsart
    Private Shared com As SerialPort = Nothing
    Private Shared comMode As Boolean = False

    Private ev As New ManualResetEvent(False)

    Private Sub FormUsart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ev.Reset()

        Dim str() As String = SerialPort.GetPortNames()
        For Each strCom As String In str
            Dim strComArr() As Char = strCom.ToCharArray
            Dim strComName As String = "COM"
            For Each strComA As Char In strComArr
                If IsNumeric(strComA) = True Then
                    strComName += strComA
                End If
            Next
            cbComList.Items.Add(strComName)
        Next

        If com Is Nothing Then
            cbComList.SelectedIndex = 0
            lbSelectedCom.Text = ""
        Else
            cbComList.SelectedIndex = cbComList.Items.IndexOf(com.PortName.ToUpper())
            lbSelectedCom.Text = com.PortName.ToUpper()
        End If
    End Sub



    Private Shared kd As FormUsart = Nothing
    Public Shared Sub LoadMe(ByVal p As FormMain)
        If kd Is Nothing Then
            kd = New FormUsart()
            kd.MdiParent = p
            kd.Show()
        End If
    End Sub
    Public Shared Sub OutPortCommand(ByVal p As FormMain, ByVal ch As Integer)
        Try
            If com.IsOpen() = True Then
                com.Close()
            End If

            Dim baud As Integer = ch And &H3
            Dim chLen As Integer = (ch >> 3) And &H3
            Dim enPar As Integer = (ch >> 4) And &H1
            Dim evPar As Integer = (ch >> 5) And &H1
            Dim stopLen As Integer = (ch >> 6) And &H3

            Dim synMode As Integer = stopLen And &H1
            Dim scs As Integer = (stopLen >> 1) And &H1

            If comMode = False Then
                ' mode
                If baud = 0 Then
                    ' sync mode ??????
                Else
                    ' async mode
                    If baud = 1 Then
                        com.BaudRate = 1536 * 1024 / 10
                    ElseIf baud = 2 Then
                        com.BaudRate = 1536 * 1024 / (10 * 16)
                    ElseIf baud = 2 Then
                        com.BaudRate = 1536 * 1024 / (10 * 64)
                    End If

                    If stopLen = 0 Then
                        com.StopBits = StopBits.None
                    ElseIf stopLen = 1 Then
                        com.StopBits = StopBits.One
                    ElseIf stopLen = 2 Then
                        com.StopBits = StopBits.OnePointFive
                    ElseIf stopLen = 3 Then
                        com.StopBits = StopBits.Two
                    End If

                End If

                If chLen = 0 Then
                    com.DataBits = 5
                ElseIf chLen = 1 Then
                    com.DataBits = 6
                ElseIf chLen = 2 Then
                    com.DataBits = 7
                ElseIf chLen = 3 Then
                    com.DataBits = 8
                End If

                If enPar = 1 Then
                    If evPar = 0 Then
                        com.Parity = Parity.Odd
                    Else
                        com.Parity = Parity.Even
                    End If
                End If

            Else
            ' command 
                Dim txen, dtr, rxe, sbrk, er, rts, ir, eh As Integer
                txen = ch And &H1
                dtr = (ch >> 1) And &H1
                rxe = (ch >> 2) And &H1
                sbrk = (ch >> 3) And &H1
                er = (ch >> 4) And &H1
                rts = (ch >> 5) And &H1
                ir = (ch >> 6) And &H1
                eh = (ch >> 7) And &H1

                If sbrk = 1 Then
                    com.BreakState = True
                Else
                    com.BreakState = False
                End If

                If dtr = 1 Then
                    com.DtrEnable = True
                Else
                    com.DtrEnable = False
                End If

                If rts = 1 Then
                    com.RtsEnable = True
                Else
                    com.RtsEnable = False
                End If

            End If

            com.Open()
            comMode = True
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function IntFromBool(ByVal b As Boolean) As Integer
        If b = True Then
            Return 1
        Else
            Return 0
        End If
    End Function

    Public Shared Function InPortCommand(ByVal p As FormMain) As Integer
        Try
            Dim txrdy, rxrdy, txempty, pe, oe, fe, bd, dsr As Integer
            If com.BytesToRead > 0 Then
                txrdy = 1
            Else
                txrdy = 0
            End If
            txrdy = txrdy And IntFromBool(Not (com.CtsHolding))

            If com.BytesToWrite > 0 Then
                txempty = 0
            Else
                txempty = 1
            End If

            If com.BytesToRead > 0 Then
                rxrdy = 1
            Else
                rxrdy = 0
            End If

            bd = IntFromBool(com.BreakState)
            pe = IntFromBool(False)
            oe = IntFromBool(False)
            fe = IntFromBool(False)
            dsr = IntFromBool(com.DsrHolding)
            Dim d As Integer
            d = txrdy
            d = d Or (rxrdy << 1)
            d = d Or (txempty << 2)
            d = d Or (pe << 3)
            d = d Or (oe << 4)
            d = d Or (fe << 5)
            d = d Or (bd << 6)
            d = d Or (dsr << 7)
            d = d And &HFF
            Return d
        Catch ex As Exception

            Return 0
        End Try
    End Function

    Public Shared Sub OutPort(ByVal p As FormMain, ByVal ch As Integer)
        Try
            Dim bt As Byte() = {CByte(ch And &HFF)}
            com.Write(bt, 0, bt.Length)
        Catch ex As Exception
        End Try

    End Sub
    Public Shared Function InPort(ByVal p As FormMain) As Integer
        Try
            Return com.ReadByte()
        Catch ex As Exception

            Return 0
        End Try
    End Function

    Private Sub FormUsart_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        kd = Nothing
    End Sub

    Public Shared Function PortInWait() As Boolean
        Try
            If Not (com Is Nothing) Then
                Return True
            End If

            If kd Is Nothing Then
                Return False
            End If

            While kd.ev.WaitOne() <> True
                Thread.Sleep(50)
            End While

            kd.ev.Reset()

        Catch ex As Exception

        End Try
        Return True
    End Function

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Try
            If Not (com Is Nothing) Then
                com.Close()
                com.Dispose()
            End If
            com = New SerialPort(cbComList.Items(cbComList.SelectedIndex).ToString)
            com.ReadTimeout = SerialPort.InfiniteTimeout
            com.WriteTimeout = SerialPort.InfiniteTimeout

            com.Open()
            lbSelectedCom.Text = com.PortName.ToUpper
            comMode = False
            ev.Set()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class