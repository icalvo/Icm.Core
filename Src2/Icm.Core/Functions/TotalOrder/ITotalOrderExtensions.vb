Imports System.Runtime.CompilerServices

Namespace Icm
    Public Module ITotalOrderExtensions

        <Extension()>
        Public Function LstIfNull(Of T As {Structure, IComparable(Of T)})(ByVal totalOrder As ITotalOrder(Of T), ByVal obj As T?) As T
            If obj.HasValue Then
                Return obj.Value
            Else
                Return totalOrder.Least
            End If
        End Function

        <Extension()>
        Public Function GstIfNull(Of T As {Structure, IComparable(Of T)})(ByVal totalOrder As ITotalOrder(Of T), ByVal obj As T?) As T
            If obj.HasValue Then
                Return obj.Value
            Else
                Return totalOrder.Greatest
            End If
        End Function

        <Extension()>
        Public Function LstIfNull(Of T As {IComparable(Of T)})(ByVal totalOrder As ITotalOrder(Of T), ByVal obj As Nullable2(Of T)) As T
            If obj.HasValue Then
                Return obj.Value
            Else
                Return totalOrder.Least
            End If
        End Function

        <Extension()>
        Public Function GstIfNull(Of T As {IComparable(Of T)})(ByVal totalOrder As ITotalOrder(Of T), ByVal obj As Nullable2(Of T)) As T
            If obj.HasValue Then
                Return obj.Value
            Else
                Return totalOrder.Greatest
            End If
        End Function

    End Module
End Namespace
