Imports Icm.Collections

<Category("Icm")>
<TestFixture()>
Public Class JoinStrExtensionsTest

    <TestCase({"a", "b", "c"}, ";", "a;b;c")>
    <TestCase({"a", "b", "c"}, "", "abc")>
    <TestCase({"a", "b", "c"}, Nothing, "abc")>
    <TestCase({"a", Nothing, "c"}, ";", "a;;c")>
    <TestCase({"a", "", "c"}, ";", "a;;c")>
    <TestCase({"asdf"}, ";", "asdf")>
    <TestCase(New String() {}, ";", "")>
    Public Sub JoinStr_NormalTests(col As IEnumerable(Of String), separator As String, expected As String)
        Dim actual As String

        actual = col.JoinStr(separator)
        Assert.That(expected, [Is].EqualTo(actual))
    End Sub

    <Test>
    Public Sub JoinStr_WithNullArray_ThrowsArgumentNullException()
        Dim col As IEnumerable(Of String) = Nothing

        Assert.That(Sub() col.JoinStr(";"), Throws.TypeOf(Of ArgumentNullException))
    End Sub
End Class

