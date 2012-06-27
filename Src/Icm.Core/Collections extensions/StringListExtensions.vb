Imports System.Runtime.CompilerServices
Imports System.Text

Namespace Icm.Collections

    ''' <summary>
    '''   String collection with extra features.
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[icalvo]	19/08/2004	Created
    ''' </history>
    Public Module StringListExtensions

        ''' <summary>
        '''     Appends a formatted string to the end of the collection,
        ''' which is built from a format string and its corresponding parameters.
        ''' </summary>
        ''' <param name="fmt"></param>
        ''' <param name="params"></param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	23/06/2005	Created
        '''     [icalvo]    07/03/2006  Documented
        ''' </history>
        <Extension()>
        Public Sub AppendFormat(ByVal l As IList(Of String), ByVal fmt As String, ByVal ParamArray params() As Object)
            l.Add(String.Format(fmt, params))
        End Sub

        ''' <summary>
        '''     Appends a formatted string to the end of the collection,
        ''' which is built from a format provider, a format string and its corresponding parameters.
        ''' </summary>
        ''' <param name="fp"></param>
        ''' <param name="fmt"></param>
        ''' <param name="params"></param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	23/06/2005	Created
        '''     [icalvo]    07/03/2006  Documented
        ''' </history>
        <Extension()>
        Public Sub AppendFormat(ByVal l As IList(Of String), ByVal fp As IFormatProvider, ByVal fmt As String, ByVal ParamArray params() As Object)
            l.Add(String.Format(fp, fmt, params))
        End Sub

        ''' <summary>
        '''     Prepends a formatted string to the start of the collection,
        ''' which is built from a format string and its corresponding parameters.
        ''' </summary>
        ''' <param name="fmt"></param>
        ''' <param name="params"></param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	23/06/2005	Created
        '''     [icalvo]    07/03/2006  Documented
        ''' </history>
        <Extension()>
        Public Sub PrependFormat(ByVal l As IList(Of String), ByVal fmt As String, ByVal ParamArray params() As Object)
            l.Insert(0, String.Format(fmt, params))
        End Sub

        ''' <summary>
        '''     Prepends a formatted string to the start of the collection,
        ''' which is built from a format provider, a format string and its corresponding parameters.
        ''' </summary>
        ''' <param name="fp"></param>
        ''' <param name="fmt"></param>
        ''' <param name="params"></param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	23/06/2005	Created
        '''     [icalvo]    07/03/2006  Documented
        ''' </history>
        <Extension()>
        Public Sub PrependFormat(ByVal l As IList(Of String), ByVal fp As IFormatProvider, ByVal fmt As String, ByVal ParamArray params() As Object)
            l.Insert(0, String.Format(fp, fmt, params))
        End Sub

        ''' <summary>
        '''     Inserts a formatted string on a given position of the collection,
        ''' which is built from a format string and its corresponding parameters.
        ''' </summary>
        ''' <param name="idx"></param>
        ''' <param name="fmt"></param>
        ''' <param name="params"></param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	23/06/2005	Created
        '''     [icalvo]    07/03/2006  Documented
        ''' </history>
        <Extension()>
        Public Sub InsertFormat(ByVal l As IList(Of String), ByVal idx As Integer, ByVal fmt As String, ByVal ParamArray params() As Object)
            l.Insert(idx, String.Format(fmt, params))
        End Sub

        ''' <summary>
        '''     Inserts a formatted string on a given position of the collection,
        ''' which is built from a format provider, a format string and its corresponding parameters.
        ''' </summary>
        ''' <param name="idx"></param>
        ''' <param name="fp"></param>
        ''' <param name="fmt"></param>
        ''' <param name="params"></param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	23/06/2005	Created
        '''     [icalvo]    07/03/2006  Documented
        ''' </history>
        <Extension()>
        Public Sub InsertFormat(ByVal l As IList(Of String), ByVal idx As Integer, ByVal fp As IFormatProvider, ByVal fmt As String, ByVal ParamArray params() As Object)
            l.Insert(idx, String.Format(fp, fmt, params))
        End Sub

    End Module

End Namespace
