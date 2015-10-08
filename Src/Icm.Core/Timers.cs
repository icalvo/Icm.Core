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
			get { return Now.Add(new TimeSpan(0, 0, 0, 0, Convert.ToInt32(Interval))); }
			set { Interval = value.Subtract(Now).TotalMilliseconds; }
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
