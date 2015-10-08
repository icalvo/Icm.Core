
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Icm.Collections
{
	public static class LinkedListExtensions
	{

		/// <summary>
		/// Copy operation for linked lists.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="l1"></param>
		/// <param name="l2"></param>
		/// <remarks></remarks>
		[Extension()]
		public static void CopyInto<T>(LinkedList<T> l1, LinkedList<T> l2) where T : ICloneable
		{
			LinkedListNode<T> itNode = default(LinkedListNode<T>);

			itNode = l1.First;
			while (!(itNode == null)) {
				T copia = (T)itNode.Value.Clone;
				l2.AddLast(copia);
				itNode = itNode.Next;
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
