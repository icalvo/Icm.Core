using System;

namespace Icm.Collections.Generic
{

	public class TrivialPeriodManager<T> : IPeriodManager<T> where T : IComparable<T>
	{


		private ITotalOrder<T> torder_;
		public TrivialPeriodManager(ITotalOrder<T> torder)
		{
			torder_ = torder;
		}

		public int Period(T obj)
		{
			return 1;
		}

		public T PeriodStart(int period)
		{
			return torder_.Least();
		}
	}
}
