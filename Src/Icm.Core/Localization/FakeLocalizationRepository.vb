Imports System.Globalization

Namespace Icm.Localization
    Public Class FakeLocalizationRepository
        Implements ILocalizationRepository

        Private ReadOnly _store As IDictionary(Of String, String)

        Public Sub New(store As IDictionary(Of String, String))
            _store = store
        End Sub

        Default Public Overloads ReadOnly Property Item(lcid As Integer, key As String) As String Implements Localization.ILocalizationRepository.Item
            Get
                If Not _store.ContainsKey(key) Then Throw New ArgumentException("No existe clave " & key)
                Return _store(key)
            End Get
        End Property

        Default Public Overloads ReadOnly Property Item(key As String) As String Implements Localization.ILocalizationRepository.Item
            Get
                If Not _store.ContainsKey(key) Then Throw New ArgumentException("No existe clave " & key)
                Return _store(key)
            End Get
        End Property

    End Class

End Namespace
