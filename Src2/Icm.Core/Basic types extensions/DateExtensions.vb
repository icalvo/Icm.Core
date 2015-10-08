Imports System.Runtime.CompilerServices

Namespace Icm

    Public Module DateExtensions

        Public Enum Seasons As Integer
            Spring = 0
            Summer = 1
            Fall = 2
            Winter = 3
        End Enum

        ''' <summary>
        ''' Season of a given date.
        ''' </summary>
        ''' <param name="d"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Function Season(ByVal d As Date) As Seasons
            Dim monthDay As Integer = d.Month * 100 + d.Day
            If monthDay >= 101 AndAlso monthDay < 321 Then
                Return Seasons.Winter
            ElseIf monthDay >= 321 AndAlso monthDay < 621 Then
                Return Seasons.Spring
            ElseIf monthDay >= 621 AndAlso monthDay < 921 Then
                Return Seasons.Summer
            ElseIf monthDay >= 921 AndAlso monthDay < 1221 Then
                Return Seasons.Fall
            Else
                Return Seasons.Winter
            End If
        End Function

        ''' <summary>
        ''' Adds a duration to a date with saturation (if the result is out of the valid range, it returns Date.MaxValue)
        ''' </summary>
        ''' <param name="d">Date</param>
        ''' <param name="dur">Duration to add</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function AddS(ByVal d As Date, ByVal dur As TimeSpan) As Date
            If dur < TimeSpan.Zero Then
                Throw New ArgumentException("Cannot accept negative durations", "dur")
            End If
            If dur = TimeSpan.MaxValue Then
                Return Date.MaxValue
            Else
                Dim maxDate = Date.MaxValue.Subtract(dur)
                If d > maxDate Then
                    Return Date.MaxValue
                Else
                    Return d.Add(dur)
                End If
            End If
        End Function

    End Module

End Namespace
