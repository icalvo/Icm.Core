Imports Icm.Collections.Generic.StructKeyStructValue

Namespace Icm.Functions

    ''' <summary>
    ''' A keyed function is a math function from TX to TY which value is determined by a series
    ''' of "keys", or pairs (x As TX, y As TY) that fulfills F(x) = y. The rest of values is
    ''' obtained by means of an interpolation that will be based on the keys.
    ''' </summary>
    ''' <typeparam name="TX">Domain</typeparam>
    ''' <typeparam name="TY">Image</typeparam>
    ''' <remarks></remarks>
    Public MustInherit Class BaseKeyedMathFunction(Of TX As {Structure, IComparable(Of TX)}, TY As {Structure, IComparable(Of TY)})
        Inherits MathFunction(Of TX, TY)
        Implements ISortedCollection(Of TX, TY)
        Implements IKeyedMathFunction(Of TX, TY)

        Private store_ As ISortedCollection(Of TX, TY)

        Protected Sub New(ByVal otx As ITotalOrder(Of TX), ByVal oty As ITotalOrder(Of TY), ByVal coll As ISortedCollection(Of TX, TY))
            MyBase.New(otx, oty)
            store_ = coll
        End Sub

        Protected Sub New(ByVal fc As IKeyedMathFunction(Of TX, TY))
            MyBase.New(fc.OrdenTotalTX, fc.OrdenTotalTY)
            store_ = fc.KeyStore
        End Sub

        Protected ReadOnly Property KeyStore() As ISortedCollection(Of TX, TY) Implements IKeyedMathFunction(Of TX, TY).KeyStore
            Get
                Return store_
            End Get
        End Property

        Public Function Range(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal includeExtremes As Boolean) As RangeIterator(Of TX, TY) Implements IKeyedMathFunction(Of TX, TY).Range
            Dim it As New RangeIterator(Of TX, TY)(Me, rangeStart, rangeEnd, includeExtremes)

            Return it
        End Function

        Public Function RangeFrom(ByVal rangeStart As TX, ByVal includeExtremes As Boolean) As RangeIterator(Of TX, TY) Implements IKeyedMathFunction(Of TX, TY).RangeFrom
            Dim it As New RangeIterator(Of TX, TY)(Me, rangeStart, GstX, includeExtremes)

            Return it
        End Function

        Public Function RangeTo(ByVal rangeEnd As TX, ByVal includeExtremes As Boolean) As RangeIterator(Of TX, TY) Implements IKeyedMathFunction(Of TX, TY).RangeTo
            Dim it As New RangeIterator(Of TX, TY)(Me, LstX, rangeEnd, includeExtremes)

            Return it
        End Function

        Public Function TotalRange(ByVal includeExtremes As Boolean) As RangeIterator(Of TX, TY) Implements IKeyedMathFunction(Of TX, TY).TotalRange
            Dim it As New RangeIterator(Of TX, TY)(Me, LstX, GstX, includeExtremes)

            Return it
        End Function

        Public Function PreviousOrLst(ByVal d As TX) As TX? Implements IKeyedMathFunction(Of TX, TY).PreviousOrLst
            If d.Equals(LstX()) Then
                Return d
            Else
                Return KeyStore.Previous(d)
            End If
        End Function



        Public Sub Add(ByVal key As TX, ByVal value As TY) Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).Add
            If AbsMaxXY.Y.CompareTo(value) < 0 Then
                AbsMaxXY.X = key
            End If
            If AbsMinXY.Y.CompareTo(value) > 0 Then
                AbsMinXY.X = key
            End If
            KeyStore.Add(key, value)
        End Sub

        Public Function ContainsKey(ByVal key As TX) As Boolean Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).ContainsKey
            Return KeyStore.ContainsKey(key)
        End Function

        Public Function Count() As Integer Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).Count
            Return KeyStore.Count
        End Function

        Public Function GetFreeKey(ByVal desiredKey As TX) As TX? Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).GetFreeKey
            Return KeyStore.GetFreeKey(desiredKey)
        End Function

        Public Function IntervalEnumerable(ByVal intStart As TX?, ByVal intEnd As TX?) As System.Collections.Generic.IEnumerable(Of Vector2(Of System.Tuple(Of TX, TY?))) Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).IntervalEnumerable
            Return KeyStore.IntervalEnumerable(intStart, intEnd)
        End Function

        Public Function IntervalEnumerable(ByVal intf As Vector2(Of TX?)) As System.Collections.Generic.IEnumerable(Of Vector2(Of System.Tuple(Of TX, TY?))) Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).IntervalEnumerable
            Return KeyStore.IntervalEnumerable(intf)
        End Function

        Public Function IntervalEnumerable() As System.Collections.Generic.IEnumerable(Of Vector2(Of System.Tuple(Of TX, TY?))) Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).IntervalEnumerable
            Return KeyStore.IntervalEnumerable(Nothing, Nothing)
        End Function

        Public Property Item(ByVal key As TX) As TY Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).Item
            Get
                Return KeyStore(key)
            End Get
            Set(ByVal value As TY)
                KeyStore(key) = value
            End Set
        End Property

        Public Function KeyOrNext(ByVal key As TX) As TX? Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).KeyOrNext
            Return KeyStore.KeyOrNext(key)
        End Function

        Public Function KeyOrPrev(ByVal key As TX) As TX? Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).KeyOrPrev
            Return KeyStore.KeyOrPrev(key)
        End Function

        Public Function [Next](ByVal key As TX) As TX? Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).Next
            Return KeyStore.Next(key)
        End Function

        Public Function PointEnumerable(ByVal intStart As TX?, ByVal intEnd As TX?) As System.Collections.Generic.IEnumerable(Of System.Tuple(Of TX, TY?)) Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).PointEnumerable
            Return KeyStore.PointEnumerable(intStart, intEnd)
        End Function

        Public Function PointEnumerable(ByVal intf As Vector2(Of TX?)) As System.Collections.Generic.IEnumerable(Of System.Tuple(Of TX, TY?)) Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).PointEnumerable
            Return KeyStore.PointEnumerable(intf)
        End Function

        Public Function PointEnumerable() As System.Collections.Generic.IEnumerable(Of System.Tuple(Of TX, TY?)) Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).PointEnumerable
            Return KeyStore.PointEnumerable(Nothing, Nothing)
        End Function

        Public Function Previous(ByVal key As TX) As TX? Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).Previous
            Return KeyStore.Previous(key)
        End Function

        Public Sub Remove(ByVal key As TX) Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).Remove
            KeyStore.Remove(key)
        End Sub

        Public Overrides Function ToString() As String Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).ToString
            Return KeyStore.ToString
        End Function

        Public Overloads Function ToString(ByVal fromKey As TX, ByVal toKey As TX) As String Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).ToString
            Return KeyStore.ToString(fromKey, toKey)
        End Function

        Public ReadOnly Property TotalOrder As ITotalOrder(Of TX) Implements Collections.Generic.StructKeyStructValue.ISortedCollection(Of TX, TY).TotalOrder
            Get
                Return KeyStore.TotalOrder
            End Get
        End Property

        Public Overrides Function AbsMaxXY() As FunctionPoint(Of TX, TY)
            Return MaxXY(TotalOrder.Least, TotalOrder.Greatest)
        End Function

        Public Overrides Function AbsMinXY() As FunctionPoint(Of TX, TY)
            Return MinXY(TotalOrder.Least, TotalOrder.Greatest)
        End Function

    End Class

End Namespace
