Namespace Icm.Localization
    Public MustInherit Class Phrase
        Implements IPhrase

        Public MustOverride Function Translate(lcid As Integer, locRepo As ILocalizationRepository) As String Implements IPhrase.Translate

        Protected Shared Function TranslateObject(locRepo As ILocalizationRepository, obj As Object) As Object
            Dim phrase = TryCast(obj, IPhrase)
            If phrase Is Nothing Then
                Return obj
            Else
                Return phrase.Translate(locRepo)
            End If
        End Function


        Public Function Translate(locRepo As ILocalizationRepository) As String Implements IPhrase.Translate
            Dim lcid = CultureInfo.CurrentCulture.LCID
            Return Translate(lcid, locRepo)
        End Function
    End Class

End Namespace
