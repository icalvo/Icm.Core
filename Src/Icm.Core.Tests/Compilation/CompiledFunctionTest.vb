Imports System.CodeDom.Compiler
Imports Icm.Compilation

<TestFixture(), Category("Icm")>
Public Class CompiledFunctionTest

    Private ReadOnly Namespaces As New List(Of String)()

    <Test()>
    Public Sub CompileOkTest()
        Dim Funct As CompiledFunction(Of Integer)
        Funct = New VBCompiledFunction(Of Integer)
        Funct.AddParameter(Of Integer)("x")
        Funct.AddParameter(Of Integer)("y")
        Funct.Code = "x+y"
        Funct.CompileAsExpression()
        Assert.That(Funct.CompilerErrors.Count = 0)
    End Sub

    <Test()>
    Public Sub EvaluateTest()
        Dim Funct As CompiledFunction(Of Integer)
        Funct = New VBCompiledFunction(Of Integer)
        Funct.AddParameter(Of Integer)("x")
        Funct.AddParameter(Of Integer)("y")
        Funct.Code = "x+y"
        Funct.CompileAsExpression()
        Assert.AreEqual(10, Funct.Evaluate(5, 5))
    End Sub

    <Test()>
    Public Sub CompileSyntaxErrorTest()

        Dim Funct As New VBCompiledFunction(Of Integer)
        Funct.AddParameter(Of Integer)("x")
        Funct.AddParameter(Of Integer)("y")
        Funct.Code = "x..y"

        Assert.Throws(Of CompileException)(Sub()
                                               Funct.CompileAsExpression()
                                           End Sub)
        Dim list = Funct.CompilerErrors
        Dim errores = list.Where(Function(ce) Not ce.IsWarning)
        Dim warnings = list.Where(Function(ce) ce.IsWarning)
        Assert.That(errores.Count = 1)
        Assert.That(warnings.Count = 0)
    End Sub

End Class
