using System;

namespace Icm.Functions
{
	public class MathFunctionRange<TX, TY> where TX : struct, IComparable<TX> where TY : struct, IComparable<TY>
	{

		private MathFunction<TX, TY> _mathFunction;
		public MathFunction<TX, TY> MathFunction {
			get { return _mathFunction; }
		}

		private TX _currentStart;
		public TX RangeStart {
			get { return _currentStart; }
		}

		private TX _currentEnd;
		public MathFunctionRange(MathFunction<TX, TY> fn, TX rangeStart, TX rangeEnd)
		{
			_currentStart = rangeStart;
			_currentEnd = rangeEnd;
			_mathFunction = fn;
		}

		public TX RangeEnd {
			get { return _currentEnd; }
		}

		public FunctionPoint<TX, TY> MinXY(ThresholdType tumbral, TY cantidad)
		{
			return MathFunction.MinXY(RangeStart, RangeEnd, tumbral, cantidad);
		}


		public FunctionPoint<TX, TY> MinXY()
		{
			return MathFunction.MinXY(RangeStart, RangeEnd, ThresholdType.RightOpen, MathFunction.Gst());
		}


		public FunctionPoint<TX, TY> MaxXY(ThresholdType tumbral, TY cantidad)
		{
			return MathFunction.MaxXY(RangeStart, RangeEnd, tumbral, cantidad);
		}

		public FunctionPoint<TX, TY> MaxXY()
		{
			return MathFunction.MaxXY(RangeStart, RangeEnd, ThresholdType.RightOpen, MathFunction.Gst());
		}


		public FunctionPoint<TX, TY> FstXY(ThresholdType tumbral, TY cantidad)
		{
			return MathFunction.FstXY(RangeStart, RangeEnd, tumbral, cantidad);
		}

		public FunctionPoint<TX, TY> FstXY()
		{
			return MathFunction.FstXY(RangeStart, RangeEnd, ThresholdType.RightOpen, MathFunction.Gst());
		}


		public FunctionPoint<TX, TY> LstXY(ThresholdType tumbral, TY cantidad)
		{
			return MathFunction.LstXY(RangeStart, RangeEnd, tumbral, cantidad);
		}

		public FunctionPoint<TX, TY> LstXY()
		{
			return MathFunction.LstXY(RangeStart, RangeEnd, ThresholdType.RightOpen, MathFunction.Gst());
		}

		public virtual TY Max()
		{
			return MathFunction.Max(RangeStart, RangeEnd);
		}

		public virtual TY Min()
		{
			return MathFunction.Min(RangeStart, RangeEnd);
		}

		public int Compare(TY d)
		{
			return Max().CompareTo(d);
		}


		/// <summary>
		/// Utilizar como sinónimo abreviado de <see cref="MathFunctionRange(Of TX, TY).RangeStart"></see> en expresiones compiladas.
		/// Los implementadores no deberían emplearlo.
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public TX RS()
		{
			return RangeStart;
		}

		/// <summary>
		/// Utilizar como sinónimo abreviado de <see cref="MathFunctionRange(Of TX, TY).RangeEnd"></see> en expresiones compiladas.
		/// Los implementadores no deberían emplearlo.
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public TX RE()
		{
			return RangeEnd;
		}

		public TY VS()
		{
			return MathFunction[RangeStart];
		}

		public TY VE()
		{
			return MathFunction[RangeEnd];
		}



		public static int Compare(MathFunctionRange<TX, TY> lt, TY d)
		{
			return lt.Max().CompareTo(d);
		}

		public static bool operator <(MathFunctionRange<TX, TY> lt, TY d)
		{
			return lt.Max().CompareTo(d) < 0;
		}

		public static bool operator <=(MathFunctionRange<TX, TY> lt, TY d)
		{
			return lt.Max().CompareTo(d) <= 0;
		}

		public static bool operator >(MathFunctionRange<TX, TY> lt, TY d)
		{
			return lt.Min().CompareTo(d) > 0;
		}

		public static bool operator >=(MathFunctionRange<TX, TY> lt, TY d)
		{
			return lt.Min().CompareTo(d) >= 0;
		}

		public static bool operator <(TY d, MathFunctionRange<TX, TY> lt)
		{
			return lt.Max().CompareTo(d) > 0;
		}

		public static bool operator <=(TY d, MathFunctionRange<TX, TY> lt)
		{
			return lt.Max().CompareTo(d) >= 0;
		}

		public static bool operator >(TY d, MathFunctionRange<TX, TY> lt)
		{
			return lt.Min().CompareTo(d) < 0;
		}

		public static bool operator >=(TY d, MathFunctionRange<TX, TY> lt)
		{
			return lt.Min().CompareTo(d) <= 0;
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
