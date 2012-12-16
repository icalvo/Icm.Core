Namespace Icm.Localization

    ''' <summary>
    '''  Methods to manage localized strings.
    ''' </summary>
    Public Interface ILocalizationRepository

        Default ReadOnly Property Item(key As String) As String
        ReadOnly Property ItemForCulture(lcid As Integer, key As String) As String

    End Interface
End Namespace