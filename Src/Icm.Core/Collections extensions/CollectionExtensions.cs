using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Icm.Collections
{
	public static class CollectionExtensions
	{
		/// <summary>
		/// Remove an item, not failing if it doesn't exist.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="c"></param>
		/// <param name="item"></param>
		/// <remarks></remarks>
		public static void ForceRemove<T>(this ICollection<T> c, T item)
		{
			if (c.Contains(item)) {
				c.Remove(item);
			}
		}
	}
}