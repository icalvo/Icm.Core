using System;

namespace Icm.Functions
{
	public class FunctionPoint<TX, TY> where TX : struct, IComparable<TX> where TY : struct, IComparable<TY>
	{
		private TX _x;
		private TY _y;

	    public TX X {
			get { return _x; }
			set {
				_x = value;
				_y = MathFunction[_x];
			}
		}

		public TY Y => _y;

	    public IMathFunction<TX, TY> MathFunction { get; }

	    public FunctionPoint(TX px, IMathFunction<TX, TY> f)
		{
			MathFunction = f;
			X = px;
		}
	}
}