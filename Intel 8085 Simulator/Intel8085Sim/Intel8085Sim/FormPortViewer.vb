Public Class FormPortViewer
    Public Shared PortPriority As Integer = 1
    Private Shared dt As DataTable = Nothing

    Private Sub FormPortViewer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        RemoveHandler FormMain.GetMachineState.OnEventInPortUpdate, AddressOf OnEventInPortUpdate
        RemoveHandler FormMain.GetMachineState.OnEventOutPortUpdate, AddressOf OnEventOutPortUpdate

    End Sub

    Private Sub FormPortViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If dt Is Nothing Then
            dt = New DataTable()
            Dim dr As DataRow
            Dim dc As DataColumn
            Dim str As String

            For col As Integer = 0 To &HF
                str = String.Format("{0:X}", col)
                dc = New DataColumn(str, GetType(String))
                dt.Columns.Add(dc)
            Next
            For row As Integer = 0 To &HF
                dr = dt.NewRow()
                For col As Integer = 0 To &HF
                    str = String.Format("{0:X}", col)
                    dr(str) = "0"
                Next
                dt.Rows.Add(dr)
            Next
        End If

        dgvw.DataSource = dt
        dgvw.RowHeadersVisible = True

        AddHandler FormMain.GetMachineState.OnEventInPortUpdate, AddressOf OnEventInPortUpdate
        AddHandler FormMain.GetMachineState.OnEventOutPortUpdate, AddressOf OnEventOutPortUpdate
    End Sub

    Private Sub dgvw_DataBindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgvw.DataBindingComplete
        If dgvw.Rows(0).HeaderCell.Value Is Nothing Then
        ElseIf dgvw.Rows(0).HeaderCell.Value.ToString().Trim.Length < 1 Then
        Else
            Return
        End If
        Dim str As String

        For row As Integer = 0 To &HF
            str = String.Format("{0:X}", row)
            dgvw.Rows(row).HeaderCell.Value = str
        Next

    End Sub

    Public Sub OnEventOutPortUpdate(ByVal data As Byte, ByVal port As Byte, ByRef macState As MachineState)
        Dim row, col As Integer
        col = port And &HF
        row = (port >> 4) And &HF
        dgvw.Rows(row).Cells.Item(col).Value = String.Format("{0:X2}", data)
    End Sub
    Public Sub OnEventInPortUpdate(ByRef data As Byte, ByVal port As Byte, ByRef macState As MachineState, ByRef prio As Integer)
        Dim row, col As Integer
        col = port And &HF
        row = (port >> 4) And &HF
        Dim str As String
        str = dgvw.Rows(row).Cells.Item(col).Value
        Dim val As Integer = 0
        Try
            val = Integer.Parse(str, Globalization.NumberStyles.HexNumber)
        Catch ex As Exception

        End Try
        val = val And &HFF
        If FormPortViewer.PortPriority > prio Then
            ' push out data
            data = val
        Else
            OnEventOutPortUpdate(data, port, macState)
        End If
    End Sub

End Class