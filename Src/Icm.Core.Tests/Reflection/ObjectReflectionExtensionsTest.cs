
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Reflection;
using System.Reflection;
using NUnit.Framework;

[TestFixture()]
public class ObjectReflectionExtensionsTest
{

	object[] GetFieldTestCases = {
		new TestCaseData(null, "myfield").Throws(typeof(ArgumentNullException)),
		new TestCaseData(null, "inexistent").Throws(typeof(ArgumentNullException)),
		new TestCaseData(new MyExample(), "inexistent").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "mynumber").Throws(typeof(InvalidCastException)),
		new TestCaseData(new MyExample(), "myfield").Returns("mytest"),
		new TestCaseData(new MyExample(), "MyProp").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "DictPropStr[key1]").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "DictPropStr[key2]").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "DictPropStr[key3]").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "DictPropInt[1234]").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "DictPropInt[4567]").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "DictPropInt[4568]").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "DictPropInt[qwer]").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "DictPropDate[2013-04-07]").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "IndexedProp[7]").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "IndexedProp2[19, asdf]").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "ArrayProp[0]").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "ArrayProp[1]").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "ArrayProp[2]").Throws(typeof(ArgumentException))

	};
	[TestCaseSource(nameof(GetFieldTestCases))]
	public string GetField_Test(MyExample obj, string fieldName)
	{
		return obj.GetField<string>(fieldName);
	}

	object[] SetFieldTestCases = {
		new TestCaseData(null, "myfield", "NEWVALUE").Throws(typeof(ArgumentNullException)),
		new TestCaseData(null, "inexistent", "NEWVALUE").Throws(typeof(ArgumentNullException)),
		new TestCaseData(new MyExample(), "inexistent", "NEWVALUE").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "myfield", 345).Returns("345"),
		new TestCaseData(new MyExample(), "myfield", "NEWVALUE").Returns("NEWVALUE")

	};
	[TestCaseSource(nameof(SetFieldTestCases))]
	public string SetField_Test(MyExample obj, string fieldName, object value)
	{
		obj.SetField(fieldName, value);
		return obj.myfield;
	}

	object[] HasFieldTestCases = {
		new TestCaseData(null, "myfield").Returns(false),
		new TestCaseData(null, "inexistent").Returns(false),
		new TestCaseData(new MyExample(), "myfield").Returns(true),
		new TestCaseData(new MyExample(), "inexistent").Returns(false)

	};
	[TestCaseSource(nameof(HasFieldTestCases))]
	public bool HasField_Test(MyExample obj, string fieldName)
	{
		return obj.HasField(fieldName);
	}

	object[] GetPropTestCases = {
		new TestCaseData(null, "MyProp").Throws(typeof(ArgumentNullException)),
		new TestCaseData(null, "inexistent").Throws(typeof(ArgumentNullException)),
		new TestCaseData(new MyExample(), "inexistent").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "MyPropNumber").Throws(typeof(InvalidCastException)),
		new TestCaseData(new MyExample(), "myfield").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "MyProp").Returns("mytestp"),
		new TestCaseData(new MyExample(), "DictPropStr[key1]").Returns("value1"),
		new TestCaseData(new MyExample(), "DictPropStr[key2]").Returns("value2"),
		new TestCaseData(new MyExample(), "DictPropStr[key3]").Throws(typeof(KeyNotFoundException)),
		new TestCaseData(new MyExample(), "DictPropInt[1234]").Returns("value1i"),
		new TestCaseData(new MyExample(), "DictPropInt[4567]").Returns("value2i"),
		new TestCaseData(new MyExample(), "DictPropInt[4568]").Throws(typeof(KeyNotFoundException)),
		new TestCaseData(new MyExample(), "DictPropInt[qwer]").Throws(typeof(FormatException)),
		new TestCaseData(new MyExample(), "DictPropDate[2013-04-07]").Returns("value2d"),
		new TestCaseData(new MyExample(), "IndexedProp[7]").Returns("<<7>>"),
		new TestCaseData(new MyExample(), "IndexedProp2[19, asdf]").Returns("<<19;asdf>>"),
		new TestCaseData(new MyExample(), "ArrayProp[0]").Returns("a"),
		new TestCaseData(new MyExample(), "ArrayProp[1]").Returns("b"),
		new TestCaseData(new MyExample(), "ArrayProp[2]").Throws(typeof(IndexOutOfRangeException))

	};
	[TestCaseSource(nameof(GetPropTestCases))]
	public string GetProp_Test(MyExample obj, string fieldName)
	{
		return obj.GetProp<string>(fieldName);
	}


	object[] GetMemberTestCases = {
		new TestCaseData(null, "myfield", null).Throws(typeof(ArgumentNullException)),
		new TestCaseData(null, "inexistent", null).Throws(typeof(ArgumentNullException)),
		new TestCaseData(new MyExample(), "inexistent", null).Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "MyPropNumber", null).Throws(typeof(InvalidCastException)),
		new TestCaseData(new MyExample(), "MyProp", null).Returns("mytestp"),
		new TestCaseData(new MyExample(), "mynumber", null).Throws(typeof(InvalidCastException)),
		new TestCaseData(new MyExample(), "myfield", null).Returns("mytest"),
		new TestCaseData(new MyExample(), "myfield.Substring", new object[] {
			1,
			3
		}).Returns("yte"),
		new TestCaseData(new MyExample(), "myfield.Length.ToString", null).Returns("6"),
		new TestCaseData(new MyExample(), "myfield.Length.ToString", { "00" }).Returns("06"),
		new TestCaseData(new MyExample(), "DictPropStr[key1]", null).Returns("value1"),
		new TestCaseData(new MyExample(), "DictPropStr[key2]", null).Returns("value2"),
		new TestCaseData(new MyExample(), "DictPropStr[key3]", null).Throws(typeof(KeyNotFoundException)),
		new TestCaseData(new MyExample(), "DictPropInt[1234]", null).Returns("value1i"),
		new TestCaseData(new MyExample(), "DictPropInt[4567]", null).Returns("value2i"),
		new TestCaseData(new MyExample(), "DictPropInt[4568]", null).Throws(typeof(KeyNotFoundException)),
		new TestCaseData(new MyExample(), "DictPropInt[qwer]", null).Throws(typeof(FormatException)),
		new TestCaseData(new MyExample(), "DictPropDate[2013-04-07]", null).Returns("value2d"),
		new TestCaseData(new MyExample(), "IndexedProp[7]", null).Returns("<<7>>"),
		new TestCaseData(new MyExample(), "IndexedProp2[19, asdf]", null).Returns("<<19;asdf>>"),
		new TestCaseData(new MyExample(), "ArrayProp[0]", null).Returns("a"),
		new TestCaseData(new MyExample(), "ArrayProp[1]", null).Returns("b"),
		new TestCaseData(new MyExample(), "ArrayProp[2]", null).Throws(typeof(IndexOutOfRangeException))

	};
	[TestCaseSource(nameof(GetMemberTestCases))]
	public string GetMember_Test(MyExample obj, string fieldName, params object[] args)
	{
		return obj.GetMember<string>(fieldName, args);
	}


	object[] SetPropTestCases = {
		new TestCaseData(null, "MyProp", "NEWVALUE").Throws(typeof(ArgumentNullException)),
		new TestCaseData(null, "inexistent", "NEWVALUE").Throws(typeof(ArgumentNullException)),
		new TestCaseData(new MyExample(), "inexistent", "NEWVALUE").Throws(typeof(ArgumentException)),
		new TestCaseData(new MyExample(), "MyProp", 345).Returns("345"),
		new TestCaseData(new MyExample(), "MyProp", "NEWVALUE").Returns("NEWVALUE")

	};
	[TestCaseSource(nameof(SetPropTestCases))]
	public string SetProp_Test(MyExample obj, string fieldName, object value)
	{
		obj.SetProp(fieldName, value);
		return obj.MyProp;
	}

	object[] HasPropTestCases = {
		new TestCaseData(null, "MyProp").Returns(false),
		new TestCaseData(null, "inexistent").Returns(false),
		new TestCaseData(new MyExample(), "MyProp").Returns(true),
		new TestCaseData(new MyExample(), "inexistent").Returns(false)

	};
	[TestCaseSource(nameof(HasPropTestCases))]
	public bool HasProp_Test(MyExample obj, string fieldName)
	{
		return obj.HasProp(fieldName);
	}

	[Test()]
	public void TypeHasPropTest()
	{
		MyExample obj = new MyExample();

		Assert.That(TypeHasProp<MyExample>("MyProp"));
		Assert.That(!TypeHasProp<MyExample>("myother"));
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
