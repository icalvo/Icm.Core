Imports System.Globalization

Namespace Icm.Localization

    Public Class ResourceLocalizationRepository
        Implements ILocalizationRepository

        Private ReadOnly _resourceManager As System.Resources.ResourceManager

        Public Sub New(rman As System.Resources.ResourceManager)
            _resourceManager = rman
        End Sub

        Default Public ReadOnly Property Item(key As String) As String Implements ILocalizationRepository.Item
            Get
                Return If(_resourceManager.GetString(key), key)
            End Get
        End Property

        Public ReadOnly Property ItemForCulture(lcid As Integer, key As String) As String Implements ILocalizationRepository.ItemForCulture
            Get
                Dim culture = CultureInfo.GetCultureInfo(lcid)
                Return If(_resourceManager.GetString(key, culture), key)
            End Get
        End Property
    End Class

End Namespace
