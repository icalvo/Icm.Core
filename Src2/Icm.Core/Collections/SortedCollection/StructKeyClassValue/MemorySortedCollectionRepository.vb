Imports Icm.Collections.Generic

Namespace Icm.Collections.Generic.StructKeyClassValue

    Public Class MemorySortedCollectionRepository(Of TKey As {Structure, IComparable(Of TKey)}, TValue)
        Implements ISortedCollectionRepository(Of TKey, TValue)

        Property List As New SortedList(Of TKey, TValue)

        Public Overridable Function GetRange(ByVal rangeStart As TKey, ByVal rangeEnd As TKey) As IEnumerable(Of Pair(Of TKey, TValue)) Implements ISortedCollectionRepository(Of TKey, TValue).GetRange
            Return List.Where(Function(kvp) rangeStart.CompareTo(kvp.Key) <= 0 AndAlso kvp.Key.CompareTo(rangeEnd) < 0) _
                    .Select(Function(kvp) New Pair(Of TKey, TValue)(kvp.Key, kvp.Value))
        End Function
        Public Overridable Sub Update(ByVal key As TKey, ByVal val As TValue) Implements ISortedCollectionRepository(Of TKey, TValue).Update
            List(key) = val
        End Sub

        Public Overridable Function GetNext(ByVal key As TKey) As TKey? Implements ISortedCollectionRepository(Of TKey, TValue).GetNext
            Return List.NextKey(key)
        End Function

        Public Function GetPrevious(ByVal key As TKey) As TKey? Implements ISortedCollectionRepository(Of TKey, TValue).GetPrevious
            Return List.PrevKey(key)
        End Function


        Public Sub Remove(ByVal key As TKey) Implements ISortedCollectionRepository(Of TKey, TValue).Remove
            List.Remove(key)
        End Sub


        Public Function Count() As Integer Implements ISortedCollectionRepository(Of TKey, TValue).Count
            Return List.Count
        End Function
    End Class
End Namespace

