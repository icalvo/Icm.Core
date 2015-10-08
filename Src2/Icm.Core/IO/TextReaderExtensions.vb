Imports System.Runtime.CompilerServices
Imports System.IO

Namespace Icm.IO

    Public Module TextReaderExtensions

        <Extension>
        Public Function Format(ByVal tr As TextReader, ByVal ParamArray args() As Object) As String
            Dim template = tr.ReadToEnd
            tr.Close()
            Return String.Format(CultureInfo.CurrentCulture, template, args)
        End Function

    End Module

End Namespace

