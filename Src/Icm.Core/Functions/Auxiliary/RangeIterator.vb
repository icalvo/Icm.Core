
Namespace Icm.Functions
    ''' <summary>
    ''' Iterator over a range of TX of function keys.
    ''' </summary>
    ''' <remarks>The iteration value is a segment between two points of the function. At the first and last points,
    ''' either extreme of the segment could be not a key (they would be the start and end of the iteration).
    ''' </remarks>
    Public Class RangeIterator(Of TX As {Structure, IComparable(Of TX)}, TY As {Structure, IComparable(Of TY)})
        Implements IEnumerator(Of FunctionPointPair(Of TX, TY)), IEnumerable(Of FunctionPointPair(Of TX, TY))

        Private _firstKey As TX
        Private _lastKey As TX
        Private f_ As IKeyedMathFunction(Of TX, TY)
        Private rangeStart_ As TX
        Private rangeEnd_ As TX
        Private current_ As FunctionPointPair(Of TX, TY)
        Private interiorKeysEnumerator_ As IEnumerator(Of Vector2(Of Tuple(Of TX, TY?)))
        Private idx_ As Integer

        Public Sub New(ByVal f As IKeyedMathFunction(Of TX, TY), ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal includeExtremes As Boolean)
            _firstKey = f.KeyStore.KeyOrNext(rangeStart).Value
            _lastKey = f.KeyStore.KeyOrPrev(rangeEnd).Value
            If includeExtremes Then
                rangeStart_ = rangeStart
                rangeEnd_ = rangeEnd
            Else
                rangeStart_ = _firstKey
                rangeEnd_ = _lastKey
            End If
            f_ = f
            interiorKeysEnumerator_ = f_.KeyStore.IntervalEnumerable(_firstKey, _lastKey).GetEnumerator
        End Sub

        Private ReadOnly Property Current() As FunctionPointPair(Of TX, TY) Implements System.Collections.Generic.IEnumerator(Of FunctionPointPair(Of TX, TY)).Current
            Get
                If current_ Is Nothing Then
                    Throw New InvalidOperationException("Enumerator has not been reset")
                End If
                Return current_
            End Get
        End Property

        Private ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
            Get
                If current_ Is Nothing Then
                    Throw New InvalidOperationException("Enumerator has not been reset")
                End If
                Return current_
            End Get
        End Property

        Private Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            If current_ Is Nothing Then
                current_ = New FunctionPointPair(Of TX, TY)(f_)
                current_.Item1.X = rangeStart_

            ElseIf current_.Item2.X.Equals(rangeEnd_) Then
                current_ = Nothing
                Return False
            Else
                current_.Item1.X = Current.Item2.X
                idx_ += 1
            End If
            If rangeStart_.Equals(_firstKey) Then
                interiorKeysEnumerator_.MoveNext()
            End If

            current_.Item2.X = interiorKeysEnumerator_.Current.Item2.Item1

            Return True
        End Function

        Private Sub Reset() Implements System.Collections.IEnumerator.Reset
            current_ = Nothing
            interiorKeysEnumerator_.Reset()
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

        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of FunctionPointPair(Of TX, TY)) Implements System.Collections.Generic.IEnumerable(Of FunctionPointPair(Of TX, TY)).GetEnumerator
            Return Me
        End Function

        Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Me
        End Function
    End Class
End Namespace
