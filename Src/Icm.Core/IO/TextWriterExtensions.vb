Imports System.IO
Imports System.Runtime.CompilerServices

Namespace Icm.IO

    Public Module TextWriterExtensions

        <Extension()>
        Sub WriteUnderline(ByVal tw As TextWriter, ByVal s As String)
            tw.WriteLine(s)
            tw.WriteLine(New String("-"c, s.Length))
        End Sub

    End Module

End Namespace
