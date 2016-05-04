
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm;
using Icm.Collections.Generic.StructKeyStructValue;
using NUnit.Framework;

[TestFixture()]
public class StructKeyStructValue_BaseSortedCollectionTest
{

	private static BaseSortedCollection<long, char> GetNewCollection()
	{
		return new MemorySortedCollection<long, char>(new LongTotalOrder());
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
		coll.Add(12, 'a');
		coll.Add(14, 'b');
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
		coll.Add(12, 'a');
		coll.Add(13, 'b');
		coll.Add(14, 'c');
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
		coll.Add(long.MaxValue, 'a');
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
		string expected = "-> 12 a" + Constants.vbCrLf + "NC 14 ---" + Constants.vbCrLf;

		// Act
		coll.Add(12, 'a');
		dynamic actual = coll.ToString(12, 14);

		Assert.AreEqual(expected, actual);
	}

	[Test()]
	public void ToString_OneElementInTheMiddle_AppearsInTheMiddle()
	{
		dynamic coll = GetNewCollection();
		string expected = "NC 12 ---" + Constants.vbCrLf + "-> 13 a" + Constants.vbCrLf + "NC 14 ---" + Constants.vbCrLf;

		// Act
		coll.Add(13, 'a');
		dynamic actual = coll.ToString(12, 14);

		Assert.AreEqual(expected, actual);
	}


	[Test()]
	public void ToString_OneElementInTheEnd_AppearsInTheEnd()
	{
		dynamic coll = GetNewCollection();
		string expected = "NC 12 ---" + Constants.vbCrLf + "-> 14 a" + Constants.vbCrLf;

		// Act
		coll.Add(14, 'a');
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
