
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Collections.Generic
{


	public class YearManager : IPeriodManager<System.DateTime>
	{


		public int Period1(System.DateTime obj)
		{
			return obj.Year;
		}
		int IPeriodManager<System.DateTime>.Period(System.DateTime obj)
		{
			return Period1(obj);
		}

		public System.DateTime PeriodStart(int period)
		{
			return new System.DateTime(period, 1, 1);

		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
