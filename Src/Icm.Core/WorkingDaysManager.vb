Namespace Icm

    ''' <summary>
    '''     Working days manager.
    ''' </summary>
    ''' <remarks>
    '''  This class does calculations with working days. When the weekly holidays
    ''' and the holiday dates are properly configured, it can say whether a date
    ''' is working or not, find the next/previous working day and add/substract
    ''' working days to a given working day.
    ''' </remarks>
    ''' <history>
    ''' 	[icalvo]	20/04/2005	Created
    ''' </history>
    Public Class WorkingDaysManager
        Private ReadOnly dayHolidays_ As IEnumerable(Of Date)
        Private ReadOnly weeklyHolidays_ As IEnumerable(Of DayOfWeek)

        ''' <summary>
        '''  
        ''' </summary>
        ''' <remarks>
        ''' Stablishes saturdays and sundays as holidays.
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	20/04/2005	Created
        ''' </history>
        Public Sub New()
            dayHolidays_ = New List(Of Date)
            weeklyHolidays_ = New List(Of DayOfWeek) From {DayOfWeek.Saturday, DayOfWeek.Sunday}
        End Sub

        ''' <summary>
        '''  
        ''' </summary>
        ''' <remarks>
        ''' 
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	20/04/2005	Created
        ''' </history>
        Public Sub New(dayHol As IEnumerable(Of Date), weekHol As IEnumerable(Of DayOfWeek))
            dayHolidays_ = dayHol
            weeklyHolidays_ = weekHol
        End Sub
        ''' <summary>
        '''     List of holiday dates.
        ''' </summary>
        ''' <value>List of holiday dates.</value>
        ''' <remarks>
        '''   Should contains only Date-compatible items.
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	20/04/2005	Created
        ''' </history>
        ReadOnly Property DayHolidays() As IEnumerable(Of Date)
            Get
                Return dayHolidays_
            End Get
        End Property

        ''' <summary>
        '''     List of weekly holidays.
        ''' </summary>
        ''' <value>List of weekly holidays.</value>
        ''' <remarks>
        '''   Should contains only System.DayOfWeek items.
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	20/04/2005	Created
        ''' </history>
        ReadOnly Property WeeklyHolidays() As IEnumerable(Of DayOfWeek)
            Get
                Return weeklyHolidays_
            End Get
        End Property

        ''' <summary>
        ''' Is the given date a working day?
        ''' </summary>
        ''' <param name="d"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsWorking(ByVal d As Date) As Boolean
            Return Not dayHolidays_.Contains(d) And Not weeklyHolidays_.Contains(d.DayOfWeek)
        End Function

        ''' <summary>
        ''' Get the next working day greater OR EQUAL to the given date.
        ''' </summary>
        ''' <param name="d"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NextWorkingDay(ByVal d As Date) As Date
            Dim result As Date
            result = d

            ' Advance until we find a working day. Don't advance
            ' if d is already a working day.
            Do Until IsWorking(result)
                result = result.AddDays(1)
            Loop


            Return result

        End Function

        ''' <summary>
        ''' Get the previous working day less OR EQUAL to the given date.
        ''' </summary>
        ''' <param name="d">Day</param>
        ''' <returns></returns>
        ''' <remarks>
        '''  
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	30/05/2005	Created
        ''' </history>
        Public Function PrevWorkingDay(ByVal d As Date) As Date

            Dim result As Date
            result = d

            ' Advance until we find a working day. Don't advance
            ' if d is already a working day.
            Do Until IsWorking(result)
                result = result.AddDays(-1)
            Loop

            Return result

        End Function

        ''' <summary>
        '''  Adds/substracts working days to a given working day.
        ''' </summary>
        ''' <param name="d">A working day</param>
        ''' <param name="n"></param>
        ''' <returns></returns>
        ''' <remarks>
        '''   This method will raise a
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	30/05/2005	Created
        ''' </history>
        Public Function AddDays(ByVal d As Date, ByVal n As Integer) As Date
            Debug.Assert(IsWorking(d), "Cannot add/substract working days to a holiday date", d & " is a holiday date")

            Dim result As Date
            result = d

            ' Advance (in the direction of n) until we count n working days.
            Dim added As Integer = 0
            Do Until added = n
                result = result.AddDays(Math.Sign(n))
                If IsWorking(result) Then
                    added += Math.Sign(n)
                End If
            Loop

            Return result
        End Function

        ''' <summary>
        ''' Same as AddDays but admits holiday dates. In that case, it starts adding working dates to the working date next to the given holiday.
        ''' </summary>
        ''' <param name="d"></param>
        ''' <param name="n"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function AddDays2(ByVal d As Date, ByVal n As Integer) As Date
            If IsWorking(d) Then
                Return AddDays(d, n)
            Else
                Return AddDays(NextWorkingDay(d), n)
            End If
        End Function

    End Class

End Namespace
