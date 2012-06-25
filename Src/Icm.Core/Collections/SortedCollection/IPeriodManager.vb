Namespace Icm.Collections.Generic

    ''' <summary>
    ''' Divides the domain of T into partitions each identified by an integer number.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks></remarks>
    Public Interface IPeriodManager(Of T)

        Function Period(ByVal obj As T) As Integer

        Function PeriodStart(ByVal period As Integer) As T

    End Interface
End Namespace