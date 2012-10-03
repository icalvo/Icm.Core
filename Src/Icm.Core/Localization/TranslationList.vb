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
                Dim last = args.LastOrDefault
                If last IsNot Nothing Then
                    If last.ToString.ToLower.StartsWith("i") Then
                        Return args.JoinStr(", ", " y ")
                    ElseIf last.ToString.ToLower.StartsWith("hi") Then
                        Return args.JoinStr(", ", " y ")
                    End If
                End If
                Return args.JoinStr(", ", " y ")
            ElseIf primaryLang = 1033 Then
                Return args.JoinStr(", ", ", and ")
            ElseIf primaryLang = 2057 Then
                Return args.JoinStr(", ", ", and ")
            Else
                Return args.JoinStr(", ")
            End If
        End Function

    End Class

End Namespace
