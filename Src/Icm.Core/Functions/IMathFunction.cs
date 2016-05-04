using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Functions
{
    public interface IMathFunction<TX, TY> where TX : struct, IComparable<TX> where TY : struct, IComparable<TY>
    {
        /// <summary>
        /// Clone of the function with the same value at inf TX.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        IMathFunction<TX, TY> EmptyClone();
        FunctionPoint<TX, TY> AbsMinXY();
        FunctionPoint<TX, TY> AbsMaxXY();
        FunctionPoint<TX, TY> MinXY(TX rangeStart, TX rangeEnd, ThresholdType thrType, TY threshold);
        FunctionPoint<TX, TY> MinXYu(TX rangeStart, TX rangeEnd, ThresholdType thrType, Func<TX, TY> fnThreshold);
        FunctionPoint<TX, TY> MaxXY(TX rangeStart, TX rangeEnd, ThresholdType thrType, TY threshold);
        FunctionPoint<TX, TY> MaxXYu(TX rangeStart, TX rangeEnd, ThresholdType thrType, Func<TX, TY> fnThreshold);
        FunctionPoint<TX, TY> FstXY(TX rangeStart, TX rangeEnd, ThresholdType thrType, TY threshold);
        FunctionPoint<TX, TY> FstXYu(TX rangeStart, TX rangeEnd, ThresholdType thrType, Func<TX, TY> fnThreshold);
        FunctionPoint<TX, TY> LstXY(TX rangeStart, TX rangeEnd, ThresholdType thrType, TY threshold);
        FunctionPoint<TX, TY> LstXYu(TX rangeStart, TX rangeEnd, ThresholdType thrType, Func<TX, TY> fnThreshold);
        MathFunctionRange<TX, TY> GetRange(TX rangeStart, TX rangeEnd);
        /// <summary>
        /// Max({y IN TY})
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        TY Gst();
        /// <summary>
        /// Min({y IN TY})
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        TY Lst();
        /// <summary>
        /// Max({x IN TX})
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        TX GstX();
        /// <summary>
        /// Min({x IN TX})
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        TX LstX();
        FunctionPoint<TX, TY> MinXY(TX rangeStart, TX rangeEnd);
        FunctionPoint<TX, TY> MaxXY(TX rangeStart, TX rangeEnd);
        FunctionPoint<TX, TY> FstXY(TX rangeStart, TX rangeEnd);
        FunctionPoint<TX, TY> LstXY(TX rangeStart, TX rangeEnd);
        ITotalOrder<TX> OrdenTotalTX { get; }
        ITotalOrder<TY> OrdenTotalTY { get; }
        long X2Long(TX x);
        TX Long2X(long d);
        long Y2Long(TY y);
        TY Long2Y(long d);
        /// <summary>
        /// Es el valor correspondiente al primer lugar dentro del rango y que cumple el umbral dado.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Si tumbral = Superior:
        /// F(Min({x EN [rangeStart,rangeEnd] / F(x) LEQ umbral))
        /// Si tumbral = Inferior:
        /// F(Min({x EN [rangeStart,rangeEnd] / F(x) GEQ umbral))
        /// </remarks>
        TY? Fst(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad);
        /// <summary>
        /// Si tumbral = Superior:
        /// F(Max({x EN [rangeStart,rangeEnd] / F(x) LEQ umbral))
        /// Si tumbral = Inferior:
        /// F(Max({x EN [rangeStart,rangeEnd] / F(x) GEQ umbral))
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        TY Lst(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad);
        /// <summary>
        /// Valor mínimo alcanzado en el rango
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        TY Min(TX rangeStart, TX rangeEnd);
        /// <summary>
        /// Valor mínimo alcanzado en el rango
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        TY Max(TX rangeStart, TX rangeEnd);
        TX? FstX(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad);
        TX? FstXu(TX rangeStart, TX rangeEnd, ThresholdType tumbral, Func<TX, TY> fnUmbral);
        TX? LstX(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad);
        /// <summary>
        /// Lugar donde se alcanza el valor máximo en el rango
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        TX? MaxX(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad);
        /// <summary>
        /// Lugar donde se alcanza el valor mínimo en el rango
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        TX? MinX(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad);
        TX? MaxX(TX rangeStart, TX rangeEnd);
        TX? MinX(TX rangeStart, TX rangeEnd);
        TY this[TX x] { get; }
        TY V0();
        TY VInf();
    }
}
