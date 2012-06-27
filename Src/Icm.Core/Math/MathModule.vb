Imports System.Runtime.CompilerServices
Namespace Icm.MathTools

    Public Module MathModule

        ''' <summary>
        '''  Linearly interpolates the f(x_2) value given x_1, x_2, x_3,
        ''' f(x_1) and f(x_3)
        ''' </summary>
        ''' <param name="x1"></param>
        ''' <param name="x2"></param>
        ''' <param name="x3"></param>
        ''' <param name="fx1"></param>
        ''' <param name="fx3"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' <para>The function will also extrapolate if x_2 is outside
        ''' x_1 and x_3 range.</para>
        ''' <para>If x_1 = x_3, the functio will raise a <see cref="DivideByZeroException"></see>.</para>
        ''' </remarks>
        Function LinearInterpolate( _
            ByVal x1 As Double, _
            ByVal x2 As Double, _
            ByVal x3 As Double, _
            ByVal fx1 As Double, _
            ByVal fx3 As Double) As Double

            Return ((x2 - x1) * (fx3 - fx1) / (x3 - x1)) + fx1
        End Function

        ''' <summary>
        '''  Linearly interpolates the x_2 value given x_1, x_3,
        ''' f(x_1), f(x_2) and f(x_3)
        ''' </summary>
        ''' <param name="x1"></param>
        ''' <param name="x3"></param>
        ''' <param name="fx1"></param>
        ''' <param name="fx2"></param>
        ''' <param name="fx3"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' <para>The function will also extrapolate if f(x_2) is outside
        ''' f(x_1) and f(x_3) range.</para>
        ''' <para>If f(x_1) = f(x_3), the function will raise a <see cref="DivideByZeroException"></see>.</para>
        ''' </remarks>
        Function InverseLinearInterpolate( _
            ByVal x1 As Double, _
            ByVal x3 As Double, _
            ByVal fx1 As Double, _
            ByVal fx2 As Double, _
            ByVal fx3 As Double) As Double

            Return ((fx2 - fx1) * (x3 - x1) / (fx3 - fx1)) + x1
        End Function

        Function Max(ByVal a As Double, ByVal b As Double, ByVal c As Double) As Double
            Return Math.Max(a, Math.Max(b, c))
        End Function

        Function Min(ByVal a As Double, ByVal b As Double, ByVal c As Double) As Double
            Return Math.Min(a, Math.Min(b, c))
        End Function

        Function MaxN(ByVal ParamArray a() As Double) As Double
            Dim max As Double = Double.NegativeInfinity
            For Each d In a
                If d > max Then
                    max = d
                End If
            Next
            Return max
        End Function

        <Extension()>
        Function NearEqual(ByVal d1 As Double, ByVal d2 As Double, ByVal precission As Double) As Boolean
            Return d1 - d2 <= (10 ^ precission)
        End Function

        ''' <summary>
        '''  Linearly interpolates the f(x_2) value given x_1, x_2, x_3,
        ''' f(x_1) and f(x_3)
        ''' </summary>
        ''' <param name="x1"></param>
        ''' <param name="x2"></param>
        ''' <param name="x3"></param>
        ''' <param name="fx1"></param>
        ''' <param name="fx3"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' <para>The function will also extrapolate if x_2 is outside
        ''' x_1 and x_3 range.</para>
        ''' <para>If x_1 = x_3, the functio will raise a <see cref="DivideByZeroException"></see>.</para>
        ''' </remarks>
        Function LinearInterpolate( _
            ByVal x1 As Long, _
            ByVal x2 As Long, _
            ByVal x3 As Long, _
            ByVal fx1 As Long, _
            ByVal fx3 As Long) As Long

            Return ((x2 - x1) * (fx3 - fx1) \ (x3 - x1)) + fx1
        End Function

        ''' <summary>
        '''  Linearly interpolates the x_2 value given x_1, x_3,
        ''' f(x_1), f(x_2) and f(x_3)
        ''' </summary>
        ''' <param name="x1"></param>
        ''' <param name="x3"></param>
        ''' <param name="fx1"></param>
        ''' <param name="fx2"></param>
        ''' <param name="fx3"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' <para>The function will also extrapolate if f(x_2) is outside
        ''' f(x_1) and f(x_3) range.</para>
        ''' <para>If f(x_1) = f(x_3), the function will raise a <see cref="DivideByZeroException"></see>.</para>
        ''' </remarks>
        Function InverseLinearInterpolate( _
            ByVal x1 As Long, _
            ByVal x3 As Long, _
            ByVal fx1 As Long, _
            ByVal fx2 As Long, _
            ByVal fx3 As Long) As Long

            Return ((fx2 - fx1) * (x3 - x1) \ (fx3 - fx1)) + x1
        End Function

        Function Max(ByVal a As Long, ByVal b As Long, ByVal c As Long) As Long
            Return Math.Max(a, Math.Max(b, c))
        End Function

        Function Min(ByVal a As Long, ByVal b As Long, ByVal c As Long) As Long
            Return Math.Min(a, Math.Min(b, c))
        End Function

        Function MaxN(ByVal ParamArray a() As Long) As Long
            Dim max As Double = Double.NegativeInfinity
            For Each d In a
                If d > max Then
                    max = d
                End If
            Next
            Return CLng(max)
        End Function
    End Module

End Namespace
