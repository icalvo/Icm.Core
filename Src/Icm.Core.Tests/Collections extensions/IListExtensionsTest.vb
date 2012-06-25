Imports System.Collections.Generic


Imports Icm.Collections



'''<summary>
'''This is a test class for IListExtensionsTest and is intended
'''to contain all IListExtensionsTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class IListExtensionsTest


    <Test(), Category("Icm")>
    Public Sub InicializarNewTest()
        Dim list As New List(Of Integer) From {1, 2, 3}
        Dim elementos As Integer = 5

        'Caso 1
        IListExtensions.InitializeNew(Of Integer)(list, elementos)
        Assert.AreEqual(elementos, list.Count)
        Assert.IsTrue(list.All(Function(e) e = 0))

        'Caso 2
        elementos = -2
        IListExtensions.InitializeNew(Of Integer)(list, elementos)
        Assert.AreNotEqual(elementos, list.Count)
        Assert.IsTrue(list.All(Function(e) e = 0))

        'Caso 3
        elementos = 0
        IListExtensions.InitializeNew(Of Integer)(list, elementos)
        Assert.AreEqual(elementos, list.Count)
        Assert.IsTrue(list.All(Function(e) e = 0))
    End Sub

    <Test(), Category("Icm")>
    Public Sub InitializeTest()
        Dim list As New List(Of Integer) From {1, 2, 3}
        Dim elementos As Integer = 5


        'Caso 1
        IListExtensions.Initialize(Of Integer)(list, elementos)
        Assert.AreEqual(elementos, list.Count)
        Assert.IsTrue(list.All(Function(e) e = 0))

        'Caso 2
        elementos = -2
        IListExtensions.Initialize(Of Integer)(list, elementos)
        Assert.AreNotEqual(elementos, list.Count)
        Assert.IsTrue(list.All(Function(e) e = 0))

        'Caso 3
        elementos = 0
        IListExtensions.Initialize(Of Integer)(list, elementos)
        Assert.AreEqual(elementos, list.Count)
        Assert.IsTrue(list.All(Function(e) e = 0))

    End Sub
End Class
