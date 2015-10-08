
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
			base.WriteLine("{0:dd/MM/yyyy HH:mm:ss} {1}", Now, value);
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
