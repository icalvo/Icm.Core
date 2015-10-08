
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace Icm.MathTools
{

	public static class MathModule
	{

		/// <summary>
		///  Linearly interpolates the f(x_2) value given x_1, x_2, x_3,
		/// f(x_1) and f(x_3)
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="x2"></param>
		/// <param name="x3"></param>
		/// <param name="fx1"></param>
		/// <param name="fx3"></param>
		/// <returns></returns>
		/// <remarks>
		/// <para>The function will also extrapolate if x_2 is outside
		/// x_1 and x_3 range.</para>
		/// <para>If x_1 = x_3, the functio will raise a <see cref="DivideByZeroException"></see>.</para>
		/// </remarks>
		public static double LinearInterpolate(double x1, double x2, double x3, double fx1, double fx3)
		{

			return ((x2 - x1) * (fx3 - fx1) / (x3 - x1)) + fx1;
		}

		/// <summary>
		///  Linearly interpolates the x_2 value given x_1, x_3,
		/// f(x_1), f(x_2) and f(x_3)
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="x3"></param>
		/// <param name="fx1"></param>
		/// <param name="fx2"></param>
		/// <param name="fx3"></param>
		/// <returns></returns>
		/// <remarks>
		/// <para>The function will also extrapolate if f(x_2) is outside
		/// f(x_1) and f(x_3) range.</para>
		/// <para>If f(x_1) = f(x_3), the function will raise a <see cref="DivideByZeroException"></see>.</para>
		/// </remarks>
		public static double InverseLinearInterpolate(double x1, double x3, double fx1, double fx2, double fx3)
		{

			return ((fx2 - fx1) * (x3 - x1) / (fx3 - fx1)) + x1;
		}

		public static double Max(double a, double b, double c)
		{
			return Math.Max(a, Math.Max(b, c));
		}

		public static double Min(double a, double b, double c)
		{
			return Math.Min(a, Math.Min(b, c));
		}

		public static double MaxN(params double[] a)
		{
			double max = double.NegativeInfinity;
			foreach (void d_loopVariable in a) {
				d = d_loopVariable;
				if (d > max) {
					max = d;
				}
			}
			return max;
		}

		[Extension()]
		public static bool NearEqual(double d1, double d2, double precission)
		{
			return d1 - d2 <= (Math.Pow(10, precission));
		}

		/// <summary>
		///  Linearly interpolates the f(x_2) value given x_1, x_2, x_3,
		/// f(x_1) and f(x_3)
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="x2"></param>
		/// <param name="x3"></param>
		/// <param name="fx1"></param>
		/// <param name="fx3"></param>
		/// <returns></returns>
		/// <remarks>
		/// <para>The function will also extrapolate if x_2 is outside
		/// x_1 and x_3 range.</para>
		/// <para>If x_1 = x_3, the functio will raise a <see cref="DivideByZeroException"></see>.</para>
		/// </remarks>
		public static long LinearInterpolate(long x1, long x2, long x3, long fx1, long fx3)
		{

			return ((x2 - x1) * (fx3 - fx1) / (x3 - x1)) + fx1;
		}

		/// <summary>
		///  Linearly interpolates the x_2 value given x_1, x_3,
		/// f(x_1), f(x_2) and f(x_3)
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="x3"></param>
		/// <param name="fx1"></param>
		/// <param name="fx2"></param>
		/// <param name="fx3"></param>
		/// <returns></returns>
		/// <remarks>
		/// <para>The function will also extrapolate if f(x_2) is outside
		/// f(x_1) and f(x_3) range.</para>
		/// <para>If f(x_1) = f(x_3), the function will raise a <see cref="DivideByZeroException"></see>.</para>
		/// </remarks>
		public static long InverseLinearInterpolate(long x1, long x3, long fx1, long fx2, long fx3)
		{

			return ((fx2 - fx1) * (x3 - x1) / (fx3 - fx1)) + x1;
		}

		public static long Max(long a, long b, long c)
		{
			return Math.Max(a, Math.Max(b, c));
		}

		public static long Min(long a, long b, long c)
		{
			return Math.Min(a, Math.Min(b, c));
		}

		public static long MaxN(params long[] a)
		{
			double max = double.NegativeInfinity;
			foreach (void d_loopVariable in a) {
				d = d_loopVariable;
				if (d > max) {
					max = d;
				}
			}
			return Convert.ToInt64(max);
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
