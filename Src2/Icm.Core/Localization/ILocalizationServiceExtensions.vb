Imports System.Runtime.CompilerServices

Namespace Icm.Localization
    Public Module ILocalizationServiceExtensions

        <Extension>
        Public Function TransAnd(locService As ILocalizationService, ParamArray args() As Object) As String
            Return New PhraseAnd(args).Translate(locService.Lcid, locService.Repository)
        End Function

        <Extension>
        Public Function TransF(locService As ILocalizationService, key As String, ParamArray args() As Object) As String
            Return New PhraseFormat(key, args).Translate(locService.Lcid, locService.Repository)
        End Function

        <Extension>
        Public Function Trans(locService As ILocalizationService, key As String) As String
            Return locService.Repository.ItemForCulture(locService.Lcid, key)
        End Function
    End Module
End Namespace
