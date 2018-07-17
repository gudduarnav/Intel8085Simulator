Imports System
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Threading

Public Class HiResTimer
    <DllImport("Kernel32.dll")> _
    Private Shared Function QueryPerformanceCounter(ByRef lpPerformanceCounter As Long) As Boolean

    End Function

    <DllImport("Kernel32.dll")> _
    Private Shared Function QueryPerformanceFrequency(ByRef lpFrequency As Long) As Boolean

    End Function

    Private startTime, stopTime As Long
    Private freq As Long

    Public Sub New()
        startTime = 0
        stopTime = 0
        freq = 0
        If QueryPerformanceFrequency(freq) = False Then
            MessageBox.Show("Hi Performance Timer is not available.....Intel 8085 Clocking will not be available", "Hi Performance Timer Absent", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub
    Public Sub StartTimer()
        ' Thread.Sleep(0)
        QueryPerformanceCounter(startTime)
    End Sub

    Public Sub StopTimer()
        QueryPerformanceCounter(stopTime)
    End Sub

    Public ReadOnly Property Duration
        Get
            If freq = 0 Then
                Return 0
            Else
                Return CType((stopTime - startTime), Double) / CType(freq, Double)
            End If
        End Get
    End Property


End Class
