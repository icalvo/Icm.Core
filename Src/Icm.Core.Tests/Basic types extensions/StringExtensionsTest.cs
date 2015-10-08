
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm;

[TestFixture(), Category("Icm")]
public class StringExtensionsTest
{

	[TestCase("bLioNNpPA", Result = "BLioNNpPA")]
	[TestCase("BaMBu", Result = "BaMBu")]
	[TestCase("áNGeL", Result = "ÁNGeL")]
	[TestCase("78a", Result = "78a")]
	[TestCase("a1", Result = "A1")]
	[TestCase("", Result = "")]
	[TestCase(null, Result = "")]
	public string ToUpperFirst_Test(string s)
	{
		return s.ToUpperFirst();
	}

	[TestCase("aa", 3, Result = "aaaaaa")]
	[TestCase("", 3, Result = "")]
	[TestCase("aa", 0, Result = "")]
	[TestCase("aa", -5, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase(null, 3, ExpectedException = typeof(NullReferenceException))]
	public string Repeat_Test(string s, int count)
	{
		return s.Repeat(count);
	}


	[TestCase("Maria", 3, Result = "Mar")]
	[TestCase("M12a", 3, Result = "M12")]
	[TestCase("Maria", 0, Result = "")]
	[TestCase("Maria", 12, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("Maria", -12, ExpectedException = typeof(ArgumentOutOfRangeException))]
	public string Left_Test(string target, int length)
	{
		return target.Left(length);
	}

	[TestCase("ViernesLunes", 2, 8, Result = "ernesLu")]
	[TestCase("ViernesLunes", 0, 0, Result = "V")]
	[TestCase("ViernesLunes", 5, 4, Result = "")]
	[TestCase("ViernesLunes", 0, -1, Result = "")]
	[TestCase("ViernesLunes", 5, 3, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("ViernesLunes", -4, 6, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("ViernesLunes", 4, -6, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("ViernesLunes", 20, 21, ExpectedException = typeof(ArgumentOutOfRangeException))]
	public string Med_Test(string target, int startIdx, int endIdx)
	{
		return target.Med(startIdx, endIdx);
	}

	[TestCase("LunesMartes", 1, 3, Result = "unesMar")]
	[TestCase("LunesMartes", 0, 0, Result = "LunesMartes")]
	[TestCase("LunesMartes", 5, 6, Result = "")]
	[TestCase("LunesMartes", 5, 7, ExpectedException = typeof(ArgumentOutOfRangeException), Description = "Overlapped lengths")]
	[TestCase("LunesMartes", -1, 3, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("LunesMartes", 1, -3, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("LunesMartes", 20, 3, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("LunesMartes", 2, 23, ExpectedException = typeof(ArgumentOutOfRangeException))]
	public string SkipBoth_Test(string target, int startLength, int endLength)
	{
		return target.SkipBoth(startLength, endLength);
	}

	[TestCase("Maria", 3, Result = "ria")]
	[TestCase("M12a", 3, Result = "12a")]
	[TestCase("Maria", 0, Result = "")]
	[TestCase("Maria", 12, ExpectedException = typeof(ArgumentOutOfRangeException))]
	[TestCase("Maria", -12, ExpectedException = typeof(ArgumentOutOfRangeException))]
	public string Right_Test(string target, int length)
	{
		return target.Right(length);
	}

	///<summary>
	///A test for SurroundedBy
	///</summary>
	[TestCase("LaCasaBonita", "La", "Bonita", Result = true)]
	[TestCase("LaCasaBonita", "Li", "Bonita", Result = false)]
	[TestCase("LaCasaBonita", "La", "Fea", Result = false)]
	public bool SurroundedBy_Test(string target, string startString, string endString)
	{
		return target.SurroundedBy(startString, endString);
	}

	[TestCase("qwer", "subst", Result = "qwer")]
	[TestCase("", "subst", Result = "subst")]
	[TestCase(null, "subst", Result = null)]
	public string IfEmpty_Test(string target, string ifEmptyString)
	{
		return target.IfEmpty(ifEmptyString);
	}

	[TestCase("qwer", Result = false)]
	[TestCase("", Result = true)]
	[TestCase(null, Result = false)]
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
