using System;
using System.Runtime.CompilerServices;

namespace Icm.Collections
{

	public static class ArrayExtensions
	{


		/// <summary>
		///  Gets the values along a single "row" of a multidimensional
		/// array (the values you obtain by fixing all the dimensions but one).
		/// </summary>
		/// <typeparam name="T">Type of the array elements</typeparam>
		/// <param name="a">Multi-dimensional array</param>
		/// <param name="iteratingDimension">Dimension alongside which we iterate.</param>
		/// <param name="fixedDimensionValues">Values for the rest of dimensions.</param>
		/// <returns>1-dimensional array of type T</returns>
		/// <exception cref="IndexOutOfRangeException">Any element in indices is outside the range of valid indexes for the corresponding dimension of the current Array.</exception>
		/// <remarks>
		/// For example, for a 4-dimensional array we may want
		/// all the elements of the form A(2, 4, i, 6). To obtain those
		/// elements we will do one of these calls:
		/// <code>
		///   result = A.MultiGetRow(2, New Integer() {2, 4, 6})
		///   result = A.MultiGetRow(2, 2, 4, 6)
		/// </code>
		/// When iterating a jagged array, it may be that some of the
		/// values of the iterated row are undefined.
		/// </remarks>
		public static T[] MultiGetRow<T>(this Array a, int iteratingDimension, params int[] fixedDimensionValues)
		{

			int[] indices = new int[fixedDimensionValues.GetLength(0) + 1];
			T[] result = new T[a.GetLength(iteratingDimension)];

			for (var i = 0; i <= iteratingDimension - 1; i++) {
				indices[i] = fixedDimensionValues[i];
			}

			for (var i = iteratingDimension + 1; i <= indices.GetUpperBound(0); i++) {
				indices[i] = fixedDimensionValues[i - 1];
			}

			for (var i = 0; i <= a.GetUpperBound(iteratingDimension); i++) {
				indices[iteratingDimension] = i;
				result[i] = (T)a.GetValue(indices);
			}

			return result;
		}

	}

}