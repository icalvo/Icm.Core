
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.CommandLineTools;

///<summary>
///This is a test class for ValuesStore
///</summary>
[TestFixture(), Category("Icm")]
public class ValuesStoreTest
{

	[TestCase(null, "asdf", ExpectedException = typeof(ArgumentNullException))]
	[TestCase("asdf", null, ExpectedException = typeof(ArgumentNullException))]
	[TestCase("a", "a", ExpectedException = typeof(ArgumentException))]
	[TestCase("a", "asdf")]
	public void AddTest(string shortName, string longName)
	{
		ValuesStore store = new ValuesStore();

		store.AddParameter(shortName, longName);

		Assert.That(store.ContainsKey(shortName));
		Assert.That(store.ContainsKey(longName));
	}

	[TestCase(null, "asdf", ExpectedException = typeof(ArgumentNullException))]
	[TestCase("asdf", null, ExpectedException = typeof(ArgumentNullException))]
	[TestCase("a", "a", ExpectedException = typeof(ArgumentException))]
	[TestCase("asdf", "a", ExpectedException = typeof(ArgumentException))]
	[TestCase("asdf", "qwer", ExpectedException = typeof(ArgumentException))]
	[TestCase("a", "asdf")]
	public void AddValue(string shortName, string longName)
	{
		ValuesStore store = new ValuesStore();

		store.AddParameter(shortName, longName);

		Assert.That(store.ContainsKey(shortName));
		Assert.That(store.ContainsKey(longName));
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
