
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.IO;
using System.IO;

[TestFixture(), Category("Icm")]
public class TextReaderExtensionsTest
{

	///<summary>
	///A test for FormatFile
	///</summary>
	[TestCase("la casa {0} esta en la calle {1}", {
		"roja",
		"Luna"
	}, Result = "la casa roja esta en la calle Luna")]
	public string Format_Test(string format, object[] args)
	{
		StringReader template = new StringReader(format);
		return template.Format(args);
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
