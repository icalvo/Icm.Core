
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


using Icm.MathTools;
using NUnit.Framework;


///<summary>
///This is a test class for STATTest and is intended
///to contain all STATTest Unit Tests
///</summary>
[TestFixture(), Category("Icm")]
public class STATTest
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
	///A test for Uniform01
	///</summary>
	[Test()]

	public void Uniform01Test()
	{
		double actual = 0;
		double media = 0;
		double expected = 0;
		ArrayList elemento = new ArrayList();
		for (i = 0; i <= 500; i++) {
			actual = STAT.Uniform01;
			media = media + actual;
			elemento.Add(actual);
		}
		media = media / 500;
		Debug.WriteLine(media);

		Assert.That(media > 0.4 & media < 0.6);
	}

	///<summary>
	///A test for Uniform
	///</summary>
	[Test()]

	public void UniformTest()
	{
		//Caso 1
		double min = 0;
		double max = 0;
		double expected = 0.0;
		double actual = 0;
		actual = STAT.Uniform(min, max);
		Assert.AreEqual(expected, actual);



	}


}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
