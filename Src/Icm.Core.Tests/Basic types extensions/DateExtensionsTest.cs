
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm;
using NUnit.Framework;

[TestFixture(), Category("Icm")]
public class DateExtensionsTest
{

	static readonly object[] SeasonTestCases = {
		new TestCaseData(new System.DateTime(2010, 4, 2)).Returns(DateExtensions.Seasons.Spring),
		new TestCaseData(new System.DateTime(2010, 12, 25)).Returns(DateExtensions.Seasons.Winter),
		new TestCaseData(new System.DateTime(2010, 10, 27)).Returns(DateExtensions.Seasons.Fall),
		new TestCaseData(new System.DateTime(2010, 7, 23)).Returns(DateExtensions.Seasons.Summer)

	};
	[TestCaseSource(nameof(SeasonTestCases))]
	public DateExtensions.Seasons Season_Test(System.DateTime d)
	{
		return d.Season();
	}

	static readonly object[] AddSTestCases = {
		new TestCaseData(new System.DateTime(2010, 4, 2), TimeSpan.FromDays(3)).Returns(new System.DateTime(2010, 4, 5)),
		new TestCaseData(System.DateTime.MaxValue, TimeSpan.FromDays(3)).Returns(System.DateTime.MaxValue),
		new TestCaseData(new System.DateTime(2010, 4, 2), TimeSpan.MaxValue).Returns(System.DateTime.MaxValue),
		new TestCaseData(new System.DateTime(2010, 4, 2), TimeSpan.FromDays(-3)).Throws(typeof(ArgumentException)),
		new TestCaseData(new System.DateTime(2010, 4, 2), TimeSpan.Zero).Returns(new System.DateTime(2010, 4, 2))

	};
	[TestCaseSource(nameof(AddSTestCases))]
	public System.DateTime AddS_Test(System.DateTime d, TimeSpan dur)
	{
		return d.AddS(dur);
	}

}


//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
