
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Tree
{

	/// <summary>
	/// This is the most basic tree structure, a node with a type value and an enumeration of children.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks>Note that ITreeElement does not have a reference to its parent.</remarks>
	public interface ITreeElement<T>
	{

		T Value { get; set; }
		IEnumerable<ITreeElement<T>> GetChildElements();
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
