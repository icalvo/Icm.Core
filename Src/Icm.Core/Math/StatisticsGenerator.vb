Namespace Icm.MathTools
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class StatisticsGenerator

        Private rng As New Random

        Public LastFour As New LimitedQueue(Of Double)(4)

#Region " Constructors "

        Public Sub New()

        End Sub

        Public Sub New(ByVal sem As Integer)
            ChangeSeed(sem)
        End Sub

#End Region

#Region " Generic sample generation "

        Public Delegate Function Function1Double(ByVal a As Double) As Double
        Public Delegate Function Function2Double(ByVal a As Double, ByVal b As Double) As Double
        Public Delegate Function Function3Double(ByVal a As Double, ByVal b As Double, ByVal c As Double) As Double
        Public Delegate Function Function4Double(ByVal a As Double, ByVal b As Double, ByVal c As Double, ByVal d As Double) As Double

#End Region

#Region " Normal "

        Private NormalUsed_ As Integer = -1
        Private NormalY_ As Double = 0.0

        ''' <summary>
        ''' Samples the standard normal probability distribution.
        ''' </summary>
        ''' <returns>A random value that follows N(9,1).</returns>
        ''' <remarks>
        '''    <para>The standard normal probability distribution function (PDF) has 
        '''    mean 0 and standard deviation 1.</para>
        '''    <para>The Box-Muller method is used, which is efficient, but 
        '''    generates two values at a time.</para>
        ''' </remarks>
        ''' <history>
        ''' [John Burkardt]	18/09/2004	Created in C#
        ''' [icalvo]		19/08/2004	Translated to VB.NET
        ''' </history>
        Function Normal01Sample() As Double
            Dim r1 As Double
            Dim r2 As Double
            Dim x As Double

            If NormalUsed_ = -1 Then
                NormalUsed_ = 0
            End If
            '
            '  If we've used an even number of values so far, generate two more, return one,
            '  and save one.
            '
            If NormalUsed_ Mod 2 = 0 Then

                Do
                    r1 = Uniform01()
                    If (r1 <> 0) Then
                        Exit Do
                    End If

                Loop
                r2 = Uniform01()
                x = Math.Sqrt(-2 * Math.Log(r1)) * Math.Cos(2 * Math.PI * r2)
                NormalY_ = Math.Sqrt(-2 * Math.Log(r1)) * Math.Sin(2 * Math.PI * r2)
            Else
                x = NormalY_
            End If
            NormalUsed_ += 1
            Return x

        End Function

        ''' <summary>
        ''' Samples the general normal probability distribution.
        ''' </summary>
        ''' <param name="mean">A random value that follows N(mean,dev)</param>
        ''' <param name="dev"></param>
        ''' <returns>
        '''    <para>The Box-Muller method is used, which is efficient, but 
        '''    generates two values at a time.</para>
        ''' </returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [icalvo]		07/04/2008	Created
        ''' </history>
        Public Function NormalSample(ByVal mean As Double, ByVal dev As Double) As Double
            Return mean + Math.Sqrt(dev) * Normal01Sample()
        End Function

        ''' <summary>
        ''' Peter Acklam's algorithm for the inverse normal cumulative distribution function.
        ''' </summary>
        ''' <param name="p">Probability</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' <para>See http://home.online.no/~pjacklam/notes/invnorm/index.html for more information.</para>
        ''' <para>Implemented in VB.NET by Geoffrey C. Barnes, Ph.D. Fels Center of Government and Jerry Lee Center of Criminology University of Pennsylvania.</para>
        ''' </remarks>
        Public Shared Function NormalCDFInv(ByVal p As Double) As Double

            Dim q As Double
            Dim r As Double

            'Coefficients in rational approximations.

            Const A1 As Double = -39.696830286653757
            Const A2 As Double = 220.9460984245205
            Const A3 As Double = -275.92851044696869
            Const A4 As Double = 138.357751867269
            Const A5 As Double = -30.66479806614716
            Const A6 As Double = 2.5066282774592392

            Const B1 As Double = -54.476098798224058
            Const B2 As Double = 161.58583685804089
            Const B3 As Double = -155.69897985988661
            Const B4 As Double = 66.80131188771972
            Const B5 As Double = -13.280681552885721

            Const C1 As Double = -0.0077848940024302926
            Const C2 As Double = -0.32239645804113648
            Const C3 As Double = -2.4007582771618381
            Const C4 As Double = -2.5497325393437338
            Const C5 As Double = 4.3746641414649678
            Const C6 As Double = 2.9381639826987831

            Const D1 As Double = 0.0077846957090414622
            Const D2 As Double = 0.32246712907003983
            Const D3 As Double = 2.445134137142996
            Const D4 As Double = 3.7544086619074162

            'Define break-points.

            Const P_LOW As Double = 0.024250000000000001
            Const P_HIGH As Double = 1 - P_LOW

            If p > 0 AndAlso p < P_LOW Then

                'Rational approximation for lower region.

                q = Math.Sqrt(-2 * Math.Log(p))

                Return (((((C1 * q + C2) * q + C3) * q + C4) * q + C5) * q + C6) / _
                   ((((D1 * q + D2) * q + D3) * q + D4) * q + 1)

            ElseIf p >= P_LOW AndAlso p <= P_HIGH Then

                'Rational approximation for central region.

                q = p - 0.5
                r = q * q

                Return (((((A1 * r + A2) * r + A3) * r + A4) * r + A5) * r + A6) * q / _
                  (((((B1 * r + B2) * r + B3) * r + B4) * r + B5) * r + 1)

            ElseIf p > P_HIGH AndAlso p < 1 Then

                'Rational approximation for upper region.

                q = Math.Sqrt(-2 * Math.Log(1 - p))

                Return -(((((C1 * q + C2) * q + C3) * q + C4) * q + C5) * q + C6) / _
                       ((((D1 * q + D2) * q + D3) * q + D4) * q + 1)

            Else

                Throw New ArgumentOutOfRangeException("p", "Probability must be > 0 and < 1")

            End If

        End Function

#End Region

#Region " Weibull "

        Public Shared Function WeibullCDFInv(ByVal cdf As Double, ByVal offset As Double, ByVal lambda As Double, ByVal k As Double) As Double
            If cdf < 0 OrElse 1 < cdf Then
                Throw New ArgumentOutOfRangeException("cdf", "CDF out of (0, 1)!")
            End If
            If k = 0 Then
                Throw New ArgumentOutOfRangeException("k", "k = 0 so the result will be infinity!")
            End If
            Return offset + lambda * Math.Pow(-Math.Log(1 - cdf), 1 / k)
        End Function

        Public Function WeibullSample(ByVal offset As Double, ByVal lambda As Double, ByVal k As Double) As Double
            Return WeibullCDFInv(Uniform01(), offset, lambda, k)
        End Function
#End Region

#Region " Uniform "

        ' Uniform deviate in the integer interval [min, max]
        Function IntUniform(ByVal min As Integer, ByVal max As Integer) As Integer
            Dim r As Double = Uniform01()
            Dim result As Integer = CInt(System.Math.Floor(r * (max - min + 1))) + min

            Return result
        End Function

        Public Shared Function Accumulate(ByVal freqArray As IEnumerable(Of Double)) As Double()
            Debug.Assert(freqArray IsNot Nothing)
            Debug.Assert(freqArray.Count > 0)

            Dim a(freqArray.Count - 1) As Double

            a(0) = freqArray(0)

            For i = 1 To freqArray.Count - 1
                a(i) = a(i - 1) + freqArray(i)
            Next

            Return a
        End Function

        ''' <summary>
        '''  Samples a discrete PDF given by an array.
        ''' </summary>
        ''' <param name="freqArray"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function PDFSample(ByVal freqArray() As Double) As Integer
            Dim U As Double = Uniform01()
            Dim accum As Double = 0

            For i = 0 To UBound(freqArray)
                accum += freqArray(i)
                If U < accum Then
                    Return i
                End If
            Next
            Return UBound(freqArray)
        End Function

        ''' <summary>
        '''  Samples a discrete CDF given by an array.
        ''' </summary>
        ''' <param name="accumArray"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CDFSample(ByVal accumArray() As Double) As Integer
            Dim U As Double = Uniform01()

            For i = 0 To UBound(accumArray)
                If U < accumArray(i) Then
                    Return i
                End If
            Next
            Return UBound(accumArray)
        End Function

        Public Function PDFSample(ByVal freqArray() As Double, ByVal values() As Double) As Double
            Dim U As Double = Uniform01()
            Dim accum As Double = 0

            For i = 0 To UBound(freqArray)
                accum += freqArray(i)
                If U < accum Then
                    Return values(i)
                End If
            Next
            Return values(UBound(freqArray))
        End Function

        Public Function CDFSample(Of T)(ByVal accumArray() As Double, ByVal values As IEnumerable(Of T)) As T
            Dim U As Double = Uniform01()

            For i = 0 To UBound(accumArray)
                If U < accumArray(i) Then
                    Return values(i)
                End If
            Next
            Return values(UBound(accumArray))
        End Function

        Public Sub ChangeSeed(ByVal newseed As Integer)
            rng = New Random(newseed Mod (Integer.MaxValue - 1))
        End Sub

        ' Uniform deviate in the real interval [0.0, 1.0)
        Function Uniform01() As Double

            Dim result As Double = rng.NextDouble()
            LastFour.Enqueue(result)

            Return result
        End Function


        ''' <summary>
        ''' Uniform deviate in the real interval [min, max)
        ''' </summary>
        ''' <param name="min"></param>
        ''' <param name="max"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	09/03/2006	Created
        ''' </history>
        Function Uniform(ByVal min As Double, ByVal max As Double) As Double
            Return Uniform01() * (max - min) + min
        End Function

        ''' <summary>
        '''   The random round function returns for a given x a random 
        ''' integer variable between floor(x) and ceiling(x), with mean = x.
        ''' </summary>
        ''' <param name="mean">Desired mean</param>
        ''' <returns>Random variable</returns>
        ''' <remarks>
        '''  Let A be an array of RandomRound(x) results.
        '''  The greater A is, the closer its mean with x.
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	09/03/2006	Created
        ''' </history>
        Function RandomRound(ByVal mean As Double) As Integer
            Dim floor As Integer = CInt(System.Math.Floor(mean))
            Dim ceiling As Integer = CInt(System.Math.Ceiling(mean))
            Dim sample As Double = Uniform01()
            If sample < ceiling - mean Then
                Return ceiling
            Else
                Return floor
            End If
        End Function

#End Region

#Region " Exponential "

        Function Exponential01Sample() As Double
            Return -System.Math.Log(1 - Uniform01())
        End Function

        Function ExponentialSample(ByVal off As Double, ByVal lambda As Double) As Double
            Return ExponentialCDFInv(Uniform01(), off, lambda)
        End Function

        Public Shared Function ExponentialCDFInv(ByVal cdf As Double, ByVal off As Double, ByVal lambda As Double) As Double
            Dim result As Double

            If cdf < 0.0 OrElse 1.0 < cdf Then
                Throw New ArgumentOutOfRangeException("CDF value is not in [0,1] (" & cdf & ")")
            End If

            If lambda <= 0.0 Then
                Throw New ArgumentOutOfRangeException("Exponential is not defined for lambda <= 0.0 (" & lambda & ")")
            End If

            result = off - lambda * Math.Log(1.0 - cdf)

            Return result
        End Function

#End Region

#Region " Erlang "

        Function ErlangSample(ByVal off As Double, ByVal mean As Double, ByVal K As Integer) As Double
            Dim result As Double

            result = off

            For i As Integer = 1 To K
                result += ExponentialSample(0.0, mean)
            Next

            Return result / K
        End Function

#End Region

    End Class
End Namespace
