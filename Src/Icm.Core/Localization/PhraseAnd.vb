Imports Icm.Collections

Namespace Icm.Localization
    Public Class PhraseAnd
        Inherits Phrase

        Private _arguments As List(Of Object)

        ReadOnly Property Arguments As ICollection(Of Object)
            Get
                Return _arguments
            End Get
        End Property

        Public Sub New(ParamArray args() As Object)
            If args Is Nothing Then
                _arguments = New List(Of Object)()
            Else
                _arguments = New List(Of Object)(args)
            End If
        End Sub

        Public Sub New(args As IEnumerable(Of Object))
            If args Is Nothing Then
                _arguments = New List(Of Object)()
            Else
                _arguments = New List(Of Object)(args)
            End If
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
                Return args.Take(args.Count - 1).JoinStr(", ") & If(args.Count <= 1, "", If(args.Count = 2, " and ", ", and ") & args.Last)
            End If
            Return args.JoinStr(", ")
        End Function

    End Class

End Namespace
