Namespace Icm.Functions
    Public Interface IMathFunction(Of TX As {Structure, IComparable(Of TX)}, TY As {Structure, IComparable(Of TY)})
        ''' <summary>
        ''' Clone of the function with the same value at inf TX.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function EmptyClone() As IMathFunction(Of TX, TY)
        Function AbsMinXY() As FunctionPoint(Of TX, TY)
        Function AbsMaxXY() As FunctionPoint(Of TX, TY)
        Function MinXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal threshold As TY) As FunctionPoint(Of TX, TY)
        Function MinXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal fnThreshold As Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
        Function MaxXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal threshold As TY) As FunctionPoint(Of TX, TY)
        Function MaxXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal fnThreshold As Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
        Function FstXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal threshold As TY) As FunctionPoint(Of TX, TY)
        Function FstXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal fnThreshold As Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
        Function LstXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal threshold As TY) As FunctionPoint(Of TX, TY)
        Function LstXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal fnThreshold As Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
        Function GetRange(ByVal rangeStart As TX, ByVal rangeEnd As TX) As MathFunctionRange(Of TX, TY)
        ''' <summary>
        ''' Max({y IN TY})
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Gst() As TY
        ''' <summary>
        ''' Min({y IN TY})
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Lst() As TY
        ''' <summary>
        ''' Max({x IN TX})
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GstX() As TX
        ''' <summary>
        ''' Min({x IN TX})
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function LstX() As TX
        Function MinXY(ByVal rangeStart As TX, ByVal rangeEnd As TX) As FunctionPoint(Of TX, TY)
        Function MaxXY(ByVal rangeStart As TX, ByVal rangeEnd As TX) As FunctionPoint(Of TX, TY)
        Function FstXY(ByVal rangeStart As TX, ByVal rangeEnd As TX) As FunctionPoint(Of TX, TY)
        Function LstXY(ByVal rangeStart As TX, ByVal rangeEnd As TX) As FunctionPoint(Of TX, TY)
        ReadOnly Property OrdenTotalTX() As ITotalOrder(Of TX)
        ReadOnly Property OrdenTotalTY() As ITotalOrder(Of TY)
        Function X2Long(ByVal x As TX) As Long
        Function Long2X(ByVal d As Long) As TX
        Function Y2Long(ByVal y As TY) As Long
        Function Long2Y(ByVal d As Long) As TY
        ''' <summary>
        ''' Es el valor correspondiente al primer lugar dentro del rango y que cumple el umbral dado.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' Si tumbral = Superior:
        ''' F(Min({x EN [rangeStart,rangeEnd] / F(x) LEQ umbral))
        ''' Si tumbral = Inferior:
        ''' F(Min({x EN [rangeStart,rangeEnd] / F(x) GEQ umbral))
        ''' </remarks>
        Function Fst(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As TY?
        ''' <summary>
        ''' Si tumbral = Superior:
        ''' F(Max({x EN [rangeStart,rangeEnd] / F(x) LEQ umbral))
        ''' Si tumbral = Inferior:
        ''' F(Max({x EN [rangeStart,rangeEnd] / F(x) GEQ umbral))
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Lst(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As TY
        ''' <summary>
        ''' Valor mínimo alcanzado en el rango
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Min(ByVal rangeStart As TX, ByVal rangeEnd As TX) As TY
        ''' <summary>
        ''' Valor mínimo alcanzado en el rango
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Max(ByVal rangeStart As TX, ByVal rangeEnd As TX) As TY
        Function FstX(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As TX?
        Function FstXu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal fnUmbral As Func(Of TX, TY)) As TX?
        Function LstX(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As TX?
        ''' <summary>
        ''' Lugar donde se alcanza el valor máximo en el rango
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function MaxX(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As TX?
        ''' <summary>
        ''' Lugar donde se alcanza el valor mínimo en el rango
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function MinX(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As TX?
        Function MaxX(ByVal rangeStart As TX, ByVal rangeEnd As TX) As TX?
        Function MinX(ByVal rangeStart As TX, ByVal rangeEnd As TX) As TX?
        Default ReadOnly Property V(ByVal x As TX) As TY
        Function V0() As TY
        Function VInf() As TY
    End Interface
End Namespace
