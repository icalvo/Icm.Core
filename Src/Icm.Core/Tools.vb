Imports System.Runtime.CompilerServices
Imports System.Text

Namespace Icm

    Public Module Tools


        ''' <summary>
        ''' String join function that accepts a ParamArray of strings.
        ''' </summary>
        ''' <param name="sep"></param>
        ''' <param name="strs"></param>
        ''' <returns></returns>
        ''' <remarks>Ignores null and empty strings.</remarks>
        Public Function JoinStr(ByVal sep As String, ByVal ParamArray strs() As String) As String
            Dim sb As New StringBuilder
            Dim i As Integer = LBound(strs)
            Dim firstOne As Boolean = True
            Do
                If i > UBound(strs) Then
                    Return sb.ToString
                ElseIf String.IsNullOrEmpty(strs(i)) Then
                    i += 1
                Else
                    If firstOne Then
                        sb.Append(strs(i))
                        firstOne = False
                    Else
                        sb.Append(sep & strs(i))
                    End If
                    i += 1
                End If
            Loop
        End Function

    End Module

End Namespace