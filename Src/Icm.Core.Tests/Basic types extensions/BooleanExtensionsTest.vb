<TestFixture()>
Public Class BooleanExtensionsTest

    <TestCase(True, Result:=1)>
    <TestCase(False, Result:=0)>
    Public Function ToInteger_Test(bool As Boolean) As Integer
        Return bool.ToInteger
    End Function

    <TestCase(True, "cadena verdad", "cadena falso", "cadena nulo", Result:="cadena verdad")>
    <TestCase(False, "cadena verdad", "cadena falso", "cadena nulo", Result:="cadena falso")>
    <TestCase(Nothing, "cadena verdad", "cadena falso", "cadena nulo", Result:="cadena nulo")>
    Public Function IfN_Test(bool As Boolean?, trueString As String, falseString As String, nullString As String) As String
        Return bool.IfN(trueString, falseString, nullString)
    End Function

End Class
