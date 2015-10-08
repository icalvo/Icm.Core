using System;
using System.IO;

namespace Icm.IO
{

	/// <summary>
	///   TextWriter that does do nothing (similar to /dev/null)
	/// </summary>
	/// <remarks>
	///   Useful when someone needs a TextWriter object (and does not like Nothing),
	/// but we don't want to write anything.
	/// </remarks>
	/// <history>
	/// 	[icalvo]	02/12/2005	Created
	///     [icalvo]    05/04/2005  Documentation
	/// </history>
	public class NullWriter : TextWriter
	{

		protected NullWriter() : base(CultureInfo.InvariantCulture)
		{
		}

		protected NullWriter(IFormatProvider formatProvider) : base(formatProvider)
		{
		}

		public override System.Text.Encoding Encoding {
			get { return System.Text.Encoding.Default; }
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
