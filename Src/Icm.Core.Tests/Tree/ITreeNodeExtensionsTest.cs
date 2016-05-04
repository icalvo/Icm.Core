
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Icm.Collections;
using Icm.Tree;
using Icm.Tree.TreeFactory;
using NUnit.Framework;

[TestFixture()]
public class ITreeNodeExtensionsTest
{

	private static TreeNode<char> GetTree()
	{
		return Node('A', Node('B'), Node('C', Node('D'), Node('E')), Node('F'));
	}

	private static string GetRepresentation(IEnumerable<char> result)
	{
		return new string(result.ToArray);
	}

	private static string GetRepresentation(IEnumerable<TraverseResult<char>> tupleResult)
	{
		return tupleResult.Select(tup => tup.Result + tup.Level).JoinStr("");
	}

	[Test()]
	public void DepthPreorderTraverseTest()
	{
		dynamic actual = GetRepresentation(GetTree().DepthPreorderTraverse);
		Assert.That(actual, Is.EqualTo("ABCDEF"));
	}

	[Test()]
	public void DepthPostorderTraverseTest()
	{
		dynamic actual = GetRepresentation(GetTree().DepthPostorderTraverse);
		Assert.That(actual, Is.EqualTo("BDECFA"));
	}

	[Test()]
	public void BreadthTraverseTest()
	{
		dynamic actual = GetRepresentation(GetTree().BreadthTraverse);
		Assert.That(actual, Is.EqualTo("ABCFDE"));
	}

	[Test()]
	public void DepthPreorderTraverseWithLevelTest()
	{
		dynamic actual = GetRepresentation(GetTree().DepthPreorderTraverseWithLevel);
		Assert.That(actual, Is.EqualTo("A0B1C1D2E2F1"));
	}

	[Test()]
	public void DepthPostorderTraverseWithLevelTest()
	{
		dynamic actual = GetRepresentation(GetTree().DepthPostorderTraverseWithLevel);
		Assert.That(actual, Is.EqualTo("B1D2E2C1F1A0"));
	}

	[Test()]
	public void BreadthTraverseWithLevelTest()
	{
		dynamic actual = GetRepresentation(GetTree().BreadthTraverseWithLevel);
		Assert.That(actual, Is.EqualTo("A0B1C1F1D2E2"));
	}

	[Test()]
	public void AncestorsTest()
	{
		dynamic leaf = Node('E');

		dynamic tree = Node('A', Node('B', Node('C', Node('D', leaf))));

		dynamic actual = GetRepresentation(leaf.Ancestors);
		Assert.That(actual, Is.EqualTo("EDCBA"));
	}

	[Test()]
	public void ProperAncestorsTest()
	{
		dynamic leaf = Node('E');

		dynamic tree = Node('A', Node('B', Node('C', Node('D', leaf))));

		dynamic actual = GetRepresentation(leaf.ProperAncestors);
		Assert.That(actual, Is.EqualTo("DCBA"));
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
