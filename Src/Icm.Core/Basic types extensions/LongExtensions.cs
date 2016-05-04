using System;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace Icm
{
	public static class LongExtensions
	{

		/// <summary>
		/// Exponent prefix in the International System
		/// </summary>
		/// <param name="exponent"></param>
		/// <returns></returns>
		/// <remarks>
		/// 
		/// </remarks>
		private static string Long1000ExponentPrefix(int exponent)
		{
			string units = null;
			switch (exponent) {
				case -8:
					units = "yocto";
					break;
				case -7:
					units = "zepto";
					break;
				case -6:
					units = "atto";
					break;
				case -5:
					units = "femto";
					break;
				case -4:
					units = "pico";
					break;
				case -3:
					units = "nano";
					break;
				case -2:
					units = "micro";
					break;
				case -1:
					units = "mili";
					break;
				case 0:
					units = "";
					break;
				case 1:
					units = "kilo";
					break;
				case 2:
					units = "mega";
					break;
				case 3:
					units = "giga";
					break;
				case 4:
					units = "tera";
					break;
				case 5:
					units = "peta";
					break;
				case 6:
					units = "hexa";
					break;
				case 7:
					units = "zeta";
					break;
				case 8:
					units = "iota";
					break;
				default:
					throw new ArgumentOutOfRangeException("exponent", exponent, "Cannot handle exponents under -8 or over 8 for decimal units");
			}
			return units;
		}

		private static string Long1024ExponentPrefix(int exponent)
		{
			string units = null;
			switch (exponent) {
				case 0:
					units = "";
					break;
				case 1:
					units = "kibi";
					break;
				case 2:
					units = "mebi";
					break;
				case 3:
					units = "gibi";
					break;
				case 4:
					units = "tebi";
					break;
				case 5:
					units = "pebi";
					break;
				case 6:
					units = "hebi";
					break;
				case 7:
					units = "zebi";
					break;
				case 8:
					units = "iobi";
					break;
				default:
					throw new ArgumentOutOfRangeException("exponent", exponent, "Cannot handle exponents under 0 or over 8 for binary units");
			}
			return units;
		}

		private static string Short1000ExponentPrefix(int exponent)
		{
			string units = null;
			switch (exponent) {
				case -8:
					units = "y";
					break;
				case -7:
					units = "z";
					break;
				case -6:
					units = "a";
					break;
				case -5:
					units = "f";
					break;
				case -4:
					units = "p";
					break;
				case -3:
					units = "n";
					break;
				case -2:
					units = "�";
					break;
				case -1:
					units = "m";
					break;
				case 0:
					units = "";
					break;
				case 1:
					units = "K";
					break;
				case 2:
					units = "M";
					break;
				case 3:
					units = "G";
					break;
				case 4:
					units = "T";
					break;
				case 5:
					units = "P";
					break;
				case 6:
					units = "H";
					break;
				case 7:
					units = "Z";
					break;
				case 8:
					units = "I";
					break;
				default:
					throw new ArgumentOutOfRangeException("exponent", exponent, "Cannot handle exponents under -8 or over 8 for decimal units");
			}
			return units;
		}

		private static string Short1024ExponentPrefix(int exponent)
		{
			string units = null;
			switch (exponent) {
				case 0:
					units = "";
					break;
				case 1:
					units = "Ki";
					break;
				case 2:
					units = "Mi";
					break;
				case 3:
					units = "Gi";
					break;
				case 4:
					units = "Ti";
					break;
				case 5:
					units = "Pi";
					break;
				case 6:
					units = "Hi";
					break;
				case 7:
					units = "Zi";
					break;
				case 8:
					units = "Ii";
					break;
				default:
					throw new ArgumentOutOfRangeException("exponent", exponent, "Cannot handle exponents under 0 or over 8 for binary units");
			}
			return units;
		}

		/// <summary>
		///   Returns a human-readable representation of a byte quantity.
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="decimalUnits">Does the function use decimal powers (e.g. 1 KB =
		///  1000 B) or binary powers (1 KiB = 1024 B)? By default it is True (decimal)
		/// </param>
		/// <param name="bigUnitNames">Does the function add the abbreviated unit symbol (MB)
		/// or the complete unit name (megabyte)? By default it is False (abbr.)</param>
		/// <param name="format">Format string for the number. Use it to limit precision
		/// or adjust other formatting options. By default, it is "0.00".</param>
		/// <returns>The </returns>
		/// <remarks>
		///     <para>The function will only admit a Long number. This is far outside
		/// the scope of zetabytes and iotabytes, but hey, you never know...</para>
		/// </remarks>
		/// <history>
		/// 	[icalvo]	14/11/2005	Created
		/// </history>
		public static string HumanFileSize(this long bytes, bool decimalUnits, bool bigUnitNames, string format)
		{

			return HumanUnit(bytes, 0, decimalUnits, bigUnitNames, "B", "byte", "bytes", format);
		}


		/// <summary>
		///   Returns a human-readable representation of a quantity with units.
		/// </summary>
		/// <param name="mantissa"></param>
		/// <param name="addedExponent">If you want to multiply the mantissa by 1000 (or 1024 if
		/// decimalUnits is False) powers this quantity.
		/// It is the way to go for quantities between 0 and 1, using negative exponents.</param>
		/// <param name="decimalUnits">Does the function use decimal powers (e.g. 1 Km =
		///  1000 m) or binary powers (1 KiB = 1024 B)? By default it is True (decimal)
		/// </param>
		/// <param name="bigUnitNames">Does the function add the abbreviated unit symbol (mm)
		/// or the complete unit name (millimeter)? By default it is False (abbr.)</param>
		/// <param name="smallUnitName">Abbreviated unit symbol (e.g. m)</param>
		/// <param name="bigUnitNameSingular">Unit name, singular (e.g. meter)</param>
		/// <param name="bigUnitNamePlural">Unit name, singular (e.g. meters)</param>
		/// <param name="numberFormat">Format string for the number. Use it to limit precision
		/// or adjust other formatting options. By default, it is "0.00".</param>
		/// <returns></returns>
		/// <remarks>
		///     <para></para>
		/// </remarks>
		/// <history>
		/// 	[icalvo]	14/11/2005	Created
		/// </history>
		public static string HumanUnit(this long mantissa, int addedExponent, bool decimalUnits, bool bigUnitNames, string smallUnitName, string bigUnitNameSingular, string bigUnitNamePlural, string numberFormat)
		{

			string formattedNumber = null;
			string units = null;
			string prefix = null;

			if (mantissa < 0) {
				throw new ArgumentOutOfRangeException("quantity", mantissa, "quantity should be a positive number");
			}

			int divisor = 0;
			int exponent = 0;

			if (decimalUnits) {
				divisor = 1000;
			} else {
				divisor = 1024;
			}

			if (mantissa == 0) {
				exponent = 0;
				formattedNumber = (0).ToString(numberFormat, CultureInfo.CurrentCulture);
			} else {
				exponent = Convert.ToInt32(Math.Floor(Math.Log(mantissa) / Math.Log(divisor))) + addedExponent;
				formattedNumber = (mantissa / Math.Pow(divisor, Math.Min(8, exponent))).ToString(numberFormat, CultureInfo.CurrentCulture);
			}

			if (bigUnitNames) {
				if (formattedNumber == "1") {
					units = bigUnitNameSingular;
				} else {
					units = bigUnitNamePlural;
				}
			} else {
				units = smallUnitName;
			}

			if (bigUnitNames) {
				if (decimalUnits) {
					prefix = Long1000ExponentPrefix(exponent);
				} else {
					prefix = Long1024ExponentPrefix(exponent);
				}
			} else {
				if (decimalUnits) {
					prefix = Short1000ExponentPrefix(exponent);
				} else {
					prefix = Short1024ExponentPrefix(exponent);
				}
			}

			return string.Format("{0} {1}{2}", formattedNumber, prefix, units);
		}


		/// <summary>
		///   Returns a human-readable representation of a byte quantity.
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns>The </returns>
		/// <remarks>
		///     <para>Equals to HumanFileSize(bytes, decimalUnits:=True, bigUnitNames:=False, format:="0.00").</para>
		/// </remarks>
		/// <history>
		/// 	[icalvo]	14/11/2005	Created
		/// </history>
		public static string HumanFileSize(this long bytes)
		{
			return HumanFileSize(bytes, decimalUnits: true, bigUnitNames: false, format: "0.00");
		}

	}
}

