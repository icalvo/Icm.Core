Namespace Icm

    Public MustInherit Class BaseTotalOrder(Of T As IComparable(Of T))
        Implements ITotalOrder(Of T)

        Public MustOverride Function Least() As T Implements ITotalOrder(Of T).Least

        Public MustOverride Function Long2T(ByVal d As Long) As T Implements ITotalOrder(Of T).Long2T

        Public MustOverride Function Greatest() As T Implements ITotalOrder(Of T).Greatest

        Public MustOverride Function T2Long(ByVal t As T) As Long Implements ITotalOrder(Of T).T2Long

        Public Overridable Function Compare(ByVal x As T, ByVal y As T) As Integer Implements System.Collections.Generic.IComparer(Of T).Compare
            Return T2Long(x).CompareTo(T2Long(y))
        End Function

        Public Overridable Function [Next](ByVal t As T) As T Implements ITotalOrder(Of T).Next
            Dim val As Long = T2Long(t)
            Dim result As T

            Do
                If val = Long.MaxValue Then
                    Throw New ArgumentOutOfRangeException("t")
                End If
                val += 1
                result = Long2T(val)
            Loop Until t.CompareTo(result) < 0

            Return result
        End Function

        Public Overridable Function Previous(ByVal t As T) As T Implements ITotalOrder(Of T).Previous
            Dim val As Long = T2Long(t)
            Dim result As T

            Do
                If val = Long.MinValue Then
                    Throw New ArgumentOutOfRangeException("t")
                End If
                val -= 1
                result = Long2T(val)
            Loop Until t.CompareTo(result) > 0

            Return result
        End Function

    End Class
End Namespace
