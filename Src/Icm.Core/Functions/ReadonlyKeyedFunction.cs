
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Functions
{


	/// <summary>
	/// FunciónClavesSóloLectura es un intermediario que permite acceso de sólo lectura a cualquier objeto que herede
	/// de FunciónClavesBase.
	/// </summary>
	/// <typeparam name="TX"></typeparam>
	/// <typeparam name="TY"></typeparam>
	/// <remarks>
	/// <para>Esta función deberá usarse cada vez que queramos manejar un objeto de tipo FunciónClavesBase sólo para
	/// obtener datos y no para escribir o borrar claves. Para ello, pasaremos ese objeto a su constructor y utilizaremos
	/// la instancia de FunciónClavesSóloLectura en lugar del original. Como FunciónClavesSóloLectura hereda de
	/// FunciónClavesBase, podemos emplear todos los métodos públicos de esta última clase.</para>
	/// <code>
	/// Dim original As FunciónClaves(Of Date, Double)
	/// 
	/// Dim intermediarioSóloLectura As New FunciónClavesSóloLectura(Of Date, Double)(original)
	/// 
	/// ' Estas llamadas producen fallos de compilación, pues FunciónClavesSóloLectura no dispone de ellas:
	/// intermediarioSóloLectura.AñadirClave(#01/02/2008#, 45.2)
	/// intermediarioSóloLectura.EliminarClave(#01/02/2008#)
	/// </code>
	/// </remarks>
	public sealed class ReadonlyKeyedFunction<TX, TY> : BaseKeyedMathFunction<TX, TY> where TX : struct, IComparable<TX> where TY : struct, IComparable<TY>
	{


		private IKeyedMathFunction<TX, TY> fc_;
		public ReadonlyKeyedFunction(IKeyedMathFunction<TX, TY> fc) : base(fc)
		{
			fc_ = fc;
		}


		public override TY this[TX d] {
			get { return fc_.V(d); }
		}

		public override IMathFunction<TX, TY> EmptyClone()
		{
			return fc_.EmptyClone;
		}

		public override FunctionPoint<TX, TY> MaxXY(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
		{
			return fc_.MaxXY(rangeStart, rangeEnd, tumbral, cantidad);
		}

		public override FunctionPoint<TX, TY> MaxXYu(TX rangeStart, TX rangeEnd, ThresholdType tumbral, Func<TX, TY> fnUmbral)
		{
			return fc_.MaxXYu(rangeStart, rangeEnd, tumbral, fnUmbral);
		}

		public override FunctionPoint<TX, TY> MinXY(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
		{
			return fc_.MinXY(rangeStart, rangeEnd, tumbral, cantidad);
		}

		public override FunctionPoint<TX, TY> MinXYu(TX rangeStart, TX rangeEnd, ThresholdType tumbral, Func<TX, TY> fnUmbral)
		{
			return fc_.MinXYu(rangeStart, rangeEnd, tumbral, fnUmbral);
		}

		public override FunctionPoint<TX, TY> FstXYu(TX rangeStart, TX rangeEnd, ThresholdType tumbral, System.Func<TX, TY> fnUmbral)
		{
			return fc_.FstXYu(rangeStart, rangeEnd, tumbral, fnUmbral);
		}

		public override FunctionPoint<TX, TY> FstXY(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
		{
			return fc_.FstXY(rangeStart, rangeEnd, tumbral, cantidad);
		}

		public override FunctionPoint<TX, TY> LstXY(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
		{
			return fc_.LstXY(rangeStart, rangeEnd, tumbral, cantidad);
		}

		public override FunctionPoint<TX, TY> LstXYu(TX rangeStart, TX rangeEnd, ThresholdType tumbral, Func<TX, TY> fnUmbral)
		{
			return fc_.LstXYu(rangeStart, rangeEnd, tumbral, fnUmbral);
		}

		public override FunctionPoint<TX, TY> AbsMaxXY()
		{
			return fc_.AbsMaxXY;
		}

		public override FunctionPoint<TX, TY> AbsMinXY()
		{
			return fc_.AbsMinXY;
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
