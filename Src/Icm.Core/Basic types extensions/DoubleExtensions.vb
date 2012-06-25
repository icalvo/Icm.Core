Imports System.Runtime.CompilerServices

Namespace Icm

    Public Module DoubleExtensions

        <Extension()> _
        Public Function ChangePrecision(ByVal num As Double, ByVal precision As Integer) As Double
            Dim result As Double
            result = num * (10 ^ precision)
            result = Math.Round(result)
            result = result / (10 ^ precision)

            Return result
        End Function

        ''' <summary>
        ''' Converts degrees to radians
        ''' </summary>
        ''' <param name="angle"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function Deg2Rad(ByVal angle As Double) As Double
            Deg2Rad = (angle * System.Math.PI) / 180
        End Function

        ''' <summary>
        ''' Converts radians to degrees
        ''' </summary>
        ''' <param name="radians"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function Rad2Deg(ByVal radians As Double) As Double
            Rad2Deg = (radians * 180) / System.Math.PI
        End Function

    End Module
End Namespace

