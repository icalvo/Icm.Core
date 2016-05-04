namespace Icm.Collections.Generic
{
	public class YearManager : IPeriodManager<System.DateTime>
	{
		int IPeriodManager<System.DateTime>.Period(System.DateTime obj)
		{
			return obj.Year;
		}

		public System.DateTime PeriodStart(int period)
		{
			return new System.DateTime(period, 1, 1);

		}
	}
}