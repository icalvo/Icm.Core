
Imports System.Collections.Generic



'''<summary>
'''This is a test class for TreeNodeTest and is intended
'''to contain all TreeNodeTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class TreeNodeTest


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




    '''<summary>
    '''A test for RemoveChild
    '''</summary>
    Public Sub RemoveChildTestHelper()
        Dim v As String = "a"
        Dim target As New TreeNode(Of String)(v)
        Dim tn As New TreeNode(Of String)("b")
        target.RemoveChild(tn)

        Assert.IsFalse(CBool(target.Children.Count(Function(child) child.Value = "b") = 1))
        Assert.IsFalse(target.Children.Count() = 1)

    End Sub

    <Test()>
    Public Sub RemoveChildTest()
        RemoveChildTestHelper()
    End Sub


    '''<summary>
    '''A test for AddChild
    '''</summary>
    Public Sub AddChildTestHelper()
        Dim v As String = "a"
        Dim target As New TreeNode(Of String)(v)
        Dim value As String = "b"
        Dim expected As New TreeNode(Of String)("b")
        Dim actual As TreeNode(Of String)

        actual = target.AddChild(value)

        Assert.That(CBool(target.Children.Count(Function(child) actual.Value = expected.Value) = 1))
        Assert.That(actual.Parent.Value = "a")
        Assert.That(CBool(target.Children.Count() = 1))

    End Sub

    <Test()>
    Public Sub AddChildTest()
        AddChildTestHelper()
    End Sub

    '''<summary>
    '''A test for AddChild
    '''</summary>
    Public Sub AddChildTest1Helper()
        Dim v As String = "a"
        Dim target As New TreeNode(Of String)(v)
        Dim tn As New TreeNode(Of String)("b")
        Dim encontrado As Boolean = False
        target.AddChild(tn)

        Assert.AreEqual(tn.Parent.Value = "a", True)
        Assert.That(target.Children.Count() = 1)
        Assert.That(target.Children.Count(Function(child) child.Value = "b") = 1)

    End Sub

    <Test()>
    Public Sub AddChildTest1()
        AddChildTest1Helper()
    End Sub

    '''<summary>
    '''A test for TestFinal
    '''</summary>
    <Test()>
    Public Sub TestFinal()
        Dim v As String = ("maria")
        Dim target As New TreeNode(Of String)(v)
        Dim ltn As New List(Of TreeNode(Of String)) From {
            New TreeNode(Of String)("b"),
            New TreeNode(Of String)("c"),
            New TreeNode(Of String)("m"),
            New TreeNode(Of String)("S")}

        target.AddChildren(ltn)
        target.AddChild("x")
        For Each elemento In ltn
            If elemento.Value = "b" Then
                target.RemoveChild(elemento)
                Exit For
            End If
        Next

        Assert.That(CBool(target.Children.Count(Function(child) child.Value = "x") = 1))
        Assert.IsFalse(CBool(target.Children.Count(Function(child) child.Value = "b") = 1))
        Assert.AreEqual(target.Children.All(Function(child) child.Parent.Value = "maria"), True)
        Assert.That(target.Children.Count() = 4)


    End Sub

    '''<summary>
    '''A test for AddChild
    '''</summary>
    <Test()>
    Public Sub AddChildrenTest()
        Dim v As String = ("maria")
        Dim target As New TreeNode(Of String)(v)
        Dim tn As New List(Of String) From {"b", "c", "m", "S"}
        Dim encontrado As Boolean = False
        Dim ResultadoList As IEnumerable(Of TreeNode(Of String))

        target.AddChildren(tn)
        ResultadoList = target.Children
        Assert.AreEqual(ResultadoList.All(Function(child) child.Parent.Value = "maria"), True)
        Assert.That(target.Children.Count() = 4)
        Assert.That(target.Children.Count(Function(child) child.Value = "b") = 1)
        Assert.That(target.Children.Count(Function(child) child.Value = "c") = 1)
        Assert.That(target.Children.Count(Function(child) child.Value = "m") = 1)
        Assert.That(target.Children.Count(Function(child) child.Value = "S") = 1)
    End Sub

    '''<summary>
    '''A test for AddChild
    '''</summary>
    <Test()>
    Public Sub AddChildrenTest2()

        Dim v As String = ("maria")
        Dim target As New TreeNode(Of String)(v)
        Dim tn As New List(Of TreeNode(Of String)) From {
            New TreeNode(Of String)("b"),
            New TreeNode(Of String)("c"),
            New TreeNode(Of String)("m"),
            New TreeNode(Of String)("S")}

        Dim encontrado As Boolean = False


        target.AddChildren(tn)
        Assert.AreEqual(tn.All(Function(child) child.Parent.Value = "maria"), True)
        Assert.That(target.Children.Count() = 4)
        Assert.That(target.Children.Count(Function(child) child.Value = "b") = 1)
        Assert.That(target.Children.Count(Function(child) child.Value = "c") = 1)
        Assert.That(target.Children.Count(Function(child) child.Value = "m") = 1)
        Assert.That(target.Children.Count(Function(child) child.Value = "S") = 1)
    End Sub

End Class
