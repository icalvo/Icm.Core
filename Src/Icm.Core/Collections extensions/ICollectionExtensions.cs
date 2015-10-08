
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Runtime.CompilerServices;

namespace Icm.Collections
{

	public static class ICollectionExtensions
	{

		/// <summary>
		/// Remove an item, not failing if it doesn't exist.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="c"></param>
		/// <param name="item"></param>
		/// <remarks></remarks>
		[Extension()]
		public static void ForceRemove<T>(ICollection<T> c, T item)
		{
			if (c.Contains(item)) {
				c.Remove(item);
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
