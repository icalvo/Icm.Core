namespace Icm
{
	public class LongTotalOrder : BaseTotalOrder<long>
	{

		public override long Least()
		{
			return long.MinValue;
		}

		public override long Long2T(long d)
		{
			return d;
		}

		public override long Greatest()
		{
			return long.MaxValue;
		}

		public override long T2Long(long t)
		{
			return t;
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
