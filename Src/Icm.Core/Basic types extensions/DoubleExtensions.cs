using System;
using System.Runtime.CompilerServices;

namespace Icm
{

	public static class DoubleExtensions
	{

		/// <summary>
		/// Changes precision of a double number.
		/// </summary>
		/// <param name="num"></param>
		/// <param name="precision"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static double ChangePrecision(double num, int precision)
		{
			double result = 0;
			result = num * (Math.Pow(10, precision));
			result = Math.Round(result);
			result = result / (Math.Pow(10, precision));

			return result;
		}

		/// <summary>
		/// Converts degrees to radians
		/// </summary>
		/// <param name="angle">Angle in degrees</param>
		/// <returns>Angle in radians</returns>
		/// <remarks></remarks>
		[Extension()]
		public static double Deg2Rad(double angle)
		{
			return (angle * Math.PI) / 180;
		}

		/// <summary>
		/// Converts radians to degrees
		/// </summary>
		/// <param name="angle">Angle in radians</param>
		/// <returns>Angle in degrees</returns>
		/// <remarks></remarks>
		[Extension()]
		public static double Rad2Deg(double angle)
		{
			return (angle * 180) / Math.PI;
		}

	}
}


//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
