Imports System.Runtime.CompilerServices

Namespace Icm

    Public Module DoubleExtensions

        ''' <summary>
        ''' Changes precision of a double number.
        ''' </summary>
        ''' <param name="num"></param>
        ''' <param name="precision"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
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
        ''' <param name="angle">Angle in degrees</param>
        ''' <returns>Angle in radians</returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function Deg2Rad(ByVal angle As Double) As Double
            Deg2Rad = (angle * Math.PI) / 180
        End Function

        ''' <summary>
        ''' Converts radians to degrees
        ''' </summary>
        ''' <param name="angle">Angle in radians</param>
        ''' <returns>Angle in degrees</returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function Rad2Deg(ByVal angle As Double) As Double
            Rad2Deg = (angle * 180) / Math.PI
        End Function

    End Module
End Namespace

