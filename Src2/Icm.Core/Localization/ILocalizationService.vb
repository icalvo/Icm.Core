Namespace Icm.Localization

    ''' <summary>
    ''' Represents a translation service for a given language.
    ''' </summary>
    ''' <remarks>The fact that the language is specified allows for very short translation functions, that are provided in the <see cref="ILocalizationServiceExtensions"></see> module.</remarks>
    Public Interface ILocalizationService

        ReadOnly Property Repository As ILocalizationRepository
        ReadOnly Property Lcid As Integer

    End Interface
End Namespace