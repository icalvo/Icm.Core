Namespace Icm
    Class LongTotalOrder
        Inherits BaseTotalOrder(Of Long)

        Public Overrides Function Least() As Long
            Return Long.MinValue
        End Function

        Public Overrides Function Long2T(ByVal d As Long) As Long
            Return d
        End Function

        Public Overrides Function Greatest() As Long
            Return Long.MaxValue
        End Function

        Public Overrides Function T2Long(ByVal t As Long) As Long
            Return t
        End Function

    End Class

End Namespace
