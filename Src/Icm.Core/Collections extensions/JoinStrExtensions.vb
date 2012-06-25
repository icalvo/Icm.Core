Imports System.Text
Imports System.Runtime.CompilerServices

Namespace Icm.Collections
    Public Module JoinStrExtensions

        ''' <summary>
        ''' String join function that accepts a ParamArray of strings.
        ''' </summary>
        ''' <param name="separator">Regular separator, separates words except the two last ones.</param>
        ''' <param name="finalSeparator">Final separator, separates the two last ones.</param>
        ''' <param name="strs"></param>
        ''' <returns></returns>
        ''' <remarks>Ignores null and empty strings.</remarks>
        <Extension()>
        Public Function JoinStr(strs As IEnumerable(Of String), ByVal separator As String, ByVal finalSeparator As String) As String
            Dim sb As New StringBuilder
            Dim i As Integer = strs.Count

            Dim nonEmpty = strs.Where(Function(s) Not String.IsNullOrEmpty(s))

            If nonEmpty.Count >= 1 Then
                sb.Append(nonEmpty(0))
            End If

            For i = 1 To nonEmpty.Count - 2
                sb.Append(separator & nonEmpty(i))
            Next

            If nonEmpty.Count >= 2 Then
                sb.Append(finalSeparator & nonEmpty(i))
            End If

            Return sb.ToString
        End Function


        ''' <summary>
        '''   Usual string join, with separator.
        ''' </summary>
        ''' <param name="separator">Separator string</param>
        ''' <returns>The joined string.</returns>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	19/08/2004	Created
        ''' 	[icalvo]	07/03/2006	Documented
        ''' </history>
        <Extension()> _
        Public Function JoinStr(ByVal l As IEnumerable(Of String), ByVal separator As String) As String
            Return String.Join(separator, l.ToArray)
        End Function

        ''' <summary>
        '''   Extended join, with separator and global prefix/suffix.
        ''' </summary>
        ''' <param name="globalprefix">Global prefix</param>
        ''' <param name="separator">Separator string</param>
        ''' <param name="globalsuffix">Global suffix</param>
        ''' <returns>The joined string.</returns>
        ''' <remarks>
        '''  <para>This method joins the collection with the separator and
        ''' (if the original array is not empty), the global prefix and suffix
        ''' are added.</para>
        ''' </remarks>
        ''' <example>
        '''   The following call will render a string in vector form, with
        ''' parentheses and comma-separated, BUT will return the empty string
        ''' if the collection has zero elements:
        ''' <code>
        '''     Dim myCollection As List(Of String)
        '''     Dim htmlList As String
        '''     ...
        '''     htmlList = myCollection.Join("(", ", ", ")")
        ''' </code>
        ''' </example>
        ''' <history>
        ''' 	[icalvo]	07/03/2006	Created
        ''' 	[icalvo]	07/03/2006	Documented
        ''' </history>
        <Extension()> _
        Public Function JoinStr(ByVal l As IEnumerable(Of String), ByVal globalprefix As String, ByVal separator As String, ByVal globalsuffix As String) As String
            If l.Count = 0 Then
                Return ""
            Else
                Return globalprefix & l.JoinStr(separator) & globalsuffix
            End If
        End Function

        ''' <summary>
        '''   Extended join, with item prefixes/suffixes, and global prefix/suffix.
        ''' </summary>
        ''' <param name="globalprefix">Global prefix</param>
        ''' <param name="itemprefix">Item prefix</param>
        ''' <param name="itemsuffix">Item suffix</param>
        ''' <param name="globalsuffix">Global suffix</param>
        ''' <returns>The joined string.</returns>
        ''' <remarks>
        '''  <para>This method is really a combination of a Map operation and a Join.</para>
        '''  <para>The Map operation substitutes each element <c>x</c> with:
        '''  <code>
        '''     itemprefix &amp; x &amp; itemsuffix
        '''  </code>
        ''' </para>
        ''' <para>Then, the resulting array is joined with the empty separator and
        ''' finally (if the original array is not empty), the global prefix and suffix
        ''' are added.</para>
        ''' </remarks>
        ''' <example>
        '''   The following call will convert a string collection into an HTML list,
        ''' but will return the empty string if the collection has zero elements:
        ''' <code>
        '''     Dim myCollection As List(Of String)
        '''     Dim htmlList As String
        '''     ...
        '''     htmlList = myCollection.Join("&lt;ul>", "&lt;li>", "&lt;/li>", "&lt;/ul>")
        ''' </code>
        '''   The following example generates a pretty printed text list:
        ''' <code>
        '''     Dim myCollection As List(Of String)
        '''     Dim textList As String
        '''     ...
        '''     textList = myCollection.Join("My list:" &amp; vbCrLf, " - ", vbCrLf, "")
        ''' </code>
        ''' </example>
        ''' <history>
        ''' 	[icalvo]	07/03/2006	Created
        ''' 	[icalvo]	07/03/2006	Documented
        '''     [icalvo]    17/03/2006  BUG: first itemprefix and last itemsuffix added
        ''' </history>
        <Extension()> _
        Public Function JoinStr(ByVal l As IEnumerable(Of String), ByVal globalprefix As String, ByVal itemprefix As String, ByVal itemsuffix As String, ByVal globalsuffix As String) As String
            If l.Count = 0 Then
                Return ""
            Else
                Return _
                    globalprefix & _
                    itemprefix & _
                    l.JoinStr(itemsuffix & itemprefix) & _
                    itemsuffix & _
                    globalsuffix
            End If
        End Function

        ''' <summary>
        '''   Extended join, with item prefixes/suffixes, separators and global prefix/suffix.
        ''' </summary>
        ''' <param name="globalprefix">Global prefix</param>
        ''' <param name="itemprefix">Item prefix</param>
        ''' <param name="separator">Separator</param>
        ''' <param name="itemsuffix">Item suffix</param>
        ''' <param name="globalsuffix">Global suffix</param>
        ''' <returns>The joined string.</returns>
        ''' <remarks>
        '''  <para>This method is really a combination of a Map operation and a Join.</para>
        '''  <para>The map operation substitutes each element <c>x</c> with:
        '''  <code>
        '''     itemprefix &amp; x &amp; itemsuffix
        '''  </code>
        ''' </para>
        ''' <para>Then, the resulting array is joined with the given separator and
        ''' finally (if the original array is not empty), the global prefix and suffix
        ''' are added.</para>
        ''' </remarks>
        ''' <example>
        '''   The following call will convert a string collection into an HTML list,
        ''' pretty printed,
        ''' but will return the empty string if the collection has zero elements:
        ''' <code>
        '''     Dim myCollection As List(Of String)
        '''     Dim htmlList As String
        '''     ...
        '''     htmlList = myCollection.Join("&lt;ul>", "&lt;li>", vbCrLf, "&lt;/li>", "&lt;/ul>")
        ''' </code>
        '''   The following example generates a parenthesized, comma-sepparated, quoted string list:
        ''' <code>
        '''     Dim myCollection As List(Of String)
        '''     Dim textList As String
        '''     ...
        '''     textList = myCollection.Join("(", "'", ", ", "'", ")")
        ''' </code>
        '''    It would return, for example, <c>('str1', 'str2', 'str3')</c>.
        ''' </example>
        ''' <history>
        ''' 	[icalvo]	17/03/2006	Created
        ''' 	[icalvo]	17/03/2006	Documented
        ''' </history>
        <Extension()> _
        Public Function JoinStr(ByVal l As IEnumerable(Of String), ByVal globalprefix As String, ByVal itemprefix As String, ByVal separator As String, ByVal itemsuffix As String, ByVal globalsuffix As String) As String
            If l.Count = 0 Then
                Return ""
            Else
                Return globalprefix & itemprefix & l.JoinStr(itemsuffix & separator & itemprefix) & itemsuffix & globalsuffix
            End If
        End Function


        ''' <summary>
        ''' String join extension function for lists of strings. It also
        ''' accepts a mapping function for the strings.
        ''' </summary>
        ''' <param name="l"></param>
        ''' <param name="sep"></param>
        ''' <param name="conv"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function JoinStr(Of T)(ByVal l As IEnumerable(Of T), ByVal sep As String, Optional ByVal conv As Func(Of T, String) = Nothing) As String
            Dim sb As New StringBuilder
            Dim i As Integer = 0
            Dim firstOne As Boolean = True
            If conv Is Nothing Then
                conv = Function(obj) obj.ToString
            End If
            Do
                If i > l.Count - 1 Then
                    Return sb.ToString
                ElseIf String.IsNullOrEmpty(conv(l(i))) Then
                    i += 1
                Else
                    If firstOne Then
                        sb.Append(conv(l(i)))
                        firstOne = False
                    Else
                        sb.Append(sep & conv(l(i)))
                    End If
                    i += 1
                End If
            Loop
        End Function




        ''' <summary>
        ''' String join extension function for collections of strings. It also
        ''' accepts a mapping function for the strings.
        ''' </summary>
        ''' <param name="l"></param>
        ''' <param name="sep"></param>
        ''' <param name="conv"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function JoinStr(Of T)(ByVal l As ICollection, ByVal sep As String, ByVal conv As Func(Of T, String)) As String
            Dim sb As New StringBuilder
            Dim i As Integer = 0

            Dim firstOne As Boolean = True
            Do
                Dim item = DirectCast(l(i), T)
                If i > l.Count - 1 Then
                    Return sb.ToString
                ElseIf String.IsNullOrEmpty(conv(item)) Then
                    i += 1
                Else
                    If firstOne Then
                        sb.Append(conv(item))
                        firstOne = False
                    Else
                        sb.Append(sep & conv(item))
                    End If
                    i += 1
                End If
            Loop
        End Function


        ''' <summary>
        '''  Joins a collection of String-convertible objects with a given separator.
        ''' </summary>
        ''' <param name="col">Collection of String-convertible objects.</param>
        ''' <param name="separator">Separator</param>
        ''' <returns>New string created by joining col with the separator.</returns>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	19/08/2004	Created
        '''     [icalvo]    31/03/2005  Documented
        '''     [icalvo]    31/03/2005  o declaration integrated in For
        ''' </history>
        <Extension()>
        Public Function JoinStrCol(ByVal col As ICollection, ByVal separator As String) As String
            If col Is Nothing Then
                Return ""
            End If

            Dim a(col.Count - 1) As String
            Dim i As Integer = 0

            For Each o As Object In col
                a(i) = o.ToString()
                i += 1
            Next
            Return String.Join(separator, a)
        End Function
    End Module

End Namespace
