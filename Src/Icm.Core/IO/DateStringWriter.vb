Imports System.IO

Namespace Icm.IO

    Public Class DateStringWriter
        Inherits StringWriter

        Public Sub New()
            MyBase.New(CultureInfo.InvariantCulture)
        End Sub

        Public Overloads Overrides Sub WriteLine(ByVal value As String)
            MyBase.WriteLine("{0:dd/MM/yyyy HH:mm:ss} {1}", Now, value)
        End Sub

    End Class

End Namespace
