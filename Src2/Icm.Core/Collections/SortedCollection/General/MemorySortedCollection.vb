Imports Icm.Functions
Imports Icm.Collections.Generic

Namespace Icm.Collections.Generic.General
    ''' <summary>
    ''' In-memory storage sorted collection. Functionally it is equivalent to a
    ''' RepositorySortedCollection, using a MemorySortedCollectionRepository, and
    ''' a TrivialPeriodManager.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <typeparam name="TValue"></typeparam>
    ''' <remarks></remarks>
    Public Class MemorySortedCollection(Of TKey As {Structure, IComparable(Of TKey)}, TValue As Structure)
        Inherits BaseSortedCollection(Of TKey, TValue)

        Private sl_ As New SortedList(Of TKey, TValue)

        Public Sub New(ByVal otkey As ITotalOrder(Of TKey))
            MyBase.New(otkey)
        End Sub

        Public Overrides Function ContainsKey(ByVal key As TKey) As Boolean
            Return sl_.ContainsKey(key)
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

        Public Overrides Function KeyOrNext(ByVal key As TKey) As Nullable2(Of TKey)
            Return sl_.KeyOrNext(key).ToNullable2
        End Function

        Public Overrides Function KeyOrPrev(ByVal key As TKey) As Nullable2(Of TKey)
            Return sl_.KeyOrPrev(key).ToNullable2
        End Function

        Public Overrides Function NextKey(ByVal key As TKey) As Nullable2(Of TKey)
            Return sl_.NextKey(key).ToNullable2
        End Function

        Public Overrides Function PreviousKey(ByVal key As TKey) As Nullable2(Of TKey)
            Return sl_.PrevKey(key).ToNullable2
        End Function

        Public Overrides Sub Remove(ByVal key As TKey)
            sl_.Remove(key)
        End Sub


        Public Overrides Function Count() As Integer
            Return sl_.Count
        End Function
    End Class

End Namespace
