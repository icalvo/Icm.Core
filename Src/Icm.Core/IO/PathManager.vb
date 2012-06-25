Imports System.IO

Namespace Icm.IO

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' <para>Use: Get an singleton with PathManager.Get().
    ''' PathManager.Get(sep) gets an instance which uses sep as separator.
    ''' PathManager.Get() gets an instance with the separator of the current system.</para>
    ''' 
    ''' <para>With the instance you can combine path chunks. You can pass to Combine()
    ''' more than two path chunks.</para>
    ''' </remarks>
    Public Class PathManager

        Private Shared ReadOnly managers_ As New Dictionary(Of String, PathManager)()

        Public Shared Function [Get]() As PathManager
            Return [Get](Path.DirectorySeparatorChar)
        End Function

        Public Shared Function [Get](ByVal sep As Char) As PathManager
            Dim result As PathManager
            If Not managers_.ContainsKey(sep) Then
                result = New PathManager(sep)
                managers_.Add(sep, result)
            Else
                result = managers_(sep)
            End If
            Return result
        End Function

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
                If pathResult.EndsWith(Separator) Then
                    pathResult &= paths(i)
                Else
                    pathResult &= Separator & paths(i)
                End If
            Next
            Return pathResult
        End Function

    End Class

End Namespace
