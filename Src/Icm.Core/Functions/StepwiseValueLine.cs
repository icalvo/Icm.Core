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
			return new StepwiseValueLine(V0(), KeyStore);
		}

	}

}
