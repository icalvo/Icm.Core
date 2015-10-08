Namespace Icm.Text.RegularExpressions

    Public Module UsualRegularExpressions
        Public Const regexReal As String = "[+-]?\d+([\.,]\d*)?"
        Public Const regexInteger As String = "[+-]?\d+"
        Public Const regexOctal As String = "[+-]?[0-7]+"
        Public Const regexHexadecimal As String = "[+-]?[0-9a-fA-F]+"

        Public Const DateOnlyValidator As String = "\d?\d/\d?\d/([1-9]\d)?\d\d"
        Public Const DateAndTimeValidator As String = "\d?\d/\d?\d/(\d\d)?\d\d\s+\d?\d:\d\d"
        Public Const TimeOnlyValidator As String = "\d?\d:\d\d"
        Public Const DateOrTimeValidator As String = "\d?\d/\d?\d/(\d\d)?\d\d(\s+\d?\d:\d\d)?"

        Public Const RealValidator As String = "^[+-]?\d+([.,]\d*)?$"
        Public Const IntegerValidator As String = "\d+"

    End Module


End Namespace

