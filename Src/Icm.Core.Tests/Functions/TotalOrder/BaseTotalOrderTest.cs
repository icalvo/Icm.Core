
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
[TestFixture()]
public class BaseTotalOrderTest
{

	public class FakeTotalOrder : BaseTotalOrder<long>
	{

		public override long Greatest()
		{
			return 1783;
		}

		public override long Least()
		{
			return -1761;
		}

		public override long Long2T(long d)
		{
			return d;
		}

		public override long T2Long(long t)
		{
			return t;
		}
	}

	[TestCase(18, 17)]
	[TestCase(0, -1)]
	[TestCase(1, 0)]
	public void Next_ForUsual_ReturnsNextConsecutive(long expected, long argument)
	{
		BaseTotalOrder<long> target = new FakeTotalOrder();

		dynamic actual = target.Next(argument);

		Assert.That(expected, Is.EqualTo(actual));
	}

	[Test()]
	public void Next_ForGreatest_ThrowsArgumentOutOfRange()
	{
		BaseTotalOrder<long> target = new FakeTotalOrder();

		Assert.That(() => target.Next(target.Greatest), Throws.InstanceOf<ArgumentOutOfRangeException>);
	}

	[TestCase(18, 19)]
	[TestCase(-1, 0)]
	[TestCase(0, 1)]
	public void Previous_ForUsual_ReturnsPreviousConsecutive(long expected, long argument)
	{
		BaseTotalOrder<long> target = new FakeTotalOrder();

		dynamic actual = target.Previous(argument);

		Assert.That(expected, Is.EqualTo(actual));
	}

	[Test()]
	public void Previous_ForLeast_ThrowsArgumentOutOfRange()
	{
		BaseTotalOrder<long> target = new FakeTotalOrder();

		Assert.That(() => target.Previous(target.Least), Throws.InstanceOf<ArgumentOutOfRangeException>);
	}


}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
