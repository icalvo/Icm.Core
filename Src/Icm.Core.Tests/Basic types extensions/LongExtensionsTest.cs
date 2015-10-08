
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm;

[TestFixture(), Category("Icm")]
public class LongExtensionsTest
{

	[TestCase(34L)]
	[TestCase(5L)]
	[TestCase(201L)]
	[TestCase(0L)]
	[TestCase(-1L, ExpectedException = typeof(ArgumentOutOfRangeException))]
	public void HumanFileSize1_Test(long target)
	{
		Assert.That(target.HumanFileSize(), Is.EqualTo(target.HumanFileSize(decimalUnits: true, bigUnitNames: false, format: "0.00")));
	}

	[TestCase(34L, true, false, null, Result = "34 B")]
	[TestCase(5L, false, false, null, Result = "5 B")]
	[TestCase(201L, true, true, "F2", Result = "201,00 bytes")]
	[TestCase(0L, true, false, null, Result = "0 B")]
	[TestCase(-1L, true, false, null, ExpectedException = typeof(ArgumentOutOfRangeException))]
	public string HumanFileSize_Test(long target, bool decimalUnits, bool bigUnitNames, string format)
	{
		return target.HumanFileSize(decimalUnits, bigUnitNames, format);
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
