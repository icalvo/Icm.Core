Imports System.Runtime.CompilerServices
Imports System.Text

Namespace Icm.Collections
    Public Module IEnumerableExtensions
        ''' <summary>
        ''' Transform an array of T1 into an array of T2. When an element of the
        ''' given array is not castable, it will be a null element in the returned one.
        ''' </summary>
        ''' <typeparam name="T1"></typeparam>
        ''' <typeparam name="T2"></typeparam>
        ''' <param name="e"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Function TryToCast(Of T1, T2 As T1)(ByVal e As IEnumerable(Of T1)) As IEnumerable(Of T2)
            Dim res As New List(Of T2)

            For Each v As T1 In e
                If TypeOf v Is T2 Then
                    res.Add(DirectCast(v, T2))
                Else
                    res.Add(Nothing)
                End If
            Next

            Return res
        End Function

        ''' <summary>
        ''' Minimum element or Nothing if the IEnumerable is empty.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="query"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function MinOrDefault(Of T As Structure)(ByVal query As IEnumerable(Of T)) As T?
            If query.Any Then
                Return query.Min
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Maximum element or Nothing if the IEnumerable is empty.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="query"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function MaxOrDefault(Of T As Structure)(ByVal query As IEnumerable(Of T)) As T?
            If query.Any Then
                Return query.Max
            Else
                Return Nothing
            End If
        End Function


        ''' <summary>
        ''' First item that reaches a minimum.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="list"></param>
        ''' <param name="eval"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' <para>The framework overloads of the function Min return the minimum value but not
        ''' the entity or entities that had the minimum. This function return the first
        ''' entity that carries the minimum value.</para>
        ''' <para>It does not also return the minimum value itself because it is easily
        ''' calculated with the supplied funtion. If the function is very resource intensive,
        ''' you can use MinPair.</para>
        ''' </remarks>
        <Extension()>
        Public Function MinEntity(Of T)(ByVal list As IEnumerable(Of T), ByVal eval As Func(Of T, Integer)) As T
            Dim minValue As Integer = Integer.MaxValue
            Dim result As T
            For Each el In list
                Dim minEl = eval(el)
                If minEl < minValue Then
                    result = el
                    minValue = minEl
                End If
            Next

            Return result
        End Function

        ''' <summary>
        ''' Pair composed by the first item that reaches a minimum, and the minimum itself.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="list"></param>
        ''' <param name="eval"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' <para>The framework overloads of the function Min return the minimum value but not
        ''' the entity or entities that had the minimum. This function return the first
        ''' entity that carries the minimum value, along with the minimum value.</para>
        ''' <para>The implementation is a little less efficient than MinEntity, because of
        ''' the maintenance of lists, so if the minimum function is not complicate (very
        ''' frequent), use MinEntity and then apply the minimum function on the returned
        ''' entity:
        ''' <code>
        ''' Dim minEntity As T
        ''' Dim minValue as Integer
        ''' 
        ''' ' Instead of:
        ''' Dim pair = list.MinEntityPair(AddressOf myMinFunction)
        ''' minEntity = pair.First()
        ''' minValue = pair.Second()
        ''' 
        ''' ' Do the following:
        ''' minEntity = list.MinEntity(AddressOf myMinFunction)
        ''' minValue = myMinFunction(minEntity)
        ''' </code>
        ''' </para>
        ''' </remarks>
        <Extension()>
        Public Function MinEntityPair(Of T)(ByVal list As IEnumerable(Of T), ByVal eval As Func(Of T, Integer)) As Pair(Of T, Integer)
            Dim minValue As Integer = Integer.MaxValue
            Dim result As T
            For Each el In list
                Dim minEl = eval(el)
                If minEl < minValue Then
                    result = el
                    minValue = minEl
                End If
            Next

            Return New Pair(Of T, Integer)(result, minValue)
        End Function

        ''' <summary>
        ''' Pair composed by a list with all the elements that reach the minimum, and the minimum itself.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="list"></param>
        ''' <param name="eval"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' <para>The framework overloads of the function Min return the minimum value but not
        ''' the entity or entities that had the minimum. This function return all the
        ''' entities that carry the minimum value, along with the minimum value.</para>
        ''' </remarks>
        <Extension()>
        Public Function MinListPair(Of T)(ByVal list As IEnumerable(Of T), ByVal eval As Func(Of T, Integer)) As ExtremeElements(Of T, Integer)
            Dim minValue As Integer = Integer.MaxValue
            Dim resultList As New List(Of T)
            For Each el In list
                Dim minEl = eval(el)
                If minEl < minValue Then
                    resultList.Clear()
                    resultList.Add(el)
                    minValue = minEl
                ElseIf minEl = minValue Then
                    resultList.Add(el)
                End If
            Next

            Return New ExtremeElements(Of T, Integer)(resultList, minValue)
        End Function

        ''' <summary>
        ''' First item that reaches a minimum.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="list"></param>
        ''' <param name="eval"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' <para>The framework overloads of the function Max return the maximum value but not
        ''' the entity or entities that had the maximum. This function return the first
        ''' entity that carries the maximum value.</para>
        ''' <para>It does not also return the maximum value itself because it is easily
        ''' calculated with the supplied funtion. If the function is very resource intensive,
        ''' you can use MaxPair.</para>
        ''' </remarks>
        <Extension()>
        Public Function MaxEntity(Of T)(ByVal list As IEnumerable(Of T), ByVal eval As Func(Of T, Integer)) As T
            Dim maxValue As Integer = Integer.MinValue
            Dim result As T
            For Each el In list
                Dim maxEl = eval(el)
                If maxEl > maxValue Then
                    result = el
                    maxValue = maxEl
                End If
            Next

            Return result
        End Function


        ''' <summary>
        ''' Pair composed by the first item that reaches a maximum, and the maximum itself.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="list"></param>
        ''' <param name="eval"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' <para>The .NET Framework overloads of the function Max return the maximum value but not
        ''' the entity or entities that had the maximum. This function return the first
        ''' entity that carries the maximum value, along with the maximum value.</para>
        ''' <para>The implementation is a little less efficient than MaxEntity, because of
        ''' the maintenance of lists, so if the maximum function is not complicate (very
        ''' frequent), use MaxEntity and then apply the maximum function on the returned
        ''' entity:
        ''' <code>
        ''' Dim maxEntity As T
        ''' Dim maxValue as Integer
        ''' 
        ''' ' Instead of:
        ''' Dim pair = MaxPair(list, AddressOf myMaxFunction)
        ''' maxEntity = pair.First()
        ''' maxValue = pair.Second()
        ''' 
        ''' ' Do the following:
        ''' maxEntity = MaxEntity(list, AddressOf myMaxFunction)
        ''' maxValue = myMaxFunction(maxEntity)
        ''' </code>
        ''' </para>
        ''' </remarks>
        <Extension()>
        Public Function MaxEntityPair(Of T)(ByVal list As IEnumerable(Of T), ByVal eval As Func(Of T, Integer)) As Pair(Of T, Integer)
            Dim maxValue As Integer = Integer.MinValue
            Dim result As T
            For Each el In list
                Dim maxEl = eval(el)
                If maxEl > maxValue Then
                    result = el
                    maxValue = maxEl
                End If
            Next

            Return New Pair(Of T, Integer)(result, maxValue)
        End Function

        ''' <summary>
        ''' Pair composed by a list with all the elements that reach the maximum, and the maximum itself.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="list"></param>
        ''' <param name="eval"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' <para>The .NET Framework overloads of the function Max return the maximum value but not
        ''' the entity or entities that had the maximum. This function return all the
        ''' entities that carries the maximum value, along with the maximum value.</para>
        ''' </remarks>
        <Extension()>
        Public Function MaxListPair(Of T)(ByVal list As IEnumerable(Of T), ByVal eval As Func(Of T, Integer)) As ExtremeElements(Of T, Integer)
            Dim maxValue As Integer = Integer.MinValue
            Dim resultList As New List(Of T)
            For Each el In list
                Dim maxEl = eval(el)
                If maxEl > maxValue Then
                    resultList.Clear()
                    resultList.Add(el)
                    maxValue = maxEl
                ElseIf maxEl = maxValue Then
                    resultList.Add(el)
                End If
            Next

            Return New ExtremeElements(Of T, Integer)(resultList, maxValue)
        End Function

    End Module
End Namespace