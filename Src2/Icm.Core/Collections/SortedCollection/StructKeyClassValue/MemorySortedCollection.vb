Imports Icm.Functions
Imports Icm.Collections.Generic

Namespace Icm.Collections.Generic.StructKeyClassValue
    ''' <summary>
    ''' In-memory storage sorted collection. Functionally it is equivalent to a
    ''' RepositorySortedCollection, using a MemorySortedCollectionRepository, and
    ''' a TrivialPeriodManager.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <typeparam name="TValue"></typeparam>
    ''' <remarks></remarks>
    Public Class MemorySortedCollection(Of TKey As {Structure, IComparable(Of TKey)}, TValue As Class)
        Inherits BaseSortedCollection(Of TKey, TValue)

        Private ReadOnly sl_ As New SortedList(Of TKey, TValue)()

        Public Sub New(ByVal otkey As ITotalOrder(Of TKey))
            MyBase.New(otkey)
        End Sub

        Public Overrides Function ContainsKey(ByVal key As TKey) As Boolean
            Return sl_.ContainsKey(key)
        End Function

        Public Overrides Function IntervalEnumerable(ByVal intf As Vector2(Of TKey?)) As System.Collections.Generic.IEnumerable(Of Vector2(Of Tuple(Of TKey, TValue)))

            Dim startKey, endKey As TKey

            startKey = TotalOrder.LstIfNull(intf.Item1)
            endKey = TotalOrder.GstIfNull(intf.Item2)

            Return sl_.Select(
                Function(kvp, idx) _
                    New Vector2(Of Tuple(Of TKey, TValue))( _
                        New Tuple(Of TKey, TValue)(kvp.Key, kvp.Value), _
                        New Tuple(Of TKey, TValue)(sl_.Keys(idx + 1), sl_(sl_.Keys(idx + 1))))) _
            .Where(Function(v2) v2.Item1.Item1.CompareTo(startKey) >= 0 AndAlso v2.Item2.Item1.CompareTo(endKey) <= 0)
        End Function

        Public Overrides Sub Add(ByVal key As TKey, ByVal value As TValue)
            sl_.Add(key, value)
        End Sub

        Default Public Overrides Property Item(ByVal key As TKey) As TValue
            Get
                Return sl_(key)
            End Get
            Set(ByVal value As TValue)
                sl_(key) = value
            End Set
        End Property

        Public Overrides Function KeyOrNext(ByVal key As TKey) As TKey?
            Return sl_.KeyOrNext(key)
        End Function

        Public Overrides Function KeyOrPrev(ByVal key As TKey) As TKey?
            Return sl_.KeyOrPrev(key)
        End Function

        Public Overrides Function [Next](ByVal key As TKey) As TKey?
            Return sl_.NextKey(key)
        End Function

        Public Overrides Function Previous(ByVal key As TKey) As TKey?
            Return sl_.PrevKey(key)
        End Function

        Public Overrides Sub Remove(ByVal key As TKey)
            sl_.Remove(key)
        End Sub


        Public Overrides Function Count() As Integer
            Return sl_.Count
        End Function

    End Class

End Namespace
