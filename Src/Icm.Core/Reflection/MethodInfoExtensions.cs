
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Icm.Reflection
{

	public static class MethodInfoExtensions
	{

		/// <summary>
		/// Get the attributes of a given method.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="mi"></param>
		/// <param name="inherit"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static T[] GetAttributes<T>(MethodInfo mi, bool inherit) where T : Attribute
		{
			return (T[])mi.GetCustomAttributes(typeof(T), inherit);
		}


		/// <summary>
		/// Do the method have the attribute?
		/// </summary>
		/// <typeparam name="T">Attribute type to check</typeparam>
		/// <param name="mi"></param>
		/// <param name="inherit"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static bool HasAttribute<T>(MethodInfo mi, bool inherit) where T : Attribute
		{
			return ((T[])mi.GetCustomAttributes(typeof(T), inherit)).Count > 0;
		}

		/// <summary>
		/// Get the given attribute, that must be unique.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="mi"></param>
		/// <param name="inherit"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static T GetAttribute<T>(MethodInfo mi, bool inherit) where T : Attribute
		{
			return ((T[])mi.GetCustomAttributes(typeof(T), inherit)).Single;
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
