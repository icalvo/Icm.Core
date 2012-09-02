Imports System.Runtime.CompilerServices

Namespace Icm.Timers

    Public Module TimerExtensions

        <Extension>
        Function GetNextElapsed(timer As System.Timers.Timer) As Date
            Return Now.AddMilliseconds(timer.Interval)
        End Function

        <Extension>
        Sub SetNextElapsed(timer As System.Timers.Timer, d As Date)
            timer.Interval = d.Subtract(Now).TotalMilliseconds
        End Sub

    End Module

End Namespace
