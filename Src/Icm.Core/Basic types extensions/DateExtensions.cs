
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Icm
{

	public static class DateExtensions
	{

		public enum Seasons : int
		{
			Spring = 0,
			Summer = 1,
			Fall = 2,
			Winter = 3
		}

		/// <summary>
		/// Season of a given date.
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static Seasons Season(System.DateTime d)
		{
			int monthDay = d.Month * 100 + d.Day;
			if (monthDay >= 101 && monthDay < 321) {
				return Seasons.Winter;
			} else if (monthDay >= 321 && monthDay < 621) {
				return Seasons.Spring;
			} else if (monthDay >= 621 && monthDay < 921) {
				return Seasons.Summer;
			} else if (monthDay >= 921 && monthDay < 1221) {
				return Seasons.Fall;
			} else {
				return Seasons.Winter;
			}
		}

		/// <summary>
		/// Adds a duration to a date with saturation (if the result is out of the valid range, it returns Date.MaxValue)
		/// </summary>
		/// <param name="d">Date</param>
		/// <param name="dur">Duration to add</param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static System.DateTime AddS(System.DateTime d, TimeSpan dur)
		{
			if (dur < TimeSpan.Zero) {
				throw new ArgumentException("Cannot accept negative durations", "dur");
			}
			if (dur == TimeSpan.MaxValue) {
				return System.DateTime.MaxValue;
			} else {
				dynamic maxDate = System.DateTime.MaxValue.Subtract(dur);
				if (d > maxDate) {
					return System.DateTime.MaxValue;
				} else {
					return d.Add(dur);
				}
			}
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
