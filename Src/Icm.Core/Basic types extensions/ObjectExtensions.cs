using System;
using System.Linq;
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
		public static T IfNothing<T>(this object o, T subst)
		{
		    if (o == null) {
				return subst;
			}

		    if (Convert.IsDBNull(o)) {
		        return subst;
		    }

		    return (T)o;
		}

	    /// <summary>
		/// If obj is not Nothing, execute action over obj
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <param name="act"></param>
		/// <remarks></remarks>
		public static void DoIfAny<T>(this T obj, Action<T> act)
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
		public static TResult GetIfAny<TObject, TResult>(this TObject o, Func<TObject, TResult> act)
		{
		    return o == null 
                ? default(TResult) 
                : act(o);
		}

	    /// <summary>
		/// Abbreviation of "IsNot Nothing".
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool IsSomething(this object o)
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
		public static T As<T>(this object o)
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
		public static bool IsOneOf<T>(this T s, params T[] sa)
		{
		    return sa != null && sa.Contains(s);
		}
	}
}
