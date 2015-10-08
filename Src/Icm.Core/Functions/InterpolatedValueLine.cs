namespace Icm.Functions
{

	/// <summary>
	/// Interpolated keyed function with Date key and Double value.
	/// </summary>
	/// <remarks></remarks>
	public class InterpolatedValueLine : InterpolatedKeyedFunction<System.DateTime, double>
	{

		public InterpolatedValueLine(double initialValue, ISortedCollection<System.DateTime, double> coll) : base(initialValue, new DateTotalOrder(), new DoubleTotalOrder(), coll)
		{
		}

		public override IMathFunction<System.DateTime, double> EmptyClone()
		{
			return new InterpolatedValueLine(V0, KeyStore);
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
