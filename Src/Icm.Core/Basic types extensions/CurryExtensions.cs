using System;
using System.Runtime.CompilerServices;

namespace Icm
{

	/// <summary>
	/// Set of currying functions. They follow the pattern:
	/// CurryX(T1, ..., TN, TResult)(fn As Func(Of T1,...TN, TResult), vx As TX) As Func(Of T1, ...all minus TX..., TN, TResult)
	/// </summary>
	/// <remarks></remarks>
	public static class CurryExtensions
	{
		public static Func<TResult> Curry1<T1, TResult>(this Func<T1, TResult> fn, T1 v1)
		{
			return () => fn(v1);
		}

		public static Func<T2, TResult> Curry1<T1, T2, TResult>(this Func<T1, T2, TResult> fn, T1 v1)
		{
			return p2 => fn(v1, p2);
		}

		public static Func<T1, TResult> Curry2<T1, T2, TResult>(this Func<T1, T2, TResult> fn, T2 v2)
		{
			return p1 => fn(p1, v2);
		}

		public static Func<T2, T3, TResult> Curry1<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> fn, T1 v1)
		{
			return (p2, p3) => fn(v1, p2, p3);
		}

		public static Func<T1, T3, TResult> Curry2<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> fn, T2 v2)
		{
			return (p1, p3) => fn(p1, v2, p3);
		}

		public static Func<T1, T2, TResult> Curry3<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> fn, T3 v3)
		{
			return (p1, p2) => fn(p1, p2, v3);
		}

		public static Func<T2, T3, T4, TResult> Curry1<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> fn, T1 v1)
		{
			return (p2, p3, p4) => fn(v1, p2, p3, p4);
		}

		public static Func<T1, T3, T4, TResult> Curry2<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> fn, T2 v2)
		{
			return (p1, p3, p4) => fn(p1, v2, p3, p4);
		}

		public static Func<T1, T2, T4, TResult> Curry3<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> fn, T3 v3)
		{
			return (p1, p2, p4) => fn(p1, p2, v3, p4);
		}

		public static Func<T1, T2, T3, TResult> Curry4<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> fn, T4 v4)
		{
			return (p1, p2, p3) => fn(p1, p2, p3, v4);
		}

		public static Func<T2, T3, T4, T5, TResult> Curry1<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> fn, T1 v1)
		{
			return (p2, p3, p4, p5) => fn(v1, p2, p3, p4, p5);
		}

		public static Func<T1, T3, T4, T5, TResult> Curry2<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> fn, T2 v2)
		{
			return (p1, p3, p4, p5) => fn(p1, v2, p3, p4, p5);
		}

		public static Func<T1, T2, T4, T5, TResult> Curry3<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> fn, T3 v3)
		{
			return (p1, p2, p4, p5) => fn(p1, p2, v3, p4, p5);
		}

		public static Func<T1, T2, T3, T5, TResult> Curry4<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> fn, T4 v4)
		{
			return (p1, p2, p3, p5) => fn(p1, p2, p3, v4, p5);
		}

		public static Func<T1, T2, T3, T4, TResult> Curry5<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> fn, T5 v5)
		{
			return (p1, p2, p3, p4) => fn(p1, p2, p3, p4, v5);
		}

		public static Action Curry1<T1>(this Action<T1> fn, T1 v1)
		{
			return () => fn(v1);
		}

		public static Action<T2> Curry1<T1, T2>(this Action<T1, T2> fn, T1 v1)
		{
			return p2 => fn(v1, p2);
		}

		public static Action<T1> Curry2<T1, T2>(this Action<T1, T2> fn, T2 v2)
		{
			return p1 => fn(p1, v2);
		}

		public static Action<T2, T3> Curry1<T1, T2, T3>(this Action<T1, T2, T3> fn, T1 v1)
		{
			return (p2, p3) => fn(v1, p2, p3);
		}

		public static Action<T1, T3> Curry2<T1, T2, T3>(this Action<T1, T2, T3> fn, T2 v2)
		{
			return (p1, p3) => fn(p1, v2, p3);
		}

		public static Action<T1, T2> Curry3<T1, T2, T3>(this Action<T1, T2, T3> fn, T3 v3)
		{
			return (p1, p2) => fn(p1, p2, v3);
		}

		public static Action<T2, T3, T4> Curry1<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> fn, T1 v1)
		{
			return (p2, p3, p4) => fn(v1, p2, p3, p4);
		}

		public static Action<T1, T3, T4> Curry2<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> fn, T2 v2)
		{
			return (p1, p3, p4) => fn(p1, v2, p3, p4);
		}

		public static Action<T1, T2, T4> Curry3<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> fn, T3 v3)
		{
			return (p1, p2, p4) => fn(p1, p2, v3, p4);
		}

		public static Action<T1, T2, T3> Curry4<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> fn, T4 v4)
		{
			return (p1, p2, p3) => fn(p1, p2, p3, v4);
		}

		public static Action<T2, T3, T4, T5> Curry1<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> fn, T1 v1)
		{
			return (p2, p3, p4, p5) => fn(v1, p2, p3, p4, p5);
		}

		public static Action<T1, T3, T4, T5> Curry2<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> fn, T2 v2)
		{
			return (p1, p3, p4, p5) => fn(p1, v2, p3, p4, p5);
		}

		public static Action<T1, T2, T4, T5> Curry3<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> fn, T3 v3)
		{
			return (p1, p2, p4, p5) => fn(p1, p2, v3, p4, p5);
		}

		public static Action<T1, T2, T3, T5> Curry4<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> fn, T4 v4)
		{
			return (p1, p2, p3, p5) => fn(p1, p2, p3, v4, p5);
		}

		public static Action<T1, T2, T3, T4> Curry5<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> fn, T5 v5)
		{
			return (p1, p2, p3, p4) => fn(p1, p2, p3, p4, v5);
		}

	}

}

