
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm;
using NUnit.Framework;

[TestFixture(), Category("Icm")]
public class Nullable2Test
{

	[Test()]
	public void HasValueTest()
	{
		Nullable2<int> nulInteger = default(Nullable2<int>);
		Assert.IsFalse(nulInteger.HasValue);

		nulInteger = null;
		Assert.IsFalse(nulInteger.HasValue);

		nulInteger = 0;
		Assert.That(nulInteger.HasValue);

		nulInteger = null;
		Assert.IsFalse(nulInteger.HasValue);

		Nullable2<string> nulString = null;

		Assert.That(nulString.HasValue);
		nulString = null;
		Assert.That(nulString.HasValue);
		nulString = "";
		Assert.That(nulString.HasValue);
		nulString = "Abc";
		Assert.That(nulString.HasValue);

	}

	[Test()]
	public void ValueTest()
	{
		Nullable2<int> nulInteger = default(Nullable2<int>);

		nulInteger = 0;
		Assert.DoesNotThrow(() =>
		{
			dynamic target = nulInteger.Value;
		});

		nulInteger = null;
		Assert.Throws<InvalidOperationException>(() =>
		{
			dynamic target = nulInteger.Value;
		});

		Nullable2<string> nulString = null;

		Assert.DoesNotThrow(() =>
		{
			dynamic target = nulString.Value;
		});

		nulString = "Abc";
		Assert.DoesNotThrow(() =>
		{
			dynamic target = nulString.Value;
		});

	}

	[Test()]
	public void HasSomethingTest()
	{
		Nullable2<int> nulInteger = default(Nullable2<int>);
		Assert.IsFalse(nulInteger.HasSomething);

		nulInteger = null;
		Assert.IsFalse(nulInteger.HasSomething);

		nulInteger = 0;
		Assert.That(nulInteger.HasSomething);

		nulInteger = null;
		Assert.IsFalse(nulInteger.HasSomething);

		Nullable2<string> nulString = null;

		Assert.IsFalse(nulString.HasSomething);
		nulString = null;
		Assert.IsFalse(nulString.HasSomething);
		nulString = "";
		Assert.That(nulString.HasSomething);
		nulString = "Abc";
		Assert.That(nulString.HasSomething);

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
