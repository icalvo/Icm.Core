Namespace Icm.Collections.Generic

    ''' <summary>
    ''' Transforms an IEnumerator(Of T) into an IEnumerator(Of Vector2(Of T)) which enumerates
    ''' the consecutive pairs of the original IEnumerator.
    ''' </summary>
    ''' <remarks>
    ''' <para>For example, if the original IEnumerator contains three elements a, b, c, this enumerator
    ''' will have the following two elements: (a, b), (b, c).</para>
    ''' <para>If the original IEnumerator contains just one element x, this class will return a single
    ''' pair (x, x).</para>
    ''' <para>This enumerator will NOT dispose the original enumerator.</para>
    ''' </remarks>
    Public Class PairEnumerator(Of T)
        Implements IEnumerator(Of Vector2(Of T)), IEnumerable(Of Vector2(Of T))

        Private current_ As Vector2(Of T)
        Private ReadOnly pointIter_ As IEnumerator(Of T)

        Public Sub New(ByVal _pointIter As IEnumerator(Of T))
            pointIter_ = _pointIter
        End Sub

        Public ReadOnly Property Current() As Vector2(Of T) Implements IEnumerator(Of Vector2(Of T)).Current
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
            Dim item2 As T

            If current_ Is Nothing Then
                pointIter_.MoveNext()
                Dim item1 = pointIter_.Current
                If pointIter_.MoveNext Then
                    item2 = pointIter_.Current
                Else
                    item2 = item1
                End If
                current_ = New Vector2(Of T)(item1, item2)
            Else
                If pointIter_.MoveNext Then
                    item2 = pointIter_.Current
                    current_ = New Vector2(Of T)(current_.Item2, item2)
                Else
                    current_ = Nothing
                    Return False
                End If
            End If
            Return True
        End Function


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

        Public Function GetEnumerator() As IEnumerator(Of Vector2(Of T)) Implements IEnumerable(Of Vector2(Of T)).GetEnumerator
            Return Me
        End Function

        Public Function GetEnumerator1() As IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Me
        End Function
    End Class
End Namespace
