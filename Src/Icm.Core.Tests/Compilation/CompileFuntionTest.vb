Imports System.CodeDom.Compiler
Imports Icm.Compilation

<TestFixture(), Category("Icm")>
Public Class CompileFuntionTest

    <Test(), Category("Icm")>
    Public Sub TestTodoOk()

        Dim Funtion As CompiledFunction(Of Integer)
        Funtion = New VBCompiledFunction(Of Integer)
        Funtion.AddParameter("x", "Integer")
        Funtion.AddParameter("y", "Integer")
        Funtion.Code = "x+y"
        Funtion.CompileAsExpression()
        If Funtion.CompilerErrors.Count = 0 Then
            Assert.IsTrue(Funtion.Evaluate(5, 5) = 10)
        End If

    End Sub

    <Test(), Category("Icm")>
    Public Sub TestErrorCode()

        Dim Funtion As CompiledFunction(Of Integer)
        Funtion = New VBCompiledFunction(Of Integer)
        Funtion.AddParameter("x", "Integer")
        Funtion.AddParameter("y", "Integer")
        Funtion.Code = "x..y"
        Try
            Funtion.CompileAsExpression()
            Assert.Fail()
        Catch ex As CompileException
            Dim list As CompilerError()
            ReDim list(Funtion.CompilerErrors.Count - 1)
            Funtion.CompilerErrors.CopyTo(list, 0)
            Dim errores = list.Where(Function(ce) Not ce.IsWarning)
            Dim warnings = list.Where(Function(ce) ce.IsWarning)
            Assert.IsTrue(Funtion.CompilerErrors.HasErrors = True)
            Assert.IsTrue(errores.Count = 1)
        Catch ex As Exception
            Assert.Fail()
        End Try
    End Sub

    <Test(), Category("Icm")>
    Public Sub TestErrorEvaluate()

        Dim Funtion As CompiledFunction(Of Integer)
        Funtion = New VBCompiledFunction(Of Integer)
        Funtion.AddParameter("x", "Integer")
        Funtion.AddParameter("y", "Integer")
        Funtion.Code = "x+y"
        Funtion.CompileAsExpression()
        If Funtion.CompilerErrors.Count = 0 Then
            Assert.IsFalse(Funtion.Evaluate(10, 5) = 25)
        End If
    End Sub

    <Test(), Category("Icm")>
    Public Sub TestEvaluateError()

        Dim Funtion As CompiledFunction(Of String)
        Funtion = New VBCompiledFunction(Of String)
        Funtion.AddParameter("x", "Exception")
        Funtion.AddParameter("y", "Exception")
        Funtion.Code = "x/y"
        Try
            Funtion.CompileAsExpression()
            Assert.Fail()
        Catch ex As CompileException

            Dim list As CompilerError()
            ReDim list(Funtion.CompilerErrors.Count - 1)
            Funtion.CompilerErrors.CopyTo(list, 0)
            Dim errores = list.Where(Function(ce) Not ce.IsWarning)
            Dim warnings = list.Where(Function(ce) ce.IsWarning)
            Assert.IsTrue(warnings.Count = 0)
            Assert.IsTrue(errores.Count = 1)

        Catch ex As Exception
            Assert.Fail()
        End Try



    End Sub

End Class
