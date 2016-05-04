
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Xml;
using Icm.Configuration;
using NUnit.Framework;


///<summary>
///This is a test class for GeneralSectionHandlerTest and is intended
///to contain all GeneralSectionHandlerTest Unit Tests
///</summary>
[TestFixture(), Category("Icm")]
[Ignore()]
public class GeneralSectionHandlerTest
{


	#region "Additional test attributes"
	//
	//You can use the following additional attributes as you write your tests:
	//
	//Use ClassInitialize to run code before running the first test in the class
	//<ClassInitialize()>  _
	//Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
	//End Sub
	//
	//Use ClassCleanup to run code after all tests in a class have run
	//<ClassCleanup()>  _
	//Public Shared Sub MyClassCleanup()
	//End Sub
	//
	//Use TestInitialize to run code before running each test
	//<TestInitialize()>  _
	//Public Sub MyTestInitialize()
	//End Sub
	//
	//Use TestCleanup to run code after each test has run
	//<TestCleanup()>  _
	//Public Sub MyTestCleanup()
	//End Sub
	//
	#endregion


	///<summary>
	///A test for ManageSection
	///</summary>
	[Test()]
	public void ManageSectionTest()
	{
		Assert.Inconclusive();
		GeneralSectionHandler target = new GeneralSectionHandler();
		XmlNode section = null;
		object expected = null;
		object actual = null;
		actual = target.ManageSection(section);
		Assert.AreEqual(expected, actual);
		Assert.Inconclusive();

	}

	///<summary>
	///A test for Create
	///</summary>
	[Test()]
	public void CreateTest()
	{
		Assert.Inconclusive();
		IConfigurationSectionHandler target = new GeneralSectionHandler();
		// TODO: Initialize to an appropriate value
		object parent = null;
		// TODO: Initialize to an appropriate value
		object configContext = null;
		// TODO: Initialize to an appropriate value
		XmlNode section = null;
		// TODO: Initialize to an appropriate value
		object expected = null;
		// TODO: Initialize to an appropriate value
		object actual = null;
		actual = target.Create(parent, configContext, section);
		Assert.AreEqual(expected, actual);
		Assert.Inconclusive();


	}

	///<summary>
	///A test for BuildHash
	///</summary>
	[Test()]
	public void BuildHashTest()
	{
		Assert.Inconclusive();
		GeneralSectionHandler target = new GeneralSectionHandler();
		// TODO: Initialize to an appropriate value
		XmlNode section = null;
		// TODO: Initialize to an appropriate value
		Dictionary<string, object> expected = null;
		// TODO: Initialize to an appropriate value
		IDictionary<string, object> actual = default(IDictionary<string, object>);
		actual = target.BuildHash(section);
		Assert.AreEqual(expected, actual);
		Assert.Inconclusive();


	}

	///<summary>
	///A test for BuildArray
	///</summary>
	[Test()]
	public void BuildArrayTest()
	{
		Assert.Inconclusive();
		GeneralSectionHandler target = new GeneralSectionHandler();
		// TODO: Initialize to an appropriate value
		XmlNode section = null;
		// TODO: Initialize to an appropriate value
		List<object> expected = null;
		// TODO: Initialize to an appropriate value
		IList<object> actual = default(IList<object>);
		actual = target.BuildArray(section);
		Assert.AreEqual(expected, actual);
		Assert.Inconclusive();


	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
