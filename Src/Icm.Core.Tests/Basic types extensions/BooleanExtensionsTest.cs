
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using NUnit.Framework;
using Icm;

[TestFixture()]
public class BooleanExtensionsTest
{

	[TestCase(true, ExpectedResult = 1)]
	[TestCase(false, ExpectedResult = 0)]
	public int ToInteger_Test(bool @bool)
	{
		return @bool.ToInteger();
	}

	[TestCase(true, "cadena verdad", "cadena falso", "cadena nulo", ExpectedResult = "cadena verdad")]
	[TestCase(false, "cadena verdad", "cadena falso", "cadena nulo", ExpectedResult = "cadena falso")]
	[TestCase(null, "cadena verdad", "cadena falso", "cadena nulo", ExpectedResult = "cadena nulo")]
	public string IfN_Test(bool? @bool, string trueString, string falseString, string nullString)
	{
		return @bool.IfN(trueString, falseString, nullString);
	}

}
