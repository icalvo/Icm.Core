using System;
using System.Runtime.CompilerServices;

namespace Icm.Functions
{

	public static class ThresholdTypeExtensions
	{

		/// <summary>
		///  Opposed threshold type
		/// </summary>
		/// <param name="tu"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static ThresholdType Opposed(ThresholdType tu)
		{
			switch (tu) {
				case ThresholdType.LeftOpen:
					return ThresholdType.RightClosed;
				case ThresholdType.LeftClosed:
					return ThresholdType.RightOpen;
				case ThresholdType.RightClosed:
					return ThresholdType.LeftOpen;
				case ThresholdType.RightOpen:
					return ThresholdType.LeftClosed;
				default:
					throw new ArgumentException();
			}
		}

		/// <summary>
		/// Converts an operator string into a threshold type.
		/// </summary>
		/// <param name="op"></param>
		/// <returns></returns>
		/// <remarks>
		/// <para>The equivalent operator for a threshold type is OP, where:</para>
		/// <code>X OP THRESHOLD</code>
		/// </remarks>
		public static ThresholdType Operator2ThresholdType(string op)
		{
			switch (op) {
				case "<":
					return ThresholdType.LeftClosed;
				case "<=":
					return ThresholdType.LeftOpen;
				case ">":
					return ThresholdType.RightClosed;
				case ">=":
					return ThresholdType.RightOpen;
				default:
					throw new ArgumentException(string.Format("Unknown operator: {0}", op), "op");
			}
		}

		/// <summary>
		/// Converts a threshold type into its equivalent operator string.
		/// </summary>
		/// <param name="tu"></param>
		/// <returns></returns>
		/// <remarks>
		/// <para>The equivalent operator for a threshold type is OP, where:</para>
		/// <code>X OP THRESHOLD</code>
		/// </remarks>
		[Extension()]
		public static string GetOperator(ThresholdType tu)
		{
			switch (tu) {
				case ThresholdType.LeftClosed:
					return "<";
				case ThresholdType.LeftOpen:
					return "<=";
				case ThresholdType.RightClosed:
					return ">";
				case ThresholdType.RightOpen:
					return ">=";
				default:
					throw new InvalidOperationException(string.Format("Unknown threshold type: {0}", Convert.ToInt32(tu)));
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
