
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Icm.Configuration
{

	public class TypedValue
	{
		public readonly Type Type;

		public readonly object Value;
		public TypedValue(Type t, object val)
		{
			Debug.Assert(val == null || object.ReferenceEquals(val.GetType, t));
			Type = t;
			Value = val;
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
