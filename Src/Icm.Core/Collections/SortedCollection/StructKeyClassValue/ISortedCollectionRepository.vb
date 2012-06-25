Namespace Icm.Collections.Generic.StructKeyClassValue

    Public Interface ISortedCollectionRepository(Of TKey As Structure, TValue)

        Function GetRange(ByVal rangeStart As TKey, ByVal rangeEnd As TKey) As IEnumerable(Of Pair(Of TKey, TValue))
        Sub Update(ByVal key As TKey, ByVal val As TValue)
        Function GetNext(ByVal key As TKey) As TKey?
        Function GetPrevious(ByVal key As TKey) As TKey?
        Sub Remove(ByVal key As TKey)
        Function Count() As Integer
    End Interface

End Namespace
