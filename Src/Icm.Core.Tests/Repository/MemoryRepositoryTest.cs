
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using NUnit.Framework;

[TestFixture()]
public class MemoryRepositoryTest
{

	private class MyRegister : IEqualityComparer<MyRegister>
	{

		public int Id;

		public string Value;
		public static MyRegister Create(int id, string val)
		{
			return new MyRegister {
				Id = id,
				Value = val
			};
		}

		public override bool Equals(object obj)
		{
			return Equals(this, (MyRegister)obj);
		}

		public bool Equals(MyRegister x, MyRegister y)
		{
			return x.Id == y.Id && x.Value == y.Value;
		}

		public int GetHashCode(MyRegister obj)
		{
			return 0;
		}

		public override string ToString()
		{
			return string.Format("[{0}:{1}]", Id, Value);
		}
	}

	[Test()]
	public void ConstructorTest()
	{
		Icm.Data.MemoryRepository<MyRegister> repo = new Icm.Data.MemoryRepository<MyRegister>(reg => reg.Id);

		Assert.That(repo.Count, Is.EqualTo(0));
	}

	[Test()]
	public void Constructor2Test()
	{
		Icm.Data.MemoryRepository<MyRegister> repo = new Icm.Data.MemoryRepository<MyRegister>(new HashSet<MyRegister> {
			MyRegister.Create(1, "asdf"),
			MyRegister.Create(2, "qwer"),
			MyRegister.Create(3, "zxcv")
		}, reg => reg.Id);

		Assert.That(repo, Is.EquivalentTo({
			MyRegister.Create(2, "qwer"),
			MyRegister.Create(1, "asdf"),
			MyRegister.Create(3, "zxcv")
		}));
	}

	[Test()]
	public void LoadTest()
	{
		Icm.Data.MemoryRepository<MyRegister> repo = new Icm.Data.MemoryRepository<MyRegister>(new HashSet<MyRegister> {
			MyRegister.Create(1, "rtyu"),
			MyRegister.Create(2, "fghj"),
			MyRegister.Create(3, "vbnm")
		}, reg => reg.Id);
		repo.Load({
			MyRegister.Create(1, "asdf"),
			MyRegister.Create(2, "qwer"),
			MyRegister.Create(3, "zxcv")
		});
		Assert.That(repo, Is.EquivalentTo({
			MyRegister.Create(1, "asdf"),
			MyRegister.Create(2, "qwer"),
			MyRegister.Create(3, "zxcv")
		}));
	}

	[Test()]
	public void Test2()
	{
		Icm.Data.MemoryRepository<MyRegister> repo = new Icm.Data.MemoryRepository<MyRegister>(new HashSet<MyRegister> {
			MyRegister.Create(1, "rtyu"),
			MyRegister.Create(2, "fghj"),
			MyRegister.Create(3, "vbnm")
		}, reg => reg.Id);

		dynamic item = repo.GetById(2);

		Assert.That(item, Is.Not.Null);
		Assert.That(item.Id, Is.EqualTo(2));
		Assert.That(item.Value, Is.EqualTo("fghj"));
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
