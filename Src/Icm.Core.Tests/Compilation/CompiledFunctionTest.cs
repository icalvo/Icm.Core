
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.CodeDom.Compiler;
using Icm;
using Icm.Compilation;
using NUnit.Framework;

[TestFixture(), Category("Icm")]
public class CompiledFunctionTest
{

	[TestCase("Nothing", 1, 1, ExpectedResult = 0)]
	[TestCase("Nothing", 2, 5, ExpectedResult = 0)]
	[TestCase("x+y", 1, 1, ExpectedResult = 2)]
	[TestCase("x+y", 2, 5, ExpectedResult = 7)]
	[TestCase("x*y", 1, 1, ExpectedResult = 1)]
	[TestCase("x*y", 2, 5, ExpectedResult = 10)]
	[TestCase("x..y", 1, 1, ExpectedException = typeof(CompileException))]
	public int Evaluate_Test(string code, int param1, int param2)
	{
		CompiledFunction<int> Funct = default(CompiledFunction<int>);
		Funct = new VBCompiledFunction<int>();
		Funct.AddParameter<int>("x");
		Funct.AddParameter<int>("y");
		Funct.Code = code;
		Funct.CompileAsExpression();
		Assert.That(Funct.CompilerErrors.Count == 0);

		return Funct.Evaluate(param1, param2);
	}

}

[TestFixture(), Category("Icm")]
public class VBCompiledFunctionTest
{

	[Test()]
	public void GeneratedCode_Test()
	{
		CompiledFunction<int> Funct = default(CompiledFunction<int>);
		Funct = new VBCompiledFunction<int>();
		Funct.AddParameter<int>("x");
		Funct.AddParameter<int>("y");
		Funct.Code = "x+y*y";

		dynamic expected = "Imports System" + Constants.vbCrLf + "Imports System.Xml" + Constants.vbCrLf + "Imports System.Data" + Constants.vbCrLf + "Imports System.Linq" + Constants.vbCrLf + "Imports Microsoft.VisualBasic" + Constants.vbCrLf + "Namespace dValuate" + Constants.vbCrLf + " Class EvalRunTime " + Constants.vbCrLf + "  Public Function EvaluateIt( _" + Constants.vbCrLf + "   ByVal x As Int32, _" + Constants.vbCrLf + "   ByVal y As Int32 _" + Constants.vbCrLf + "  ) As Int32" + Constants.vbCrLf + "    Return x+y*y" + Constants.vbCrLf + "  End Function" + Constants.vbCrLf + " End Class " + Constants.vbCrLf + "End Namespace" + Constants.vbCrLf;

		Assert.That(Funct.GeneratedCode, Is.EqualTo(expected));
	}


}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
