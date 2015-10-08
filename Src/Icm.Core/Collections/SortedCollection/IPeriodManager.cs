
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Collections.Generic
{

	/// <summary>
	/// Divides the domain of T into partitions each identified by an integer number.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks></remarks>
	public interface IPeriodManager<T>
	{

		int Period(T obj);

		T PeriodStart(int period);

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
