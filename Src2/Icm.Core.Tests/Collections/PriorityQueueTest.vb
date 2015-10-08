
Imports Icm.Collections.Generic
Imports System.Collections

<TestFixture(), Category("Icm")>
Public Class PriorityQueueTest

    <Test()>
    Public Sub PriorityQueue_GeneralTest()
        Dim miQ As New PriorityQueue(Of Integer)(maxprio:=3)
        'Enqueue with priority 0
        miQ.Enqueue(7)
        miQ.Enqueue(8)
        miQ.Enqueue(10)
        'Enqueue with priority 2
        Dim prio As Integer = 2
        miQ.Enqueue(prio, 25)
        miQ.Enqueue(prio, 90)
        'Enqueue with priority 1
        prio = 1
        miQ.Enqueue(prio, 56)
        miQ.Enqueue(prio, 21)

        Assert.AreEqual(miQ.Count, 7)

        'Dequeue
        Dim result As Integer

        result = miQ.Dequeue()
        Assert.AreEqual(result, 25)
        Assert.AreEqual(miQ.Count, 6)

        result = miQ.Dequeue()
        Assert.AreEqual(result, 90)
        Assert.AreEqual(miQ.Count, 5)

        result = miQ.Dequeue()
        Assert.AreEqual(result, 56)
        Assert.AreEqual(miQ.Count, 4)

        result = miQ.Dequeue()
        Assert.AreEqual(result, 21)
        Assert.AreEqual(miQ.Count, 3)


        miQ.Clear()

        Assert.AreEqual(miQ.Count, 0)

        'Dequeue with empty queue
        Assert.That(Sub() miQ.Dequeue(), Throws.InvalidOperationException)
    End Sub

End Class
