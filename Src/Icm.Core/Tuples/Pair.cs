
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm
{

	/// <summary>
	/// Modifiable Pair with default constructor.
	/// </summary>
	/// <typeparam name="T1"></typeparam>
	/// <typeparam name="T2"></typeparam>
	/// <remarks>
	/// Tuple has no default constructor and the properties of KeyValuePair are read-only.
	/// </remarks>
	public class Pair<T1, T2>
	{


		public Pair()
		{
		}

		public Pair(T1 f, T2 s)
		{
			First = f;
			Second = s;
		}

		public T1 First { get; set; }

		public T2 Second { get; set; }
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
