Imports System.CodeDom.Compiler
Imports System.Reflection

Namespace Icm.Compilation

    ''' <summary>
    ''' Compiles a function out of the source code.
    ''' </summary>
    ''' <typeparam name="T">Function return type</typeparam>
    ''' <remarks>
    ''' Use property <see cref="CompiledFunction.Code" /> to establish the code of
    ''' the function.
    ''' Use the method <see cref="CompiledFunction.AddParameter" />  to establish the
    ''' parameters of the function.
    ''' Use <see cref="CompiledFunction.CompileAsExpression" /> to compile 
    ''' a function that returns the given code. This method returns False if
    ''' the compilation fails. Use property <see cref="CompiledFunction.CompilerErrors" />
    ''' to obtain the compilation errors.
    ''' La función <see cref="CompiledFunction.Evaluate">CompileAsExpression</see> se utiliza para 
    ''' tomar la decisión, y se le deben pasar como argumentos los parámetros
    ''' previamente configurados.
    ''' </remarks>
    Public MustInherit Class CompiledFunction(Of T)

        Protected oCParams As New CompilerParameters
        Protected oCodeProvider As CodeDomProvider
        Protected oExecInstance As Object
        Protected oMethodInfo As MethodInfo

        Public ReadOnly Parameters As New List(Of CompiledParameterInfo)
        Public Code As String
        Public ReadOnly CompilerErrors As New List(Of System.CodeDom.Compiler.CompilerError)

        Public Shared Function CreateCompiledFunction(ByVal lang As String) As CompiledFunction(Of T)
            Select Case lang
                Case "VisualBasic"
                    Return New VBCompiledFunction(Of T)
                Case Else
                    Throw New ArgumentException(String.Format("Language {0} not supported", lang))
            End Select
        End Function

        Public Sub AddParameter(ByVal n As String, ByVal t As String)
            Parameters.Add(New CompiledParameterInfo(n, t))
        End Sub

        Public MustOverride Function GeneratedCode() As String

        Public Function Evaluate(ByVal ParamArray args() As Object) As T
            Dim oRetObj As T

            Try
                oRetObj = CType(oMethodInfo.Invoke(oExecInstance, args), T)
                Return oRetObj
            Catch ex As TargetInvocationException
                Debug.WriteLine(ex.InnerException.Message)
                Throw New InvalidOperationException(String.Format("{0}: {1}", Code, ex.InnerException.Message), ex.InnerException)
            Catch ex As Exception
                ' Compile Time Errors Are Caught Here
                ' Some other weird error 
                Debug.WriteLine(ex.Message)
                Throw ex
            End Try

            Return oRetObj

        End Function

        Private Shared ReadOnly instancias As New Dictionary(Of String, Object)()

        ''' <summary>
        '''  Code is used as an VB expression that the compiled function will
        ''' directly return.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub CompileAsExpression()
            Dim oCResults As CompilerResults
            Dim oAssy As Assembly
            Dim oType As Type

            Dim genCode As String

            genCode = GeneratedCode()

            CompilerErrors.Clear()

            If instancias.ContainsKey(Code) Then
                oExecInstance = instancias(Code)
                oType = oExecInstance.GetType
                oMethodInfo = oType.GetMethod("EvaluateIt")
            Else

                ' Compile and get results 
                oCResults = oCodeProvider.CompileAssemblyFromSource(oCParams, genCode)

                ' Check for compile time errors 
                If oCResults.Errors.Count <> 0 Then
                    For Each compileError As CompilerError In oCResults.Errors
                        CompilerErrors.Add(compileError)
                    Next

                    Throw New CompileException(oCResults.Errors, Nothing)
                Else
                    ' No Errors On Compile, so continue to process...

                    oAssy = oCResults.CompiledAssembly
                    oExecInstance = oAssy.CreateInstance("dValuate.EvalRunTime")
                    instancias.Add(Code, oExecInstance)
                    oType = oExecInstance.GetType
                    oMethodInfo = oType.GetMethod("EvaluateIt")
                End If
            End If


        End Sub

    End Class

End Namespace