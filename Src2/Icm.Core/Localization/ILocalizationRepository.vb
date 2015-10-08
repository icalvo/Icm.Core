Namespace Icm.Localization

    ''' <summary>
    '''  Methods to manage localized strings.
    ''' </summary>
    Public Interface ILocalizationRepository

        Default ReadOnly Property ItemForCulture(lcid As Integer, key As String) As String

    End Interface
End Namespace