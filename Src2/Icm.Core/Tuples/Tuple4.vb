Namespace Icm

    ''' <summary>
    ''' Modifiable Tuple with default constructor.
    ''' </summary>
    ''' <typeparam name="T1"></typeparam>
    ''' <typeparam name="T2"></typeparam>
    ''' <typeparam name="T3"></typeparam>
    ''' <typeparam name="T4"></typeparam>
    ''' <remarks>
    ''' System.Tuple has no default constructor and its properties are read-only.
    ''' </remarks>    
    Public Class Tuple4(Of T1, T2, T3, T4)

        Public Sub New(ByVal f As T1, ByVal s As T2, ByVal t As T3, ByVal fo As T4)
            First = f
            Second = s
            Third = t
            Fourth = fo
        End Sub

        Property First() As T1

        Property Second() As T2

        Property Third() As T3

        Property Fourth() As T4
    End Class

End Namespace