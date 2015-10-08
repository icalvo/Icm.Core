
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Collections;
using Icm.Tree;

///<summary>
///This is a test class for TreeNodeTest and is intended
///to contain all TreeNodeTest Unit Tests
///</summary>
[TestFixture(), Category("Icm")]
public class TreeNodeTest
{

	///<summary>
	///A test for RemoveChild
	///</summary>
	public void RemoveChildTestHelper()
	{
		string v = "a";
		TreeNode<string> target = new TreeNode<string>(v);
		TreeNode<string> tn = new TreeNode<string>("b");
		target.RemoveChild(tn);

		Assert.IsFalse(Convert.ToBoolean(target.Children.Count(child => child.Value == "b") == 1));
		Assert.IsFalse(target.Children.Count() == 1);

	}

	[Test()]
	public void RemoveChildTest()
	{
		RemoveChildTestHelper();
	}


	///<summary>
	///A test for AddChild
	///</summary>
	public void AddChildTestHelper()
	{
		string v = "a";
		TreeNode<string> target = new TreeNode<string>(v);
		string value = "b";
		TreeNode<string> expected = new TreeNode<string>("b");
		TreeNode<string> actual = default(TreeNode<string>);

		actual = target.AddChild(value);

		Assert.That(Convert.ToBoolean(target.Children.Count(child => actual.Value == expected.Value) == 1));
		Assert.That(actual.Parent.Value == "a");
		Assert.That(Convert.ToBoolean(target.Children.Count() == 1));

	}

	[Test()]
	public void AddChildTest()
	{
		AddChildTestHelper();
	}

	///<summary>
	///A test for AddChild
	///</summary>
	public void AddChildTest1Helper()
	{
		string v = "a";
		TreeNode<string> target = new TreeNode<string>(v);
		TreeNode<string> tn = new TreeNode<string>("b");
		bool encontrado = false;
		target.AddChild(tn);

		Assert.AreEqual(tn.Parent.Value == "a", true);
		Assert.That(target.Children.Count() == 1);
		Assert.That(target.Children.Count(child => child.Value == "b") == 1);

	}

	[Test()]
	public void AddChildTest1()
	{
		AddChildTest1Helper();
	}

	///<summary>
	///A test for TestFinal
	///</summary>
	[Test()]
	public void TestFinal()
	{
		string v = ("maria");
		TreeNode<string> target = new TreeNode<string>(v);
		List<TreeNode<string>> ltn = new List<TreeNode<string>> {
			new TreeNode<string>("b"),
			new TreeNode<string>("c"),
			new TreeNode<string>("m"),
			new TreeNode<string>("S")
		};

		target.AddChildren(ltn);
		target.AddChild("x");
		foreach (void elemento_loopVariable in ltn) {
			elemento = elemento_loopVariable;
			if (elemento.Value == "b") {
				target.RemoveChild(elemento);
				break; // TODO: might not be correct. Was : Exit For
			}
		}

		Assert.That(Convert.ToBoolean(target.Children.Count(child => child.Value == "x") == 1));
		Assert.IsFalse(Convert.ToBoolean(target.Children.Count(child => child.Value == "b") == 1));
		Assert.AreEqual(target.Children.All(child => child.Parent.Value == "maria"), true);
		Assert.That(target.Children.Count() == 4);


	}

	///<summary>
	///A test for AddChild
	///</summary>
	[Test()]
	public void AddChildrenTest()
	{
		string v = ("maria");
		TreeNode<string> target = new TreeNode<string>(v);
		List<string> tn = new List<string> {
			"b",
			"c",
			"m",
			"S"
		};
		bool encontrado = false;
		IEnumerable<TreeNode<string>> ResultadoList = default(IEnumerable<TreeNode<string>>);

		target.AddChildren(tn);
		ResultadoList = target.Children;
		Assert.AreEqual(ResultadoList.All(child => child.Parent.Value == "maria"), true);
		Assert.That(target.Children.Count() == 4);
		Assert.That(target.Children.Count(child => child.Value == "b") == 1);
		Assert.That(target.Children.Count(child => child.Value == "c") == 1);
		Assert.That(target.Children.Count(child => child.Value == "m") == 1);
		Assert.That(target.Children.Count(child => child.Value == "S") == 1);
	}

	///<summary>
	///A test for AddChild
	///</summary>
	[Test()]

	public void AddChildrenTest2()
	{
		string v = ("maria");
		TreeNode<string> target = new TreeNode<string>(v);
		List<TreeNode<string>> tn = new List<TreeNode<string>> {
			new TreeNode<string>("b"),
			new TreeNode<string>("c"),
			new TreeNode<string>("m"),
			new TreeNode<string>("S")
		};

		bool encontrado = false;


		target.AddChildren(tn);
		Assert.AreEqual(tn.All(child => child.Parent.Value == "maria"), true);
		Assert.That(target.Children.Count() == 4);
		Assert.That(target.Children.Count(child => child.Value == "b") == 1);
		Assert.That(target.Children.Count(child => child.Value == "c") == 1);
		Assert.That(target.Children.Count(child => child.Value == "m") == 1);
		Assert.That(target.Children.Count(child => child.Value == "S") == 1);
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
