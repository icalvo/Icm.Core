Imports System.Runtime.CompilerServices

Namespace Icm.Localization

    Public Module IPhraseExtensions

        <Extension>
        Public Function Translate(phrase As IPhrase, ByVal locRepo As ILocalizationRepository) As String
            Return phrase.Translate(CultureInfo.CurrentCulture.LCID, locRepo)
        End Function

    End Module

End Namespace