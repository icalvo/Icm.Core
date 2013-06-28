Namespace Icm.Tree
    ''' <summary>
    ''' Proxy for a ITreeElement(Of T1) that implements an ITreeElement(Of T2).)
    ''' </summary>
    ''' <typeparam name="T1"></typeparam>
    ''' <typeparam name="T2"></typeparam>
    ''' <remarks>A TransformTreeElement(Of T1,T2) gives the full funcionality of
    ''' an ITreeElement(Of T2) by using a transformation function from T1 to T2. It builds its members on the fly,
    ''' whenever GetChildren is called, by building new TransformTreeElements wrapping the original children.</remarks>
    Public Class TransformTreeElement(Of T1, T2)
        Implements ITreeElement(Of T2)

        Private ReadOnly _basenode As ITreeElement(Of T1)
        Private ReadOnly _transform As Func(Of T1, T2)
        Private _value As T2

        ''' <summary>
        ''' Constructs with a given ITreeElement and a transformation function.
        ''' </summary>
        ''' <param name="node"></param>
        ''' <param name="transform"></param>
        ''' <remarks></remarks>
        Public Sub New(node As ITreeElement(Of T1), transform As Func(Of T1, T2))
            _basenode = node
            _transform = transform
        End Sub

        ''' <inheritdoc />
        Public Function GetChildren() As IEnumerable(Of ITreeElement(Of T2))
#If Framework = "Net35" Then
                Return _basenode.GetChildren().Select(Function(child) DirectCast(New TransformTreeNode(Of T1, T2)(child, _transform), ITreeNode(Of T2)))
#Else
            Return _basenode.Select(
                Function(child)
                    Dim result As ITreeElement(Of T2)
                    If TypeOf child Is ITreeNode(Of T1) Then
                        result = New TransformTreeNode(Of T1, T2)(DirectCast(child, ITreeNode(Of T1)), _transform)
                    Else
                        result = New TransformTreeElement(Of T1, T2)(child, _transform)
                    End If
                    Return result
                End Function)
#End If
        End Function

        ''' <inheritdoc />
        Property Value As T2 Implements ITreeElement(Of T2).Value
            Get
                If _value Is Nothing Then
                    _value = _transform(_basenode.Value)
                End If
                Return _value
            End Get
            Set(value As T2)
                Throw New InvalidOperationException("Cannot modify value of a TransformTreeNode")
            End Set
        End Property

        ''' <inheritdoc />
        Public Function GetEnumerator() As IEnumerator(Of ITreeElement(Of T2)) Implements IEnumerable(Of ITreeElement(Of T2)).GetEnumerator
            Return GetChildren.GetEnumerator
        End Function

        ''' <inheritdoc />
        Public Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetChildren.GetEnumerator
        End Function

    End Class
End Namespace
