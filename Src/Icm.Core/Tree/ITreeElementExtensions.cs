using System.Collections.Generic;
using System.Linq;
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
		public static IEnumerable<T> DepthPreorderTraverse<T>(this ITreeElement<T> tn)
		{
			Stack<IEnumerator<ITreeElement<T>>> childStack = new Stack<IEnumerator<ITreeElement<T>>>();

			childStack.Push(tn.GetChildElements().GetEnumerator());
			yield return tn.Value;
			while (childStack.Count != 0) {
				var childEnum = childStack.Peek();

				if (childEnum.MoveNext()) {
					var child = childEnum.Current;
					childStack.Push(child.GetChildElements().GetEnumerator());
					yield return child.Value;
				} else {
					childStack.Pop();
				}
			}
		}

		public static IEnumerable<T> DepthPostorderTraverse<T>(this ITreeElement<T> tn)
		{
			Stack<IEnumerator<ITreeElement<T>>> childStack = new Stack<IEnumerator<ITreeElement<T>>>();
			var rootEnum = new[] { tn }.ToList().GetEnumerator();
			childStack.Push(rootEnum);
			do {
				var childEnum = childStack.Peek();

				if (childEnum.MoveNext()) {
					var child = childEnum.Current;
					childStack.Push(child.GetChildElements().GetEnumerator());
				} else {
					childStack.Pop();
					yield return childStack.Peek().Current.Value;
				}
			} while (childStack.Count != 1);
		}
        
		public static IEnumerable<T> BreadthTraverse<T>(this ITreeElement<T> tn)
		{
			Queue<ITreeElement<T>> queue = new Queue<ITreeElement<T>>();

			queue.Enqueue(tn);
			while (queue.Count != 0) {
				tn = queue.Dequeue();
				yield return tn.Value;
				foreach (var child in tn.GetChildElements()) {
					queue.Enqueue(child);
				}
			}
		}

		public static IEnumerable<TraverseResult<T>> DepthPreorderTraverseWithLevel<T>(this ITreeElement<T> tn)
		{
			Stack<IEnumerator<ITreeElement<T>>> childStack = new Stack<IEnumerator<ITreeElement<T>>>();

			childStack.Push(tn.GetChildElements().GetEnumerator());
			yield return TraverseResult.Create(tn.Value, childStack.Count - 1);
			while (childStack.Count != 0) {
				var childEnum = childStack.Peek();

				if (childEnum.MoveNext()) {
					var child = childEnum.Current;
					childStack.Push(child.GetChildElements().GetEnumerator());
					yield return TraverseResult.Create(child.Value, childStack.Count - 1);
				} else {
					childStack.Pop();
				}
			}

		}
        
		public static IEnumerable<TraverseResult<T>> DepthPostorderTraverseWithLevel<T>(this ITreeElement<T> tn)
		{
			Stack<IEnumerator<ITreeElement<T>>> childStack = new Stack<IEnumerator<ITreeElement<T>>>();
			var rootEnum = new[] { tn }.ToList().GetEnumerator();
			childStack.Push(rootEnum);
			do {
				var childEnum = childStack.Peek();

				if (childEnum.MoveNext()) {
					var child = childEnum.Current;
					childStack.Push(child.GetChildElements().GetEnumerator());
				} else {
					childStack.Pop();
					yield return TraverseResult.Create(childStack.Peek().Current.Value, childStack.Count - 1);
				}
			} while (childStack.Count != 1);
		}
        
		private static IEnumerable<TraverseResult<T>> DepthPostorderTraverseWithLevel<T>(this ITreeElement<T> tn, int level)
		{
		    foreach (var result in tn.GetChildElements().SelectMany(child => child.DepthPostorderTraverseWithLevel(level + 1)))
		    {
		        yield return result;
		    }

		    yield return TraverseResult.Create(tn.Value, level);
		}

		public static IEnumerable<TraverseResult<T>> BreadthTraverseWithLevel<T>(this ITreeElement<T> tn)
		{
			Queue<ITreeElement<T>> queue = new Queue<ITreeElement<T>>();
			Queue<int> levelQueue = new Queue<int>();

			queue.Enqueue(tn);
			levelQueue.Enqueue(0);
			while (queue.Count != 0) {
				tn = queue.Dequeue();
				var level = levelQueue.Dequeue();
				yield return TraverseResult.Create(tn.Value, level);
				foreach (var child in tn.GetChildElements()) {
					queue.Enqueue(child);
					levelQueue.Enqueue(level + 1);
				}
			}
		}

	}
}