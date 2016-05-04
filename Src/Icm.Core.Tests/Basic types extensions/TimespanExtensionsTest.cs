
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm;
using NUnit.Framework;

[TestFixture(), Category("Icm")]
public class TimespanExtensionsTest
{

	static readonly object[] DividedByTestCases = {
		new TestCaseData(TimeSpan.FromHours(3), 3).Returns(TimeSpan.FromHours(1)),
		new TestCaseData(TimeSpan.FromHours(-3), 3).Returns(TimeSpan.FromHours(-1)),
		new TestCaseData(TimeSpan.FromHours(3), -3).Returns(TimeSpan.FromHours(-1)),
		new TestCaseData(TimeSpan.Zero, 3).Returns(TimeSpan.Zero),
		new TestCaseData(TimeSpan.FromHours(3), 0).Throws(typeof(OverflowException))

	};
	[TestCaseSource(nameof(DividedByTestCases))]
	public TimeSpan DividedBy_Test(TimeSpan span, float divisor)
	{
		return span.DividedBy(divisor);
	}

	static readonly object[] IsZeroTestCases = {
		new TestCaseData(TimeSpan.FromHours(3)).Returns(false),
		new TestCaseData(TimeSpan.FromHours(-3)).Returns(false),
		new TestCaseData(TimeSpan.Zero).Returns(true)

	};
	[TestCaseSource(nameof(IsZeroTestCases))]
	public bool IsZero_Test(TimeSpan target)
	{
		return target.IsZero();
	}

	static readonly object[] IsNotZeroTestCases = {
		new TestCaseData(TimeSpan.FromHours(3)).Returns(true),
		new TestCaseData(TimeSpan.FromHours(-3)).Returns(true),
		new TestCaseData(TimeSpan.Zero).Returns(false)

	};
	[TestCaseSource(nameof(IsNotZeroTestCases))]
	public bool IsNotZero_Test(TimeSpan target)
	{
		return target.IsNotZero();
	}

	static readonly object[] ToAbbrevTestCases = {
		new TestCaseData(new TimeSpan(2, 14, 2)).Returns("2h14'2''"),
		new TestCaseData(new TimeSpan(0, 0, 2)).Returns("2''"),
		new TestCaseData(TimeSpan.Zero).Returns("0"),
		new TestCaseData(new TimeSpan(-5, 14, 18)).Returns("-4h45'42''")

	};
	[TestCaseSource(nameof(ToAbbrevTestCases))]
	public string ToAbbrev_Test(TimeSpan target)
	{
		return target.ToAbbrev;
	}

	static readonly object[] ToHHmmTestCases = {
		new TestCaseData(new TimeSpan(2, 14, 18)).Returns("02:14"),
		new TestCaseData(new TimeSpan(0, 0, 2)).Returns("00:00"),
		new TestCaseData(TimeSpan.Zero).Returns("00:00"),
		new TestCaseData(new TimeSpan(-5, 14, 18)).Returns("-04:45")

	};
	[TestCaseSource(nameof(ToHHmmTestCases))]
	public string ToHHmm_Test(TimeSpan target)
	{
		return target.ToHHmm;
	}

	static readonly object[] ToHHmmssTestCases = {
		new TestCaseData(new TimeSpan(2, 14, 18)).Returns("02:14:18"),
		new TestCaseData(new TimeSpan(0, 0, 2)).Returns("00:00:02"),
		new TestCaseData(TimeSpan.Zero).Returns("00:00:00"),
		new TestCaseData(new TimeSpan(-5, 14, 18)).Returns("-04:45:42")

	};
	[TestCaseSource(nameof(ToHHmmssTestCases))]
	public string ToHHmmss_Test(TimeSpan target)
	{
		return target.ToHHmmss;
	}

	static readonly object[] TommsstttTestCases = {
		new TestCaseData(new TimeSpan(2, 14, 18, 25)).Returns("3738:25.000"),
		new TestCaseData(new TimeSpan(0, 0, 2)).Returns("00:02.000"),
		new TestCaseData(TimeSpan.Zero).Returns("00:00.000"),
		new TestCaseData(new TimeSpan(-5, 14, 18)).Returns("-285:42.000")

	};
	[TestCaseSource(nameof(TommsstttTestCases))]
	public string Tommssttt_Test(TimeSpan target)
	{
		return target.Tommssttt;
	}

	static readonly object[] ToHHmmsstttTestCases = {
		new TestCaseData(new TimeSpan(2, 14, 18, 25)).Returns("62:18:25.000"),
		new TestCaseData(new TimeSpan(0, 0, 2)).Returns("00:00:02.000"),
		new TestCaseData(TimeSpan.Zero).Returns("00:00:00.000"),
		new TestCaseData(new TimeSpan(-5, 14, 18)).Returns("-04:45:42.000")

	};
	[TestCaseSource(nameof(ToHHmmsstttTestCases))]
	public string ToHHmmssttt_Test(TimeSpan target)
	{
		return target.ToHHmmssttt;
	}

	static readonly object[] TotalMicrosecondsTestCases = {
		new TestCaseData(new TimeSpan(2, 14, 18, 25)).Returns(224305000000L),
		new TestCaseData(new TimeSpan(0, 0, 0, 2)).Returns(2000000),
		new TestCaseData(TimeSpan.Zero).Returns(0),
		new TestCaseData(new TimeSpan(-5, 14, 18)).Returns(-17142000000L)

	};
	[TestCaseSource(nameof(TotalMicrosecondsTestCases))]
	public long TotalMicroseconds_Test(TimeSpan target)
	{
		return target.TotalMicroseconds;
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
