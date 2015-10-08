namespace Icm
{

	/// <summary>
	/// Modifiable Tuple with default constructor.
	/// </summary>
	/// <typeparam name="T1"></typeparam>
	/// <typeparam name="T2"></typeparam>
	/// <typeparam name="T3"></typeparam>
	/// <typeparam name="T4"></typeparam>
	/// <remarks>
	/// System.Tuple has no default constructor and its properties are read-only.
	/// </remarks>    
	public class Tuple4<T1, T2, T3, T4>
	{

		public Tuple4(T1 f, T2 s, T3 t, T4 fo)
		{
			First = f;
			Second = s;
			Third = t;
			Fourth = fo;
		}

		public T1 First { get; set; }

		public T2 Second { get; set; }

		public T3 Third { get; set; }

		public T4 Fourth { get; set; }
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
