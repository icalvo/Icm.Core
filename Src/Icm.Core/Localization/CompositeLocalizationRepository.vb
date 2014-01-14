Namespace Icm.Localization

    Public Class CompositeLocalizationRepository
        Implements ILocalizationRepository

        Private ReadOnly _subrepositories As IEnumerable(Of ILocalizationRepository)

        Public Sub New(subrepositories As IEnumerable(Of ILocalizationRepository))
            If subrepositories Is Nothing Then
                Throw New ArgumentNullException("subrepositories")
            End If

            If subrepositories.Count = 0 Then
                Throw New ArgumentException("subrepositories")
            End If

            _subrepositories = subrepositories.ToList()
        End Sub

        Public Sub New(ParamArray subrepositories() As ILocalizationRepository)
            If subrepositories Is Nothing Then
                Throw New ArgumentNullException("subrepositories")
            End If

            If subrepositories.Count = 0 Then
                Throw New ArgumentException("subrepositories")
            End If

            _subrepositories = subrepositories.ToList()
        End Sub

        Default Public ReadOnly Property ItemForCulture(lcid As Integer, key As String) As String Implements ILocalizationRepository.ItemForCulture
            Get
                For Each subrepository In _subrepositories
                    Dim result As String = subrepository.ItemForCulture(lcid, key)
                    If result IsNot Nothing Then
                        Return result
                    End If
                Next

                Return Nothing
            End Get
        End Property
    End Class

End Namespace

