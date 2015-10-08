namespace Icm
{

	/// <summary>
	/// Modifiable Tuple with default constructor.
	/// </summary>
	/// <typeparam name="T1"></typeparam>
	/// <typeparam name="T2"></typeparam>
	/// <typeparam name="T3"></typeparam>
	/// <remarks>
	/// System.Tuple has no default constructor and its properties are read-only.
	/// </remarks>    
	public class Tuple3<T1, T2, T3>
	{

		public Tuple3(T1 f, T2 s, T3 t)
		{
			First = f;
			Second = s;
			Third = t;
		}

		public T1 First { get; set; }

		public T2 Second { get; set; }

		public T3 Third { get; set; }
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
