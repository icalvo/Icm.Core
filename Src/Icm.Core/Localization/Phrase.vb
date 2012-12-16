Imports System.Runtime.CompilerServices
Imports Icm.Collections

Namespace Icm.Localization

    Public MustInherit Class Phrase2
        Implements IPhrase

        Public MustOverride Function Translate(lcid As Integer, locRepo As ILocalizationRepository) As String Implements IPhrase.Translate

        Protected Shared Function TranslateObject(locRepo As ILocalizationRepository, trans As Object) As Object
            Dim translit = TryCast(trans, IPhrase)
            If translit Is Nothing Then
                Return trans
            Else
                Return translit.Translate(locRepo)
            End If
        End Function


        Public Function Translate(locRepo As ILocalizationRepository) As String Implements IPhrase.Translate
            Return Translate(3082, locRepo)
        End Function
    End Class

End Namespace