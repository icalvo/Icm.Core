
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm
{
	public class DateTotalOrder : BaseTotalOrder<System.DateTime>
	{

		public override System.DateTime Least()
		{
			return System.DateTime.MinValue;
		}

		public override System.DateTime Greatest()
		{
			return System.DateTime.MaxValue;
		}

		public override long T2Long(System.DateTime t)
		{
			return t.Ticks;
		}

		public override System.DateTime Long2T(long d)
		{
			return new System.DateTime(d);
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
