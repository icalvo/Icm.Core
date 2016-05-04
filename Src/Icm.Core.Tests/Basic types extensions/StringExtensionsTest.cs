
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm;
using NUnit.Framework;

[TestFixture(), Category("Icm")]
public class StringExtensionsTest
{

	[TestCase("bLioNNpPA", ExpectedResult = "BLioNNpPA")]
	[TestCase("BaMBu", ExpectedResult = "BaMBu")]
	[TestCase("áNGeL", ExpectedResult = "ÁNGeL")]
	[TestCase("78a", ExpectedResult = "78a")]
	[TestCase("a1", ExpectedResult = "A1")]
	[TestCase("", ExpectedResult = "")]
	[TestCase(null, ExpectedResult = "")]
	public string ToUpperFirst_Test(string s)
	{
		return s.ToUpperFirst();
	}

	[TestCase("aa", 3, ExpectedResult = "aaaaaa")]
	[TestCase("", 3, ExpectedResult = "")]
	[TestCase("aa", 0, ExpectedResult = "")]
	[TestCase("aa", -5, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase(null, 3, ExpectedException = typeof(NullReferenceException))]
	public string Repeat_Test(string s, int count)
	{
		return s.Repeat(count);
	}


	[TestCase("Maria", 3, ExpectedResult = "Mar")]
	[TestCase("M12a", 3, ExpectedResult = "M12")]
	[TestCase("Maria", 0, ExpectedResult = "")]
	[TestCase("Maria", 12, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("Maria", -12, ExpectedException = typeof(ArgumentOutOfRangeException))]
	public string Left_Test(string target, int length)
	{
		return target.Left(length);
	}

	[TestCase("ViernesLunes", 2, 8, ExpectedResult = "ernesLu")]
	[TestCase("ViernesLunes", 0, 0, ExpectedResult = "V")]
	[TestCase("ViernesLunes", 5, 4, ExpectedResult = "")]
	[TestCase("ViernesLunes", 0, -1, ExpectedResult = "")]
	[TestCase("ViernesLunes", 5, 3, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("ViernesLunes", -4, 6, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("ViernesLunes", 4, -6, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("ViernesLunes", 20, 21, ExpectedException = typeof(ArgumentOutOfRangeException))]
	public string Med_Test(string target, int startIdx, int endIdx)
	{
		return target.Med(startIdx, endIdx);
	}

	[TestCase("LunesMartes", 1, 3, ExpectedResult = "unesMar")]
	[TestCase("LunesMartes", 0, 0, ExpectedResult = "LunesMartes")]
	[TestCase("LunesMartes", 5, 6, ExpectedResult = "")]
	[TestCase("LunesMartes", 5, 7, ExpectedException = typeof(ArgumentOutOfRangeException), Description = "Overlapped lengths")]
	[TestCase("LunesMartes", -1, 3, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("LunesMartes", 1, -3, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("LunesMartes", 20, 3, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("LunesMartes", 2, 23, ExpectedException = typeof(ArgumentOutOfRangeException))]
	public string SkipBoth_Test(string target, int startLength, int endLength)
	{
		return target.SkipBoth(startLength, endLength);
	}

	[TestCase("Maria", 3, ExpectedResult = "ria")]
	[TestCase("M12a", 3, ExpectedResult = "12a")]
	[TestCase("Maria", 0, ExpectedResult = "")]
	[TestCase("Maria", 12, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("Maria", -12, ExpectedException = typeof(ArgumentOutOfRangeException))]
	public string Right_Test(string target, int length)
	{
		return target.Right(length);
	}

	///<summary>
	///A test for SurroundedBy
	///</summary>
	[TestCase("LaCasaBonita", "La", "Bonita", ExpectedResult = true)]
	[TestCase("LaCasaBonita", "Li", "Bonita", ExpectedResult = false)]
	[TestCase("LaCasaBonita", "La", "Fea", ExpectedResult = false)]
	public bool SurroundedBy_Test(string target, string startString, string endString)
	{
		return target.SurroundedBy(startString, endString);
	}

	[TestCase("qwer", "subst", ExpectedResult = "qwer")]
	[TestCase("", "subst", ExpectedResult = "subst")]
	[TestCase(null, "subst", ExpectedResult = null)]
	public string IfEmpty_Test(string target, string ifEmptyString)
	{
		return target.IfEmpty(ifEmptyString);
	}

	[TestCase("qwer", ExpectedResult = false)]
	[TestCase("", ExpectedResult = true)]
	[TestCase(null, ExpectedResult = false)]
	public bool IsEmpty_Test(string target)
	{
		return target.IsEmpty();
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
