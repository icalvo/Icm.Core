
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
[TestFixture()]
public class BooleanExtensionsTest
{

	[TestCase(true, Result = 1)]
	[TestCase(false, Result = 0)]
	public int ToInteger_Test(bool @bool)
	{
		return @bool.ToInteger;
	}

	[TestCase(true, "cadena verdad", "cadena falso", "cadena nulo", Result = "cadena verdad")]
	[TestCase(false, "cadena verdad", "cadena falso", "cadena nulo", Result = "cadena falso")]
	[TestCase(null, "cadena verdad", "cadena falso", "cadena nulo", Result = "cadena nulo")]
	public string IfN_Test(bool? @bool, string trueString, string falseString, string nullString)
	{
		return @bool.IfN(trueString, falseString, nullString);
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
