using System;
using System.Linq;
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
		public static T[] GetAttributes<T>(this MethodInfo mi, bool inherit) where T : Attribute
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
		public static bool HasAttribute<T>(this MethodInfo mi, bool inherit) where T : Attribute
		{
			return ((T[])mi.GetCustomAttributes(typeof(T), inherit)).Length > 0;
		}

		/// <summary>
		/// Get the given attribute, that must be unique.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="mi"></param>
		/// <param name="inherit"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static T GetAttribute<T>(this MethodInfo mi, bool inherit) where T : Attribute
		{
			return ((T[])mi.GetCustomAttributes(typeof(T), inherit)).Single();
		}
	}
}