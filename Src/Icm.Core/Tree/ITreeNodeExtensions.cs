using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Icm.Tree
{

	public static class ITreeNodeExtensions
	{

		/// <summary>
		/// Transforms a tree of T1 into a tree of T2 by means of a TransformTreeNode and a transform function that
		/// converts T1 into T2.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <param name="tn"></param>
		/// <param name="transform"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static ITreeNode<T2> Select<T1, T2>(ITreeNode<T1> tn, Func<T1, T2> transform)
		{
			return new TransformTreeNode<T1, T2>(tn, transform);
		}

		/// <summary>
		/// Yields the ancestors of a node, not including the node
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="tn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static IEnumerable<T> ProperAncestors<T>(ITreeNode<T> tn)
		{
			dynamic current = tn.GetParent;
			while (!(current == null)) {
				yield return current.Value;
				current = current.GetParent;
			}
		}

		/// <summary>
		/// Yields the ancestors of a node, including the node
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="tn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static IEnumerable<T> Ancestors<T>(ITreeNode<T> tn)
		{
			dynamic current = tn;
			do {
				yield return current.Value;
				current = current.GetParent;
			} while (!(current == null));
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
