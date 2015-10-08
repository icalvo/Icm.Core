
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
[TestFixture()]
public class NullableExtensionsTest
{

	[TestCase(3)]
	[TestCase(null, ExpectedException = typeof(InvalidOperationException))]
	public void V_Test(Nullable<int> obj)
	{
		Assert.That(obj.V, Is.EqualTo(obj.Value));
	}

	[TestCase(3)]
	[TestCase(null)]
	public void HasNotValue_Test(Nullable<int> obj)
	{
		Assert.That(obj.HasNotValue, Is.EqualTo(!obj.HasValue));
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
