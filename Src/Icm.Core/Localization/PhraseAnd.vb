Imports Icm.Collections

Namespace Icm.Localization
    Public Class PhraseAnd
        Inherits Phrase

        Property Arguments As Object()

        Public Sub New(args() As Object)
            For Each o In args
                Arguments = args
            Next
        End Sub


        Public Overrides Function Translate(ByVal lcid As Integer, locRepo As ILocalizationRepository) As String
            Dim args = Arguments.Select(Function(trans) TranslateObject(locRepo, trans).ToString)

            If args.Count = 0 Then
                Return ""
            End If

            Dim primaryLang = lcid And &H3FF

            If primaryLang = 10 Then
                Return args.Take(args.Count - 1).JoinStr(", ") & If(args.Count <= 1, "", " y ") & args.Last
            ElseIf primaryLang = 22 Then
                Return args.Take(args.Count - 1).JoinStr(", ") & If(args.Count <= 1, "", " y ") & args.Last
            Else
                Return args.Take(args.Count - 1).JoinStr(", ") & If(args.Count <= 1, "", ", and ") & args.Last
            End If
            Return args.JoinStr(", ")
        End Function

    End Class

End Namespace
