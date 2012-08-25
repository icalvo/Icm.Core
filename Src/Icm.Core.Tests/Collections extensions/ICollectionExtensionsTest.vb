Imports Icm.Collections

<Category("Icm")>
<TestFixture()>
Public Class ICollectionExtensionsTest

    <TestCase({"a", "b", "c"}, ";", "a;b;c")>
    <TestCase({"a", "b", "c"}, "", "abc")>
    <TestCase({"a", "b", "c"}, Nothing, "abc")>
    <TestCase({"a", Nothing, "c"}, ";", "a;;c")>
    <TestCase({"a", "", "c"}, ";", "a;;c")>
    <TestCase({"asdf"}, ";", "asdf")>
    <TestCase({}, ";", "")>
    Public Sub JoinStrTest(col As IEnumerable(Of String), separator As String, expected As String)
        Dim actual As String

        actual = col.JoinStr(separator)
        Assert.That(expected, [Is].EqualTo(actual))
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
