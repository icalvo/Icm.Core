using System;
using System.Linq;

namespace Icm.Reflection
{
	public static class TypeExtensions
	{
		/// <summary>
		/// Get attributes of a type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="type"></param>
		/// <param name="inherit"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static T[] GetAttributes<T>(this Type type, bool inherit) where T : Attribute
		{
			return (T[])type.GetCustomAttributes(typeof(T), inherit);
		}

		/// <summary>
		/// Has the given type an attribute?
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="type"></param>
		/// <param name="inherit"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool HasAttribute<T>(this Type type, bool inherit) where T : Attribute
		{
			return ((T[])type.GetCustomAttributes(typeof(T), inherit)).Length > 0;
		}

		/// <summary>
		/// Gets an attribute that must appear a single time, or nothing if it does not appear.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="type"></param>
		/// <param name="inherit"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static T GetAttribute<T>(this Type type, bool inherit) where T : Attribute
		{
			return ((T[])type.GetCustomAttributes(typeof(T), inherit)).SingleOrDefault();
		}

		/// <summary>
		/// Is type of the form Nullable(Of T)?
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool IsNullable(this Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}
	}
}