Imports System.Runtime.CompilerServices

Namespace Icm.Collections

    Public Module ArrayExtensions


        ''' <summary>
        '''  Gets the values along a single "row" of a multidimensional
        ''' array (the values you obtain by fixing all the dimensions but one).
        ''' </summary>
        ''' <typeparam name="T">Type of the array elements</typeparam>
        ''' <param name="a">Multi-dimensional array</param>
        ''' <param name="iteratingDimension">Dimension alongside which we iterate.</param>
        ''' <param name="fixedDimensionValues">Values for the rest of dimensions.</param>
        ''' <returns>1-dimensional array of type T</returns>
        ''' <exception cref="IndexOutOfRangeException">Any element in indices is outside the range of valid indexes for the corresponding dimension of the current Array.</exception>
        ''' <remarks>
        ''' For example, for a 4-dimensional array we may want
        ''' all the elements of the form A(2, 4, i, 6). To obtain those
        ''' elements we will do one of these calls:
        ''' <code>
        '''   result = A.MultiGetRow(2, New Integer() {2, 4, 6})
        '''   result = A.MultiGetRow(2, 2, 4, 6)
        ''' </code>
        ''' When iterating a jagged array, it may be that some of the
        ''' values of the iterated row are undefined.
        ''' </remarks>
        <Extension>
        Function MultiGetRow(Of T)(
            ByVal a As Array,
            ByVal iteratingDimension As Integer,
            ByVal ParamArray fixedDimensionValues() As Integer) As T()

            Dim indices(fixedDimensionValues.GetLength(0)) As Integer
            Dim result(a.GetLength(iteratingDimension) - 1) As T

            For i = 0 To iteratingDimension - 1
                indices(i) = fixedDimensionValues(i)
            Next

            For i = iteratingDimension + 1 To indices.GetUpperBound(0)
                indices(i) = fixedDimensionValues(i - 1)
            Next

            For i = 0 To a.GetUpperBound(iteratingDimension)
                indices(iteratingDimension) = i
                result(i) = DirectCast(a.GetValue(indices), T)
            Next

            Return result
        End Function

    End Module

End Namespace
