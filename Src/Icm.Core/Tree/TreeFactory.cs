
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Icm.Tree
{

	/// <summary>
	/// Functions to build tree nodes and tree elements.
	/// </summary>
	/// <remarks>
	/// These functions provide the easiest way to build a tree, using nested function calls:
	/// 
	/// <code>
	/// Dim myTree = Node("root",
	///                Node("child1"),
	///                Node("child2",
	///                  Node("grandchild1"))
	///                Node("child3")))
	/// </code>
	/// </remarks>
	public class TreeFactory
	{

		public static TreeNode<T> Node<T>(T v, params TreeNode<T>[] children)
		{
			TreeNode<T> result = new TreeNode<T>(v);
			result.AddChildren(children);
			return result;
		}

		public static TreeElement<T> Element<T>(T v, params TreeElement<T>[] children)
		{
			TreeElement<T> result = new TreeElement<T>(v);
			result.AddRange(children);
			return result;
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
