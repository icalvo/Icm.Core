Namespace Icm.Localization

    Public Class DictionaryLocalizationRepository
        Inherits Dictionary(Of LocalizationKey, String)
        Implements ILocalizationRepository

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(dict As Dictionary(Of LocalizationKey, String))
            MyBase.New()

            For Each element In dict
                Me.Add(element.Key, element.Value)
            Next
        End Sub

        ReadOnly Property ItemForCulture(ByVal lcid As Integer, ByVal key As String) As String Implements ILocalizationRepository.ItemForCulture
            Get
                Dim multkey = New LocalizationKey(key, lcid)

                If ContainsKey(multkey) Then
                    Return Item(multkey)
                Else
                    Return Nothing
                End If
            End Get
        End Property

    End Class


End Namespace
