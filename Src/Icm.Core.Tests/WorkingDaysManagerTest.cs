
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm;
using NUnit.Framework;

///<summary>
///This is a test class for WorkingDaysManagerTest and is intended
///to contain all WorkingDaysManagerTest Unit Tests
///</summary>
[TestFixture(), Category("Icm")]
public class WorkingDaysManagerTest
{

	WorkingDaysManager target = new WorkingDaysManager({ 1/1/2011 12:00:00 AM }, {
		DayOfWeek.Saturday,
		DayOfWeek.Sunday

	});
	[Test()]
	public void WeeklyHolidaysTest()
	{
		IEnumerable<DayOfWeek> actual = default(IEnumerable<DayOfWeek>);
		actual = target.WeeklyHolidays;
		Assert.That(actual.Count, Is.EqualTo(2));
		Assert.That(actual.Contains(DayOfWeek.Saturday));
		Assert.That(actual.Contains(DayOfWeek.Sunday));
	}

	///<summary>
	///A test for DayHolidays
	///</summary>
	[Test()]
	public void DayHolidaysTest()
	{
		IEnumerable<System.DateTime> actual = default(IEnumerable<System.DateTime>);
		actual = target.DayHolidays;
		dynamic expected = new List<System.DateTime>({ 1/1/2011 12:00:00 AM });
		Assert.That(actual, Is.EqualTo(expected));
	}

	///<summary>
	///A test for PrevWorkingDay
	///</summary>
	[Test()]
	public void PrevWorkingDayTest()
	{
		WorkingDaysManager target = new WorkingDaysManager();
		System.DateTime d = new System.DateTime();
		System.DateTime expected = new System.DateTime();
		System.DateTime actual = default(System.DateTime);

		//Caso 1
		d = new System.DateTime(2010, 4, 21);
		expected = new System.DateTime(2010, 4, 21);
		actual = target.PrevWorkingDay(d);
		Assert.AreEqual(expected, actual);

		//Caso 1
		d = new System.DateTime(2010, 4, 25);
		expected = new System.DateTime(2010, 4, 23);
		actual = target.PrevWorkingDay(d);
		Assert.AreEqual(expected, actual);

	}

	///<summary>
	///A test for NextWorkingDay
	///</summary>
	[Test()]
	public void NextWorkingDayTest()
	{
		WorkingDaysManager target = new WorkingDaysManager();
		System.DateTime d = default(System.DateTime);
		System.DateTime expected = default(System.DateTime);
		System.DateTime actual = default(System.DateTime);

		//Caso 1
		d = new System.DateTime(2010, 4, 25);
		expected = new System.DateTime(2010, 4, 26);
		actual = target.NextWorkingDay(d);
		Assert.AreEqual(expected, actual);

		//Caso 2
		d = new System.DateTime(2010, 4, 20);
		expected = new System.DateTime(2010, 4, 20);
		actual = target.NextWorkingDay(d);
		Assert.AreEqual(expected, actual);

	}

	///<summary>
	///A test for IsWorking
	///</summary>
	[Test()]
	public void IsWorkingTest()
	{
		WorkingDaysManager target = new WorkingDaysManager();
		System.DateTime d = default(System.DateTime);
		bool expected = false;
		bool actual = false;

		//Caso 1
		d = new System.DateTime(2010, 4, 20);
		actual = target.IsWorking(d);
		expected = true;
		Assert.AreEqual(expected, actual);

		//Caso 2
		d = new System.DateTime(2010, 4, 24);
		actual = target.IsWorking(d);
		expected = false;
		Assert.AreEqual(expected, actual);

	}

	///<summary>
	///A test for AddDays
	///</summary>
	[Test()]
	public void AddDaysTest()
	{
		WorkingDaysManager target = new WorkingDaysManager();
		System.DateTime d = new System.DateTime();
		int n = 0;
		System.DateTime expected = new System.DateTime();
		System.DateTime actual = default(System.DateTime);

		//Caso 1
		n = 3;
		expected = new System.DateTime(2010, 4, 23);
		d = new System.DateTime(2010, 4, 20);
		actual = target.AddDays(d, n);
		Assert.AreEqual(expected, actual);

		//Caso 2
		n = -5;
		expected = new System.DateTime(2010, 4, 13);
		d = new System.DateTime(2010, 4, 20);
		actual = target.AddDays(d, n);
		Assert.AreEqual(expected, actual);

		//Caso 3
		n = 0;
		expected = new System.DateTime(2010, 4, 20);
		d = new System.DateTime(2010, 4, 20);
		actual = target.AddDays(d, n);
		Assert.AreEqual(expected, actual);

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
