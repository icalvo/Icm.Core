Imports System.IO
Imports System.Text

Namespace Icm.IO
    Public Class TextWriterFactory

        Public Shared Function FromFilename(ByVal fnout As String) As TextWriter
            Return FromStream(New FileStream(fnout, FileMode.Create, FileAccess.Write))
        End Function

        Public Shared Function FromStream(ByVal tw As Stream, Optional ByVal enc As Encoding = Nothing) As TextWriter
            Return New StreamWriter(tw, If(enc, Encoding.UTF8))
        End Function

        Public Shared Function FromBuilder(ByVal sb As StringBuilder) As TextWriter
            Return New StringWriter(sb)
        End Function

    End Class
End Namespace
