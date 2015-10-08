Namespace Icm

    ''' <summary>
    ''' Dictionary whose lists of Keys and Values maintain the order of insertion.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <typeparam name="TValue"></typeparam>
    ''' <remarks></remarks>
    Public Class OrderedDictionary(Of TKey, TValue)
        Implements IDictionary(Of TKey, TValue)


        Private ReadOnly list_ As New List(Of TValue)()
        Private ReadOnly dictIndices_ As New Dictionary(Of TKey, Integer)()
        Private ReadOnly dict_ As New Dictionary(Of TKey, TValue)()
        Private isReadOnly_ As Boolean = False

        Private ReadOnly Property Coll As ICollection(Of KeyValuePair(Of TKey, TValue))
            Get
                Return dict_
            End Get
        End Property

        Public Sub SetReadOnly()
            isReadOnly_ = True
        End Sub

        Private Sub Add(ByVal item As KeyValuePair(Of TKey, TValue)) Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Add
            If isReadOnly_ Then
                Throw New NotSupportedException("Add is not supported on read-only dictionaries")
            End If
            Coll.Add(item)
            list_.Add(item.Value)
            dictIndices_.Add(item.Key, list_.Count - 1)
        End Sub

        Public Sub Clear() Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Clear
            If isReadOnly_ Then
                Throw New NotSupportedException("Clear is not supported on read-only dictionaries")
            End If
            dict_.Clear()
            list_.Clear()
            dictIndices_.Clear()
        End Sub

        Public Sub Append(ByVal other As OrderedDictionary(Of TKey, TValue))
            If isReadOnly_ Then
                Throw New NotSupportedException("Append is not supported on read-only dictionaries")
            End If
            For i = 0 To other.Count - 1
                Add(other.Keys(i), other.Values(i))
            Next
        End Sub

        Private Function Contains1(ByVal item As KeyValuePair(Of TKey, TValue)) As Boolean Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Contains
            Return Coll.Contains(item)
        End Function

        Private Sub CopyTo(ByVal array() As KeyValuePair(Of TKey, TValue), ByVal arrayIndex As Integer) Implements ICollection(Of KeyValuePair(Of TKey, TValue)).CopyTo
            Coll.CopyTo(array, arrayIndex)
        End Sub

        Public ReadOnly Property Count As Integer Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Count
            Get
                Return dict_.Count
            End Get
        End Property

        Private ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of KeyValuePair(Of TKey, TValue)).IsReadOnly
            Get
                Return isReadOnly_
            End Get
        End Property

        Private Function Remove1(ByVal item As KeyValuePair(Of TKey, TValue)) As Boolean Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Remove
            If isReadOnly_ Then
                Throw New NotSupportedException("Remove is not supported on read-only dictionaries")
            End If
            If Coll.Remove(item) Then
                list_.RemoveAt(dictIndices_(item.Key))
                dictIndices_.Remove(item.Key)
                Return True
            Else
                Return False
            End If
        End Function

        Public Function ContainsKey(ByVal key As TKey) As Boolean Implements IDictionary(Of TKey, TValue).ContainsKey
            Return dict_.ContainsKey(key)
        End Function

        Default Public Overloads Property Item(ByVal key As TKey) As TValue Implements IDictionary(Of TKey, TValue).Item
            Get
                Return dict_(key)
            End Get
            Set(ByVal value As TValue)
                If isReadOnly_ Then
                    Throw New NotSupportedException("Item set is not supported on read-only dictionaries")
                End If
                dict_(key) = value
                list_(dictIndices_(key)) = value
            End Set
        End Property

        Public ReadOnly Property Keys As ICollection(Of TKey) Implements IDictionary(Of TKey, TValue).Keys
            Get
                Return dict_.Keys
            End Get
        End Property

        Public Function RemoveKey(ByVal key As TKey) As Boolean Implements IDictionary(Of TKey, TValue).Remove
            If isReadOnly_ Then
                Throw New NotSupportedException("RemoveKey is not supported on read-only dictionaries")
            End If
            If dict_.Remove(key) Then
                list_.RemoveAt(dictIndices_(key))
                dictIndices_.Remove(key)
                Return True
            Else
                Return False
            End If
        End Function

        Public Function TryGetValue(ByVal key As TKey, ByRef value As TValue) As Boolean Implements IDictionary(Of TKey, TValue).TryGetValue
            Return dict_.TryGetValue(key, value)
        End Function

        Public ReadOnly Property Values As ICollection(Of TValue) Implements IDictionary(Of TKey, TValue).Values
            Get
                Return list_
            End Get
        End Property

        Private Function GetEnumerator() As IEnumerator(Of KeyValuePair(Of TKey, TValue)) Implements IEnumerable(Of KeyValuePair(Of TKey, TValue)).GetEnumerator
            Return dict_.GetEnumerator
        End Function

        Private Function GetEnumerator2() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return dict_.GetEnumerator
        End Function

        Public Sub Add(ByVal key As TKey, ByVal value As TValue) Implements System.Collections.Generic.IDictionary(Of TKey, TValue).Add
            If isReadOnly_ Then
                Throw New NotSupportedException("Add is not supported on read-only dictionaries")
            End If
            dict_.Add(key, value)
            list_.Add(value)
            dictIndices_.Add(key, list_.Count - 1)
        End Sub
    End Class

End Namespace
