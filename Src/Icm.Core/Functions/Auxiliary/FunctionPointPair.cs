using System;

namespace Icm.Functions
{

	public class FunctionPointPair<TX, TY> : Tuple<FunctionPoint<TX, TY>, FunctionPoint<TX, TY>> where TX : struct, IComparable<TX> where TY : struct, IComparable<TY>
	{

		public TX X1 => Item1.X;
	    public TY Y1 => Item1.Y;
	    public TX X2 => Item2.X;
	    public TY Y2 => Item2.Y;
	    public FunctionPoint<TX, TY> P1 => Item1;
	    public FunctionPoint<TX, TY> P2 => Item2;

	    public FunctionPointPair(IMathFunction<TX, TY> fun) : base(
            new FunctionPoint<TX, TY>(default(TX), fun), 
            new FunctionPoint<TX, TY>(default(TX), fun))
		{
		}

		public FunctionPointPair(IMathFunction<TX, TY> fun, TX px1, TX px2) : base(
            new FunctionPoint<TX, TY>(px1, fun), 
            new FunctionPoint<TX, TY>(px2, fun))
		{
		}
	}
}