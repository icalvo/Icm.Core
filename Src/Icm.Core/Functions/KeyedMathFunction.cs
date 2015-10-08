using System;

namespace Icm.Functions
{

	/// <summary>
	/// Functions by key that allow modification of key points (add and delete)
	/// </summary>
	/// <typeparam name="TX">Domain</typeparam>
	/// <typeparam name="TY">Image</typeparam>
	/// <remarks></remarks>
	public abstract class KeyedMathFunction<TX, TY> : BaseKeyedMathFunction<TX, TY> where TX : struct, IComparable<TX> where TY : struct, IComparable<TY>
	{

		protected KeyedMathFunction(TY initialValue, ITotalOrder<TX> otx, ITotalOrder<TY> oty, ISortedCollection<TX, TY> coll) : base(otx, oty, coll)
		{
			KeyStore.Add(LstX, initialValue);
		}

		public void Forzar(TX d, TY v)
		{
			KeyStore(d) = v;
		}

		// This introduces one interpolated function point as a key.
		public void Forzar(TX d)
		{
			KeyStore(d) = V(d);
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
