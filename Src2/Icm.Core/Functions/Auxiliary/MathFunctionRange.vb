Namespace Icm.Functions
    Public Class MathFunctionRange(Of TX As {Structure, IComparable(Of TX)}, TY As {Structure, IComparable(Of TY)})

        Private _mathFunction As MathFunction(Of TX, TY)
        ReadOnly Property MathFunction() As MathFunction(Of TX, TY)
            Get
                Return _mathFunction
            End Get
        End Property

        Private _currentStart As TX
        ReadOnly Property RangeStart() As TX
            Get
                Return _currentStart
            End Get
        End Property

        Private _currentEnd As TX
        Public Sub New(ByVal fn As MathFunction(Of TX, TY), ByVal rangeStart As TX, ByVal rangeEnd As TX)
            _currentStart = rangeStart
            _currentEnd = rangeEnd
            _mathFunction = fn
        End Sub

        ReadOnly Property RangeEnd() As TX
            Get
                Return _currentEnd
            End Get
        End Property

        Public Function MinXY(ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Return MathFunction.MinXY(RangeStart, RangeEnd, tumbral, cantidad)
        End Function


        Public Function MinXY() As FunctionPoint(Of TX, TY)
            Return MathFunction.MinXY(RangeStart, RangeEnd, ThresholdType.RightOpen, MathFunction.Gst)
        End Function


        Public Function MaxXY(ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Return MathFunction.MaxXY(RangeStart, RangeEnd, tumbral, cantidad)
        End Function

        Public Function MaxXY() As FunctionPoint(Of TX, TY)
            Return MathFunction.MaxXY(RangeStart, RangeEnd, ThresholdType.RightOpen, MathFunction.Gst)
        End Function


        Public Function FstXY(ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Return MathFunction.FstXY(RangeStart, RangeEnd, tumbral, cantidad)
        End Function

        Public Function FstXY() As FunctionPoint(Of TX, TY)
            Return MathFunction.FstXY(RangeStart, RangeEnd, ThresholdType.RightOpen, MathFunction.Gst)
        End Function


        Public Function LstXY(ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Return MathFunction.LstXY(RangeStart, RangeEnd, tumbral, cantidad)
        End Function

        Public Function LstXY() As FunctionPoint(Of TX, TY)
            Return MathFunction.LstXY(RangeStart, RangeEnd, ThresholdType.RightOpen, MathFunction.Gst)
        End Function

        Public Overridable Function Max() As TY
            Return MathFunction.Max(RangeStart, RangeEnd)
        End Function

        Public Overridable Function Min() As TY
            Return MathFunction.Min(RangeStart, RangeEnd)
        End Function

        Public Function Compare(ByVal d As TY) As Integer
            Return Max().CompareTo(d)
        End Function


        ''' <summary>
        ''' Utilizar como sinónimo abreviado de <see cref="MathFunctionRange(Of TX, TY).RangeStart"></see> en expresiones compiladas.
        ''' Los implementadores no deberían emplearlo.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function RS() As TX
            Return RangeStart
        End Function

        ''' <summary>
        ''' Utilizar como sinónimo abreviado de <see cref="MathFunctionRange(Of TX, TY).RangeEnd"></see> en expresiones compiladas.
        ''' Los implementadores no deberían emplearlo.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function RE() As TX
            Return RangeEnd
        End Function

        Public Function VS() As TY
            Return MathFunction.V(RangeStart)
        End Function

        Public Function VE() As TY
            Return MathFunction.V(RangeEnd)
        End Function



        Public Shared Function Compare(ByVal lt As MathFunctionRange(Of TX, TY), ByVal d As TY) As Integer
            Return lt.Max().CompareTo(d)
        End Function

        Public Shared Operator <(ByVal lt As MathFunctionRange(Of TX, TY), ByVal d As TY) As Boolean
            Return lt.Max().CompareTo(d) < 0
        End Operator

        Public Shared Operator <=(ByVal lt As MathFunctionRange(Of TX, TY), ByVal d As TY) As Boolean
            Return lt.Max().CompareTo(d) <= 0
        End Operator

        Public Shared Operator >(ByVal lt As MathFunctionRange(Of TX, TY), ByVal d As TY) As Boolean
            Return lt.Min().CompareTo(d) > 0
        End Operator

        Public Shared Operator >=(ByVal lt As MathFunctionRange(Of TX, TY), ByVal d As TY) As Boolean
            Return lt.Min().CompareTo(d) >= 0
        End Operator

        Public Shared Operator <(ByVal d As TY, ByVal lt As MathFunctionRange(Of TX, TY)) As Boolean
            Return lt.Max().CompareTo(d) > 0
        End Operator

        Public Shared Operator <=(ByVal d As TY, ByVal lt As MathFunctionRange(Of TX, TY)) As Boolean
            Return lt.Max().CompareTo(d) >= 0
        End Operator

        Public Shared Operator >(ByVal d As TY, ByVal lt As MathFunctionRange(Of TX, TY)) As Boolean
            Return lt.Min().CompareTo(d) < 0
        End Operator

        Public Shared Operator >=(ByVal d As TY, ByVal lt As MathFunctionRange(Of TX, TY)) As Boolean
            Return lt.Min().CompareTo(d) <= 0
        End Operator

    End Class
End Namespace
