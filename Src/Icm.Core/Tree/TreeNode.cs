using System.Collections.Generic;

namespace Icm.Tree
{

	/// <summary>
	/// A TreeNode is a physical, in-memory implementation of ITreeNode.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks>
	/// <para>A TreeNode only relates with other TreeNodes: its parent can only be a TreeNode and
	/// its children are also bound to be TreeNodes. This allows to implement automatic Parent-Children
	/// relationship management, so that if you add a child to a TreeNode, this child will automatically have
	/// the current node as its parent; and if you set a TreeNode to be the parent of another TreeNode, that
	/// one will be automatically added to the children collection of the current one.</para>
	/// <para>In addition, a TreeNode maintains its level, which is zero for a parentless node and
	/// equal to the parent's level plus one if the node has a parent.</para>
	/// </remarks>
	public class TreeNode<T> : ITreeNode<T>
	{

		private TreeNode<T> _parent;
		private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();

		private int _level;
		public TreeNode(T v)
		{
			Value = v;
		}

		public T Value { get; set; }

		public IEnumerable<TreeNode<T>> Children {
			get { return _children; }
		}

		public TreeNode<T> Parent {
			get { return _parent; }
			set {
				if (_parent != null) {
					_parent._children.Remove(this);
				}
				_parent = value;
				if (_parent != null) {
					_parent._children.Add(this);
					_level = _parent._level;
				} else {
					_level = 0;
				}
			}
		}

		public int Level {
			get { return _level; }
		}

		public TreeNode<T> AddChild(T value)
		{
			TreeNode<T> tn = new TreeNode<T>(value);
			AddChild(tn);
			return tn;
		}

		public TreeNode<T> AddChild(TreeNode<T> tn)
		{
			tn._parent = this;
			tn._level = _level + 1;
			_children.Add(tn);

			return tn;
		}

		public void AddChildren(IEnumerable<T> l)
		{
			List<TreeNode<T>> result = new List<TreeNode<T>>();
			foreach (void element_loopVariable in l) {
				element = element_loopVariable;
				result.Add(AddChild(element));
			}
		}

		public void AddChildren(IEnumerable<TreeNode<T>> l)
		{
			foreach (void element_loopVariable in l) {
				element = element_loopVariable;
				AddChild(element);
			}
		}

		public void RemoveChild(TreeNode<T> tn)
		{
			if (_parent != null) {
				_parent.RemoveChild(this);
			}
			tn._parent = null;
			tn._level = 0;
			_children.Remove(tn);
		}

		public ITreeNode<T> GetParent()
		{
			return Parent;
		}

		private IEnumerable<ITreeElement<T>> GetChildElements()
		{
			#if FrameworkNet35
			return _children.ConvertAll<ITreeElement<T>>(elem => elem);
			#elif
			return _children;
			#endif
		}

		private IEnumerable<ITreeNode<T>> GetChildNodes()
		{
			#if FrameworkNet35
			return _children.ConvertAll<ITreeNode<T>>(elem => elem);
			#elif
			return _children;
			#endif
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
