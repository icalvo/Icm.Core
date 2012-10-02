Imports System.Globalization

Namespace Icm.Localization

    ''' <summary>
    '''  Methods to manage localized strings.
    ''' </summary>
    Public Interface ILocalizationRepository
        Default ReadOnly Property Item(ByVal key As String) As String
        Default ReadOnly Property Item(ByVal lcid As Integer, ByVal key As String) As String
    End Interface
End Namespace