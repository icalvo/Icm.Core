using System;
using Icm;
using NUnit.Framework;

[TestFixture()]
public class NullableExtensionsTest
{

	[TestCase(3)]
	public void V_Test(int? obj)
	{
		Assert.That(obj.V(), Is.EqualTo(obj.Value));
	}
    
    [TestCase(null)]
    public void V_Test2(int? obj)
    {
        Assert.That(() => obj.V(), Throws.InvalidOperationException);
    }

    [TestCase(3)]
	[TestCase(null)]
	public void HasNotValue_Test(int? obj)
	{
		Assert.That(obj.HasNotValue(), Is.EqualTo(!obj.HasValue));
	}

}
