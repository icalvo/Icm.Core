using System.Collections;
using System.Collections.Generic;

namespace Icm.Text
{

	/// <summary>
	/// Returns always the string passed to the constructor.
	/// </summary>
	/// <remarks></remarks>
	public class PlainStringGenerator : IEnumerator<string>, IEnumerable<string>
	{


		private readonly string s_;
		public PlainStringGenerator(string s)
		{
			s_ = s;
		}

		protected PlainStringGenerator()
		{
		}

		public string Current {
			get { return s_; }
		}

		public object Current1 {
			get { return Current; }
		}
		object IEnumerator.Current {
			get { return Current1; }
		}

		public bool MoveNext()
		{
			return false;
		}

		public void Reset()
		{
		}


		public void Dispose()
		{
		}

		public IEnumerator<string> GetEnumerator()
		{
			return this;
		}

		public IEnumerator GetEnumerator1()
		{
			return this;
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator1();
		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
