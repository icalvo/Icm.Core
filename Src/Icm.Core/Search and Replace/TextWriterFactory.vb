Imports System.IO
Imports System.Text

Namespace Icm.IO

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' Since <see cref="Icm.Text.Replacer"></see> uses TextWriters as
    ''' output, and you usually want to produce in-memory strings or
    ''' files that you know the name or have a stream, these functions come handy
    ''' to obtain a corresponding TextWriter.
    ''' </remarks>
    Public Class TextWriterFactory

        ''' <summary>
        ''' Get a TextWriter given a file name.
        ''' </summary>
        ''' <param name="fnout"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FromFilename(ByVal fnout As String) As TextWriter
            Return FromStream(New FileStream(fnout, FileMode.Create, FileAccess.Write))
        End Function

        ''' <summary>
        ''' Get a TextWriter given a stream.
        ''' </summary>
        ''' <param name="tw"></param>
        ''' <param name="enc"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FromStream(ByVal tw As Stream, Optional ByVal enc As Encoding = Nothing) As TextWriter
            Return New StreamWriter(tw, If(enc, Encoding.UTF8))
        End Function

        ''' <summary>
        ''' Get a TextWriter given a string builder.
        ''' </summary>
        ''' <param name="sb"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FromBuilder(ByVal sb As StringBuilder) As TextWriter
            Return New StringWriter(sb)
        End Function

    End Class
End Namespace
