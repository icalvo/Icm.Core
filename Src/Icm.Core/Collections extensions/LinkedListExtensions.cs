using System;
using System.Collections.Generic;
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
		public static void CopyInto<T>(this LinkedList<T> l1, LinkedList<T> l2) where T : ICloneable
		{
		    var itNode = l1.First;
		    while (itNode != null) {
				T copia = (T)itNode.Value.Clone();
				l2.AddLast(copia);
				itNode = itNode.Next;
			}
		}
	}
}