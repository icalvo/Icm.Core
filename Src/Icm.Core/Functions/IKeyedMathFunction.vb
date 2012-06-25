Imports Icm.Collections.Generic.StructKeyStructValue

Namespace Icm.Functions
    Public Interface IKeyedMathFunction(Of TX As {Structure, IComparable(Of TX)}, TY As {Structure, IComparable(Of TY)})
        Inherits IMathFunction(Of TX, TY)

        Function Range(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal includeExtremes As Boolean) As RangeIterator(Of TX, TY)
        Function RangeFrom(ByVal rangeStart As TX, ByVal includeExtremes As Boolean) As RangeIterator(Of TX, TY)
        Function RangeTo(ByVal rangeEnd As TX, ByVal includeExtremes As Boolean) As RangeIterator(Of TX, TY)
        Function TotalRange(ByVal includeExtremes As Boolean) As RangeIterator(Of TX, TY)
        Function PreviousOrLst(ByVal d As TX) As TX?
        ReadOnly Property KeyStore() As ISortedCollection(Of TX, TY)
    End Interface
End Namespace
