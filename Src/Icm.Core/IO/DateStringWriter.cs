using System;
using System.Globalization;
using System.IO;

namespace Icm.IO
{

	public class DateStringWriter : StringWriter
	{

		public DateStringWriter() : base(CultureInfo.InvariantCulture)
		{
		}

		public override void WriteLine(string value)
		{
			base.WriteLine("{0:dd/MM/yyyy HH:mm:ss} {1}", DateTime.Now, value);
		}

	}

}