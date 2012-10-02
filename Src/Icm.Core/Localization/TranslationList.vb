Imports System.Runtime.CompilerServices
Imports Icm.Collections

Namespace Icm.Localization

    Public Class TranslationAnd
        Inherits Translation

        Property Arguments As Object()

        Public Sub New(args() As Object)
            For Each o In args
                Arguments = args
            Next
        End Sub


        Public Overrides Function Translate(ByVal lcid As Integer, locRepo As ILocalizationRepository) As String
            Dim args = Arguments.Select(Function(trans) TranslateObject(locRepo, trans).ToString)
            Dim primaryLang = lcid And &H3FF

            If primaryLang = 3082 Then

            Else
                Return args.JoinStr(", ")
            End If
        End Function

    End Class

End Namespace
