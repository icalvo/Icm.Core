using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Functions
{
    /// <summary>
    /// This class represents a mathematic function with domain at TX and range at TY.
    /// Either TX and TY have total order, defined by the properties TotalOrderTX and TotalOrderTY.
    /// </summary>
    /// <typeparam name="TX"></typeparam>
    /// <typeparam name="TY"></typeparam>
    /// <remarks></remarks>
    public abstract class MathFunction<TX, TY> : IMathFunction<TX, TY> where TX : struct, IComparable<TX> where TY : struct, IComparable<TY>
    {

        protected MathFunction(ITotalOrder<TX> otx, ITotalOrder<TY> oty)
        {
            totalOrderTX_ = otx;
            totalOrderTY_ = oty;
        }

        /// <summary>
        /// Clone of the function with the same value at inf TX.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public abstract IMathFunction<TX, TY> EmptyClone();

        #region " Attributes "

        /// <summary>
        /// Nombre de la línea de valores.
        /// </summary>
        /// <remarks></remarks>

        private string name_;
        private ITotalOrder<TX> totalOrderTX_;

        private ITotalOrder<TY> totalOrderTY_;
        #endregion


        #region " Abstract "

        /// <summary>
        ///  Value of function in x
        /// </summary>
        /// <param name="x"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public abstract TY this[TX x] { get; set; }

        public abstract FunctionPoint<TX, TY> AbsMinXY();
        public abstract FunctionPoint<TX, TY> AbsMaxXY();

        public abstract FunctionPoint<TX, TY> MinXY(TX rangeStart, TX rangeEnd, ThresholdType thrType, TY threshold);
        public abstract FunctionPoint<TX, TY> MinXYu(TX rangeStart, TX rangeEnd, ThresholdType thrType, Func<TX, TY> fnThreshold);
        public abstract FunctionPoint<TX, TY> MaxXY(TX rangeStart, TX rangeEnd, ThresholdType thrType, TY threshold);
        public abstract FunctionPoint<TX, TY> MaxXYu(TX rangeStart, TX rangeEnd, ThresholdType thrType, Func<TX, TY> fnThreshold);
        public abstract FunctionPoint<TX, TY> FstXY(TX rangeStart, TX rangeEnd, ThresholdType thrType, TY threshold);
        public abstract FunctionPoint<TX, TY> FstXYu(TX rangeStart, TX rangeEnd, ThresholdType thrType, Func<TX, TY> fnThreshold);
        public abstract FunctionPoint<TX, TY> LstXY(TX rangeStart, TX rangeEnd, ThresholdType thrType, TY threshold);
        public abstract FunctionPoint<TX, TY> LstXYu(TX rangeStart, TX rangeEnd, ThresholdType thrType, Func<TX, TY> fnThreshold);

        #endregion

        public MathFunctionRange<TX, TY> GetRange(TX rangeStart, TX rangeEnd)
        {
            return new MathFunctionRange<TX, TY>(this, rangeStart, rangeEnd);
        }

        /// <summary>
        /// Max({y IN TY})
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public TY Gst()
        {
            return TotalOrderTY.Greatest();
        }

        /// <summary>
        /// Min({y IN TY})
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public TY Lst()
        {
            return TotalOrderTY.Least();
        }

        /// <summary>
        /// Max({x IN TX})
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public TX GstX()
        {
            return TotalOrderTX.Greatest();
        }


        /// <summary>
        /// Min({x IN TX})
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public TX LstX()
        {
            return TotalOrderTX.Least();
        }

        public FunctionPoint<TX, TY> MinXY(TX rangeStart, TX rangeEnd)
        {
            return MinXY(rangeStart, rangeEnd, ThresholdType.RightOpen, Gst());
        }

        public FunctionPoint<TX, TY> MaxXY(TX rangeStart, TX rangeEnd)
        {
            return MaxXY(rangeStart, rangeEnd, ThresholdType.RightOpen, Gst());
        }

        public FunctionPoint<TX, TY> FstXY(TX rangeStart, TX rangeEnd)
        {
            return FstXY(rangeStart, rangeEnd, ThresholdType.RightOpen, Gst());
        }

        public FunctionPoint<TX, TY> LstXY(TX rangeStart, TX rangeEnd)
        {
            return LstXY(rangeStart, rangeEnd, ThresholdType.RightOpen, Gst());
        }

        public ITotalOrder<TX> TotalOrderTX
        {
            get { return totalOrderTX_; }
        }
        ITotalOrder<TX> IMathFunction<TX, TY>.OrdenTotalTX
        {
            get { return TotalOrderTX; }
        }

        public ITotalOrder<TY> TotalOrderTY
        {
            get { return totalOrderTY_; }
        }
        ITotalOrder<TY> IMathFunction<TX, TY>.OrdenTotalTY
        {
            get { return TotalOrderTY; }
        }

        public long X2Long(TX x)
        {
            return TotalOrderTX.T2Long(x);
        }

        public TX Long2X(long d)
        {
            return TotalOrderTX.Long2T(d);
        }

        public long Y2Long(TY y)
        {
            return TotalOrderTY.T2Long(y);
        }

        public TY Long2Y(long d)
        {
            return TotalOrderTY.Long2T(d);
        }

        /// <summary>
        /// Es el valor correspondiente al primer lugar dentro del rango y que cumple el umbral dado.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Si tumbral = Superior:
        ///   F(Min({x EN [rangeStart,rangeEnd] / F(x) LEQ umbral))
        /// Si tumbral = Inferior:
        ///   F(Min({x EN [rangeStart,rangeEnd] / F(x) GEQ umbral))
        /// </remarks>
        public virtual TY? Fst(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
        {
            var pnt = FstXY(rangeStart, rangeEnd, tumbral, cantidad);
            return pnt?.Y;
        }

        /// <summary>
        /// Si tumbral = Superior:
        ///   F(Max({x EN [rangeStart,rangeEnd] / F(x) LEQ umbral))
        /// Si tumbral = Inferior:
        ///   F(Max({x EN [rangeStart,rangeEnd] / F(x) GEQ umbral))
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public virtual TY Lst(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
        {
            var pnt = LstXY(rangeStart, rangeEnd, tumbral, cantidad);
            return pnt == null ? default(TY) : pnt.Y;
        }

        /// <summary>
        /// Valor mínimo alcanzado en el rango
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public virtual TY Min(TX rangeStart, TX rangeEnd)
        {
            return MinXY(rangeStart, rangeEnd, ThresholdType.RightOpen, Gst()).Y;
        }

        /// <summary>
        /// Valor mínimo alcanzado en el rango
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public virtual TY Max(TX rangeStart, TX rangeEnd)
        {
            return MaxXY(rangeStart, rangeEnd, ThresholdType.RightOpen, Gst()).Y;
        }

        public virtual TX? FstX(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
        {
            var pnt = FstXY(rangeStart, rangeEnd, tumbral, cantidad);
            return pnt?.X;
        }

        public virtual TX? FstXu(TX rangeStart, TX rangeEnd, ThresholdType tumbral, Func<TX, TY> fnUmbral)
        {
            var pnt = FstXYu(rangeStart, rangeEnd, tumbral, fnUmbral);
            return pnt?.X;
        }

        public virtual TX? LstX(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
        {
            var pnt = LstXY(rangeStart, rangeEnd, tumbral, cantidad);
            return pnt?.X;
        }

        /// <summary>
        /// Lugar donde se alcanza el valor máximo en el rango
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public virtual TX? MaxX(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
        {
            var pnt = MaxXY(rangeStart, rangeEnd, tumbral, cantidad);
            return pnt?.X;
        }

        /// <summary>
        /// Lugar donde se alcanza el valor mínimo en el rango
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public virtual TX? MinX(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
        {
            var pnt = MinXY(rangeStart, rangeEnd, tumbral, cantidad);
            return pnt?.X;
        }

        public virtual TX? MaxX(TX rangeStart, TX rangeEnd)
        {
            return MaxX(rangeStart, rangeEnd, ThresholdType.RightOpen, Lst());
        }

        public virtual TX? MinX(TX rangeStart, TX rangeEnd)
        {
            return MinX(rangeStart, rangeEnd, ThresholdType.LeftOpen, Gst());
        }

        public TY V0()
        {
            return this[LstX()];
        }

        public TY VInf()
        {
            return this[GstX()];
        }


        protected bool Compare(TY? y, ThresholdType threshold, TY umbral)
        {
            if (!y.HasValue)
            {
                return false;
            }

            switch (threshold)
            {
                case ThresholdType.LeftOpen:
                    return y.Value.CompareTo(umbral) > 0;
                case ThresholdType.LeftClosed:
                    return y.Value.CompareTo(umbral) >= 0;
                case ThresholdType.RightOpen:
                    return y.Value.CompareTo(umbral) < 0;
                case ThresholdType.RightClosed:
                    return y.Value.CompareTo(umbral) <= 0;
                default:
                    throw new ArgumentException("threshold", "threshold");
            }
        }
    }

}
 