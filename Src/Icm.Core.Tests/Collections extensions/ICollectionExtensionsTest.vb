Imports Icm.Collections

<Category("Icm")>
<TestFixture()>
Public Class ICollectionExtensionsTest

    <TestCase({"a", "b", "c"}, "b", {"a", "c"})>
    <TestCase({"a", "b", "c"}, "x", {"a", "b", "c"})>
    <TestCase({"a", Nothing, "c"}, Nothing, {"a", "c"})>
    <TestCase({"a", "", "c"}, "", {"a", "c"})>
    <TestCase(New String() {}, "a", New String() {})>
    <TestCase(New String() {}, Nothing, New String() {})>
    <TestCase(New String() {}, "", New String() {})>
    Public Sub ForceRemove_NormalTests(col As IEnumerable(Of String), itemRemoved As String, expected As IEnumerable(Of String))
        Dim list As New List(Of String)(col)
        list.ForceRemove(itemRemoved)
        Assert.That(list, [Is].EquivalentTo(expected))
    End Sub

    <Test>
    Public Sub ForceRemove_WithNullArray_ThrowsNullReferenceException()
        Dim col As ICollection(Of String) = Nothing

        Assert.That(Sub() col.ForceRemove(";"), Throws.TypeOf(Of NullReferenceException))
    End Sub

End Class
