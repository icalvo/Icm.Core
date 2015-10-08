
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using Icm.Text;

[TestFixture(), Category("Icm")]
public class ReplacerTest
{

	static readonly object[] ConstructorTestCases = {
		new TestCaseData(new StringReader(""), new StringWriter(), "", "}").Throws(typeof(ArgumentException)),
		new TestCaseData(new StringReader(""), new StringWriter(), "{", "").Throws(typeof(ArgumentException)),
		new TestCaseData(new StringReader(""), new StringWriter(), null, "}").Throws(typeof(ArgumentException)),
		new TestCaseData(new StringReader(""), new StringWriter(), "{", null).Throws(typeof(ArgumentException)),
		new TestCaseData(new StringReader(""), null, "{", "}").Throws(typeof(ArgumentNullException)),
		new TestCaseData(null, new StringWriter(), "{", "}").Throws(typeof(ArgumentNullException))

	};
	[TestCaseSource("ConstructorTestCases")]
	public void Constructor_Test(StringReader sr, StringWriter sw, string tgstart, string tgend)
	{
		Replacer target = new Replacer(sr, sw, tgstart, tgend);
	}

	static readonly object[] ReplaceTestCases = {
		new TestCaseData("HOLA SOY {<NOMBRE>} HOY ES {<FECHA>}", "{<", ">}", {
			{
				"NOMBRE",
				"MARIA"
			},
			{
				"FECHA",
				"25/04/2010"
			}
		}).Returns("HOLA SOY MARIA HOY ES 25/04/2010"),
		new TestCaseData("HOLA SOY {<NOMBRE>} HOY ES {<FECHA>}", "{<", ">}", { {
			"a",
			"n"
		} }).Returns("HOLA SOY {<NOMBRE>} HOY ES {<FECHA>}"),
		new TestCaseData("HOLA SOY {<NOMBRE>OTRO>} HOY ES {<FECHA>}", "{<", ">}", {
			{
				"NOMBRE>OTRO",
				"MARIA"
			},
			{
				"FECHA",
				"25/04/2010"
			}
		}).Returns("HOLA SOY MARIA HOY ES 25/04/2010"),
		new TestCaseData("HOLA SOY {<NOMBRE>} HOY ES {<FECHA", "{<", ">}", {
			{
				"NOMBRE",
				"MARIA"
			},
			{
				"FECHA",
				"25/04/2010"
			}
		}).Returns("HOLA SOY MARIA HOY ES 25/04/2010"),
		new TestCaseData("Noreps", "{<", ">}", {
			{
				"NOMBRE",
				"MARIA"
			},
			{
				"FECHA",
				"25/04/2010"
			}
		}).Returns("Noreps"),
		new TestCaseData("", "{<", ">}", {
			{
				"NOMBRE",
				"MARIA"
			},
			{
				"FECHA",
				"25/04/2010"
			}
		}).Returns(""),
		new TestCaseData("HOLA SOY {<{<NOMBRE>}>} HOY ES {<FECHA>}", "{<", ">}", {
			{
				"NOMBRE",
				"MARIA"
			},
			{
				"FECHA",
				"25/04/2010"
			}
		}).Returns("HOLA SOY {<{<NOMBRE>}>} HOY ES 25/04/2010")

	};
	[TestCaseSource("ReplaceTestCases")]
	public string ReplaceAndClose_Test(string source, string tgstart, string tgend, string[,] replacements)
	{
		StringWriter sw = new StringWriter();
		StringReader sr = new StringReader(source);
		Replacer target = new Replacer(sr, sw, tgstart, tgend);
		Dictionary<string, string> repDict = new Dictionary<string, string>();

		for (i = 0; i <= replacements.GetUpperBound(0); i++) {
			target.AddReplacement(replacements(i, 0), replacements(i, 1));
		}
		target.ReplaceAndClose();

		return sw.ToString;
	}


	///<summary>
	///A test for ModifyReplacement
	///</summary>
	[Test()]

	public void ModifyReplacementTest1()
	{
		string s1 = "HOLA SOY -NOMBRE- HOY ES -FECHA-";
		StringWriter sw = new StringWriter();
		StringReader sr = new StringReader(s1);
		string tgstart = "-";
		string tgend = "-";

		Replacer target = new Replacer(sr, sw, tgstart, tgend);
		target.AddReplacement("NOMBRE", "MARIA");
		target.AddReplacement("FECHA", "25/04/2010");
		target.ModifyReplacement("FECHA", "26/04/2010");
		target.ReplaceAndClose();

		Debug.WriteLine(sw);
		string resultado = sw.ToString;
		string expected1 = "HOLA SOY MARIA HOY ES 26/04/2010";
		Assert.AreEqual(expected1, resultado);
	}


	///<summary>
	///A test for AddReplacement
	///</summary>
	[Test()]

	public void AddReplacementTest()
	{
		string s1 = "HOLA SOY -NOMBRE- HOY ES -NOMBRE-";
		StringWriter sw = new StringWriter();
		StringReader sr = new StringReader(s1);
		string tgstart = "-";
		string tgend = "-";

		Replacer target = new Replacer(sr, sw, tgstart, tgend);
		string search = "NOMBRE";
		AutoNumberGenerator replacement = new AutoNumberGenerator();
		target.AddReplacement(search, replacement);
		target.ReplaceAndClose();
		Debug.WriteLine(sw);

		string resultado = sw.ToString;
		string expected1 = "HOLA SOY 2 HOY ES 3";
		Assert.AreEqual(expected1, resultado);

	}



}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
