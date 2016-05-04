
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


using Icm.Configuration;
using System.Configuration;
using NUnit.Framework;


///<summary>
///This is a test class for SettingsTest and is intended
///to contain all SettingsTest Unit Tests
///</summary>
[TestFixture(), Category("Icm")]
public class SettingsTest
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
	///A test for GetCfg
	///</summary>
	[Test()]
	public void GetCfgTest()
	{
		string key = "system.data";
		object actual = null;
		actual = Settings.GetCfg(key);
		Assert.IsInstanceOf<System.Data.DataSet>(actual);
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
