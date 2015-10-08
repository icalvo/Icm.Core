using System.Runtime.CompilerServices;

namespace Icm.Timers
{

	public static class TimerExtensions
	{

		[Extension()]
		public static System.DateTime GetNextElapsed(System.Timers.Timer timer)
		{
			return Now.AddMilliseconds(timer.Interval);
		}

		[Extension()]
		public static void SetNextElapsed(System.Timers.Timer timer, System.DateTime d)
		{
			timer.Interval = d.Subtract(Now).TotalMilliseconds;
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
