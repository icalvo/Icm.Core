Imports System.CodeDom.Compiler
Imports Icm.Compilation

<TestFixture(), Category("Icm")>
Public Class CompiledFunctionTest

    <Test(), Category("Icm")>
    Public Sub CompileOkTest()
        Dim Funct As CompiledFunction(Of Integer)
        Funct = New VBCompiledFunction(Of Integer)
        Funct.AddParameter("x", "Integer")
        Funct.AddParameter("y", "Integer")
        Funct.Code = "x+y"
        Funct.CompileAsExpression()
        Assert.IsTrue(Funct.CompilerErrors.Count = 0)
    End Sub

    <Test(), Category("Icm")>
    Public Sub EvaluateTest()
        Dim Funct As CompiledFunction(Of Integer)
        Funct = New VBCompiledFunction(Of Integer)
        Funct.AddParameter("x", "Integer")
        Funct.AddParameter("y", "Integer")
        Funct.Code = "x+y"
        Funct.CompileAsExpression()
        Assert.AreEqual(10, Funct.Evaluate(5, 5))
    End Sub

    <Test(), Category("Icm")>
    Public Sub CompileSyntaxErrorTest()

        Dim Funct As CompiledFunction(Of Integer)
        Funct = New VBCompiledFunction(Of Integer)
        Funct.AddParameter("x", "Integer")
        Funct.AddParameter("y", "Integer")
        Funct.Code = "x..y"

        Assert.Throws(Of CompileException)(Sub()
                                               Funct.CompileAsExpression()
                                           End Sub)
        Dim list = Funct.CompilerErrors
        Dim errores = list.Where(Function(ce) Not ce.IsWarning)
        Dim warnings = list.Where(Function(ce) ce.IsWarning)
        Assert.IsTrue(errores.Count = 1)
        Assert.IsTrue(warnings.Count = 0)
    End Sub

End Class
