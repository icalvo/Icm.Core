Imports res = My.Resources.CommandLineTools

Namespace Icm.CommandLineTools

    Public Class ValuesStore

        Private ReadOnly _store As New Dictionary(Of String, List(Of String))()
        Private ReadOnly _mainStore As New List(Of String)()

        Public Sub Clear()
            _store.Clear()
            _mainStore.Clear()
        End Sub

        Public Function ContainsKey(ByVal s As String) As Boolean
            Return _store.ContainsKey(s)
        End Function

        Public Sub AddParameter(ByVal shortName As String, ByVal longName As String)
            If shortName Is Nothing Then
                Throw New ArgumentNullException("shortName")
            End If
            If longName Is Nothing Then
                Throw New ArgumentNullException("longName")
            End If
            If shortName.Length >= longName.Length Then
                Throw New ArgumentException(String.Format(res.S_ERR_SHORT_LONGER_THAN_LONG, shortName, longName))
            End If
            _store.Add(shortName, New List(Of String))
            _store.Add(longName, New List(Of String))
        End Sub

        Public Sub RemoveValues(ByVal shortName As String, ByVal longName As String)
            _store(shortName).Clear()
            _store(longName).Clear()
        End Sub

        Public Sub AddValue(ByVal shortName As String, ByVal longName As String, ByVal val As String)
            _store(shortName).Add(val)
            _store(longName).Add(val)
        End Sub

        Public Sub AddValues(ByVal shortName As String, ByVal longName As String, ByVal vals As IEnumerable(Of String))
            _store(shortName).AddRange(vals)
            _store(longName).AddRange(vals)
        End Sub

        Public Sub AddMainValue(ByVal val As String)
            _mainStore.Add(val)
        End Sub

        Public Sub AddMainValues(ByVal vals As IEnumerable(Of String))
            _mainStore.AddRange(vals)
        End Sub

        Public ReadOnly Property Values(ByVal s As String) As ICollection(Of String)
            Get
                If _store.ContainsKey(s) Then
                    Return _store(s).AsReadOnly
                Else
                    Return (New List(Of String)).AsReadOnly
                End If
            End Get
        End Property

        Public ReadOnly Property MainValues() As ICollection(Of String)
            Get
                Return _mainStore.AsReadOnly
            End Get
        End Property
    End Class
End Namespace
