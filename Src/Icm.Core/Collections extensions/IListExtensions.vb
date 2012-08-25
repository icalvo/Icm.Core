Imports System.Runtime.CompilerServices

Namespace Icm.Collections

    Public Module IListExtensions

        ''' <summary>
        ''' Clears the list and adds a given amount of new items (calling the empty constructor).
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="list"></param>
        ''' <param name="quantity"></param>
        ''' <remarks></remarks>
        <Extension()>
        Public Sub Initialize(Of T As New)(ByVal list As IList(Of T), ByVal quantity As Integer)
            list.Clear()
            For i As Integer = 1 To quantity
                list.Add(New T)
            Next
        End Sub

        ''' <summary>
        ''' Binary search over a range given by start index and length, with custom comparer.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="lst"></param>
        ''' <param name="index"></param>
        ''' <param name="length"></param>
        ''' <param name="value"></param>
        ''' <param name="comparer"></param>
        ''' <returns></returns>
        ''' <remarks>
        '''   <see cref="List(Of T)"></see> have a BinarySearch method but IList not. This
        ''' is an implementation for IList.
        ''' </remarks>
        <Extension()>
        Public Function Search(Of T)(ByVal lst As IList(Of T), ByVal index As Integer, ByVal length As Integer, ByVal value As T, ByVal comparer As IComparer(Of T)) As Integer
            If (comparer Is Nothing) Then
                comparer = System.Collections.Generic.Comparer(Of T).Default
            End If
            Dim num As Integer = index
            Dim num2 As Integer = ((index + length) - 1)
            Do While (num <= num2)
                Dim num3 As Integer = (num + ((num2 - num) >> 1))
                Dim cmp As Integer = comparer.Compare(lst(num3), value)
                If (cmp = 0) Then
                    Return num3
                End If
                If (cmp < 0) Then
                    num = (num3 + 1)
                Else
                    num2 = (num3 - 1)
                End If
            Loop
            Return Not num
        End Function

        ''' <summary>
        ''' Binary search over a range given by start index and length.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="lst"></param>
        ''' <param name="index"></param>
        ''' <param name="length"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        ''' <remarks>
        '''   <see cref="List(Of T)"></see> have a BinarySearch method but IList not. This
        ''' is an implementation for IList.
        ''' </remarks>
        <Extension()>
        Public Function Search(Of T)(ByVal lst As IList(Of T), ByVal index As Integer, ByVal length As Integer, ByVal value As T) As Integer
            Return lst.Search(index, length, value, Nothing)
        End Function

        ''' <summary>
        ''' Binary search.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="lst"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        ''' <remarks>
        '''   <see cref="List(Of T)"></see> have a BinarySearch method but IList not. This
        ''' is an implementation for IList.
        ''' </remarks>
        <Extension()>
        Public Function Search(Of T)(ByVal lst As IList(Of T), ByVal value As T) As Integer
            Return lst.Search(0, lst.Count, value, Nothing)
        End Function

        ''' <summary>
        ''' Binary search with custom comparer.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="lst"></param>
        ''' <param name="value"></param>
        ''' <param name="comparer"></param>
        ''' <returns></returns>
        ''' <remarks>
        '''   <see cref="List(Of T)"></see> have a BinarySearch method but IList not. This
        ''' is an implementation for IList.
        ''' </remarks>
        <Extension()>
        Public Function Search(Of T)(ByVal lst As IList(Of T), ByVal value As T, ByVal comparer As IComparer(Of T)) As Integer
            Return lst.Search(0, lst.Count, value, comparer)
        End Function

        ''' <summary>
        ''' </summary>
        ''' <param name="sc"></param>
        ''' <remarks>
        '''   <see cref="List(Of T)"></see> have an AddRange method but IList not. This
        ''' is an implementation for IList.
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	23/06/2005	Created
        '''     [icalvo]    07/03/2006  Documented
        ''' </history>
        <Extension()>
        Public Sub AddRange(Of T)(ByVal l As IList(Of T), ByVal sc As IEnumerable(Of T))
            For Each s In sc
                l.Add(s)
            Next
        End Sub

        ''' <summary>
        '''   This methods builds a new List containing a tail of the current IList.
        ''' </summary>
        ''' <param name="start"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	23/06/2005	Created
        '''     [icalvo]    07/03/2006  Documented
        ''' </history>
        <Extension()>
        Public Function Subrange(Of T)(ByVal l As IList(Of T), ByVal start As Integer) As IList(Of T)
            Dim r As New List(Of T)
            For i As Integer = start To l.Count - 1
                r.Add(l(i))
            Next
            Return r
        End Function

    End Module
End Namespace