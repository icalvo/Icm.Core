
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;

using System.Security.Cryptography;



using Icm.Security.Cryptography;



///<summary>
///This is a test class for HasherTest and is intended
///to contain all HasherTest Unit Tests
///</summary>
[TestFixture(), Category("Icm")]
public class HashAlgorithmExtensionsTest
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
	///A test for StringHash
	///</summary>
	[Test()]

	public void StringHashTest2()
	{
		MD5CryptoServiceProvider target = new MD5CryptoServiceProvider();
		byte[] b = { 1 };
		string expected = "55a54008ad1ba589aa210d2629c1df41";
		string actual = null;
		actual = target.StringHash(b);
		Assert.AreEqual(expected, actual);

	}


	///<summary>
	///A test for StringHash
	///</summary>
	[Test()]

	public void StringHashTest()
	{
		//Caso1
		MD5CryptoServiceProvider target = new MD5CryptoServiceProvider();
		string s = "hola";
		string expected = "4d186321c1a7f0f354b297e8914ab240";
		string actual = null;
		actual = target.StringHash(s);
		Assert.AreEqual(expected, actual);

		//Caso 2
		s = "";
		expected = "d41d8cd98f00b204e9800998ecf8427e";
		actual = target.StringHash(s);
		Assert.AreEqual(expected, actual);

		//Caso3
		s = null;
		try {
			actual = target.StringHash(s);
			Assert.Fail("NullReferenceException: Empty string");

		} catch (NullReferenceException ex) {
		} catch (Exception ex) {
			Assert.Fail("NullReferenceException: Empty string");
		}
	}

	///<summary>
	///A test for ByteToString
	///</summary>
	[Test()]

	public void ByteToStringTest()
	{
		//Caso 1
		MD5CryptoServiceProvider target = new MD5CryptoServiceProvider();
		byte[] hash = {
			
		};
		string expected = "";
		string actual = null;
		actual = hash.ToHex;
		Assert.AreEqual(expected, actual);

	}



	///<summary>
	///A test for ByteHash
	///</summary>
	[Test()]

	public void ByteHashTest2()
	{
		//Caso1
		MD5CryptoServiceProvider target = new MD5CryptoServiceProvider();
		string s = "hola hola hola";
		byte[] expected = new byte[16];
		byte[] actual = null;
		actual = target.ComputeHash(s);
		Assert.AreEqual(expected.Length, actual.Length);

		//Caso2
		s = null;
		expected = new byte[15];
		try {
			actual = target.ComputeHash(s);
			Assert.Fail("NullReferenceException: Empty string");

		} catch (NullReferenceException ex) {
		} catch (Exception ex) {
			Assert.Fail("NullReferenceException: Empty string");
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
