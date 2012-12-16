Imports System.Globalization

Namespace Icm.Localization

    Public Class DictionaryLocalizationRepository
        Inherits Dictionary(Of Tuple(Of String, Integer), String)
        Implements ILocalizationRepository

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(dict As Dictionary(Of Tuple(Of String, Integer), String))
            MyBase.New()

            For Each element In dict
                Me.Add(element.Key, element.Value)
            Next
        End Sub

        Public ReadOnly Property LocItem(ByVal key As String) As String Implements ILocalizationRepository.Item
            Get
                Return ItemForCulture(CultureInfo.CurrentCulture.LCID, key)
            End Get
        End Property

        ReadOnly Property ItemForCulture(ByVal lcid As Integer, ByVal key As String) As String Implements ILocalizationRepository.ItemForCulture
            Get
                Dim multkey = Tuple.Create(key, lcid)

                If ContainsKey(multkey) Then
                    Return Item(multkey)
                Else
                    Return Nothing
                End If
            End Get
        End Property

    End Class


End Namespace
