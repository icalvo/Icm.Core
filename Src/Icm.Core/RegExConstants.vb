
Namespace Icm
    Public Class RegExConstants
        Private Sub New()
        End Sub

        Public Const DateOnlyValidator As String = "\d?\d/\d?\d/([1-9]\d)?\d\d"
        Public Const DateAndTimeValidator As String = "\d?\d/\d?\d/(\d\d)?\d\d\s+\d?\d:\d\d"
        Public Const TimeOnlyValidator As String = "\d?\d:\d\d"
        Public Const DateOrTimeValidator As String = "\d?\d/\d?\d/(\d\d)?\d\d(\s+\d?\d:\d\d)?"

        Public Const RealValidator As String = "^[+-]?\d+([.,]\d*)?$"
        Public Const IntegerValidator As String = "\d+"

    End Class
End Namespace
