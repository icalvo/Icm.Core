
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Collections;

[Category("Icm")]
[TestFixture()]
public class JoinStrExtensionsTest
{

	[TestCase({
		"a",
		"b",
		"c"
	}, ";", "a;b;c")]
	[TestCase({
		"a",
		"b",
		"c"
	}, "", "abc")]
	[TestCase({
		"a",
		"b",
		"c"
	}, null, "abc")]
	[TestCase({
		"a",
		null,
		"c"
	}, ";", "a;c")]
	[TestCase({
		"a",
		"",
		"c"
	}, ";", "a;c")]
	[TestCase({ "asdf" }, ";", "asdf")]
	[TestCase(new string[], ";", "")]
	[TestCase(null, ";", "", ExpectedException = typeof(ArgumentNullException))]
	public void JoinStr1_Test(IEnumerable<string> col, string separator, string expected)
	{
		string actual = null;

		actual = col.JoinStr(separator);
		Assert.That(expected, Is.EqualTo(actual));
	}

	[TestCase(new string[], "")]
	[TestCase({ "a" }, "a")]
	[TestCase({
		"a",
		"b"
	}, "a y b")]
	[TestCase({
		"a",
		"b",
		"c"
	}, "a, b y c")]
	public void JoinStr2_Test(IEnumerable<string> col, string expected)
	{
		Assert.That(col.JoinStr(", ", " y "), Is.EqualTo(expected));
	}

}


//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
