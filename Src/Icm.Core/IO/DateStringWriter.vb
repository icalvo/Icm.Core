Imports System.IO

Namespace Icm.IO

    Public Class DateStringWriter
        Inherits StringWriter

        Public Overloads Overrides Sub WriteLine(ByVal s As String)
            Dim ahora As Date = Now
            MyBase.WriteLine("{0:dd/MM/yyyy HH:mm:ss} {1}", ahora, s)
        End Sub

    End Class

End Namespace
