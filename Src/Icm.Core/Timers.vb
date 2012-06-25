Namespace Icm.Timers

    Public Class Timer
        Inherits System.Timers.Timer

        Public Sub New()

        End Sub

        Public Sub New(ByVal interval As Double)
            MyBase.New(interval)
        End Sub

        Property NextElapsed() As Date
            Get
                Return Now.Add(New TimeSpan(0, 0, 0, 0, CInt(Interval)))
            End Get
            Set(ByVal Value As Date)
                Interval = Value.Subtract(Now).TotalMilliseconds
            End Set
        End Property
    End Class

End Namespace
