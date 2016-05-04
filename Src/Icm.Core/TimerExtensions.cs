using System;

namespace Icm.Timers
{
	public static class TimerExtensions
	{
		public static DateTime GetNextElapsed(this System.Timers.Timer timer)
		{
			return DateTime.Now.AddMilliseconds(timer.Interval);
		}

		public static void SetNextElapsed(this System.Timers.Timer timer, DateTime d)
		{
			timer.Interval = d.Subtract(DateTime.Now).TotalMilliseconds;
		}
	}
}