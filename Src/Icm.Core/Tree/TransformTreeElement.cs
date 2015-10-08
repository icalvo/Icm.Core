
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Tree
{
	/// <summary>
	/// Proxy for a ITreeElement(Of T1) that implements an ITreeElement(Of T2).)
	/// </summary>
	/// <typeparam name="T1"></typeparam>
	/// <typeparam name="T2"></typeparam>
	/// <remarks>A TransformTreeElement(Of T1,T2) gives the full funcionality of
	/// an ITreeElement(Of T2) by using a transformation function from T1 to T2. It builds its members on the fly,
	/// whenever GetChildren is called, by building new TransformTreeElements wrapping the original children.</remarks>
	public class TransformTreeElement<T1, T2> : ITreeElement<T2>
	{

		private readonly ITreeElement<T1> _baseElement;
		private readonly Func<T1, T2> _transform;

		private T2 _value;
		public TransformTreeElement(ITreeElement<T1> node, Func<T1, T2> transform)
		{
			_baseElement = node;
			_transform = transform;
		}

		protected Func<T1, T2> Transform {
			get { return _transform; }
		}

		public IEnumerable<ITreeElement<T2>> GetChildren()
		{
			return _baseElement.GetChildElements.Select(child => new TransformTreeElement<T1, T2>(child, Transform));
		}
		IEnumerable<ITreeElement<T2>> ITreeElement<T2>.GetChildElements()
		{
			return GetChildren();
		}

		public T2 Value {
			get {
				if (_value == null) {
					_value = Transform(_baseElement.Value);
				}
				return _value;
			}
			set {
				throw new InvalidOperationException("Cannot modify value of a TransformTreeNode");
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
