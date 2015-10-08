<TestFixture()>
Public Class ObjectExtensionsTest

    Shared ReadOnly IfNothingTestCases As Object() = {
        New TestCaseData("value", "subst").Returns("value"),
        New TestCaseData(Nothing, "subst").Returns("subst"),
        New TestCaseData("value", Nothing).Returns("value"),
        New TestCaseData(Nothing, Nothing).Returns(Nothing),
        New TestCaseData(DBNull.Value, "subst").Returns("subst")
    }

    <TestCaseSource("IfNothingTestCases")>
    Public Function IfNothing_Test(target As Object, subst As String) As String
        Return ObjectExtensions.IfNothing(target, subst)
    End Function

    <TestCase({"hola", "maria", "pato", "perro"}, "hola", Result:=True)>
    <TestCase({"hola", "maria", "pato", "perro"}, "adios", Result:=False)>
    <TestCase({"hola", "maria", "pato", "perro"}, Nothing, Result:=False)>
    <TestCase(New String() {}, "hola", Result:=False)>
    <TestCase(Nothing, "hola", Result:=False)>
    <TestCase(Nothing, Nothing, Result:=False)>
    Public Function IsOneOf_Test(sa As String(), s As String) As Boolean
        Return s.IsOneOf(sa)
    End Function

End Class
