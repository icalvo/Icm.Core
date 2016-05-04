
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Collections;
using NUnit.Framework;

[Category("Icm")]
[TestFixture()]
public class ICollectionExtensionsTest
{

	[TestCase({
		"a",
		"b",
		"c"
	}, "b", {
		"a",
		"c"
	})]
	[TestCase({
		"a",
		"b",
		"c"
	}, "x", {
		"a",
		"b",
		"c"
	})]
	[TestCase({
		"a",
		null,
		"c"
	}, null, {
		"a",
		"c"
	})]
	[TestCase({
		"a",
		"",
		"c"
	}, "", {
		"a",
		"c"
	})]
	[TestCase(new string[], "a", new string[])]
	[TestCase(new string[], null, new string[])]
	[TestCase(new string[], "", new string[])]
	public void ForceRemove_NormalTests(IEnumerable<string> col, string itemRemoved, IEnumerable<string> expected)
	{
		List<string> list = new List<string>(col);
		list.ForceRemove(itemRemoved);
		Assert.That(list, Is.EquivalentTo(expected));
	}

	[Test()]
	public void ForceRemove_WithNullArray_ThrowsNullReferenceException()
	{
		ICollection<string> col = null;

		Assert.That(() => col.ForceRemove(";"), Throws.TypeOf<NullReferenceException>);
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
