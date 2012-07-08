Imports System.IO

Namespace Icm.IO
    ''' <summary>
    '''   Tools for managing files.
    ''' </summary>
    ''' <history>
    ''' 	[icalvo]	05/04/2005	Created for FormatFile
    ''' 	[icalvo]	05/04/2005	Documentation
    ''' </history>
    Public Module FileTools


        ''' <summary>
        '''  Uses a file as a format template for String.Format.
        ''' </summary>
        ''' <param name="templatefn">Name of the file that will be used as template.</param>
        ''' <param name="args">Arguments for the Format call.</param>
        ''' <returns>String with the formatted template.</returns>
        ''' <remarks>
        '''  This function makes a common task: it takes a file, reads it into a String,
        '''  and passes this String as the first argument of String.Format(t, args).
        '''  The remaining optional arguments of String.Format are taken directly.
        ''' </remarks>
        ''' <history>
        '''     [icalvo]    31/03/2005  Created
        ''' 	[icalvo]	05/04/2005	Removed from Icm.Tools
        ''' 	[icalvo]	05/04/2005	Documentation
        ''' </history>
        Public Function FormatFile(ByVal templatefn As String, ByVal ParamArray args() As Object) As String
            Dim sr = File.OpenText(templatefn)
            Return FormatFile(sr, args)
        End Function

        Public Function FormatFile(ByVal tr As TextReader, ByVal ParamArray args() As Object) As String
            Dim template = tr.ReadToEnd
            tr.Close()
            Return String.Format(CultureInfo.CurrentCulture, template, args)
        End Function

    End Module

End Namespace

