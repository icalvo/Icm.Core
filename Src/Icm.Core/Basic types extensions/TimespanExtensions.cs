using System;
using System.Runtime.CompilerServices;

namespace Icm
{
	public static class TimespanExtensions
	{
		/// <summary>
		/// Division of a Timespan by a number
		/// </summary>
		/// <param name="t"></param>
		/// <param name="divisor"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static TimeSpan DividedBy(this TimeSpan t, double divisor)
		{
			return new TimeSpan(Convert.ToInt64(t.Ticks / divisor));
		}

		/// <summary>
		/// Is the timespan equal to Timespan.Zero?
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool IsZero(this TimeSpan t)
		{
			return t == TimeSpan.Zero;
		}

		/// <summary>
		/// Is the timespan not equal to Timespan.Zero?
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool IsNotZero(this TimeSpan t)
		{
			return t != TimeSpan.Zero;
		}

		/// <summary>
		/// Abbreviated format (7d2h3'30'') that omits zero parts
		/// </summary>
		/// <param name="ts"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string ToAbbrev(this TimeSpan ts)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			if (ts == TimeSpan.Zero) {
				return "0";
			}
			TimeSpan absoluteTs = default(TimeSpan);
			if (ts < TimeSpan.Zero) {
				absoluteTs = ts.Negate();
				sb.Append("-");
			} else {
				absoluteTs = ts;
			}

			if (absoluteTs.Days != 0) {
				sb.Append(absoluteTs.Days + "d");
			}
			if (absoluteTs.Hours != 0) {
				sb.Append(absoluteTs.Hours + "h");
			}
			if (absoluteTs.Minutes != 0) {
				sb.Append(absoluteTs.Minutes + "'");
			}
			if (absoluteTs.Seconds != 0) {
				sb.Append(absoluteTs.Seconds + "''");
			}

			return sb.ToString();
		}

	    private static int Fix(double number)
	    {
	        return (int)(Math.Sign(number) * (int)Math.Truncate(Math.Abs(number)));
	    }
		/// <summary>
		/// Minutes time format (03:30.235) up to milliseconds.
		/// </summary>
		/// <param name="ts"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string Tommssttt(this TimeSpan ts)
		{
			return string.Format("{0:00}:{1:00}.{2:000}", Fix(ts.TotalMinutes), Math.Abs(ts.Seconds), Math.Abs(ts.Milliseconds));
		}

		/// <summary>
		/// Hour time format (02:03:30.235) up to milliseconds.
		/// </summary>
		/// <param name="ts"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string ToHHmmssttt(this TimeSpan ts)
		{
			return string.Format("{0:00}:{1:00}:{2:00}.{3:000}", Fix(ts.TotalHours), Math.Abs(ts.Minutes), Math.Abs(ts.Seconds), Math.Abs(ts.Milliseconds));
		}

		/// <summary>
		/// Hour time format (02:03:30) up to seconds.
		/// </summary>
		/// <param name="ts"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string ToHHmmss(this TimeSpan ts)
		{
			return string.Format("{0:00}:{1:00}:{2:00}", Fix(ts.TotalHours), Math.Abs(ts.Minutes), Math.Abs(ts.Seconds));
		}

		/// <summary>
		/// Hour format (02:03) up to minutes
		/// </summary>
		/// <param name="ts"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string ToHHmm(this TimeSpan ts)
		{
			return string.Format("{0:00}:{1:00}", Fix(ts.TotalHours), Math.Abs(ts.Minutes));
		}

		/// <summary>
		/// Total microseconds
		/// </summary>
		/// <param name="ts"></param>
		/// <returns></returns>
		/// <remarks>Don't expect much precision</remarks>
		public static long TotalMicroseconds(this TimeSpan ts)
		{
			return 1000 * ts.Ticks / TimeSpan.TicksPerMillisecond;
		}

	}

}
