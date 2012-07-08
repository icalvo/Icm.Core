Imports System.IO

Namespace Icm.IO

    ''' <summary>
    ''' PathManager is an enhanced manager for joining paths.
    ''' </summary>
    ''' <remarks>
    ''' <para>With the instance you can combine path chunks. You can pass to Combine()
    ''' more than two path chunks. Doesn't matter if the chunks end with the separator or
    ''' not, Combine will add the separator when necessary.</para>
    ''' <para>Combine returns String.Empty if no paths are provided.</para>
    ''' </remarks>
    Public Class PathManager

        Private Sub New(ByVal sep As Char)
            Separator = sep
        End Sub

        Property Separator() As Char

        Public Function Combine(ByVal ParamArray paths() As String) As String
            Dim pathResult As String = ""
            If paths Is Nothing OrElse paths.Length = 0 Then
                Return pathResult
            End If
            pathResult = paths(0)
            For i As Integer = 1 To paths.Length - 1
                If pathResult.EndsWith(Separator, StringComparison.Ordinal) Then
                    pathResult &= paths(i)
                Else
                    pathResult &= Separator & paths(i)
                End If
            Next
            Return pathResult
        End Function

    End Class

End Namespace
