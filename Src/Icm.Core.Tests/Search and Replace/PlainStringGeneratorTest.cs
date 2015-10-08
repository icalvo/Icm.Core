
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Text;

[TestFixture(), Category("Icm")]
public class PlainStringGeneratorTest
{

	[Test()]
	public void PlainStringGenerator_Test()
	{
		string s = "HOLA";
		PlainStringGenerator target = new PlainStringGenerator(s);

		dynamic i = 0;

		foreach (void element_loopVariable in target) {
			element = element_loopVariable;
			Assert.That(element, Is.EqualTo(s));
			i += 1;
			if (i == 20)
				break; // TODO: might not be correct. Was : Exit For
		}
	}


}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
