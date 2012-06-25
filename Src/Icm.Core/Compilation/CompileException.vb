Imports System
Imports System.CodeDom.Compiler

Namespace Icm.Compilation

    Public Class CompileException
        Inherits Exception

        Public ReadOnly ErrorList As CompilerErrorCollection

        Public Sub New(ByVal errorList As CompilerErrorCollection, ByVal inner As Exception)
            MyBase.New("Error compiling expression", inner)

            Me.ErrorList = errorList
        End Sub
    End Class

End Namespace