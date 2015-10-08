Imports System.CodeDom.Compiler
Imports Icm.Compilation

<TestFixture(), Category("Icm")>
Public Class CompiledFunctionTest

    <TestCase("Nothing", 1, 1, Result:=0)>
    <TestCase("Nothing", 2, 5, Result:=0)>
    <TestCase("x+y", 1, 1, Result:=2)>
    <TestCase("x+y", 2, 5, Result:=7)>
    <TestCase("x*y", 1, 1, Result:=1)>
    <TestCase("x*y", 2, 5, Result:=10)>
    <TestCase("x..y", 1, 1, ExpectedException:=GetType(CompileException))>
    Public Function Evaluate_Test(code As String, param1 As Integer, param2 As Integer) As Integer
        Dim Funct As CompiledFunction(Of Integer)
        Funct = New VBCompiledFunction(Of Integer)
        Funct.AddParameter(Of Integer)("x")
        Funct.AddParameter(Of Integer)("y")
        Funct.Code = code
        Funct.CompileAsExpression()
        Assert.That(Funct.CompilerErrors.Count = 0)

        Return Funct.Evaluate(param1, param2)
    End Function

End Class

<TestFixture(), Category("Icm")>
Public Class VBCompiledFunctionTest

    <Test>
    Public Sub GeneratedCode_Test()
        Dim Funct As CompiledFunction(Of Integer)
        Funct = New VBCompiledFunction(Of Integer)
        Funct.AddParameter(Of Integer)("x")
        Funct.AddParameter(Of Integer)("y")
        Funct.Code = "x+y*y"

        Dim expected = _
            "Imports System" & vbCrLf & _
            "Imports System.Xml" & vbCrLf & _
            "Imports System.Data" & vbCrLf & _
            "Imports System.Linq" & vbCrLf & _
            "Imports Microsoft.VisualBasic" & vbCrLf & _
            "Namespace dValuate" & vbCrLf & _
            " Class EvalRunTime " & vbCrLf & _
            "  Public Function EvaluateIt( _" & vbCrLf & _
            "   ByVal x As Int32, _" & vbCrLf & _
            "   ByVal y As Int32 _" & vbCrLf & _
            "  ) As Int32" & vbCrLf & _
            "    Return x+y*y" & vbCrLf & _
            "  End Function" & vbCrLf & _
            " End Class " & vbCrLf & _
            "End Namespace" & vbCrLf

        Assert.That(Funct.GeneratedCode, [Is].EqualTo(expected))
    End Sub


End Class
