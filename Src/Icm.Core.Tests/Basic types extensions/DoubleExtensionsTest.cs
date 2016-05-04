
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm;
using NUnit.Framework;

[Category("Icm")]
[TestFixture()]
public class DoubleExtensionsTest
{

	[TestCase(5.2348, 1, ExpectedResult = 5.2)]
	public double ChangePrecision_Test(double target, int precision)
	{
		return target.ChangePrecision(precision);
	}

	[TestCase(90.0, ExpectedResult = Math.PI / 2)]
	[TestCase(0.0, ExpectedResult = 0)]
	public double Deg2Rad_Test(double target)
	{
		return target.Deg2Rad();
	}

	[TestCase(Math.PI / 2, ExpectedResult = 90.0)]
	[TestCase(0.0, ExpectedResult = 0.0)]
	public double Rad2Deg_Test(double target)
	{
		return target.Rad2Deg();
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
