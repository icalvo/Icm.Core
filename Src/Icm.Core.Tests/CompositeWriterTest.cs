
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.IO.StringWriter;
using Icm.IO;

[TestFixture(), Category("Icm")]
public class CompositeWriterTest
{

	///<summary>
	///A test for WriteLine
	///</summary>
	[Test()]
	public void Write_Test()
	{
		CompositeWriter target = new CompositeWriter();
		dynamic s1 = "hola";
		dynamic s2 = "quetal";
		dynamic s3 = "adios";
		StringWriter sw1 = new StringWriter();
		StringWriter sw2 = new StringWriter();
		StringWriter sw3 = new StringWriter();

		target.Add(sw1);
		target.Write(s1);

		Assert.That(sw1.ToString == "hola");

		target.Add(sw2);
		target.Write(s2);

		Assert.That(sw1.ToString == "holaquetal");
		Assert.That(sw2.ToString == "quetal");

		target.Add(sw3);
		target.Write(s3);
		Assert.That(sw1.ToString == "holaquetaladios");
		Assert.That(sw2.ToString == "quetaladios");
		Assert.That(sw3.ToString == "adios");
		target.Close();
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
