
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm
{

	/// <summary>
	/// IComparable types establish a total order relationship by defining a CompareTo function
	/// defined for every pair of elements.
	/// Total order relationships provide a Least element, a Greatest element and also the
	/// possibility of establishing a bijection between the type and the real straight line, in
	/// such a way that the order is preserved.
	/// Functions Greatest, Least, T2Long and Long2T implement those operations.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks>
	/// Invariant:
	/// 
	/// For each t IN T / t LE Greatest()
	/// For each t IN T / Least() LE t
	/// For each t1,t2 IN T / t1 LE t2 EQV T2Double(t1) LE T2Double(t2)
	/// For each t IN T / Double2T(T2Double(t)) = t
	/// </remarks>
	public interface ITotalOrder<T> : IComparer<T> where T : IComparable<T>
	{

		T Greatest();
		T Least();
		long T2Long(T t);
		T Long2T(long d);
		T Next(T t);
		T Previous(T t);
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
