Imports Icm.Collections



'''<summary>
'''This is a test class for ICollectionExtensionsTest and is intended
'''to contain all ICollectionExtensionsTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class ICollectionExtensionsTest


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
    '''A test for Join
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub JoinStrTest()
        Dim col As IEnumerable(Of String) = {"a", "b", "c"}
        Dim separator As String = String.Empty
        Dim expected As String = String.Empty
        Dim actual As String

        'Caso 1
        separator = ";"
        expected = "a;b;c"
        actual = col.JoinStr(separator)
        Assert.AreEqual(expected, actual)


        'Caso2
        col = {"a", "1", "c"}
        separator = {";"c}
        expected = "a;1;c"
        actual = col.JoinStr(separator)
        Assert.AreEqual(expected, actual)


        'Caso3
        col = {"05/02/2010 05:10", "04/23/2010 10:25"}
        separator = {";"c}
        expected = "05/02/2010 05:10;04/23/2010 10:25"
        actual = col.JoinStr(separator)
        Assert.AreEqual(expected, actual)

    End Sub

    <Test(), Category("Icm")>
    Public Sub ForceRemoveTest()

        Dim c As ICollection(Of String) = Nothing
        Dim item As String = "b"

        c = New List(Of String) From {"a", "b", "c"}
        ICollectionExtensions.ForceRemove(Of String)(c, item)
        Assert.IsFalse(c.Contains(item))

        'Caso 1
        c = New List(Of String) From {"a", "b", "c"}
        item = "x"
        ICollectionExtensions.ForceRemove(Of String)(c, item)
        Assert.IsFalse(c.Contains(item))

        'Caso 2
        c = New List(Of String) From {"a", "b", "c"}
        item = Nothing
        ICollectionExtensions.ForceRemove(Of String)(c, item)
        Assert.IsFalse(c.Contains(item))

        'Caso 3
        c = New List(Of String) From {"a", "b", "c"}
        item = ""
        ICollectionExtensions.ForceRemove(Of String)(c, item)
        Assert.IsFalse(c.Contains(item))

        'Caso 4
        c = New List(Of String) From {""}
        item = "b"
        ICollectionExtensions.ForceRemove(Of String)(c, item)
        Assert.IsFalse(c.Contains(item))

    End Sub
End Class
