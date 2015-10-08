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
		[Extension()]
		public static string GetInfo<T>(Expression<Func<T, object>> action) where T : class
		{
			dynamic expression = GetMemberInfo(action);
			return expression.Member.Name;
		}

		private static MemberExpression GetMemberInfo(Expression method)
		{
			dynamic lambda = method as LambdaExpression;
			if (lambda == null) {
				throw new ArgumentNullException("method");
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

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
