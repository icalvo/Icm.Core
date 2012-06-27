Imports System

Namespace Icm.Compilation

    Public MustInherit Class CompiledParameterInfo

        Private _name As String

        Public Property Name() As String
            Get
                Return _name
            End Get
            Protected Set(ByVal value As String)
                _name = value
            End Set
        End Property

        MustOverride ReadOnly Property ArgType As String

    End Class

    Public Class CompiledParameterInfo(Of T)
        Inherits CompiledParameterInfo

        Private Shared ReadOnly _argType As String

        Shared Sub New()
            _argType = GetType(T).Name
        End Sub

        Public Sub New(ByVal name As String)
            Me.Name = name
        End Sub

        Public Overrides ReadOnly Property ArgType As String
            Get
                Return _argType
            End Get
        End Property
    End Class

End Namespace