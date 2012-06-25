Namespace Icm.MathTools

    Public Class LimitedQueue(Of T)

        Private store_ As Generic.LinkedList(Of T)

        Private limit_ As Integer

        Public Sub New(ByVal limit As Integer)
            store_ = New Generic.LinkedList(Of T)
            limit_ = limit
        End Sub

        Public Sub Enqueue(ByVal o As T)
            If store_.Count = limit_ Then
                Dequeue()
            End If
            store_.AddLast(o)
        End Sub

        Public Sub Dequeue()
            store_.RemoveFirst()
        End Sub

        Public Function Head() As T
            Return store_.First.Value
        End Function

        Public Function Tail() As T
            Return store_.Last.Value
        End Function
    End Class
End Namespace
