
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
[TestFixture()]
public class DateTimeIntervalTest
{

	object[] Constructor1TestCases = {
		new TestCaseData(new System.DateTime(2010, 4, 5)),
		new TestCaseData(System.DateTime.MaxValue).Throws(typeof(ArgumentOutOfRangeException)),
		new TestCaseData(System.DateTime.MinValue)
	};
	[TestCaseSource("Constructor1TestCases")]
	public void Constructor1_Test(System.DateTime point)
	{
		DateTimeInterval dti = new DateTimeInterval(point);

		Assert.That(dti.Start, Is.EqualTo(point));
		Assert.That(dti.End, Is.EqualTo(point));
	}

	object[] Constructor2TestCases = {
		new TestCaseData(new System.DateTime(2010, 4, 5), new System.DateTime(2010, 4, 7)),
		new TestCaseData(new System.DateTime(2010, 4, 5), new System.DateTime(2010, 4, 5)),
		new TestCaseData(new System.DateTime(2010, 4, 5), new System.DateTime(2010, 4, 3)).Throws(typeof(ArgumentException)),
		new TestCaseData(System.DateTime.MaxValue, System.DateTime.MaxValue).Throws(typeof(ArgumentOutOfRangeException)),
		new TestCaseData(System.DateTime.MinValue, new System.DateTime(2010, 4, 7))
	};
	[TestCaseSource("Constructor2TestCases")]
	public void Constructor2_Test(System.DateTime startInt, System.DateTime endInt)
	{
		DateTimeInterval dti = new DateTimeInterval(startInt, endInt);

		Assert.That(dti.Start, Is.EqualTo(startInt));
		Assert.That(dti.End, Is.EqualTo(endInt));
	}

	object[] Constructor3TestCases = {
		new TestCaseData(new System.DateTime(2010, 4, 5), TimeSpan.FromDays(2), new System.DateTime(2010, 4, 7)),
		new TestCaseData(new System.DateTime(2010, 4, 5), TimeSpan.Zero, new System.DateTime(2010, 4, 5)),
		new TestCaseData(new System.DateTime(2010, 4, 5), TimeSpan.FromDays(-2), new System.DateTime(2010, 4, 3)).Throws(typeof(ArgumentOutOfRangeException)),
		new TestCaseData(System.DateTime.MaxValue, TimeSpan.Zero, System.DateTime.MaxValue).Throws(typeof(ArgumentOutOfRangeException)),
		new TestCaseData(System.DateTime.MinValue, TimeSpan.FromDays(2), new System.DateTime(1, 1, 3))
	};
	[TestCaseSource("Constructor3TestCases")]
	public void Constructor3_Test(System.DateTime startInt, TimeSpan ts, System.DateTime expectedEnd)
	{
		DateTimeInterval dti = new DateTimeInterval(startInt, ts);

		Assert.That(dti.Start, Is.EqualTo(startInt));
		Assert.That(dti.End, Is.EqualTo(expectedEnd));
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
