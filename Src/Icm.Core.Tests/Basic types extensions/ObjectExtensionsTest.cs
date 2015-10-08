
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
[TestFixture()]
public class ObjectExtensionsTest
{

	static readonly object[] IfNothingTestCases = {
		new TestCaseData("value", "subst").Returns("value"),
		new TestCaseData(null, "subst").Returns("subst"),
		new TestCaseData("value", null).Returns("value"),
		new TestCaseData(null, null).Returns(null),
		new TestCaseData(DBNull.Value, "subst").Returns("subst")

	};
	[TestCaseSource("IfNothingTestCases")]
	public string IfNothing_Test(object target, string subst)
	{
		return ObjectExtensions.IfNothing(target, subst);
	}

	[TestCase({
		"hola",
		"maria",
		"pato",
		"perro"
	}, "hola", Result = true)]
	[TestCase({
		"hola",
		"maria",
		"pato",
		"perro"
	}, "adios", Result = false)]
	[TestCase({
		"hola",
		"maria",
		"pato",
		"perro"
	}, null, Result = false)]
	[TestCase(new string[], "hola", Result = false)]
	[TestCase(null, "hola", Result = false)]
	[TestCase(null, null, Result = false)]
	public bool IsOneOf_Test(string[] sa, string s)
	{
		return s.IsOneOf(sa);
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
