using System;
using System.Runtime.CompilerServices;

namespace Icm
{

	public static class ObjectExtensions
	{

		/// <summary>
		/// If a variable is Nothing or DBNull, return subst, else return the same variable
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="o"></param>
		/// <param name="subst"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static T IfNothing<T>(object o, T subst)
		{
			if (o == null) {
				return subst;
			} else if (Information.IsDBNull(o)) {
				return subst;
			} else {
				return (T)o;
			}
		}

		/// <summary>
		/// If obj is not Nothing, execute action over obj
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <param name="act"></param>
		/// <remarks></remarks>
		[Extension()]
		public static void DoIfAny<T>(T obj, Action<T> act)
		{
			if (obj == null) {
				// Do nothing
			} else {
				act(obj);
			}
		}

		/// <summary>
		/// If o is not null, act applied to o; otherwise null.
		/// </summary>
		/// <typeparam name="TObject"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="o"></param>
		/// <param name="act"></param>
		/// <returns>If o is not null, act applied to o; otherwise null.</returns>
		/// <remarks></remarks>
		[Extension()]
		public static TResult GetIfAny<TObject, TResult>(TObject o, Func<TObject, TResult> act)
		{
			if (o == null) {
				return null;
			} else {
				return act(o);
			}
		}

		/// <summary>
		/// Abbreviation of "IsNot Nothing".
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static bool IsSomething(object o)
		{
			return o != null;
		}

		/// <summary>
		/// Direct casting shortcut.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="o"></param>
		/// <returns></returns>
		/// <remarks>
		/// Casting shortcut. Use:
		/// <code>
		/// myObj = otherObj.As(Of DerivedType)
		/// </code>
		/// instead of:
		/// <code>
		/// myObj = DirectCast(otherObj, DerivedType)
		/// myObj = CType(otherObj, DerivedType)
		/// </code>
		/// 
		/// A little bit shorter than CType, and with a pleasant object syntax.
		/// </remarks>
		[Extension()]
		public static T As<T>(object o)
		{
			return (T)o;
		}

		/// <summary>
		/// Is the first parameter one of the rest of parameters?
		/// </summary>
		/// <param name="s"></param>
		/// <param name="sa"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static bool IsOneOf<T>(T s, params T[] sa)
		{
			if (sa == null) {
				return false;
			}
			return sa.Contains(s);
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
