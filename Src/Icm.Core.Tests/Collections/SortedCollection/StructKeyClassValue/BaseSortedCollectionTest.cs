
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Collections.Generic.StructKeyClassValue;

[TestFixture()]
public class BaseSortedCollectionTest
{

	private static BaseSortedCollection<long, string> GetNewCollection()
	{
		return new MemorySortedCollection<long, string>(new LongTotalOrder());
	}

	[Test()]
	public void GetFreeKey_WithEmptyCollection_ReturnsSameKey()
	{
		dynamic coll = GetNewCollection();
		long expected = 13;

		dynamic actual = coll.GetFreeKey(13);

		Assert.That(actual.HasValue);
		Assert.AreEqual(expected, actual);
	}

	[Test()]
	public void GetFreeKey_IfKeyNotPresent_ReturnsSameKey()
	{
		dynamic coll = GetNewCollection();
		long expected = 13;

		// Act
		coll.Add(12, "value1");
		coll.Add(14, "value2");
		dynamic actual = coll.GetFreeKey(13);

		Assert.That(actual.HasValue);
		Assert.AreEqual(expected, actual);
	}

	[Test()]
	public void GetFreeKey_IfKeyPresent_ReturnsNextAvailableKey()
	{
		dynamic coll = GetNewCollection();
		long expected = 15;

		// Act
		coll.Add(12, "value1");
		coll.Add(13, "value1");
		coll.Add(14, "value2");
		dynamic actual = coll.GetFreeKey(13);

		// Assert
		Assert.That(actual.HasValue);
		Assert.AreEqual(expected, actual);
	}

	[Test()]
	public void GetFreeKey_IfRunsOutOfValues_ReturnsNothing()
	{
		dynamic coll = GetNewCollection();
		long? expected = null;

		// Act
		coll.Add(long.MaxValue, "value1");
		dynamic actual = coll.GetFreeKey(long.MaxValue);

		Assert.AreEqual(expected, actual);
	}

	[Test()]
	public void ToString_EmptyCollection()
	{
		dynamic coll = GetNewCollection();
		string expected = "NC 12 ---" + Constants.vbCrLf + "NC 14 ---" + Constants.vbCrLf;

		// Act
		dynamic actual = coll.ToString(12, 14);

		// Assert
		Assert.AreEqual(expected, actual);
	}

	[Test()]
	public void ToString_OneElementAtStart_AppearsAtStart()
	{
		dynamic coll = GetNewCollection();
		string expected = "-> 12 value1" + Constants.vbCrLf + "NC 14 ---" + Constants.vbCrLf;

		// Act
		coll.Add(12, "value1");
		dynamic actual = coll.ToString(12, 14);

		Assert.AreEqual(expected, actual);
	}

	[Test()]
	public void ToString_OneElementInTheMiddle_AppearsInTheMiddle()
	{
		dynamic coll = GetNewCollection();
		string expected = "NC 12 ---" + Constants.vbCrLf + "-> 13 value1" + Constants.vbCrLf + "NC 14 ---" + Constants.vbCrLf;

		// Act
		coll.Add(13, "value1");
		dynamic actual = coll.ToString(12, 14);

		Assert.AreEqual(expected, actual);
	}


	[Test()]
	public void ToString_OneElementInTheEnd_AppearsInTheEnd()
	{
		dynamic coll = GetNewCollection();
		string expected = "NC 12 ---" + Constants.vbCrLf + "-> 14 value1" + Constants.vbCrLf;

		// Act
		coll.Add(14, "value1");
		dynamic actual = coll.ToString(12, 14);

		Assert.AreEqual(expected, actual);
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
