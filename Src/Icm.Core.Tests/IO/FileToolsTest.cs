
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.IO;
using System.IO;

[TestFixture(), Category("Icm")]
public class FileToolsTest
{

	///<summary>
	///A test for FormatFile
	///</summary>
	[Test()]

	public void FormatFileTest2()
	{
		//Caso 1
		StringReader template = new StringReader("la casa {0} esta en la calle {1}");
		object[] args = {
			"roja",
			"Luna"
		};
		string expected = "la casa roja esta en la calle Luna";
		string actual = null;
		actual = FileTools.FormatFile(template, args);
		Assert.AreEqual(expected, actual);

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
