using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Icm
{
	/// <summary>
	/// Enables the efficient, dynamic composition of query predicates.
	/// </summary>
	/// <remarks>Pete Montgomery's Universal Predicate Builder. See https://petemontgomery.wordpress.com/2011/02/10/a-universal-predicatebuilder/ for mor information.</remarks>
	public static class PredicateBuilder
	{

		/// <summary>
		/// Creates a predicate that evaluates to true.
		/// </summary>
		public static Expression<Func<T, bool>> True<T>()
		{
			return param => true;
		}

		/// <summary>
		/// Creates a predicate that evaluates to false.
		/// </summary>
		public static Expression<Func<T, bool>> False<T>()
		{
			return param => false;
		}

		/// <summary>
		/// Creates a predicate expression from the specified lambda expression.
		/// </summary>
		public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> predicate)
		{
			return predicate;
		}

		/// <summary>
		/// Combines the first predicate with the second using the logical "and".
		/// </summary>
		[Extension()]
		public static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
		{
			return first.Compose(second, Expression.AndAlso);
		}

		/// <summary>
		/// Combines the first predicate with the second using the logical "or".
		/// </summary>
		[Extension()]
		public static Expression<Func<T, bool>> Or<T>(Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
		{
			return first.Compose(second, Expression.OrElse);
		}

		/// <summary>
		/// Negates the predicate.
		/// </summary>
		[Extension()]
		public static Expression<Func<T, bool>> Not<T>(Expression<Func<T, bool>> expr)
		{
			dynamic negated = Expression.Not(expr.Body);
			return Expression.Lambda<Func<T, bool>>(negated, expr.Parameters);
		}

		/// <summary>
		/// Combines the first expression with the second using the specified merge function.
		/// </summary>
		[Extension()]
		private static Expression<T> Compose<T>(Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
		{
			// zip parameters (map from parameters of second to parameters of first)
			dynamic map = first.Parameters.Select((f, i) => new {
				f,
				s = second.Parameters(i)
			}).ToDictionary(p => p.s, p => p.f);

			// replace parameters in the second lambda expression with the parameters in the first
			dynamic secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

			// create a merged lambda expression with parameters from the first expression
			return Expression.Lambda<T>(merge(first.Body, (Expression)secondBody), first.Parameters);
		}

		private class ParameterRebinder : ExpressionVisitor
		{


			private readonly Dictionary<ParameterExpression, ParameterExpression> map;
			private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
			{
				this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
			}

			public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
			{
				return new ParameterRebinder(map).Visit(exp);
			}

			protected override Expression VisitParameter(ParameterExpression p)
			{
				ParameterExpression replacement = null;

				if (map.TryGetValue(p, replacement)) {
					p = replacement;
				}

				return base.VisitParameter(p);
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
