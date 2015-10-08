
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
	/// Tree traversals.
	/// </summary>
	/// <remarks>
	/// 
	/// </remarks>
	public static class ITreeElementExtensions
	{


		[Extension()]
		public static IEnumerable<T> DepthPreorderTraverse<T>(ITreeElement<T> tn)
		{
			Stack<IEnumerator<ITreeElement<T>>> childStack = new Stack<IEnumerator<ITreeElement<T>>>();

			childStack.Push(tn.GetChildElements.GetEnumerator);
			yield return tn.Value;
			while (!(childStack.Count == 0)) {
				dynamic childEnum = childStack.Peek;

				if (childEnum.MoveNext()) {
					dynamic child = childEnum.Current;
					childStack.Push(child.GetChildElements.GetEnumerator);
					yield return child.Value;
				} else {
					childStack.Pop();
				}
			}
		}

		[Extension()]
		public static IEnumerable<T> DepthPostorderTraverse<T>(ITreeElement<T> tn)
		{
			Stack<IEnumerator<ITreeElement<T>>> childStack = new Stack<IEnumerator<ITreeElement<T>>>();
			dynamic rootEnum = { tn }.ToList.GetEnumerator;
			childStack.Push(rootEnum);
			do {
				dynamic childEnum = childStack.Peek;

				if (childEnum.MoveNext()) {
					dynamic child = childEnum.Current;
					childStack.Push(child.GetChildElements.GetEnumerator);
				} else {
					childStack.Pop();
					yield return childStack.Peek.Current.Value;
				}
			} while (!(childStack.Count == 1));
		}

		[Extension()]
		public static IEnumerable<T> BreadthTraverse<T>(ITreeElement<T> tn)
		{
			Queue<ITreeElement<T>> queue = new Queue<ITreeElement<T>>();

			queue.Enqueue(tn);
			while (!(queue.Count == 0)) {
				tn = queue.Dequeue();
				yield return tn.Value;
				foreach (void child_loopVariable in tn.GetChildElements) {
					child = child_loopVariable;
					queue.Enqueue(child);
				}
			}
		}


		[Extension()]
		public static IEnumerable<TraverseResult<T>> DepthPreorderTraverseWithLevel<T>(ITreeElement<T> tn)
		{
			Stack<IEnumerator<ITreeElement<T>>> childStack = new Stack<IEnumerator<ITreeElement<T>>>();

			childStack.Push(tn.GetChildElements.GetEnumerator);
			yield return TraverseResult.Create(tn.Value, childStack.Count - 1);
			while (!(childStack.Count == 0)) {
				dynamic childEnum = childStack.Peek;

				if (childEnum.MoveNext()) {
					dynamic child = childEnum.Current;
					childStack.Push(child.GetChildElements.GetEnumerator);
					yield return TraverseResult.Create(child.Value, childStack.Count - 1);
				} else {
					childStack.Pop();
				}
			}

		}

		[Extension()]
		public static IEnumerable<TraverseResult<T>> DepthPostorderTraverseWithLevel<T>(ITreeElement<T> tn)
		{
			Stack<IEnumerator<ITreeElement<T>>> childStack = new Stack<IEnumerator<ITreeElement<T>>>();
			dynamic rootEnum = { tn }.ToList.GetEnumerator;
			childStack.Push(rootEnum);
			do {
				dynamic childEnum = childStack.Peek;

				if (childEnum.MoveNext()) {
					dynamic child = childEnum.Current;
					childStack.Push(child.GetChildElements.GetEnumerator);
				} else {
					childStack.Pop();
					yield return TraverseResult.Create(childStack.Peek.Current.Value, childStack.Count - 1);
				}
			} while (!(childStack.Count == 1));
		}

		[Extension()]
		private static IEnumerable<TraverseResult<T>> DepthPostorderTraverseWithLevel<T>(ITreeElement<T> tn, int level)
		{
			foreach (void child_loopVariable in tn.GetChildElements) {
				child = child_loopVariable;
				foreach (void result_loopVariable in child.DepthPostorderTraverseWithLevel(level + 1)) {
					result = result_loopVariable;
					yield return result;
				}
			}
			yield return TraverseResult.Create(tn.Value, level);
		}


		[Extension()]
		public static IEnumerable<TraverseResult<T>> BreadthTraverseWithLevel<T>(ITreeElement<T> tn)
		{
			Queue<ITreeElement<T>> queue = new Queue<ITreeElement<T>>();
			Queue<int> levelQueue = new Queue<int>();

			queue.Enqueue(tn);
			levelQueue.Enqueue(0);
			while (!(queue.Count == 0)) {
				tn = queue.Dequeue();
				dynamic level = levelQueue.Dequeue;
				yield return TraverseResult.Create(tn.Value, level);
				foreach (void child_loopVariable in tn.GetChildElements) {
					child = child_loopVariable;
					queue.Enqueue(child);
					levelQueue.Enqueue(level + 1);
				}
			}
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
