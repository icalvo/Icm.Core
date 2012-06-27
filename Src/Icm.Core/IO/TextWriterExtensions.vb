Imports System.IO
Imports System.Runtime.CompilerServices

Namespace Icm.IO

    Public Module TextWriterExtensions

        ''' <summary>
        ''' Writes underlined text.
        ''' </summary>
        ''' <param name="tw"></param>
        ''' <param name="s"></param>
        ''' <remarks></remarks>
        <Extension()>
        Sub WriteUnderline(ByVal tw As TextWriter, ByVal s As String)
            tw.WriteLine(s)
            tw.WriteLine(New String("-"c, s.Length))
        End Sub

    End Module

End Namespace
