
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

namespace Icm.IO
{

	public static class TextReaderExtensions
	{

		[Extension()]
		public static string Format(TextReader tr, params object[] args)
		{
			dynamic template = tr.ReadToEnd;
			tr.Close();
			return string.Format(CultureInfo.CurrentCulture, template, args);
		}

	}

}


//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
