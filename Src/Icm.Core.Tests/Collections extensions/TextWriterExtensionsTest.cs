
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using Icm;
using Icm.IO;
using NUnit.Framework;

[TestFixture(), Category("Icm")]
public class TextWriterExtensionsTest
{

	[Test()]
	public void WriteUnderlineTest()
	{
		TextWriter tw = new StringWriter();
		dynamic s = "hola";
		tw.WriteUnderline(s);

		dynamic actual = tw.ToString;

		dynamic expected = "hola" + Constants.vbCrLf + "----" + Constants.vbCrLf;

		Assert.That(actual, Is.EqualTo(expected));
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
