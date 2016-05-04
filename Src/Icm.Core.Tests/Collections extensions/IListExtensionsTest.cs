
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Icm.Collections;
using NUnit.Framework;


[Category("Icm")]
[TestFixture()]
public class IListExtensionsTest
{

	public class EmptyClass
	{

	}

	static object[] InitializeTestCases = { new object[] {
		new List<EmptyClass>(),
		5,
		5

	} };
	[TestCase({
		1,
		2,
		3
	}, 5, 5)]
	[TestCase({
		1,
		2,
		3
	}, -2, 0)]
	[TestCase({
		1,
		2,
		3
	}, 0, 0)]
	[TestCase(new int[], 5, 5)]
	[TestCase(new int[], -2, 0)]
	[TestCase(new int[], 0, 0)]

	public void Initialize_NormalTestInteger(int[] data, int providedCount, int expectedCount)
	{
		List<int> originalList = new List<int>(data);
		originalList.Initialize(providedCount);
		Assert.That(originalList.Count, Is.EqualTo(expectedCount));
		Assert.That(originalList.All(e => e == 0));
	}

	[TestCaseSource(nameof(InitializeTestCases))]
	public void Initialize_NormalTestEmptyClass(List<EmptyClass> originalList, int providedCount, int expectedCount)
	{
		originalList.Initialize(providedCount);
		Assert.That(expectedCount, Is.EqualTo(originalList.Count));
		Assert.That(originalList.All(e => e != null));
	}

	[Test()]
	public void InitializeNew_WithNull_ThrowsNullReferenceException()
	{
		List<int> originalList = null;

		Assert.That(() => originalList.Initialize(5), Throws.TypeOf<NullReferenceException>);
	}

	[TestCase({
		1,
		3,
		5,
		5,
		8,
		10
	}, -3, -1)]
	[TestCase({
		1,
		3,
		5,
		5,
		8,
		10
	}, 0, -1)]
	[TestCase({
		1,
		3,
		5,
		5,
		8,
		10
	}, 1, 0)]
	[TestCase({
		1,
		3,
		5,
		5,
		8,
		10
	}, 4, -3)]
	[TestCase({
		1,
		3,
		5,
		5,
		8,
		10
	}, 5, 2)]
	[TestCase({
		1,
		3,
		5,
		5,
		8,
		10
	}, 10, 5)]
	[TestCase({
		1,
		3,
		5,
		5,
		8,
		10
	}, 20, -7)]
	public void Search1_NormalTest(int[] data, int searchedElement, int expectedIndex)
	{
		IList<int> list = new List<int>(data);

		dynamic actual = list.Search(searchedElement);
		Assert.That(actual, Is.EqualTo(expectedIndex));
	}


	[TestCase({
		1,
		3,
		5,
		5,
		8,
		10
	}, 0, 6, -3, -1)]
	[TestCase({
		1,
		3,
		5,
		5,
		8,
		10
	}, 0, 6, 0, -1)]
	[TestCase({
		1,
		3,
		5,
		5,
		8,
		10
	}, 0, 6, 1, 0)]
	[TestCase({
		1,
		3,
		5,
		5,
		8,
		10
	}, 0, 6, 4, -3)]
	[TestCase({
		1,
		3,
		5,
		5,
		8,
		10
	}, 0, 6, 5, 2)]
	[TestCase({
		1,
		3,
		5,
		5,
		8,
		10
	}, 0, 6, 10, 5)]
	[TestCase({
		1,
		3,
		5,
		5,
		8,
		10
	}, 0, 6, 20, -7)]
	public void Search3_NormalTest(int[] data, int index, int length, int searchedElement, int expectedIndex)
	{
		IList<int> list = new List<int>(data);

		dynamic actual = list.Search(index, length, searchedElement);
		Assert.That(actual, Is.EqualTo(expectedIndex));
	}


}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
