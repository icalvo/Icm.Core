namespace Icm.Tree
{

	public class TraverseResult<T>
	{

		public TraverseResult(T result, int level)
		{
			this.Result = result;
			this.Level = level;
		}

		public T Result { get; set; }

		public int Level { get; set; }

	}

	public class TraverseResult
	{

		public static TraverseResult<T> Create<T>(T result, int level)
		{
			return new TraverseResult<T>(result, level);
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
