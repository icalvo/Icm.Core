Namespace Icm.Text

    Public Class AutoNumberGenerator
        Inherits StringGenerator
        Private counter_ As Integer
        Public Sub New()
            counter_ = 1
        End Sub
        Public Overrides Function Generate() As String
            counter_ = counter_ + 1
            Return CStr(counter_)
        End Function
    End Class

End Namespace
