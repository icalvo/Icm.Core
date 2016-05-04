
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Reflection;
using NUnit.Framework;


[TestFixture()]
public class ObjectCopyExtensionsTest
{

	[Test()]
	public void CloneTest()
	{
		MyExample source = new MyExample();

		source.mynumber = 3849;
		source.MyProp = "clone value 1";
		source.MyPropNumber = 457;

		dynamic clone = source.Clone;

		Assert.That(source.MyProp, Is.EqualTo(clone.MyProp));
		Assert.That(source.MyPropNumber, Is.EqualTo(clone.MyPropNumber));
		Assert.That(source.mynumber, Is.EqualTo(clone.mynumber));
	}

	[Test()]
	public void ClonePropertyExcludeTest()
	{
		MyExample source = new MyExample();

		source.mynumber = 3849;
		source.MyProp = "clone value 1";
		source.MyPropNumber = 457;

		dynamic clone = source.Clone(excludedMembers: { "MyProp" });

		Assert.That(source.MyProp, Is.Not.EqualTo(clone.MyProp));
		Assert.That(source.MyPropNumber, Is.EqualTo(clone.MyPropNumber));
		Assert.That(source.mynumber, Is.EqualTo(clone.mynumber));
	}

	[Test()]
	public void CloneTypeExcludeTest()
	{
		MyExample source = new MyExample();

		source.mynumber = 3849;
		source.MyProp = "clone value 1";
		source.MyPropNumber = 457;

		dynamic clone = source.Clone(excludedTypes: { "String" });

		Assert.That(source.MyProp, Is.Not.EqualTo(clone.MyProp));
		Assert.That(source.MyPropNumber, Is.EqualTo(clone.MyPropNumber));
		Assert.That(source.mynumber, Is.EqualTo(clone.mynumber));
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
