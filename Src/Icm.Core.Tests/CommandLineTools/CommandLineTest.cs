
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.CommandLineTools;

///<summary>
///This is a test class for CommandLine
///</summary>
[TestFixture(), Category("Icm")]
public class CommandLineTest
{

	private static CommandLine GetCommandLineExample()
	{
		CommandLine cmdline = new CommandLine();

		cmdline.Required("b", "base", "Base of the logarithm", SubArgument.Required("base")).Optional("e", "exponent", "Exponent of the logarithm", SubArgument.Required("exponent")).Optional("x", "extensions", "Extensions", SubArgument.List("extensions")).MainParametersExactly({ new UnnamedParameter("number", "Number which logarithm is extracted") });

		return cmdline;
	}

	private static CommandLine GetCommandLineExample2()
	{
		CommandLine cln = new CommandLine();

		cln.Required("b", "base", "Base of the logarithm", SubArgument.Required("base")).Optional("e", "exponent", "Exponent of the logarithm", SubArgument.Required("exponent"));

		return cln;
	}

	[Test(), Category("Icm")]
	public void IsPresent_WithUndefinedOption_Fails()
	{
		dynamic cln = GetCommandLineExample();

		cln.ProcessArguments("example.exe", "-b", "2", "32");

		Assert.Throws<UndefinedOptionException>(() =>
		{
			dynamic res = cln.IsPresent("f");
		});

		Assert.Throws<UndefinedOptionException>(() =>
		{
			dynamic res = cln.IsPresent("foo");
		});
	}


	[Test(), Category("Icm")]
	public void GetValue_WithUndefinedOption_Fails()
	{
		dynamic cln = GetCommandLineExample();

		cln.ProcessArguments("example.exe", "-b", "2", "32");

		Assert.Throws<UndefinedOptionException>(() =>
		{
			dynamic res = cln.GetValue("f");
		});

		Assert.Throws<UndefinedOptionException>(() =>
		{
			dynamic res = cln.GetValue("foo");
		});

	}


	[Test(), Category("Icm")]
	public void GetValues_WithUndefinedOptionTest_Fails()
	{
		dynamic cln = GetCommandLineExample();

		cln.ProcessArguments("example.exe", "-b", "2", "32");

		Assert.Throws<UndefinedOptionException>(() =>
		{
			dynamic res = cln.GetValues("f");
		});

		Assert.Throws<UndefinedOptionException>(() =>
		{
			dynamic res = cln.GetValues("foo");
		});
	}


	///<summary>
	///A test for MainArgumentsConfig
	///</summary>
	[Test(), Category("Icm")]

	public void ProcessArguments_SeparatesMainArgumentsTest()
	{
		dynamic cln = GetCommandLineExample();

		cln.ProcessArguments("example.exe", "-b", "2", "32");

		Assert.That(!cln.HasErrors);
	}

	[Test(), Category("Icm")]
	public void IsPresent_TrueWithShortTest()
	{
		dynamic cln = GetCommandLineExample();

		cln.ProcessArguments("example.exe", "-b", "2", "32");

		Assert.That(cln.IsPresent("b"));
		Assert.That(cln.IsPresent("base"));
	}

	[Test(), Category("Icm")]
	public void IsPresent_TrueWithLongTest()
	{
		dynamic cln = GetCommandLineExample();

		cln.ProcessArguments("example.exe", "--base", "2", "32");

		Assert.That(cln.IsPresent("b"));
		Assert.That(cln.IsPresent("base"));
	}

	[Test(), Category("Icm")]
	public void IsPresent_FalseWithLongTest()
	{
		dynamic cln = GetCommandLineExample();

		cln.ProcessArguments("example.exe", "-b", "2", "32");

		Assert.That(!cln.IsPresent("e"));
		Assert.AreEqual(cln.GetValue("e"), null);
		Assert.That(!cln.IsPresent("exponent"));
		Assert.AreEqual(cln.GetValue("exponent"), null);
	}

	[Test(), Category("Icm")]
	public void MainArgumentTest()
	{
		dynamic cln = GetCommandLineExample();

		cln.ProcessArguments("example.exe", "-b", "2", "32");

		Assert.AreEqual(cln.MainValue, "32");
		Assert.AreEqual(cln.MainValues.Count, 1);
	}

	[Test(), Category("Icm")]
	public void MainArgumentsTest()
	{
		dynamic arg = new UnnamedParameter("arg", "");
		dynamic cln = GetCommandLineExample2().MainParametersAtMost({
			arg,
			arg,
			arg
		});

		cln.ProcessArguments("example.exe", "-b", "2", "32", "asdf");

		Assert.AreEqual(cln.MainValue, "32");
		Assert.AreEqual(cln.MainValues.Count, 2);
		Assert.AreEqual(cln.MainValues(0), "32");
		Assert.AreEqual(cln.MainValues(1), "asdf");

	}

	[Test(), Category("Icm")]
	public void MainAtLeast1Test()
	{
		dynamic arg = new UnnamedParameter("arg", "");
		dynamic cln = GetCommandLineExample2().MainParametersAtLeast(arg, {
			arg,
			arg,
			arg
		});

		cln.ProcessArguments("example.exe", "-b", "2", "32", "asdf");

		Assert.That(cln.HasErrors());
	}

	[Test(), Category("Icm")]
	public void MainAtLeast2Test()
	{
		dynamic arg = new UnnamedParameter("arg", "");
		dynamic cln = GetCommandLineExample2().MainParametersAtLeast(arg, {
			arg,
			arg,
			arg
		});

		cln.ProcessArguments("example.exe", "-b", "2", "32", "asdf", "qwer");

		Assert.That(!cln.HasErrors());
	}


	[Test(), Category("Icm")]
	public void MainAtMost1Test()
	{
		dynamic arg = new UnnamedParameter("arg", "");
		dynamic cln = GetCommandLineExample2().MainParametersAtMost({
			arg,
			arg
		});

		cln.ProcessArguments("example.exe", "-b", "2", "32", "asdf");

		Assert.That(!cln.HasErrors());
	}

	[Test(), Category("Icm")]
	public void MainAtMost2Test()
	{
		dynamic arg = new UnnamedParameter("arg", "");
		dynamic cln = GetCommandLineExample2().MainParametersAtMost({
			arg,
			arg
		});

		cln.ProcessArguments("example.exe", "-b", "2", "32", "asdf", "qwer");

		Assert.That(cln.HasErrors());
	}

	[Test(), Category("Icm")]

	public void GetValuesTest()
	{
		dynamic cln = GetCommandLineExample();

		cln.ProcessArguments("example.exe", "-b", "4", "-x", "*.vb", "*.asdf", "--", "32");

		Assert.That(!cln.HasErrors);
		Assert.That(cln.IsPresent("x"));
		Assert.AreEqual(2, cln.GetValues("x").Count);
		Assert.That(cln.GetValues("x").Contains("*.vb"));
		Assert.That(cln.GetValues("x").Contains("*.asdf"));
		Assert.That(cln.MainValue == "32");
	}

	[Test(), Category("Icm")]

	public void GetValues2Test()
	{
		dynamic cln = GetCommandLineExample();

		cln.ProcessArguments("example.exe", "-b", "4", "-x", "--", "32");

		Assert.That(!cln.HasErrors);
		Assert.That(cln.IsPresent("x"));
		Assert.AreEqual(0, cln.GetValues("x").Count);
		Assert.That(cln.MainValue == "32");
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
