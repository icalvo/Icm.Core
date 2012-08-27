Imports System.Runtime.CompilerServices

Namespace Icm

    Public Module TimespanExtensions

        ''' <summary>
        ''' Division of a Timespan by a number
        ''' </summary>
        ''' <param name="t"></param>
        ''' <param name="divisor"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Function DividedBy(ByVal t As TimeSpan, ByVal divisor As Double) As TimeSpan
            Return New TimeSpan(CLng(t.Ticks / divisor))
        End Function

        ''' <summary>
        ''' Is the timespan equal to Timespan.Zero?
        ''' </summary>
        ''' <param name="t"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Function IsZero(ByVal t As TimeSpan) As Boolean
            Return t = TimeSpan.Zero
        End Function

        ''' <summary>
        ''' Is the timespan not equal to Timespan.Zero?
        ''' </summary>
        ''' <param name="t"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
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
            Dim absoluteTs As TimeSpan
            If ts < TimeSpan.Zero Then
                absoluteTs = ts.Negate
                sb.Append("-")
            Else
                absoluteTs = ts
            End If

            If absoluteTs.Days <> 0 Then
                sb.Append(absoluteTs.Days & "d")
            End If
            If absoluteTs.Hours <> 0 Then
                sb.Append(absoluteTs.Hours & "h")
            End If
            If absoluteTs.Minutes <> 0 Then
                sb.Append(absoluteTs.Minutes & "'")
            End If
            If absoluteTs.Seconds <> 0 Then
                sb.Append(absoluteTs.Seconds & "''")
            End If

            Return sb.ToString
        End Function

        ''' <summary>
        ''' Minutes time format (03:30.235) up to milliseconds.
        ''' </summary>
        ''' <param name="ts"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function Tommssttt(ByVal ts As TimeSpan) As String
            Return String.Format("{0:00}:{1:00}.{2:000}", Fix(ts.TotalMinutes), Math.Abs(ts.Seconds), Math.Abs(ts.Milliseconds))
        End Function

        ''' <summary>
        ''' Hour time format (02:03:30.235) up to milliseconds.
        ''' </summary>
        ''' <param name="ts"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
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
        Public Function TotalMicroseconds(ByVal ts As TimeSpan) As Long
            Return 1000 * ts.Ticks \ TimeSpan.TicksPerMillisecond
        End Function

    End Module

End Namespace
