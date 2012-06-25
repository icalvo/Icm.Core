Imports System.Runtime.CompilerServices
Imports System.Collections.Generic

Namespace Icm.Collections.Generic
    Public Module SortedListExtensions

        ''' <summary>
        ''' Enhanced "Item" method that prints the key in the exception message.
        ''' </summary>
        ''' <typeparam name="K"></typeparam>
        ''' <typeparam name="V"></typeparam>
        ''' <param name="dic"></param>
        ''' <param name="key"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function It(Of K, V)(ByVal dic As Dictionary(Of K, V), ByVal key As K) As V
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
        ''' <typeparam name="K">Type of keys</typeparam>
        ''' <typeparam name="V">Type of values</typeparam>
        ''' <param name="sl">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be sl.Count if the key is greater than the greatest key of the list.</remarks>
        <Extension()>
        Public Function KeyOrNext(Of K As Structure, V)(ByVal sl As SortedList(Of K, V), ByVal key As K) As K?
            Dim index As Integer

            index = sl.IndexOfKeyOrNext(key)

            If index < sl.Count Then
                Return sl.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Returns the next key. Never returns the same key if it already exists. Returns Nothing if there
        ''' isn't next key.
        ''' </summary>
        ''' <typeparam name="K"></typeparam>
        ''' <typeparam name="V"></typeparam>
        ''' <param name="sl"></param>
        ''' <param name="key"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function NextKey(Of K As Structure, V)(ByVal sl As SortedList(Of K, V), ByVal key As K) As K?
            Dim index As Integer
            index = sl.IndexOfNextKey(key)

            If index < sl.Count Then
                Return sl.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        <Extension()>
        Public Function NextKey2(Of K, V)(ByVal sl As SortedList(Of K, V), ByVal key As K) As Nullable2(Of K)
            Dim index As Integer
            index = sl.IndexOfNextKey(key)

            If index < sl.Count Then
                Return sl.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Returns the previous key. Never returns the same key if it already exists. Returns Nothing if there
        ''' isn't previous key.
        ''' </summary>
        ''' <typeparam name="K"></typeparam>
        ''' <typeparam name="V"></typeparam>
        ''' <param name="sl"></param>
        ''' <param name="key"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function PrevKey(Of K As Structure, V)(ByVal sl As SortedList(Of K, V), ByVal key As K) As K?
            Dim index As Integer
            index = sl.IndexOfPrevKey(key)

            If index >= 0 Then
                Return sl.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        <Extension()>
        Public Function PrevKey2(Of K, V)(ByVal sl As SortedList(Of K, V), ByVal key As K) As Nullable2(Of K)
            Dim index As Integer
            index = sl.IndexOfPrevKey(key)

            If index >= 0 Then
                Return sl.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the key or, if the key does not exist, the index of the former lesser key on the list.
        ''' </summary>
        ''' <typeparam name="K">Type of keys</typeparam>
        ''' <typeparam name="V">Type of values</typeparam>
        ''' <param name="sl">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be -1 if the key is less than the least key of the list.</remarks>
        <Extension()>
        Public Function KeyOrPrev(Of K As Structure, V)(ByVal sl As SortedList(Of K, V), ByVal key As K) As K?
            Dim index As Integer

            index = sl.IndexOfKeyOrPrev(key)

            If index >= 0 Then
                Return sl.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the index of the key or, if the key does not exist, the index in which the key would be inserted
        ''' if you add it to the list. It can also be interpreted as the index of the next greater or equal key on the list.
        ''' </summary>
        ''' <typeparam name="K">Type of keys</typeparam>
        ''' <typeparam name="V">Type of values</typeparam>
        ''' <param name="sl">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be sl.Count if the key is greater than the greatest key of the list.</remarks>
        <Extension()>
        Public Function IndexOfKeyOrNext(Of K, V)(ByVal sl As SortedList(Of K, V), ByVal key As K) As Integer
            Dim index As Integer

            index = sl.Keys.Search(key)

            If index >= 0 Then
                Return index
            Else
                Return (index Xor -1)
            End If
        End Function


        ''' <summary>
        '''  Returns the index of the next key. It can also be interpreted as the index of the next greater key on the list.
        ''' </summary>
        ''' <typeparam name="K">Type of keys</typeparam>
        ''' <typeparam name="V">Type of values</typeparam>
        ''' <param name="sl">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be sl.Count if the key is greater than the greatest key of the list.</remarks>
        <Extension()>
        Public Function IndexOfNextKey(Of K, V)(ByVal sl As SortedList(Of K, V), ByVal key As K) As Integer
            Dim index As Integer

            index = sl.Keys.Search(key)

            If index >= 0 Then
                Return index + 1
            Else
                Return (index Xor -1)
            End If
        End Function

        ''' <summary>
        '''  Returns the index of the key or, if the key does not exist, the index of the former lesser key on the list.
        ''' </summary>
        ''' <typeparam name="K">Type of keys</typeparam>
        ''' <typeparam name="V">Type of values</typeparam>
        ''' <param name="sl">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be -1 if the key is less than the least key of the list.</remarks>
        <Extension()>
        Public Function IndexOfKeyOrPrev(Of K, V)(ByVal sl As SortedList(Of K, V), ByVal key As K) As Integer
            Dim index As Integer

            index = sl.Keys.Search(key)

            If index >= 0 Then
                Return index
            Else
                Return (index Xor -1) - 1
            End If
        End Function

        ''' <summary>
        '''  Returns the index of the previous key. It can also be interpreted as the index of the former lesser key on the list.
        ''' </summary>
        ''' <typeparam name="K">Type of keys</typeparam>
        ''' <typeparam name="V">Type of values</typeparam>
        ''' <param name="sl">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be -1 if the key is less than the least key of the list.</remarks>
        <Extension()>
        Public Function IndexOfPrevKey(Of K, V)(ByVal sl As SortedList(Of K, V), ByVal key As K) As Integer
            Dim index As Integer

            index = sl.Keys.Search(key)

            If index >= 0 Then
                Return index - 1
            Else
                Return (index Xor -1) - 1
            End If
        End Function

        ''' <summary>
        '''  Returns the value at the key or, if the key does not exist, the value of the former element on the list.
        ''' </summary>
        ''' <typeparam name="K">Type of keys</typeparam>
        ''' <typeparam name="V">Type of values</typeparam>
        ''' <param name="sl">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be Nothing if the key is less than the least key of the list.</remarks>
        <Extension()>
        Public Function ValueOfKeyOrPrev(Of K, V)(ByVal sl As SortedList(Of K, V), ByVal key As K) As V
            Dim index As Integer

            index = sl.IndexOfKeyOrPrev(key)

            If index >= 0 Then
                Return sl.Values(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the value at the key or, if the key does not exist, the value of the next element on the list.
        ''' </summary>
        ''' <typeparam name="K">Type of keys</typeparam>
        ''' <typeparam name="V">Type of values</typeparam>
        ''' <param name="sl">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be null (Nothing) if the key is greater than the greatest key of the list.</remarks>
        <Extension()>
        Public Function ValueOfKeyOrNext(Of K, V)(ByVal sl As SortedList(Of K, V), ByVal key As K) As V
            Dim index As Integer

            index = sl.IndexOfKeyOrNext(key)

            If index >= 0 Then
                Return sl.Values(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the key or, if the key does not exist, the index in which the key would be inserted
        ''' if you add it to the list. It can also be interpreted as the index of the next greater key on the list.
        ''' </summary>
        ''' <typeparam name="K">Type of keys</typeparam>
        ''' <typeparam name="V">Type of values</typeparam>
        ''' <param name="sl">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be sl.Count if the key is greater than the greatest key of the list.</remarks>
        <Extension()>
        Public Function KeyOrNext(Of K, V)(ByVal sl As SortedList(Of K, V), ByVal key As K, ByVal cond As Func(Of K, V, Boolean)) As K
            Dim index As Integer

            index = sl.IndexOfKeyOrNext(key)
            Do Until index >= sl.Count OrElse cond(sl.Keys(index), sl.Values(index))
                index += 1
            Loop

            If index >= 0 OrElse index <= sl.Count Then
                Return sl.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the key or, if the key does not exist, the index of the former lesser key on the list.
        ''' </summary>
        ''' <typeparam name="K">Type of keys</typeparam>
        ''' <typeparam name="V">Type of values</typeparam>
        ''' <param name="sl">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be -1 if the key is less than the least key of the list.</remarks>
        <Extension()>
        Public Function KeyOrPrev(Of K, V)(ByVal sl As SortedList(Of K, V), ByVal key As K, ByVal cond As Func(Of K, V, Boolean)) As K
            Dim index As Integer

            index = sl.IndexOfKeyOrPrev(key)
            Do Until index >= sl.Count OrElse cond(sl.Keys(index), sl.Values(index))
                index -= 1
            Loop

            If index >= 0 OrElse index <= sl.Count Then
                Return sl.Keys(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the index of the key or, if the key does not exist, the index in which the key would be inserted
        ''' if you add it to the list. It can also be interpreted as the index of the next greater key on the list.
        ''' </summary>
        ''' <typeparam name="K">Type of keys</typeparam>
        ''' <typeparam name="V">Type of values</typeparam>
        ''' <param name="sl">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be sl.Count if the key is greater than the greatest key of the list.</remarks>
        <Extension()>
        Public Function IndexOfKeyOrNext(Of K, V)(ByVal sl As SortedList(Of K, V), ByVal key As K, ByVal cond As Func(Of K, V, Boolean)) As Integer
            Dim index As Integer

            index = sl.IndexOfKeyOrNext(key)
            Do Until index >= sl.Count OrElse cond(sl.Keys(index), sl.Values(index))
                index += 1
            Loop

            If index >= 0 OrElse index <= sl.Count Then
                Return index
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the index of the key or, if the key does not exist, the index of the former lesser key on the list.
        ''' </summary>
        ''' <typeparam name="K">Type of keys</typeparam>
        ''' <typeparam name="V">Type of values</typeparam>
        ''' <param name="sl">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <returns></returns>
        ''' <remarks>Result will be -1 if the key is less than the least key of the list.</remarks>
        <Extension()>
        Public Function IndexOfKeyOrPrev(Of K, V)(ByVal sl As SortedList(Of K, V), ByVal key As K, ByVal cond As Func(Of K, V, Boolean)) As Integer
            Dim index As Integer

            index = sl.IndexOfKeyOrPrev(key)
            Do Until index >= sl.Count OrElse cond(sl.Keys(index), sl.Values(index))
                index -= 1
            Loop

            If index >= 0 OrElse index <= sl.Count Then
                Return index
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the value at the key or, if the key does not exist, the value of the former element on the list.
        ''' </summary>
        ''' <typeparam name="K">Type of keys</typeparam>
        ''' <typeparam name="V">Type of values</typeparam>
        ''' <param name="sl">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <param name="cond"></param>
        ''' <returns></returns>
        ''' <remarks>Result will be Nothing if the key is less than the least key of the list.</remarks>
        <Extension()>
        Public Function ValueOfKeyOrPrev(Of K, V)(ByVal sl As SortedList(Of K, V), ByVal key As K, ByVal cond As Func(Of K, V, Boolean)) As V
            Dim index As Integer

            index = sl.IndexOfKeyOrPrev(key)
            Do Until index >= sl.Count OrElse cond(sl.Keys(index), sl.Values(index))
                index -= 1
            Loop

            If index >= 0 OrElse index <= sl.Count Then
                Return sl.Values(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        '''  Returns the value at the key or, if the key does not exist, the value of the next element on the list.
        ''' </summary>
        ''' <typeparam name="K">Type of keys</typeparam>
        ''' <typeparam name="V">Type of values</typeparam>
        ''' <param name="sl">Sorted list</param>
        ''' <param name="key">Searched key</param>
        ''' <param name="cond"></param>
        ''' <returns></returns>
        ''' <remarks>Result will be null (Nothing) if the key is greater than the greatest key of the list.</remarks>
        <Extension()>
        Public Function ValueOfKeyOrNext(Of K, V)(ByVal sl As SortedList(Of K, V), ByVal key As K, ByVal cond As Func(Of K, V, Boolean)) As V
            Dim index As Integer

            index = sl.IndexOfKeyOrNext(key)
            Do Until index >= sl.Count OrElse cond(sl.Keys(index), sl.Values(index))
                index += 1
            Loop

            If index >= 0 OrElse index <= sl.Count Then
                Return sl.Values(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Adds or modifies a value associated with a key.
        ''' </summary>
        ''' <typeparam name="K"></typeparam>
        ''' <typeparam name="V"></typeparam>
        ''' <param name="sl"></param>
        ''' <param name="key"></param>
        ''' <param name="value"></param>
        ''' <remarks>
        ''' <para>Despite what is specified in IDictionary.Item, SortedList throws an exception if the key
        ''' does not exist, instead of adding a new element. So this function recovers the original
        ''' semantics.</para>
        ''' </remarks>
        <Extension()>
        Public Sub ForceAdd(Of K, V)(ByVal sl As SortedList(Of K, V), ByVal key As K, ByVal value As V)
            If Not sl.ContainsKey(key) Then
                sl.Add(key, value)
            Else
                sl(key) = value
            End If
        End Sub
    End Module
End Namespace
