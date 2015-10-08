Namespace Icm.Collections.Generic.StructKeyStructValue

    ''' <summary>
    ''' Represents a generic sorted list of elements.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <typeparam name="TValue"></typeparam>
    ''' <remarks>
    ''' <para>In order to easily implement advanced storing schemes (a buckets system against
    ''' a database for example), it is mandatory not to reference an underlying indexing system
    ''' that associates consecutive integer numbers to the elements of the collection, something
    ''' happening with IDictionary through the properties Keys and Values.</para>
    ''' <para>Nevertheless, integer indexing is a very useful method for navigating these
    ''' structure, so advanced alternatie methods must be provided. </para>
    ''' </remarks>
    Public Interface ISortedCollection(Of TKey As {Structure, IComparable(Of TKey)}, TValue As Structure)

        Function GetFreeKey(ByVal desiredKey As TKey) As TKey?
        ''' <summary>
        ''' Devuelve una representación de cadena de un intervalo de la línea de tiempo.
        ''' Para imprimir los elementos se recurrirá a la función ToString.
        ''' </summary>
        ''' <param name="fromKey">Fecha inicial.</param>
        ''' <param name="toKey">Fecha final.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function ToString(ByVal fromKey As TKey, ByVal toKey As TKey) As String
        Function ToString() As String

        ' Key navigation
        Function [Next](ByVal key As TKey) As TKey?
        Function Previous(ByVal key As TKey) As TKey?
        Function KeyOrNext(ByVal key As TKey) As TKey?
        Function KeyOrPrev(ByVal key As TKey) As TKey?

        ReadOnly Property TotalOrder As ITotalOrder(Of TKey)

        Sub Add(ByVal key As TKey, ByVal value As TValue)
        Default Property Item(ByVal key As TKey) As TValue
        Sub Remove(ByVal key As TKey)
        Function ContainsKey(ByVal key As TKey) As Boolean

        Function IntervalEnumerable(ByVal intStart As TKey?, ByVal intEnd As TKey?) As IEnumerable(Of Vector2(Of Tuple(Of TKey, TValue?)))
        Function IntervalEnumerable(ByVal intf As Vector2(Of TKey?)) As IEnumerable(Of Vector2(Of Tuple(Of TKey, TValue?)))
        Function IntervalEnumerable() As IEnumerable(Of Vector2(Of Tuple(Of TKey, TValue?)))
        Function PointEnumerable(ByVal intStart As TKey?, ByVal intEnd As TKey?) As IEnumerable(Of Tuple(Of TKey, TValue?))
        Function PointEnumerable(ByVal intf As Vector2(Of TKey?)) As IEnumerable(Of Tuple(Of TKey, TValue?))
        Function PointEnumerable() As IEnumerable(Of Tuple(Of TKey, TValue?))

        Function Count() As Integer
    End Interface

End Namespace
