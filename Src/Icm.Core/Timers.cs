using System;

namespace Icm.Timers
{

	public class Timer : System.Timers.Timer
	{


		public Timer()
		{
		}

		public Timer(double interval) : base(interval)
		{
		}

		public System.DateTime NextElapsed {
			get { return DateTime.Now.Add(new TimeSpan(0, 0, 0, 0, Convert.ToInt32(Interval))); }
			set { Interval = value.Subtract(DateTime.Now).TotalMilliseconds; }
		}
	}

}
