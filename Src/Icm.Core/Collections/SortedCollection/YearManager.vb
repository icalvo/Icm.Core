Namespace Icm.Collections.Generic


    Public Class YearManager
        Implements IPeriodManager(Of Date)


        Public Function Period1(ByVal obj As Date) As Integer Implements IPeriodManager(Of Date).Period
            Return obj.Year
        End Function

        Public Function PeriodStart(ByVal period As Integer) As Date Implements IPeriodManager(Of Date).PeriodStart
            Return New Date(period, 1, 1)

        End Function
    End Class
End Namespace
