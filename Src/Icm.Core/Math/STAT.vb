Namespace Icm.MathTools
    ''' <summary>
    '''  Module for easy statistical data generation. Not safe for multithreading,
    ''' though, you should use one StatisticsGenerator for each thread in order to
    ''' avoid problems.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module STAT
        Private shrng As New Random

        Sub ChangeSeed(ByVal newseed As Integer)
            shrng = New Random(newseed)
        End Sub

        Sub New()
            shrng = New Random
        End Sub

        Function Uniform01() As Double

            Dim result As Double = shrng.NextDouble()

            Return result
        End Function

        Function Uniform(ByVal min As Double, ByVal max As Double) As Double
            Return Uniform01() * (max - min) + min
        End Function

    End Module
End Namespace
