Imports System.Runtime.CompilerServices
Imports System.Collections.Generic

Namespace Icm.Collections.Generic
    Public Module SortedListExtensions

        ''' <summary>
        ''' Enhanced "Item" method that prints the key in the exception message.
        ''' </summary>
        ''' <typeparam name="TKey"></typeparam>
        ''' <typeparam name="TValue"></typeparam>
        ''' <param name="dic"></param>
        ''' <param name="key"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function It(Of TKey, TValue)(ByVal dic As Dictionary(Of TKey, TValue), ByVal key As TKey) As TValue
            If dic.ContainsKey(key) Then
                Return dic.Item(key)
            Else
                Throw New ArgumentException(String.Format("The dictionary does not contain key [{0}]", key))
            End If
        End Function

        ''' <summary>
        '''  Returns the key or, if the key does not exist, the index in which the key would be inserted
        ''' if you add it to the list. It can also be interpreted as the index of the next greater key on the list.
        ''' </summary>
        ''' <typeparam name="TKey">Type of keys</typeparam>
        ''' <typeparam name="TValue">Type of values</typeparam>
        ''' <param name="list">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be list.Count if the key is greater than the greatest key of the list.</remarks>
        <Extension()>
        Public Function KeyOrNext(Of TKey As Structure, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey) As TKey?
            Dim index As Integer

            index = list.IndexOfKeyOrNext(key)

            If index < list.Count Then
                Return list.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Returns the next key. Never returns the same key if it already exists. Returns Nothing if there
        ''' isn't next key.
        ''' </summary>
        ''' <typeparam name="TKey"></typeparam>
        ''' <typeparam name="TValue"></typeparam>
        ''' <param name="list"></param>
        ''' <param name="key"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function NextKey(Of TKey As Structure, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey) As TKey?
            Dim index As Integer
            index = list.IndexOfNextKey(key)

            If index < list.Count Then
                Return list.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        <Extension()>
        Public Function NextKey2(Of TKey, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey) As Nullable2(Of TKey)
            Dim index As Integer
            index = list.IndexOfNextKey(key)

            If index < list.Count Then
                Return list.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Returns the previous key. Never returns the same key if it already exists. Returns Nothing if there
        ''' isn't previous key.
        ''' </summary>
        ''' <typeparam name="TKey"></typeparam>
        ''' <typeparam name="TValue"></typeparam>
        ''' <param name="list"></param>
        ''' <param name="key"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function PrevKey(Of TKey As Structure, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey) As TKey?
            Dim index As Integer
            index = list.IndexOfPrevKey(key)

            If index >= 0 Then
                Return list.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Returns the previous key, but NOT the same key if it already exists. Returns Nothing if there
        ''' isn't previous key. Returns a <see cref="Nullable2(Of T)"></see>.
        ''' </summary>
        ''' <typeparam name="TKey"></typeparam>
        ''' <typeparam name="TValue"></typeparam>
        ''' <param name="list"></param>
        ''' <param name="key"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function PrevKey2(Of TKey, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey) As Nullable2(Of TKey)
            Dim index As Integer
            index = list.IndexOfPrevKey(key)

            If index >= 0 Then
                Return list.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the key or, if the key does not exist, the index of the former lesser key on the list.
        ''' </summary>
        ''' <typeparam name="TKey">Type of keys</typeparam>
        ''' <typeparam name="TValue">Type of values</typeparam>
        ''' <param name="list">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be -1 if the key is less than the least key of the list.</remarks>
        <Extension()>
        Public Function KeyOrPrev(Of TKey As Structure, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey) As TKey?
            Dim index As Integer

            index = list.IndexOfKeyOrPrev(key)

            If index >= 0 Then
                Return list.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the index of the key or, if the key does not exist, the index in which the key would be inserted
        ''' if you add it to the list. It can also be interpreted as the index of the next greater or equal key on the list.
        ''' </summary>
        ''' <typeparam name="TKey">Type of keys</typeparam>
        ''' <typeparam name="TValue">Type of values</typeparam>
        ''' <param name="list">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be list.Count if the key is greater than the greatest key of the list.</remarks>
        <Extension()>
        Public Function IndexOfKeyOrNext(Of TKey, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey) As Integer
            Dim index As Integer

            index = list.Keys.Search(key)

            If index >= 0 Then
                Return index
            Else
                Return (index Xor -1)
            End If
        End Function


        ''' <summary>
        '''  Returns the index of the next key. It can also be interpreted as the index of the next greater key on the list.
        ''' </summary>
        ''' <typeparam name="TKey">Type of keys</typeparam>
        ''' <typeparam name="TValue">Type of values</typeparam>
        ''' <param name="list">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be list.Count if the key is greater than the greatest key of the list.</remarks>
        <Extension()>
        Public Function IndexOfNextKey(Of TKey, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey) As Integer
            Dim index As Integer

            index = list.Keys.Search(key)

            If index >= 0 Then
                Return index + 1
            Else
                Return (index Xor -1)
            End If
        End Function

        ''' <summary>
        '''  Returns the index of the key or, if the key does not exist, the index of the former lesser key on the list.
        ''' </summary>
        ''' <typeparam name="TKey">Type of keys</typeparam>
        ''' <typeparam name="TValue">Type of values</typeparam>
        ''' <param name="list">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be -1 if the key is less than the least key of the list.</remarks>
        <Extension()>
        Public Function IndexOfKeyOrPrev(Of TKey, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey) As Integer
            Dim index As Integer

            index = list.Keys.Search(key)

            If index >= 0 Then
                Return index
            Else
                Return (index Xor -1) - 1
            End If
        End Function

        ''' <summary>
        '''  Returns the index of the previous key. It can also be interpreted as the index of the former lesser key on the list.
        ''' </summary>
        ''' <typeparam name="TKey">Type of keys</typeparam>
        ''' <typeparam name="TValue">Type of values</typeparam>
        ''' <param name="list">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be -1 if the key is less than the least key of the list.</remarks>
        <Extension()>
        Public Function IndexOfPrevKey(Of TKey, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey) As Integer
            Dim index As Integer

            index = list.Keys.Search(key)

            If index >= 0 Then
                Return index - 1
            Else
                Return (index Xor -1) - 1
            End If
        End Function

        ''' <summary>
        '''  Returns the value at the key or, if the key does not exist, the value of the former element on the list.
        ''' </summary>
        ''' <typeparam name="TKey">Type of keys</typeparam>
        ''' <typeparam name="TValue">Type of values</typeparam>
        ''' <param name="list">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be Nothing if the key is less than the least key of the list.</remarks>
        <Extension()>
        Public Function ValueOfKeyOrPrev(Of TKey, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey) As TValue
            Dim index As Integer

            index = list.IndexOfKeyOrPrev(key)

            If index >= 0 Then
                Return list.Values(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the value at the key or, if the key does not exist, the value of the next element on the list.
        ''' </summary>
        ''' <typeparam name="TKey">Type of keys</typeparam>
        ''' <typeparam name="TValue">Type of values</typeparam>
        ''' <param name="list">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be null (Nothing) if the key is greater than the greatest key of the list.</remarks>
        <Extension()>
        Public Function ValueOfKeyOrNext(Of TKey, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey) As TValue
            Dim index As Integer

            index = list.IndexOfKeyOrNext(key)

            If index >= 0 Then
                Return list.Values(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the key or, if the key does not exist, the index in which the key would be inserted
        ''' if you add it to the list. It can also be interpreted as the index of the next greater key on the list.
        ''' </summary>
        ''' <typeparam name="TKey">Type of keys</typeparam>
        ''' <typeparam name="TValue">Type of values</typeparam>
        ''' <param name="list">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be list.Count if the key is greater than the greatest key of the list.</remarks>
        <Extension()>
        Public Function KeyOrNext(Of TKey, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey, ByVal condition As Func(Of TKey, TValue, Boolean)) As TKey
            Dim index As Integer

            index = list.IndexOfKeyOrNext(key)
            Do Until index >= list.Count OrElse condition(list.Keys(index), list.Values(index))
                index += 1
            Loop

            If index >= 0 OrElse index <= list.Count Then
                Return list.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the key or, if the key does not exist, the index of the former lesser key on the list.
        ''' </summary>
        ''' <typeparam name="TKey">Type of keys</typeparam>
        ''' <typeparam name="TValue">Type of values</typeparam>
        ''' <param name="list">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be -1 if the key is less than the least key of the list.</remarks>
        <Extension()>
        Public Function KeyOrPrev(Of TKey, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey, ByVal condition As Func(Of TKey, TValue, Boolean)) As TKey
            Dim index As Integer

            index = list.IndexOfKeyOrPrev(key)
            Do Until index >= list.Count OrElse condition(list.Keys(index), list.Values(index))
                index -= 1
            Loop

            If index >= 0 OrElse index <= list.Count Then
                Return list.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the index of the key or, if the key does not exist, the index in which the key would be inserted
        ''' if you add it to the list. It can also be interpreted as the index of the next greater key on the list.
        ''' </summary>
        ''' <typeparam name="TKey">Type of keys</typeparam>
        ''' <typeparam name="TValue">Type of values</typeparam>
        ''' <param name="list">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be list.Count if the key is greater than the greatest key of the list.</remarks>
        <Extension()>
        Public Function IndexOfKeyOrNext(Of TKey, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey, ByVal condition As Func(Of TKey, TValue, Boolean)) As Integer
            Dim index As Integer

            index = list.IndexOfKeyOrNext(key)
            Do Until index >= list.Count OrElse condition(list.Keys(index), list.Values(index))
                index += 1
            Loop

            If index >= 0 OrElse index <= list.Count Then
                Return index
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the index of the key or, if the key does not exist, the index of the former lesser key on the list.
        ''' </summary>
        ''' <typeparam name="TKey">Type of keys</typeparam>
        ''' <typeparam name="TValue">Type of values</typeparam>
        ''' <param name="list">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be -1 if the key is less than the least key of the list.</remarks>
        <Extension()>
        Public Function IndexOfKeyOrPrev(Of TKey, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey, ByVal condition As Func(Of TKey, TValue, Boolean)) As Integer
            Dim index As Integer

            index = list.IndexOfKeyOrPrev(key)
            Do Until index >= list.Count OrElse condition(list.Keys(index), list.Values(index))
                index -= 1
            Loop

            If index >= 0 OrElse index <= list.Count Then
                Return index
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the value at the key or, if the key does not exist, the value of the former element on the list.
        ''' </summary>
        ''' <typeparam name="TKey">Type of keys</typeparam>
        ''' <typeparam name="TValue">Type of values</typeparam>
        ''' <param name="list">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <param name="condition"></param>
        ''' <returns></returns>
        ''' <remarks>Result will be Nothing if the key is less than the least key of the list.</remarks>
        <Extension()>
        Public Function ValueOfKeyOrPrev(Of TKey, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey, ByVal condition As Func(Of TKey, TValue, Boolean)) As TValue
            Dim index As Integer

            index = list.IndexOfKeyOrPrev(key)
            Do Until index >= list.Count OrElse condition(list.Keys(index), list.Values(index))
                index -= 1
            Loop

            If index >= 0 OrElse index <= list.Count Then
                Return list.Values(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the value at the key or, if the key does not exist, the value of the next element on the list.
        ''' </summary>
        ''' <typeparam name="TKey">Type of keys</typeparam>
        ''' <typeparam name="TValue">Type of values</typeparam>
        ''' <param name="list">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <param name="condition"></param>
        ''' <returns></returns>
        ''' <remarks>Result will be null (Nothing) if the key is greater than the greatest key of the list.</remarks>
        <Extension()>
        Public Function ValueOfKeyOrNext(Of TKey, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey, ByVal condition As Func(Of TKey, TValue, Boolean)) As TValue
            Dim index As Integer

            index = list.IndexOfKeyOrNext(key)
            Do Until index >= list.Count OrElse condition(list.Keys(index), list.Values(index))
                index += 1
            Loop

            If index >= 0 OrElse index <= list.Count Then
                Return list.Values(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Adds or modifies a value associated with a key.
        ''' </summary>
        ''' <typeparam name="TKey"></typeparam>
        ''' <typeparam name="TValue"></typeparam>
        ''' <param name="list"></param>
        ''' <param name="key"></param>
        ''' <param name="value"></param>
        ''' <remarks>
        ''' <para>Despite what is specified in IDictionary.Item, SortedList throws an exception if the key
        ''' does not exist, instead of adding a new element. So this function recovers the original
        ''' semantics.</para>
        ''' </remarks>
        <Extension()>
        Public Sub ForceAdd(Of TKey, TValue)(ByVal list As SortedList(Of TKey, TValue), ByVal key As TKey, ByVal value As TValue)
            If Not list.ContainsKey(key) Then
                list.Add(key, value)
            Else
                list(key) = value
            End If
        End Sub
    End Module
End Namespace
