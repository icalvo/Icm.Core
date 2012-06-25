Imports System.IO

Namespace Icm.IO

    ''' <summary>
    '''   TextWriter that does do nothing (similar to /dev/null)
    ''' </summary>
    ''' <remarks>
    '''   Useful when someone needs a TextWriter object (and does not like Nothing),
    ''' but we don't want to write anything.
    ''' </remarks>
    ''' <history>
    ''' 	[icalvo]	02/12/2005	Created
    '''     [icalvo]    05/04/2005  Documentation
    ''' </history>
    Public Class NullWriter
        Inherits TextWriter

        Protected Sub New()

        End Sub

        Protected Sub New(ByVal formatProvider As IFormatProvider)
            MyBase.New(formatProvider)
        End Sub

        Public Overrides ReadOnly Property Encoding() As System.Text.Encoding
            Get
                Return System.Text.Encoding.Default
            End Get
        End Property
    End Class

End Namespace
