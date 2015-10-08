using System;
using System.Runtime.CompilerServices;

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
		[Extension()]
		public static T[] GetAttributes<T>(Type type, bool inherit) where T : Attribute
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
		[Extension()]
		public static bool HasAttribute<T>(Type type, bool inherit) where T : Attribute
		{
			return ((T[])type.GetCustomAttributes(typeof(T), inherit)).Count > 0;
		}

		/// <summary>
		/// Gets an attribute that must appear a single time, or nothing if it does not appear.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="type"></param>
		/// <param name="inherit"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static T GetAttribute<T>(Type type, bool inherit) where T : Attribute
		{
			return ((T[])type.GetCustomAttributes(typeof(T), inherit)).SingleOrDefault;
		}

		/// <summary>
		/// Is type of the form Nullable(Of T)?
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static bool IsNullable(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
		}


	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
