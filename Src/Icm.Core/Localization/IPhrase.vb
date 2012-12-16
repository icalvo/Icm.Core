Imports System.Runtime.CompilerServices
Imports Icm.Collections

Namespace Icm.Localization
    Public Interface IPhrase
        Function Translate(ByVal locRepo As ILocalizationRepository) As String
        Function Translate(ByVal lcid As Integer, ByVal locRepo As ILocalizationRepository) As String
    End Interface

End Namespace
