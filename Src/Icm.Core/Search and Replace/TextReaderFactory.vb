Imports System.IO
Imports System.Text

Namespace Icm.IO
    Public Class TextReaderFactory

        Public Shared Function FromString(ByVal str As String) As TextReader
            Return New StringReader(str)
        End Function

        Public Shared Function FromFilename(ByVal fnin As String) As TextReader
            Return FromStream(New FileStream(fnin, FileMode.Open, FileAccess.Read))
        End Function

        Public Shared Function FromStream(ByVal tr As Stream, Optional ByVal enc As Encoding = Nothing) As TextReader
            Return New StreamReader(tr, If(enc, Encoding.UTF8))
        End Function

    End Class
End Namespace
