Imports System.Runtime.Serialization

Namespace Icm.CommandLineTools
    Public Class UndefinedOptionException
        Inherits Exception

        Private _s As String

        Public Sub New()
            MyBase.New("Undefined option")
        End Sub

        Public Sub New(ByVal opt As String)
            MyBase.New("Undefined option " & opt)
            _s = opt
        End Sub

        Public Sub New(ByVal message As String, ByVal innerException As Exception)
            MyBase.New(message, innerException)

        End Sub

        Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
            MyBase.New(info, context)
        End Sub

    End Class

End Namespace
