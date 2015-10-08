namespace Icm
{
	public static class ModifiableTuplesFactory
	{

		public static Pair<T1, T2> NewPair<T1, T2>(T1 o1, T2 o2)
		{
			return new Pair<T1, T2>(o1, o2);
		}

		public static Tuple3<T1, T2, T3> NewTuple3<T1, T2, T3>(T1 o1, T2 o2, T3 o3)
		{
			return new Tuple3<T1, T2, T3>(o1, o2, o3);
		}

		public static Tuple4<T1, T2, T3, T4> NewTuple4<T1, T2, T3, T4>(T1 o1, T2 o2, T3 o3, T4 o4)
		{
			return new Tuple4<T1, T2, T3, T4>(o1, o2, o3, o4);
		}


	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
