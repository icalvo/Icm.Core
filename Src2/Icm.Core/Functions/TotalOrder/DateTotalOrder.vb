Namespace Icm
    Public Class DateTotalOrder
        Inherits BaseTotalOrder(Of Date)

        Public Overrides Function Least() As Date
            Return Date.MinValue
        End Function

        Public Overrides Function Greatest() As Date
            Return Date.MaxValue
        End Function

        Public Overrides Function T2Long(ByVal t As Date) As Long
            Return t.Ticks
        End Function

        Public Overrides Function Long2T(ByVal d As Long) As Date
            Return New Date(d)
        End Function

    End Class
End Namespace
