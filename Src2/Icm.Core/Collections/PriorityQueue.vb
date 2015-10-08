Imports System.Collections.Generic

Namespace Icm.Collections.Generic

    ''' <summary>
    ''' Priority queue with N possible priorities. You can introduce elements
    ''' with a given priority.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks></remarks>
    Public Class PriorityQueue(Of T)

        Private store_ As List(Of Queue(Of T))
        Private count_ As Integer

        ''' <summary>
        '''  Build a new priority queue with a given maximum priority.
        ''' </summary>
        ''' <param name="maxprio">Zero-based maximum priority.</param>
        ''' <remarks>Remember that priorities start with 0.</remarks>
        Public Sub New(ByVal maxprio As Integer)
            Debug.Assert(maxprio >= 0, "The maximum priority must be greater or equal than zero")
            store_ = New List(Of Queue(Of T))(maxprio)
            For i As Integer = 0 To maxprio
                store_.Add(New Queue(Of T))
            Next
            count_ = 0
        End Sub

        ''' <summary>
        '''  Enqueue an element with the lowest priority.
        ''' </summary>
        ''' <param name="val">Enqueued element.</param>
        ''' <remarks></remarks>
        Public Sub Enqueue(ByVal val As T)
            store_(0).Enqueue(val)
            count_ += 1
        End Sub

        ''' <summary>
        '''  Enqueue an element with the given priority.
        ''' </summary>
        ''' <param name="prio">Priority with which the element will be enqueued.</param>
        ''' <param name="val">Enqueued element.</param>
        ''' <remarks></remarks>
        Public Sub Enqueue(ByVal prio As Integer, ByVal val As T)
            Debug.Assert(prio >= 0, "The maximum priority must be greater or equal than zero")
            Debug.Assert(prio < store_.Count - 1, "The priority is greater than the maximum priority defined for this queue")

            store_(prio).Enqueue(val)
            count_ += 1
        End Sub

        ''' <summary>
        '''  Dequeue and return the first element in the queue with the given priority.
        ''' </summary>
        ''' <param name="prio">A priority.</param>
        ''' <returns>The dequeued element.</returns>
        ''' <remarks>This function modifies the object. It is protected because it can break the class invariant.</remarks>
        Protected Function Dequeue(ByVal prio As Integer) As T
            Debug.Assert(prio >= 0, "The maximum priority must be greater or equal than zero")
            Debug.Assert(prio < store_.Count - 1, "The priority is greater than the maximum priority defined for this queue")

            Return store_(prio).Dequeue()
            count_ -= 1
        End Function

        ''' <summary>
        '''  Dequeue the first element of the queue of greatest priority.
        ''' </summary>
        ''' <returns>The dequeued element.</returns>
        ''' <remarks>This function modifies the object.</remarks>
        Public Function Dequeue() As T
            For i As Integer = store_.Count - 1 To 0 Step -1
                If store_(i).Count > 0 Then
                    count_ -= 1
                    Return store_(i).Dequeue
                End If
            Next
            Throw New InvalidOperationException("The priority queue is empty")
        End Function

        ''' <summary>
        '''  Gets the number of elements actually contained in the PriorityQueue.
        ''' </summary>
        ''' <value>The number of elements actually contained in the PriorityQueue.</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property Count() As Integer
            Get
                Return count_
            End Get
        End Property

        ''' <summary>
        '''  Wipes the contents.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Clear()
            For Each q As Queue(Of T) In store_
                q.Clear()
            Next
            count_ = 0
        End Sub
    End Class

End Namespace
