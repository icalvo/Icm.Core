Imports System

Namespace Icm.Compilation

    Public Class CompiledParameterInfo
        Public ReadOnly Name As String
        Public ReadOnly ArgType As String

        Public Sub New(ByVal name As String, ByVal argType As String)
            Me.Name = name
            Me.ArgType = argType
        End Sub
    End Class

End Namespace