
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Tree
{


	/// <summary>
	/// Proxy for a ITreeNode(Of T1) that implements an ITreeNode(Of T2).
	/// </summary>
	/// <typeparam name="T1"></typeparam>
	/// <typeparam name="T2"></typeparam>
	/// <remarks>A TransformTreeNode(Of T1,T2) gives the full funcionality of
	/// an ITreeNode(Of T2) by using a transformation function from T1 to T2. It builds its members on the fly,
	/// whenever GetChildren is called, by building new TransformTreeNodes wrapping the original children.</remarks>
	public class TransformTreeNode<T1, T2> : TransformTreeElement<T1, T2>, ITreeNode<T2>
	{


		private readonly ITreeNode<T1> _baseNode;
		public TransformTreeNode(ITreeNode<T1> node, Func<T1, T2> transform) : base(node, transform)
		{
			_baseNode = node;
		}

		public ITreeNode<T2> GetParent()
		{
			dynamic sourceParent = _baseNode.GetParent;
			if (sourceParent == null) {
				return null;
			} else {
				return new TransformTreeNode<T1, T2>(sourceParent, Transform);
			}
		}

		public IEnumerable<ITreeNode<T2>> GetChildNodes()
		{
			return _baseNode.GetChildNodes.Select(child => new TransformTreeNode<T1, T2>(child, Transform));
		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
