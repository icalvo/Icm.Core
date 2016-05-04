using System.Collections.Generic;

namespace Icm.Tree
{

	/// <summary>
	/// Physical, in-memory implementation of a ITreeElement.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks>
	/// <para>Unlike TreeNode, TreeElement doesn't forces its relatives to be TreeElements. The absence of parent
	/// concept in ITreeElements makes it impossible to maintain the parent-child relationship.</para>
	/// <para>A TreeElement is just implementing the children enumeration with a list and providing methods to
	/// manage that list.</para>
	/// </remarks>
	public class TreeElement<T> : List<ITreeElement<T>>, ITreeElement<T>
	{


		public TreeElement(T v)
		{
			Value = v;
		}

		public T Value { get; set; }

		public TreeElement<T> AddChild(T value)
		{
			TreeElement<T> tn = new TreeElement<T>(value);
			Add(tn);
			return tn;
		}

		public virtual void AddChildren(IEnumerable<T> l)
		{
			foreach (var element in l) {
				Add(AddChild(element));
			}
		}

		public IEnumerable<ITreeElement<T>> GetChildElements()
		{
			return this;
		}
	}
}
