Namespace Icm.Localization
    Public Class DictionaryFixedCultureLocalizationRepository
        Inherits Dictionary(Of String, String)
        Implements ILocalizationRepository

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(dict As Dictionary(Of String, String))
            MyBase.New()

            For Each element In dict
                Add(element.Key, element.Value)
            Next
        End Sub

        ReadOnly Property ItemForCulture(ByVal lcid As Integer, ByVal key As String) As String Implements ILocalizationRepository.ItemForCulture
            Get
                If ContainsKey(key) Then
                    Return Item(key)
                Else
                    Return Nothing
                End If
            End Get
        End Property

    End Class

End Namespace
