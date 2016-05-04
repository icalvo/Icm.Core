using System;
using System.Diagnostics;

namespace Icm.Configuration
{

	public class TypedValue
	{
		public readonly Type Type;

		public readonly object Value;
		public TypedValue(Type t, object val)
		{
			Debug.Assert(val == null || ReferenceEquals(val.GetType(), t));
			Type = t;
			Value = val;
		}
	}

}