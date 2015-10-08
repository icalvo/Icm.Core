Imports System.IO
Imports System.Text

Namespace Icm.IO

    ''' <summary>
    ''' </summary>
    ''' <remarks>
    ''' Since <see cref="Icm.Text.Replacer"></see> uses TextReaders as
    ''' input, and you usually want to perform replacements on in-memory strings or
    ''' files that you know the name or have a stream, these functions come handy
    ''' to obtain a corresponding TextReader.
    ''' </remarks>
    Public Class TextReaderFactory

        ''' <summary>
        ''' Get a TextReader given a string.
        ''' </summary>
        ''' <param name="str"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FromString(ByVal str As String) As StringReader
            Return New StringReader(str)
        End Function

        ''' <summary>
        ''' Get a TextReader given a file name.
        ''' </summary>
        ''' <param name="fnin"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FromFilename(ByVal fnin As String) As StreamReader
            Return FromStream(New FileStream(fnin, FileMode.Open, FileAccess.Read))
        End Function

        ''' <summary>
        ''' Get a TextReader given a stream.
        ''' </summary>
        ''' <param name="tr"></param>
        ''' <param name="enc"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FromStream(ByVal tr As Stream, Optional ByVal enc As Encoding = Nothing) As StreamReader
            Return New StreamReader(tr, If(enc, Encoding.UTF8))
        End Function

    End Class
End Namespace
