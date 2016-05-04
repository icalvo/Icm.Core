using System;
using Icm;
using NUnit.Framework;

[TestFixture, Category("Icm")]
public class LongExtensionsTest
{

	[TestCase(34L)]
	[TestCase(5L)]
	[TestCase(201L)]
	[TestCase(0L)]
	public void HumanFileSize1_Test(long target)
	{
		Assert.That(target.HumanFileSize(), Is.EqualTo(target.HumanFileSize(decimalUnits: true, bigUnitNames: false, format: "0.00")));
	}

    [TestCase(-1L)]
    public void HumanFileSize1_Test2(long target)
    {
        Assert.That(target.HumanFileSize(), 
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase(34L, true, false, null, ExpectedResult = "34 B")]
	[TestCase(5L, false, false, null, ExpectedResult = "5 B")]
	[TestCase(201L, true, true, "F2", ExpectedResult = "201,00 bytes")]
	[TestCase(0L, true, false, null, ExpectedResult = "0 B")]
	[TestCase(-1L, true, false, null)]
	public string HumanFileSize_Test(long target, bool decimalUnits, bool bigUnitNames, string format)
	{
		return target.HumanFileSize(decimalUnits, bigUnitNames, format);
	}

    [TestCase(-1L, true, false, null)]
    public void HumanFileSize_Test2(long target, bool decimalUnits, bool bigUnitNames, string format)
    {
        Assert.That(target.HumanFileSize(decimalUnits, bigUnitNames, format), 
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }
}