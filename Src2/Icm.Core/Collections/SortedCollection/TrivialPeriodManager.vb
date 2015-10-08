Namespace Icm.Collections.Generic

    Public Class TrivialPeriodManager(Of T As IComparable(Of T))
        Implements IPeriodManager(Of T)

        Private torder_ As ITotalOrder(Of T)

        Public Sub New(ByVal torder As ITotalOrder(Of T))
            torder_ = torder
        End Sub

        Public Function Period(ByVal obj As T) As Integer Implements IPeriodManager(Of T).Period
            Return 1
        End Function

        Public Function PeriodStart(ByVal period As Integer) As T Implements IPeriodManager(Of T).PeriodStart
            Return torder_.Least
        End Function
    End Class
End Namespace
