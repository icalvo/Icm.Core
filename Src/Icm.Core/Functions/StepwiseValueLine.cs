
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Collections.Generic.StructKeyStructValue;

namespace Icm.Functions
{


	/// <summary>
	/// Stepwise keyed function with Date key and Double value.
	/// </summary>
	/// <remarks></remarks>
	public class StepwiseValueLine : StepwiseKeyedFunction<System.DateTime, double>
	{

		public StepwiseValueLine(double initialValue, ISortedCollection<System.DateTime, double> coll) : base(initialValue, new DateTotalOrder(), new DoubleTotalOrder(), coll)
		{
		}

		public override IMathFunction<System.DateTime, double> EmptyClone()
		{
			return new StepwiseValueLine(V0, KeyStore);
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
