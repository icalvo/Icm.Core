Imports System
Imports System.CodeDom.Compiler
Imports System.Runtime.Serialization

Namespace Icm.Compilation

    <Serializable()>
    Public Class CompileException
        Inherits Exception

        Public ReadOnly ErrorList As CompilerErrorCollection

        Public Sub New()
            MyBase.New("Error compiling expression")
            ErrorList = New CompilerErrorCollection
        End Sub

        Public Sub New(ByVal errorList As CompilerErrorCollection, ByVal inner As Exception)
            MyBase.New("Error compiling expression", inner)

            Me.ErrorList = errorList
        End Sub
        Public Sub New(ByVal message As String)
            MyBase.New(message)
            ErrorList = New CompilerErrorCollection
        End Sub
        Public Sub New(ByVal message As String, ByVal innerException As Exception)
            MyBase.New(message, innerException)
            ErrorList = New CompilerErrorCollection
        End Sub
        Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
            MyBase.New(info, context)
            ErrorList = New CompilerErrorCollection
        End Sub

    End Class

End Namespace