Imports Icm.Collections

<Category("Icm")>
<TestFixture()>
Public Class JoinStrExtensionsTest

    <TestCase({"a", "b", "c"}, ";", "a;b;c")>
    <TestCase({"a", "b", "c"}, "", "abc")>
    <TestCase({"a", "b", "c"}, Nothing, "abc")>
    <TestCase({"a", Nothing, "c"}, ";", "a;c")>
    <TestCase({"a", "", "c"}, ";", "a;c")>
    <TestCase({"asdf"}, ";", "asdf")>
    <TestCase(New String() {}, ";", "")>
    <TestCase(Nothing, ";", "", ExpectedException:=GetType(ArgumentNullException))>
    Public Sub JoinStr1_Test(col As IEnumerable(Of String), separator As String, expected As String)
        Dim actual As String

        actual = col.JoinStr(separator)
        Assert.That(expected, [Is].EqualTo(actual))
    End Sub

    <TestCase(New String() {}, "")>
    <TestCase({"a"}, "a")>
    <TestCase({"a", "b"}, "a y b")>
    <TestCase({"a", "b", "c"}, "a, b y c")>
    Public Sub JoinStr2_Test(col As IEnumerable(Of String), expected As String)
        Assert.That(col.JoinStr(", ", " y "), [Is].EqualTo(expected))
    End Sub

End Class

