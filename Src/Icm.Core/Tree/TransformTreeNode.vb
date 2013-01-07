Namespace Icm

    ''' <summary>
    ''' A TransformTreeNode(Of T1,T2) acts as a proxy for a ITreeNode(Of T1), giving the full funcionality of
    ''' an ITreeNode(Of T2) by using a transformation function from T1 to T2. It builds its members
    ''' </summary>
    ''' <typeparam name="T1"></typeparam>
    ''' <typeparam name="T2"></typeparam>
    ''' <remarks></remarks>
    Public Class TransformTreeNode(Of T1, T2)
        Implements ITreeNode(Of T2)

        Private _basenode As ITreeNode(Of T1)
        Private _transform As Func(Of T1, T2)
        Private _node As ITreeNode(Of T2)
        Private _value As T2

        Public Sub New(node As ITreeNode(Of T1), transform As Func(Of T1, T2))
            _basenode = node
            _transform = transform
        End Sub
        Public Function GetChildren() As IEnumerable(Of ITreeNode(Of T2)) Implements ITreeNode(Of T2).GetChildren
#If Framework = "Net35" Then
            Return _basenode.GetChildren().Select(Function(child) New TransformTreeNode(Of T1, T2)(child, _transform)))
#Else
            Return _basenode.GetChildren().Select(Function(child) DirectCast(New TransformTreeNode(Of T1, T2)(child, _transform), ITreeNode(Of T2)))
#End If
        End Function

        Public Function GetParent() As ITreeNode(Of T2) Implements ITreeNode(Of T2).GetParent
            Dim sourceParent = _basenode.GetParent
            If sourceParent Is Nothing Then
                Return Nothing
            Else
                Return New TransformTreeNode(Of T1, T2)(sourceParent, _transform)
            End If
        End Function

        Property Value As T2 Implements ITreeNode(Of T2).Value
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
    End Class
End Namespace
