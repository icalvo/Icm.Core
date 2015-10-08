using System;
using System.Collections;
using System.Collections.Generic;

namespace Icm.Text
{

	/// <summary>
	/// Returns the natural numbers, starting with 1
	/// </summary>
	/// <remarks></remarks>
	public class AutoNumberGenerator : IEnumerator<string>
	{



		private int counter_;
		public AutoNumberGenerator()
		{
			counter_ = 0;
		}


		public string Current {
			get { return Convert.ToString(counter_); }
		}

		public object Current1 {
			get { return Current; }
		}
		object IEnumerator.Current {
			get { return Current1; }
		}

		public bool MoveNext()
		{
			counter_ = counter_ + 1;
			return false;
		}

		public void Reset()
		{
			throw new NotImplementedException();
		}


		public void Dispose()
		{
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
