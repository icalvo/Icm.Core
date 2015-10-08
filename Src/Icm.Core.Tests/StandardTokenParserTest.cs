
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using NUnit.Framework;

[TestFixture()]
public class StandardTokenParserTest
{

	private static IEnumerable<TestCaseData> ParseTestSource()
	{
		return new List<TestCaseData> {
			new TestCaseData(null, new string[], new ParseError[]),
			new TestCaseData("", new string[], new ParseError[]),
			new TestCaseData("   " + Constants.vbTab, new string[], new ParseError[]),
			new TestCaseData(Constants.vbTab + "command    ", { "command" }, new ParseError[]),
			new TestCaseData("command -arg arg - arg2", {
				"command",
				"-arg",
				"arg",
				"-",
				"arg2"
			}, new ParseError[]),
			new TestCaseData("command -arg \"arg   - arg2\" arg3  ", {
				"command",
				"-arg",
				"arg   - arg2",
				"arg3"
			}, new ParseError[]),
			new TestCaseData("command -arg \"arg \\\"  - arg2\" arg3  ", {
				"command",
				"-arg",
				"arg \"  - arg2",
				"arg3"
			}, new ParseError[]),
			new TestCaseData("command -arg \"arg -  arg2", {
				"command",
				"-arg",
				"arg -  arg2"
			}, { new ParseError(1, 25, 13) })
		};
	}

	[Test()]
	[TestCaseSource("ParseTestSource")]
	public void ParseTest(string line, IEnumerable<string> tokens, IEnumerable<ParseError> errors)
	{
		StandardTokenParser parser = new StandardTokenParser();

		parser.Parse(line);

		Assert.That(parser.Tokens, Is.EquivalentTo(tokens));
		Assert.That(parser.Errors, Is.EquivalentTo(errors));

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
