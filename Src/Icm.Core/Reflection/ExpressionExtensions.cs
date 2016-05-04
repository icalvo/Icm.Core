using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Icm.Reflection
{

	public static class ExpressionExtensions
	{

		/// <summary>
		/// Gets the name of a class member invoked in a expression.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="action"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string GetInfo<T>(this Expression<Func<T, object>> action) where T : class
		{
			var expression = GetMemberInfo(action);
			return expression.Member.Name;
		}

		private static MemberExpression GetMemberInfo(Expression method)
		{
			var lambda = method as LambdaExpression;
			if (lambda == null) {
				throw new ArgumentNullException(nameof(method));
			}

			MemberExpression memberExpr = null;

			switch (lambda.Body.NodeType) {
				case ExpressionType.Convert:
					memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
					break;
				case ExpressionType.MemberAccess:
					memberExpr = lambda.Body as MemberExpression;
					break;
				default:
					throw new ArgumentException("The expression is not of type Convert or MemberAccess", "method");
			}

			return memberExpr;
		}
	}
}