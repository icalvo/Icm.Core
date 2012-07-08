' Floating Point Example.
' 
' Copyright 2005, Extreme Optimization. (http://www.extremeoptimization.com)
' All rights reserved.
' 
' Redistribution and use in source and binary forms, with or without modification, 
' are permitted provided that the following conditions are met:
'
'  * Redistributions of source code must retain the above copyright notice, 
'    this list of conditions and the following disclaimer. 
'  * Redistributions in binary form must reproduce the above copyright notice,
'    this list of conditions and the following disclaimer in the documentation
'    and/or other materials provided with the distribution. 
'  * Neither the name of Extreme Optimization nor the names of its contributors 
'    may be used to endorse or promote products derived from this software
'    without specific prior written permission. 
'
' THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
' AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
' THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
' ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
' FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
' (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
' LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
' ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
' (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
' EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

Namespace Extreme.FloatingPoint
    ''' <summary>
    ''' Enumerates the possible values for the class of a floating-point number.
    ''' </summary>
    Public Enum IeeeClass
        ''' <summary>
        ''' The value is a signaling NaN (Not a Number).
        ''' </summary>
        SignalingNaN
        ''' <summary>
        ''' The value is a quiet (non-signaling) NaN (Not a Number).
        ''' </summary>
        QuietNaN
        ''' <summary>
        ''' The value is positive infinity.
        ''' </summary>
        PositiveInfinity
        ''' <summary>
        ''' The value is negative infinity.
        ''' </summary>
        NegativeInfinity
        ''' <summary>
        ''' The value is a normal, positive number.
        ''' </summary>
        PositiveNormalized
        ''' <summary>
        ''' The value is a normal, negative number.
        ''' </summary>
        NegativeNormalized
        ''' <summary>
        ''' The value is a denormalized positive number.
        ''' </summary>
        PositiveDenormalized
        ''' <summary>
        ''' The value is a denormalized negative number.
        ''' </summary>
        NegativeDenormalized
        ''' <summary>
        ''' The value is positive zero.
        ''' </summary>
        PositiveZero
        ''' <summary>
        ''' The value is negative zero.
        ''' </summary>
        NegativeZero
    End Enum

    ''' <summary>
    ''' Summary description for Ieee.
    ''' </summary>
    Public Module FloatingPointModule
#Region "Private instance members"

        'Private   ReadOnly NegativeZero As Double = BitConverter.Int64BitsToDouble(8 * &H1000000000000000)
        Private Const MinDouble As [Double] = 4.9406564584124654E-324

        Private Const _signMask As Long = -1 - &H7FFFFFFFFFFFFFFF
        Private Const _signClearMask As Long = &H7FFFFFFFFFFFFFFF

        Private Const _mantissaMask As Long = &HFFFFFFFFFFFFF
        Private Const _mantissaClearMask As Long = _signMask Or _exponentMask

        Private Const _exponentMask As Long = &H7FF0000000000000
        Private Const _exponentClearMask As Long = _signMask Or _mantissaMask

        Private Const _bias As Integer = 1023
        Private Const _mantissaBits As Integer = 52
#End Region

#Region "Methods for getting parts of a double's binary representation."
        ''' <summary>
        ''' Returns the mantissa part of a <see cref="double"/>.
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns>An <see cref="long"/> value containing the 53 bits of the mantissa.
        ''' </returns>
        ''' <remarks>The implicit leading bit is not included.</remarks>
        Public Function Mantissa(ByVal x As Double) As Long
            Return BitConverter.DoubleToInt64Bits(x) And _mantissaMask
        End Function

        ''' <summary>
        ''' Returns the mantissa of a floating-point number.
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns>A number between 1 and 2, or zero if <paramref name="x"/> is zero.</returns>
        Public Function Significand(ByVal x As Double) As Double
            If [Double].IsNaN(x) Then
                Return x
            End If
            If [Double].IsInfinity(x) Then
                Return x
            End If
            Dim exponent As Integer = BiasedExponent(x)
            Dim mantissa__1 As Long = Mantissa(x)
            ' We must treat 0 and denormalized numbers separately.
            If exponent = 0 Then
                ' If x is zero, return zero.
                If mantissa__1 = 0 Then
                    Return 0.0R
                End If
                ' else, shift the mantissa to the left until it is 53 bits long...
                While mantissa__1 < _mantissaMask
                    mantissa__1 <<= 1
                End While
                ' ...and chop of the leading bit:
                mantissa__1 = mantissa__1 And _mantissaMask
            End If
            Return BitConverter.Int64BitsToDouble((_bias << 52) + mantissa__1)
        End Function

        ''' <summary>
        ''' Returns the unbiased exponent of a floating-point number.
        ''' </summary>
        ''' <param name="x">A number.</param>
        ''' <returns>The unbiased exponent of <paramref name="x"/>.</returns>
        ''' <remarks>The unbiased exponent is obtained by subtracting the bias from the biased exponent
        ''' taken directly from the binary representation of <paramref name="x"/>. Special values are not
        ''' considered.</remarks>
        Public Function Exponent(ByVal x As Double) As Integer
            Return CInt(((BitConverter.DoubleToInt64Bits(x) And _exponentMask) >> 52)) - _bias
        End Function

        ''' <summary>
        ''' Returns the biased exponent of a floating-point number.
        ''' </summary>
        ''' <param name="x">A number.</param>
        ''' <returns>The biased exponent of <paramref name="x"/>.</returns>
        ''' <remarks>The return value is taken directly from the binary representation of <paramref name="x"/>. 
        ''' Special values are not considered.</remarks>
        Public Function BiasedExponent(ByVal x As Double) As Integer
            Return CInt(((BitConverter.DoubleToInt64Bits(x) And _exponentMask) >> 52))
        End Function

        ''' <summary>
        ''' Returns the value of the sign bit of a number.
        ''' </summary>
        ''' <param name="x">A number.</param>
        ''' <returns>The sign bit taken directly from the binary representation of <paramref name="x"/>.</returns>
        Public Function SignBit(ByVal x As Double) As Integer
            Return If(((BitConverter.DoubleToInt64Bits(x) And _signMask) <> 0), 1, 0)
        End Function
#End Region

#Region " Implementation of the IEEE-754 ""Recommended functions"" "
        ''' <summary>
        ''' Returns the IEEE floating-point class of a number.
        ''' </summary>
        ''' <param name="x">A <see cref="double"/>.</param>
        ''' <returns>An <see cref="IeeeClass"/> value.</returns>
        Public Function [Class](ByVal x As Double) As IeeeClass
            Dim bits As Long = BitConverter.DoubleToInt64Bits(x)
            Dim positive As Boolean = (bits >= 0)
            bits = bits And _signClearMask
            If bits >= &H7FF0000000000000 Then
                ' overflow / NAN
                If (bits And _mantissaMask) = 0 Then
                    ' Infinity
                    If positive Then
                        Return IeeeClass.PositiveInfinity
                    Else
                        Return IeeeClass.NegativeInfinity
                    End If
                Else
                    If (bits And _mantissaMask) < &H8000000000000 Then
                        Return IeeeClass.QuietNaN
                    Else
                        Return IeeeClass.SignalingNaN
                    End If
                End If
            ElseIf bits < 4503599627370496L Then
                ' 0 or denormalized
                If bits = 0 Then
                    If positive Then
                        Return IeeeClass.PositiveZero
                    Else
                        Return IeeeClass.NegativeZero
                    End If
                Else
                    If positive Then
                        Return IeeeClass.PositiveDenormalized
                    Else
                        Return IeeeClass.NegativeDenormalized
                    End If
                End If
            Else
                If positive Then
                    Return IeeeClass.PositiveNormalized
                Else
                    Return IeeeClass.NegativeNormalized
                End If
            End If
        End Function

        ''' <summary>
        ''' Copies the sign of a number.
        ''' </summary>
        ''' <param name="sizeValue">The number whose sign to copy.</param>
        ''' <param name="signValue">The number whose value to copy.</param>
        ''' <returns>A <see cref="double"/> with the magnitude of <paramref name="sizeValue"/>
        ''' and the sign of <paramref name="sizeValue"/>.</returns>
        Public Function CopySign(ByVal sizeValue As Double, ByVal signValue As Double) As Double
            ' This is straightforward bit manipulation. Copy the first bit of signValue to sizeValue.
            Return BitConverter.Int64BitsToDouble((BitConverter.DoubleToInt64Bits(sizeValue) And _signClearMask) Or (BitConverter.DoubleToInt64Bits(signValue) And _signMask))
        End Function

        ''' <summary>
        ''' Gets a value that indicates whether a number is finite.
        ''' </summary>
        ''' <param name="x">A real number.</param>
        ''' <returns><see langword="true"/> if the number <paramref name="x"/> is finite;
        ''' otherwise <see langword="false"/>.</returns>
        Public Function IsFinite(ByVal x As Double) As Boolean
            ' Check the exponent part. If it is all 1's then we have infinity/NaN
            Dim bits As Long = BitConverter.DoubleToInt64Bits(x)
            Return ((bits And _exponentMask) = _exponentMask)
        End Function

        ''' <summary>
        ''' Returns the unbiased exponent of a number.
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns>The unbiased exponent of a number.</returns>
        ''' <remarks>This method returns an exponent <c>e</c> such that <c>1 &lt;= 2<sup>-e</sup>x &lt; 2</c>. This is true
        ''' even for denormalized numbers. If <paramref name="x"/> is zero, then <see cref="Double.NegativeInfinity"/> is returned.</remarks>
        Public Function Logb(ByVal x As Double) As Double
            ' Treat special cases first.
            If Double.IsNaN(x) Then
                Return x
            End If
            If Double.IsInfinity(x) Then
                Return Double.PositiveInfinity
            End If
            If x = 0 Then
                Return Double.NegativeInfinity
            End If

            Dim e As Integer = BiasedExponent(x)
            ' See if the number is denormalized, and take appropriate action.
            If e = 0 Then
                e = -1074
                ' Get the mantissa without the sign.
                Dim bits As Long = BitConverter.DoubleToInt64Bits(x) And _signClearMask
                ' We covered the case where bits = 0 already.
                Do
                    bits >>= 1
                    e += 1
                Loop While bits > 0
                Return e
            End If

            ' e was biased, so subtract the bias to get the unbiased exponent.
            Return e - _bias
        End Function

        ''' <summary>
        ''' Gets the next floating-point number in the direction of another number.
        ''' </summary>
        ''' <param name="fromValue">The starting point.</param>
        ''' <param name="toValue">The value indicating the direction in which to find
        ''' the next number.</param>
        ''' <returns></returns>
        Public Function NextAfter(ByVal fromValue As Double, ByVal toValue As Double) As Double
            ' If toValue equals fromValue, we have no direction to go in, so we return fromValue.
            If fromValue = toValue Then
                Return fromValue
            End If

            ' NaN's return themselves.
            If Double.IsNaN(fromValue) Then
                Return fromValue
            End If
            If Double.IsNaN(toValue) Then
                Return toValue
            End If

            ' Infinities always remain infinite.
            If Double.IsInfinity(fromValue) Then
                Return fromValue
            End If

            ' Handle the special case 0.
            If fromValue = 0 Then
                Return If((toValue > 0), MinDouble, -MinDouble)
            End If

            ' All other cases are handled by incrementing or decrementing the bits value.
            ' Transitions to infinity, to denormalized, and to zero are all taken care of this way.
            Dim bits As Long = BitConverter.DoubleToInt64Bits(fromValue)
            ' A xor here avoids nesting conditionals. We have to increment if
            ' fromValue lies between 0 and toValue.
            If (fromValue > 0) Xor (fromValue > toValue) Then
                bits += 1
            Else
                bits -= 1
            End If
            Return BitConverter.Int64BitsToDouble(bits)
        End Function

        ''' <summary>
        ''' Returns a number scaled by a power of 2.
        ''' </summary>
        ''' <param name="x">A number.</param>
        ''' <param name="n">The integer exponent of the scale factor.</param>
        ''' <returns>The value 2<sup><paramref name="n"/></sup><paramref name="x"/>.</returns>
        ''' <remarks>.</remarks>
        Public Function Scalb(ByVal x As Double, ByVal n As Integer) As Double
            ' Treat special cases first.
            If x = 0 OrElse Double.IsInfinity(x) OrElse Double.IsNaN(x) Then
                Return x
            End If
            If n = 0 Then
                Return x
            End If

            Dim e As Integer = BiasedExponent(x)
            Dim mantissa__1 As Long = Mantissa(x)
            Dim sign As Long = (If((x > 0), 0, _signMask))

            ' Is x denormalized?
            If e = 0 Then
                If n < 0 Then
                    ' n negative means we have to shift the mantissa -n bits to the right.
                    mantissa__1 >>= -n
                    Return BitConverter.Int64BitsToDouble(sign Or mantissa__1)
                Else
                    ' n positive means we need to shift to the left until we get a normalized number...
                    ' if we get there, that is.
                    While mantissa__1 <= _mantissaMask AndAlso n > 0
                        mantissa__1 <<= 1
                        n -= 1
                    End While
                    If mantissa__1 > _mantissaMask Then
                        n += 1
                    End If
                    ' The value of n is now the biased exponent.

                    ' Does the result overflow?
                    If n > 2 * _bias Then
                        Return If((x > 0), Double.PositiveInfinity, Double.NegativeInfinity)
                    End If

                    ' n is the biased exponent of the result.
                    Return BitConverter.Int64BitsToDouble(sign Or (CLng(n) << 52) Or (mantissa__1 And _mantissaMask))
                End If
            End If

            ' If we get here, we know x is normalized.
            ' Do scaling. e becomes the biased exponent of the result.
            e = e + n

            ' Check for 0 or denormalized.
            If e < 0 Then
                mantissa__1 = ((1L << 52) + mantissa__1) >> (1 - e)
                Return BitConverter.Int64BitsToDouble(sign Or mantissa__1)
            End If

            ' Check for overflow.
            If e > 2 * _bias Then
                Return If((x > 0), Double.PositiveInfinity, Double.NegativeInfinity)
            End If

            ' If we're here, the result is normalized.
            Dim bits As Long = sign Or (CLng(e) << 52) Or mantissa__1
            Return BitConverter.Int64BitsToDouble(bits)
        End Function

        ''' <summary>
        ''' Returns a value that indicates whether two values are unordered.
        ''' </summary>
        ''' <param name="x">A number.</param>
        ''' <param name="y">A number.</param>
        ''' <returns><see langword="true"/> if either <paramref name="x"/> or <paramref name="y"/> is <see cref="Double.NaN"/>;
        ''' otherwise <see langword="false"/>.</returns>
        Public Function Unordered(ByVal x As Double, ByVal y As Double) As Boolean
            Return Double.IsNaN(x) OrElse Double.IsNaN(y)
        End Function
#End Region

    End Module
End Namespace
