
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm;
using NUnit.Framework;

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
	[TestCaseSource(nameof(IfNothingTestCases))]
	public string IfNothing_Test(object target, string subst)
	{
		return ObjectExtensions.IfNothing(target, subst);
	}

	[TestCase({
		"hola",
		"maria",
		"pato",
		"perro"
	}, "hola", ExpectedResult = true)]
	[TestCase({
		"hola",
		"maria",
		"pato",
		"perro"
	}, "adios", ExpectedResult = false)]
	[TestCase({
		"hola",
		"maria",
		"pato",
		"perro"
	}, null, ExpectedResult = false)]
	[TestCase(new string[], "hola", ExpectedResult = false)]
	[TestCase(null, "hola", ExpectedResult = false)]
	[TestCase(null, null, ExpectedResult = false)]
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
