Imports System.Runtime.CompilerServices

Namespace Icm.Localization

    Public Module ILocalizationRepositoryExtensions

        <Extension>
        Public Function Trans(locRepo As ILocalizationRepository, key As String) As String
            Return locRepo(CultureInfo.CurrentCulture.LCID, key)
        End Function

    End Module

End Namespace