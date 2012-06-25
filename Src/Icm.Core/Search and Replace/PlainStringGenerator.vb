
Namespace Icm.Text
    Public Class PlainStringGenerator
        Inherits StringGenerator

        Private s_ As String

        Public Sub New(ByVal s As String)
            s_ = s
        End Sub

        Protected Sub New()

        End Sub


        Public Overrides Function Generate() As String
            Return s_
        End Function

    End Class
End Namespace
