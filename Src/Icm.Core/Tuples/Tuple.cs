
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm
{

	public class Tuple
	{

		public static Tuple<T1, T2> Create<T1, T2>(T1 v1, T2 v2)
		{
			return new Tuple<T1, T2>(v1, v2);
		}

	}


	public class Tuple<T1, T2>
	{

		public readonly T1 Item1;

		public readonly T2 Item2;
		public Tuple(T1 v1, T2 v2)
		{
			Item1 = v1;
			Item2 = v2;
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
