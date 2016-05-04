
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm;
using Icm.Collections.Generic;
using Icm.Collections.Generic.StructKeyClassValue;
using NUnit.Framework;

[TestFixture()]
public class RepositorySortedCollectionTest
{

	public SortedList<System.DateTime, string> InitialList = new SortedList<System.DateTime, string> {
		{
			1/1/2007 12:00:00 AM,
			"Item 1a"
		},
		{
			2/1/2007 12:00:00 AM,
			"Item 1b"
		},
		{
			1/1/2008 12:00:00 AM,
			"Item 2"
		},
		{
			1/1/2009 12:00:00 AM,
			"Item 3"
		},
		{
			1/1/2010 12:00:00 AM,
			"Item 4"
		},
		{
			1/1/2012 12:00:00 AM,
			"Item 5"
		}

	};
	public class FakeRepo : MemorySortedCollectionRepository<System.DateTime, string>
	{

		public FakeRepo(SortedList<System.DateTime, string> InitialList)
		{
			foreach (void kvp_loopVariable in InitialList) {
				kvp = kvp_loopVariable;
				List.Add(kvp.Key, kvp.Value);
			}
		}


	}

	[Test()]
	public void Constructor()
	{
		// ARRANGE
		dynamic repo = new FakeRepo(InitialList);

		// ACT
		RepositorySortedCollection<System.DateTime, string> sc = new RepositorySortedCollection<System.DateTime, string>(repo, new YearManager(), maxBuckets: 2, totalOrder: new DateTotalOrder());

		// ASSERT
		Assert.AreEqual("/", sc.BucketQueue);
		Assert.That(repo.List.SequenceEqual(InitialList));
	}

	[Test()]

	public void ContainsKey_IfThereAreNoDatesInBucket_DoesNotGetBucket()
	{
		// ARRANGE
		dynamic repo = new FakeRepo(InitialList);
		RepositorySortedCollection<System.DateTime, string> sc = new RepositorySortedCollection<System.DateTime, string>(repo, new YearManager(), maxBuckets: 2, totalOrder: new DateTotalOrder());
		bool result = false;

		// ACT
		result = sc.ContainsKey(1/5/2011 12:00:00 AM);
		// ASSERT
		Assert.AreEqual("/", sc.BucketQueue);
		Assert.IsFalse(result);
		Assert.That(repo.List.SequenceEqual(InitialList));
	}

	[Test()]
	public void ContainsKey_IfThereAreDatesInBucket_GetsBucket()
	{
		// ARRANGE
		dynamic repo = new FakeRepo(InitialList);
		RepositorySortedCollection<System.DateTime, string> sc = new RepositorySortedCollection<System.DateTime, string>(repo, new YearManager(), maxBuckets: 2, totalOrder: new DateTotalOrder());
		bool result = false;

		// ACT
		result = sc.ContainsKey(1/1/2007 12:00:00 AM);
		// ASSERT
		Assert.AreEqual("/2007/", sc.BucketQueue);
		Assert.That(result);

	}

	[Test()]
	public void ContainsKey_LastBucketAccessedGetsFirst()
	{
		// ARRANGE
		dynamic repo = new FakeRepo(InitialList);
		RepositorySortedCollection<System.DateTime, string> sc = new RepositorySortedCollection<System.DateTime, string>(repo, new YearManager(), maxBuckets: 3, totalOrder: new DateTotalOrder());
		bool result = false;

		// ACT
		result = sc.ContainsKey(1/1/2009 12:00:00 AM);
		// -> /2009/
		result = sc.ContainsKey(1/1/2007 12:00:00 AM);
		// -> /2007/2009/
		result = sc.ContainsKey(1/5/2011 12:00:00 AM);
		// -> /2007/2009/
		result = sc.ContainsKey(1/1/2008 12:00:00 AM);
		// -> /2008/2007/2009/
		result = sc.ContainsKey(1/1/2007 12:00:00 AM);
		// -> /2007/2008/2009/

		// ASSERT
		Assert.AreEqual("/2007/2008/2009/", sc.BucketQueue);

	}

	[Test()]
	public void ContainsKey_OldestBucketIsThrownAway()
	{
		// ARRANGE
		dynamic repo = new FakeRepo(InitialList);
		RepositorySortedCollection<System.DateTime, string> sc = new RepositorySortedCollection<System.DateTime, string>(repo, new YearManager(), maxBuckets: 2, totalOrder: new DateTotalOrder());
		bool result = false;

		// ACT
		result = sc.ContainsKey(1/1/2009 12:00:00 AM);
		result = sc.ContainsKey(1/1/2007 12:00:00 AM);

		// Third year examined causes the oldest (2009) to be expelled.
		result = sc.ContainsKey(1/1/2008 12:00:00 AM);
		// ASSERT
		Assert.AreEqual("/2008/2007/", sc.BucketQueue);
	}

	[Test()]
	public void Next_WhenInSameBucket()
	{
		// ARRANGE
		dynamic repo = new FakeRepo(InitialList);
		RepositorySortedCollection<System.DateTime, string> sc = new RepositorySortedCollection<System.DateTime, string>(repo, new YearManager(), maxBuckets: 2, totalOrder: new DateTotalOrder());
		System.DateTime? result = default(System.DateTime?);
		// ACT
		result = sc.Next(1/1/2007 12:00:00 AM);

		// ASSERT
		Assert.AreEqual(2/1/2007 12:00:00 AM, result);
	}


	[Test()]
	public void Next_WhenInOtherNotLoadedBucket()
	{
		// ARRANGE
		dynamic repo = new FakeRepo(InitialList);
		RepositorySortedCollection<System.DateTime, string> sc = new RepositorySortedCollection<System.DateTime, string>(repo, new YearManager(), maxBuckets: 2, totalOrder: new DateTotalOrder());
		System.DateTime? result = default(System.DateTime?);
		// ACT
		Assert.AreEqual("/", sc.BucketQueue);
		result = sc.Next(2/1/2007 12:00:00 AM);

		// ASSERT
		Assert.AreEqual(1/1/2008 12:00:00 AM, result);
		Assert.AreEqual("/2008/", sc.BucketQueue);
	}


	[Test()]
	public void Next_WhenInOtherLoadedBucket()
	{
		// ARRANGE
		dynamic repo = new FakeRepo(InitialList);
		RepositorySortedCollection<System.DateTime, string> sc = new RepositorySortedCollection<System.DateTime, string>(repo, new YearManager(), maxBuckets: 2, totalOrder: new DateTotalOrder());
		System.DateTime? result = default(System.DateTime?);
		// ACT
		sc.ContainsKey(1/1/2008 12:00:00 AM);

		Assert.AreEqual("/2008/", sc.BucketQueue);
		result = sc.Next(2/1/2007 12:00:00 AM);

		// ASSERT
		Assert.AreEqual(1/1/2008 12:00:00 AM, result);
		Assert.AreEqual("/2008/", sc.BucketQueue);
	}

	[Test()]
	public void Next_WhenInOtherNotConsecutiveBucket()
	{
		// ARRANGE
		dynamic repo = new FakeRepo(InitialList);
		RepositorySortedCollection<System.DateTime, string> sc = new RepositorySortedCollection<System.DateTime, string>(repo, new YearManager(), maxBuckets: 2, totalOrder: new DateTotalOrder());
		System.DateTime? result = default(System.DateTime?);
		// ACT
		result = sc.Next(1/1/2010 12:00:00 AM);

		// ASSERT
		Assert.AreEqual(1/1/2012 12:00:00 AM, result);
		Assert.AreEqual("/2012/", sc.BucketQueue);
	}

	[Test()]
	public void Next_WhenDoesNotExist()
	{
		// ARRANGE
		dynamic repo = new FakeRepo(InitialList);
		RepositorySortedCollection<System.DateTime, string> sc = new RepositorySortedCollection<System.DateTime, string>(repo, new YearManager(), maxBuckets: 2, totalOrder: new DateTotalOrder());
		System.DateTime? result = default(System.DateTime?);
		// ACT
		result = sc.Next(1/1/2013 12:00:00 AM);

		// ASSERT
		Assert.That(result, Is.Null);

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
