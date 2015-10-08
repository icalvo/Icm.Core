
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Functions
{

	public class FunctionPoint<TX, TY> where TX : struct, IComparable<TX> where TY : struct, IComparable<TY>
	{
		private TX x_;
		private TY y_;

		private readonly IMathFunction<TX, TY> función_;
		public TX X {
			get { return x_; }
			set {
				x_ = value;
				y_ = MathFunction(x_);
			}
		}

		public TY Y {
			get { return y_; }
		}

		public IMathFunction<TX, TY> MathFunction {
			get { return función_; }
		}

		public FunctionPoint(TX px, IMathFunction<TX, TY> f)
		{
			función_ = f;
			X = px;
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
