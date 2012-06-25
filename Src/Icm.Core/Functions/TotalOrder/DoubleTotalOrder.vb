Namespace Icm
    Public Class DoubleTotalOrder
        Inherits BaseTotalOrder(Of Double)

        Public Overrides Function Least() As Double
            Return Double.NegativeInfinity
        End Function

        Public Overrides Function Greatest() As Double
            Return Double.PositiveInfinity
        End Function

        Public Overrides Function T2Long(ByVal t As Double) As Long
            Return BitConverter.DoubleToInt64Bits(t)
        End Function

        Public Overrides Function Long2T(ByVal d As Long) As Double
            Return BitConverter.Int64BitsToDouble(d)
        End Function

        Public Overrides Function [Next](ByVal t As Double) As Double
            Return Extreme.FloatingPoint.NextAfter(t, Double.MaxValue)
        End Function

    End Class
End Namespace
