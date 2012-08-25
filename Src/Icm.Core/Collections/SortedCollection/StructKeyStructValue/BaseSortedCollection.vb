Namespace Icm.Collections.Generic.StructKeyStructValue
    Public MustInherit Class BaseSortedCollection(Of TKey As {Structure, IComparable(Of TKey)}, TValue As Structure)
        Implements ISortedCollection(Of TKey, TValue)

        Public Sub New(ByVal otkey As ITotalOrder(Of TKey))
            _totalOrder = otkey
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Private Class RangePointIterator
            Implements IEnumerator(Of Tuple(Of TKey, TValue?)), IEnumerable(Of Tuple(Of TKey, TValue?))

            Private f_ As ISortedCollection(Of TKey, TValue)
            Private rangeStart_ As TKey
            Private rangeEnd_ As TKey
            Private current_ As Tuple(Of TKey, TValue?)

            Private idx_ As Date

            Public Sub New(ByVal f As ISortedCollection(Of TKey, TValue), ByVal rangeStart As TKey?, ByVal rangeEnd As TKey?)
                rangeStart_ = f.TotalOrder.LstIfNull(rangeStart)
                rangeEnd_ = f.TotalOrder.GstIfNull(rangeEnd)
                f_ = f
            End Sub

            Public Sub New(ByVal f As ISortedCollection(Of TKey, TValue), ByVal intf As Vector2(Of TKey?))
                MyClass.New(f, intf.Item1, intf.Item2)
            End Sub

            Public ReadOnly Property Current() As Tuple(Of TKey, TValue?) Implements IEnumerator(Of Tuple(Of TKey, TValue?)).Current
                Get
                    If current_ Is Nothing Then
                        Throw New InvalidOperationException("Enumerator has not been reset")
                    End If
                    Return current_
                End Get
            End Property

            Public ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
                Get
                    If current_ Is Nothing Then
                        Throw New InvalidOperationException("Enumerator has not been reset")
                    End If
                    Return current_
                End Get
            End Property

            Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
                If current_ Is Nothing Then
                    current_ = Pair(rangeStart_)
                ElseIf current_.Item1.Equals(rangeEnd_) Then
                    current_ = Nothing
                    Return False
                Else
                    Dim sig = f_.Next(current_.Item1)


                    If sig.HasValue Then
                        If sig.Value.CompareTo(rangeEnd_) > 0 Then
                            current_ = Pair(rangeEnd_)
                            Return False
                        Else
                            current_ = Pair(sig.Value)
                        End If
                    Else
                        If current_.Item1.CompareTo(rangeEnd_) < 0 Then
                            current_ = Pair(rangeEnd_)
                        Else
                            ' NO DEBERÍA SUCEDER
                            Throw New Exception
                        End If
                    End If
                End If

                Return True
            End Function

            ReadOnly Property Pair(ByVal key As TKey) As Tuple(Of TKey, TValue?)
                Get
                    If f_.ContainsKey(key) Then
                        Return New Tuple(Of TKey, TValue?)(key, f_(key))
                    Else
                        Return New Tuple(Of TKey, TValue?)(key, Nothing)
                    End If
                End Get
            End Property

            Public Sub Reset() Implements IEnumerator.Reset
                current_ = Nothing
            End Sub

            Private disposedValue As Boolean = False        ' To detect redundant calls

            ' IDisposable
            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        ' free other state (managed objects).
                    End If

                    ' free your own state (unmanaged objects).
                    ' set large fields to null.
                End If
                Me.disposedValue = True
            End Sub

#Region " IDisposable Support "
            ' This code added by Visual Basic to correctly implement the disposable pattern.
            Public Sub Dispose() Implements IDisposable.Dispose
                ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub
#End Region

            Public Function GetEnumerator() As IEnumerator(Of Tuple(Of TKey, TValue?)) Implements IEnumerable(Of Tuple(Of TKey, TValue?)).GetEnumerator
                Return Me
            End Function

            Public Function GetEnumerator1() As IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
                Return Me
            End Function
        End Class


        Private _totalOrder As ITotalOrder(Of TKey)

        ReadOnly Property TotalOrder As ITotalOrder(Of TKey) Implements ISortedCollection(Of TKey, TValue).TotalOrder
            Get
                Return _totalOrder
            End Get
        End Property

        Public MustOverride Sub Add(ByVal key As TKey, ByVal value As TValue) Implements ISortedCollection(Of TKey, TValue).Add

        Public MustOverride Function ContainsKey(ByVal key As TKey) As Boolean Implements ISortedCollection(Of TKey, TValue).ContainsKey

        Public Function GetFreeKey(ByVal desiredKey As TKey) As TKey? Implements ISortedCollection(Of TKey, TValue).GetFreeKey
            Dim fFinal As TKey?
            fFinal = desiredKey
            Do While fFinal.HasValue AndAlso ContainsKey(fFinal.Value)
                If TotalOrder.Compare(fFinal.Value, TotalOrder.Greatest) = 0 Then
                    Return Nothing
                End If
                fFinal = TotalOrder.Next(fFinal.Value)
            Loop
            Return fFinal
        End Function

        Default Public MustOverride Property Item(ByVal key As TKey) As TValue Implements ISortedCollection(Of TKey, TValue).Item

        Public MustOverride Function [Next](ByVal key As TKey) As TKey? Implements ISortedCollection(Of TKey, TValue).Next

        Public MustOverride Function Previous(ByVal key As TKey) As TKey? Implements ISortedCollection(Of TKey, TValue).Previous

        Public Overrides Function ToString() As String Implements ISortedCollection(Of TKey, TValue).ToString
            Return ToString(TotalOrder.Least, TotalOrder.Greatest)
        End Function


        ''' <summary>
        ''' String representation of an interval of the sorted collection.
        ''' For printing values, ToString will be used.
        ''' </summary>
        ''' <param name="f1">Initial key.</param>
        ''' <param name="f2">Final key.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Overloads Function ToString(ByVal f1 As TKey, ByVal f2 As TKey) As String Implements ISortedCollection(Of TKey, TValue).ToString
            Dim result As New System.Text.StringBuilder

            For Each element In PointEnumerable(f1, f2)
                If element.Item2 Is Nothing Then
                    result.AppendFormat("NC {0} ---" & vbCrLf, element.Item1)
                Else
                    result.AppendFormat("-> {0} {1}" & vbCrLf, element.Item1, element.Item2.ToString)
                End If
            Next

            Return result.ToString
        End Function

        Public MustOverride Function KeyOrNext(ByVal key As TKey) As TKey? Implements ISortedCollection(Of TKey, TValue).KeyOrNext

        Public MustOverride Function KeyOrPrev(ByVal key As TKey) As TKey? Implements ISortedCollection(Of TKey, TValue).KeyOrPrev

        Public MustOverride Sub Remove(ByVal key As TKey) Implements ISortedCollection(Of TKey, TValue).Remove

        Public Function IntervalEnumerable(ByVal intf As Vector2(Of TKey?)) As IEnumerable(Of Vector2(Of Tuple(Of TKey, TValue?))) Implements ISortedCollection(Of TKey, TValue).IntervalEnumerable
            Return IntervalEnumerable(intf.Item1, intf.Item2)
        End Function

        Public Function IntervalEnumerable(ByVal intStart As TKey?, ByVal intEnd As TKey?) As IEnumerable(Of Vector2(Of Tuple(Of TKey, TValue?))) Implements ISortedCollection(Of TKey, TValue).IntervalEnumerable
            Dim it = New PairEnumerator(Of Tuple(Of TKey, TValue?))(
                New RangePointIterator(Me, intStart, intEnd))
            Return it
        End Function

        Public MustOverride Function Count() As Integer Implements ISortedCollection(Of TKey, TValue).Count

        Public Function PointEnumerable(ByVal intStart As TKey?, ByVal intEnd As TKey?) As System.Collections.Generic.IEnumerable(Of System.Tuple(Of TKey, TValue?)) Implements ISortedCollection(Of TKey, TValue).PointEnumerable
            Return New RangePointIterator(Me, intStart, intEnd)
        End Function

        Public Function PointEnumerable(ByVal intf As Vector2(Of TKey?)) As System.Collections.Generic.IEnumerable(Of System.Tuple(Of TKey, TValue?)) Implements ISortedCollection(Of TKey, TValue).PointEnumerable
            Return PointEnumerable(intf.Item1, intf.Item2)
        End Function

        Public Function IntervalEnumerable() As System.Collections.Generic.IEnumerable(Of Vector2(Of System.Tuple(Of TKey, TValue?))) Implements ISortedCollection(Of TKey, TValue).IntervalEnumerable
            Return IntervalEnumerable(Nothing, Nothing)
        End Function

        Public Function PointEnumerable() As System.Collections.Generic.IEnumerable(Of System.Tuple(Of TKey, TValue?)) Implements ISortedCollection(Of TKey, TValue).PointEnumerable
            Return PointEnumerable(Nothing, Nothing)
        End Function
    End Class
End Namespace

