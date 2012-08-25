
Imports Icm.Collections.Generic
Imports System.Collections



'''<summary>
'''This is a test class for PriorityQueueTest and is intended
'''to contain all PriorityQueueTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class PriorityQueueTest


#Region "Additional test attributes"
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    'End Sub
    '
    'Use ClassCleanup to run code after all tests in a class have run
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    'Use TestInitialize to run code before running each test
    '<TestInitialize()>  _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    'Use TestCleanup to run code after each test has run
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region


    <Test()>
    Public Sub GeneralTestQueues()
        Dim miQ As New PriorityQueue(Of Integer)(3)
        'Los encolamos en prioridad 0
        miQ.Enqueue(7)
        miQ.Enqueue(8)
        miQ.Enqueue(10)
        'encolamos en prioridad 2
        Dim prio As Integer = 2
        miQ.Enqueue(prio, 25)
        miQ.Enqueue(prio, 90)
        'encolamos en prioridad 1
        prio = 1
        miQ.Enqueue(prio, 56)
        miQ.Enqueue(prio, 21)

        Assert.AreEqual(miQ.Count, 7)

        'desencolamos
        Dim resultadoCola As Integer
        resultadoCola = miQ.Dequeue()

        Assert.AreEqual(resultadoCola, 25)
        Assert.AreEqual(miQ.Count, 6)

        miQ.Clear()

        Assert.AreEqual(miQ.Count, 0)

        'Empty Dequeue

        Assert.Throws(Of InvalidOperationException)(Sub()
                                                        miQ.Dequeue()
                                                    End Sub)
    End Sub

End Class
