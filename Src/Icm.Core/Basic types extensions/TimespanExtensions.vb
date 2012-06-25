Imports System.Runtime.CompilerServices

Namespace Icm

    Public Module TimespanExtensions


        <Extension()> _
        Function DividedBy(ByVal t As TimeSpan, ByVal divisor As Double) As TimeSpan
            Return New TimeSpan(CLng(t.Ticks / divisor))
        End Function

        <Extension()> _
        Function IsZero(ByVal t As TimeSpan) As Boolean
            Return t = TimeSpan.Zero
        End Function

        <Extension()> _
        Function IsNotZero(ByVal t As TimeSpan) As Boolean
            Return t <> TimeSpan.Zero
        End Function

        ''' <summary>
        ''' Abbreviated format (7d2h3'30'') that omits zero parts
        ''' </summary>
        ''' <param name="ts"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function ToAbbrev(ByVal ts As TimeSpan) As String
            Dim sb As New System.Text.StringBuilder
            If ts = TimeSpan.Zero Then
                Return "0"
            End If

            If ts.Days <> 0 Then
                sb.Append(ts.Days & "d")
            End If
            If ts.Hours <> 0 Then
                sb.Append(ts.Hours & "h")
            End If
            If ts.Minutes <> 0 Then
                sb.Append(ts.Minutes & "'")
            End If
            If ts.Seconds <> 0 Then
                sb.Append(ts.Seconds & "''")
            End If

            Return sb.ToString
        End Function

        ''' <summary>
        ''' Minutes time format (03:30.235) up to milliseconds.
        ''' </summary>
        ''' <param name="ts"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function Tommssttt(ByVal ts As TimeSpan) As String
            Return String.Format("{0:00}:{1:00}.{2:000}", Fix(ts.TotalMinutes), Math.Abs(ts.Seconds), Math.Abs(ts.Milliseconds))
        End Function

        ''' <summary>
        ''' Hour time format (02:03:30.235) up to milliseconds.
        ''' </summary>
        ''' <param name="ts"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function ToHHmmssttt(ByVal ts As TimeSpan) As String
            Return String.Format("{0:00}:{1:00}:{2:00}.{3:000}", Fix(ts.TotalHours), Math.Abs(ts.Minutes), Math.Abs(ts.Seconds), Math.Abs(ts.Milliseconds))
        End Function

        ''' <summary>
        ''' Hour time format (02:03:30) up to seconds.
        ''' </summary>
        ''' <param name="ts"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function ToHHmmss(ByVal ts As TimeSpan) As String
            Return String.Format("{0:00}:{1:00}:{2:00}", Fix(ts.TotalHours), Math.Abs(ts.Minutes), Math.Abs(ts.Seconds))
        End Function

        ''' <summary>
        ''' Hour format (02:03) up to minutes
        ''' </summary>
        ''' <param name="ts"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function ToHHmm(ByVal ts As TimeSpan) As String
            Return String.Format("{0:00}:{1:00}", Fix(ts.TotalHours), Math.Abs(ts.Minutes))
        End Function

        ''' <summary>
        ''' Total microseconds
        ''' </summary>
        ''' <param name="ts"></param>
        ''' <returns></returns>
        ''' <remarks>Don't expect much precision</remarks>
        <Extension()>
        Public Function ToMicroseconds(ByVal ts As TimeSpan) As Long
            Return 1000 * ts.Ticks \ TimeSpan.TicksPerMillisecond
        End Function

        ''' <summary>
        ''' Milliseconds format with one decimal
        ''' </summary>
        ''' <param name="ts"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function ToMillisecondsAndOne(ByVal ts As TimeSpan) As String
            Return (ts.Ticks / TimeSpan.TicksPerMillisecond).ToString("#0.0")
        End Function

    End Module

End Namespace
