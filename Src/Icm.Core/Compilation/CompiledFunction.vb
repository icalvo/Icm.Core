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
        Implements IDisposable

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared ReadOnly _instances As New Dictionary(Of String, Object)()

        Protected Property CompilerParameters As New CompilerParameters
        Protected Property CodeProvider As CodeDomProvider
        Protected ReadOnly Namespaces As New List(Of String)()

        Private _execInstance As Object
        Private _MethodInfo As MethodInfo

        Private ReadOnly _parameters As New List(Of CompiledParameterInfo)
        Private ReadOnly _compilerErrors As New List(Of CompilerError)

        ReadOnly Property Parameters As List(Of CompiledParameterInfo)
            Get
                Return _parameters
            End Get
        End Property

        Property Code As String

        Public Shared Function CreateCompiledFunction(ByVal lang As String) As CompiledFunction(Of T)
            Select Case lang
                Case "VisualBasic"
                    Return New VBCompiledFunction(Of T)
                Case Else
                    Throw New ArgumentException(String.Format("Language {0} not supported", lang))
            End Select
        End Function

        Public Sub AddParameter(Of TParam)(ByVal n As String)
            Parameters.Add(New CompiledParameterInfo(Of TParam)(n))
        End Sub

        Public Sub AddNamespace(ByVal nspace As String, ByVal assm As String)
            If Not CompilerParameters.ReferencedAssemblies.Contains(assm) Then
                CompilerParameters.ReferencedAssemblies.Add(assm)
            End If
            Namespaces.Add(nspace)

        End Sub

        Public MustOverride Function GeneratedCode() As String

        ''' <summary>
        '''  Code is used as an VB expression that the compiled function will
        ''' directly return.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub CompileAsExpression()
            Dim compilerResults As CompilerResults
            Dim assembly As Assembly
            Dim type As Type
            Dim code As String

            code = GeneratedCode()

            CompilerErrors.Clear()

            If _instances.ContainsKey(Code) Then
                _execInstance = _instances(Code)
                type = _execInstance.GetType
                _MethodInfo = type.GetMethod("EvaluateIt")
            Else

                ' Compile and get results 
                compilerResults = CodeProvider.CompileAssemblyFromSource(CompilerParameters, code)

                ' Check for compile time errors 
                If compilerResults.Errors.Count <> 0 Then
                    For Each compileError As CompilerError In compilerResults.Errors
                        CompilerErrors.Add(compileError)
                    Next

                    Throw New CompileException(compilerResults.Errors, Nothing)
                Else
                    ' No Errors On Compile, so continue to process...

                    assembly = compilerResults.CompiledAssembly
                    _execInstance = assembly.CreateInstance("dValuate.EvalRunTime")
                    _instances.Add(Code, _execInstance)
                    type = _execInstance.GetType
                    _MethodInfo = type.GetMethod("EvaluateIt")
                End If
            End If
        End Sub

        ''' <summary>
        ''' Compilation errors.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property CompilerErrors As List(Of CompilerError)
            Get
                Return _compilerErrors
            End Get
        End Property

        ''' <summary>
        ''' Evaluate the compiled function.
        ''' </summary>
        ''' <param name="args"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' You must call CompileAsExpression before.
        ''' </remarks>
        Public Function Evaluate(ByVal ParamArray args() As Object) As T
            Dim oRetObj As T

            Try
                oRetObj = CType(_MethodInfo.Invoke(_execInstance, args), T)
                Return oRetObj
            Catch ex As TargetInvocationException
                ' Runtime exception caused by the compiled code
                Debug.WriteLine(ex.InnerException.Message)
                Throw New InvalidOperationException(String.Format("{0}: {1}", Code, ex.InnerException.Message), ex.InnerException)
            Catch ex As Exception
                ' Some other weird error 
                Debug.WriteLine(ex.Message)
                Throw ex
            End Try

            Return oRetObj
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If CodeProvider IsNot Nothing Then
                    CodeProvider.Dispose()
                    CodeProvider = Nothing
                End If
            End If
        End Sub

        Protected Overrides Sub Finalize()
            Dispose(False)
        End Sub

    End Class

End Namespace