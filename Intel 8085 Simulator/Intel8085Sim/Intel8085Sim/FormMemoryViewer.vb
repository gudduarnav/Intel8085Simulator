Public Class FormMemoryViewer
    Private Shared dt As DataTable = Nothing

    Private Sub FormMemoryViewer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        RemoveHandler FormMain.GetMachineState().OnEventMemoryUpdate, AddressOf OnEventMemoryUpdate

    End Sub

    Private Sub FormMemoryViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadMemory()
        AddHandler FormMain.GetMachineState().OnEventMemoryUpdate, AddressOf OnEventMemoryUpdate

    End Sub
    ' Address : Row(Hi):Col(Lo)


    Public Sub LoadMemory()
        Try
            If dt Is Nothing Then

                dt = New DataTable()
                Dim dc As DataColumn
                Dim dr As DataRow

                Dim str, str1 As String

                For col As Integer = 0 To &HFF
                    str = String.Format("{0:X2}", col)
                    dc = New DataColumn(str, GetType(String))
                    dt.Columns.Add(dc)
                Next

                For row As Integer = 0 To &HFF
                    dr = dt.NewRow()
                    For col As Integer = 0 To &HFF
                        str = String.Format("{0:X2}", col)
                        str1 = String.Format("{0:X2}", FormMain.GetMachineState().GetMemory((row << 8) Or col))
                        dr(str) = str1
                    Next

                    dt.Rows.Add(dr)
                Next
            End If

            dgview.DataSource = dt
            dgview.RowHeadersVisible = True

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub
    Private Sub RefreshDGView()
        dgview.Width = Me.Width
        dgview.Height = Me.Height
        dgview.Left = 0
        dgview.Top = 0
    End Sub

    Private Sub dgview_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgview.CellEndEdit
        Dim row, col, addr As Integer
        row = e.RowIndex
        col = e.ColumnIndex
        addr = (row << 8) Or col

        Dim str As String
        str = dgview.Rows(row).Cells.Item(col).Value.ToString()

        Dim data As Integer = 0
        If Integer.TryParse(str, System.Globalization.NumberStyles.HexNumber, Nothing, data) = True Then
            FormMain.GetMachineState().SetMemory(addr, data And &HFF, False)
            Dim valx As Integer
            valx = FormMain.GetMachineState().GetMemory(addr)
            If Not (valx = data) Then
                dgview.Rows(row).Cells.Item(col).Value = valx.ToString()
            End If
        End If
    End Sub

    Private Sub dgview_DataBindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgview.DataBindingComplete
        If dgview.Rows(0).HeaderCell.Value Is Nothing Then
        ElseIf dgview.Rows(0).HeaderCell.Value.ToString().Trim.Length < 1 Then
        Else
            Return
        End If

        Dim str As String
        For row As Integer = 0 To &HFF
            str = String.Format("{0:X2}", row)
            dgview.Rows(row).HeaderCell.Value = str
        Next
    End Sub

    Public Sub OnEventMemoryUpdate(ByVal addr As UInt16, ByRef macState As MachineState)
        Dim row, col As Integer
        Dim data As Byte = macState.GetMemory(addr)
        col = addr And &HFF
        row = (addr >> 8) And &HFF

        Dim str As String
        str = String.Format("{0:X2}", data)
        dgview.Rows(row).Cells.Item(col).Value = str
    End Sub

End Class