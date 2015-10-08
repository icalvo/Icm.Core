namespace Icm
{

	public struct ParseError
	{

		private readonly int _index;
		private readonly int _startIndex;

		private readonly int _code;
		public ParseError(int code, int index, int errorStartIndex)
		{
			_code = code;
			_index = index;
			_startIndex = errorStartIndex;
		}

		public int Code {
			get { return _code; }
		}

		public int Index {
			get { return _index; }
		}

		public int StartIndex {
			get { return _startIndex; }
		}

		public override string ToString()
		{
			return string.Format("Parse error code {0} at {1}, starting at {2}", Code, Index, StartIndex);
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
