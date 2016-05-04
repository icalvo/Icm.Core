
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Icm.Reflection;
using NUnit.Framework;

[TestFixture()]
public class MethodInfoExtensionsTest
{

	[Test()]
	public void GetAttributesTest()
	{
		dynamic mi = typeof(ClassB).GetMethod("Routine");

		ExampleAttribute[] attrs = null;
		attrs = mi.GetAttributes<Example1Attribute>(false);
		Assert.That(attrs.Count == 1);
		Assert.AreEqual("classB", attrs(0).Message);

		attrs = mi.GetAttributes<Example1Attribute>(true);
		Assert.That(attrs.Count == 3);
		Assert.That(attrs.Any(attr => attr.Message == "classAfirst"));
		Assert.That(attrs.Any(attr => attr.Message == "classAsecond"));
		Assert.That(attrs.Any(attr => attr.Message == "classB"));

		attrs = mi.GetAttributes<Example3Attribute>(true);
		Assert.That(attrs.Count == 0);
	}

	[Test()]
	public void HasAttributeTest()
	{
		dynamic mi = typeof(ClassB).GetMethod("Routine");

		Assert.IsFalse(mi.HasAttribute<Example2Attribute>(false));
		Assert.That(mi.HasAttribute<Example2Attribute>(true));
	}

	[Test()]
	public void GetAttribute_FailsWhenNonExistentTest()
	{
		dynamic mi = typeof(ClassB).GetMethod("Routine");

		Assert.Throws<InvalidOperationException>(() => { mi.GetAttribute<Example2Attribute>(false); });

		Assert.Throws<InvalidOperationException>(() => { mi.GetAttribute<Example3Attribute>(true); });
	}

	[Test()]
	public void GetAttribute_FailsWhenMultipleTest()
	{
		dynamic mi = typeof(ClassB).GetMethod("Routine");

		Assert.Throws<InvalidOperationException>(() => { mi.GetAttribute<Example1Attribute>(true); });
	}

	[Test()]
	public void GetAttributeTest()
	{
		dynamic mi = typeof(ClassB).GetMethod("Routine");

		ExampleAttribute attr = null;
		attr = mi.GetAttribute<Example1Attribute>(false);
		Assert.AreEqual("classB", attr.Message);
	}

}

abstract class ExampleAttribute : Attribute
{

	public string Message { get; set; }

	public ExampleAttribute(string msg = "")
	{
		Message = msg;
	}

}


[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
class Example1Attribute : ExampleAttribute
{

	public Example1Attribute(string msg = "") : base(msg)
	{
	}
}

[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
class Example2Attribute : ExampleAttribute
{

	public Example2Attribute(string msg = "") : base(msg)
	{
	}
}

[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
class Example3Attribute : ExampleAttribute
{

	public Example3Attribute(string msg = "") : base(msg)
	{
	}
}

class ClassA
{

	[Example1("classAfirst"), Example1("classAsecond"), Example2("classA")]

	public virtual void Routine()
	{
	}

}


class ClassB : ClassA
{

	[Example1("classB")]

	public override void Routine()
	{
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
