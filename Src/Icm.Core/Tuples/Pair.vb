Namespace Icm

    ''' <summary>
    ''' Modifiable Pair with default constructor.
    ''' </summary>
    ''' <typeparam name="T1"></typeparam>
    ''' <typeparam name="T2"></typeparam>
    ''' <remarks>
    ''' Tuple has no default constructor and the properties of KeyValuePair are read-only.
    ''' </remarks>
    Public Class Pair(Of T1, T2)

        Public Sub New()

        End Sub

        Public Sub New(ByVal f As T1, ByVal s As T2)
            First = f
            Second = s
        End Sub

        Property First() As T1

        Property Second() As T2
    End Class
End Namespace