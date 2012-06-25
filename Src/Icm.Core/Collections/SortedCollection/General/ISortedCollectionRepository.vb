Namespace Icm.Collections.Generic.General

    ''' <summary>
    ''' In order to use a RepositorySortedCollection, an implementation of this interface must be provided.
    ''' It is a simple CRUD repository, except for the reading methods.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <typeparam name="TValue"></typeparam>
    ''' <remarks></remarks>
    Public Interface ISortedCollectionRepository(Of TKey, TValue)

        Function GetRange(ByVal rangeStart As TKey, ByVal rangeEnd As TKey) As IEnumerable(Of Pair(Of TKey, TValue))
        Function GetNext(ByVal key As TKey) As Nullable2(Of TKey)
        Function GetPrevious(ByVal key As TKey) As Nullable2(Of TKey)

        Sub Add(ByVal key As TKey, ByVal val As TValue)
        Sub Update(ByVal key As TKey, ByVal val As TValue)
        Sub Remove(ByVal key As TKey)
        Function Count() As Integer
    End Interface

End Namespace
