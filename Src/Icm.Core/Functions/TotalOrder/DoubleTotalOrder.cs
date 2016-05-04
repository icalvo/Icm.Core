using System;

namespace Icm
{
	public class DoubleTotalOrder : BaseTotalOrder<double>
	{

		public override double Least()
		{
			return double.NegativeInfinity;
		}

		public override double Greatest()
		{
			return double.PositiveInfinity;
		}

		public override long T2Long(double t)
		{
			return BitConverter.DoubleToInt64Bits(t);
		}

		public override double Long2T(long d)
		{
			return BitConverter.Int64BitsToDouble(d);
		}

		public override double Next(double t)
		{
			return Extreme.FloatingPoint.FloatingPointModule.NextAfter(t, double.MaxValue);
		}

	}
}