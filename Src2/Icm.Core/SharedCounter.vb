
Namespace Icm
    ''' <summary>
    '''   Shared incremental counter.
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[icalvo]	15/09/2005	Created
    ''' 	[icalvo]	05/04/2005	Removed FormatFile from Icm.Tools
    '''     [icalvo]    05/04/2005  Documentation
    ''' </history>
    Public Class SharedCounter
        Private Sub New()
        End Sub

        ''' <summary>
        '''     Shared counter for providing incremental unique (per execution) numbers.
        ''' </summary>
        ''' <history>
        ''' 	[icalvo]	15/09/2005	Created
        '''     [icalvo]    05/04/2005  Documentation
        ''' </history>
        Private Shared counter_ As Integer

        ''' <summary>
        '''     New unique (per execution) number.
        ''' </summary>
        ''' <returns>A new unique (per execution) number.</returns>
        ''' <remarks>
        '''   Be careful; since the counter is shared, two consecutive calls to
        ''' NextNumber could not provide consecutive numbers. NextNumber is only
        ''' guaranteed to provide unique (not necessarily consecutive) numbers.
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	15/09/2005	Created
        '''     [icalvo]    05/04/2005  Documentation
        ''' </history>
        Public Shared Function NextNumber() As Integer
            counter_ += 1
            Return counter_
        End Function

    End Class
End Namespace
