
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Tree
{

	/// <summary>
	/// Adds notion of parent. The parent is an ITreeNode itself, so that we can follow the lineage
	/// up to the root (see <see cref="ITreeNodeExtensions"></see>)
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks></remarks>
	public interface ITreeNode<T> : ITreeElement<T>
	{

		ITreeNode<T> GetParent();
		IEnumerable<ITreeNode<T>> GetChildNodes();
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
