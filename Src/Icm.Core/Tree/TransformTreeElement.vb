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

        Private ReadOnly _baseElement As ITreeElement(Of T1)
        Private ReadOnly _transform As Func(Of T1, T2)
        Private _value As T2

        Public Sub New(node As ITreeElement(Of T1), transform As Func(Of T1, T2))
            _baseElement = node
            _transform = transform
        End Sub

        Protected ReadOnly Property Transform As Func(Of T1, T2)
            Get
                Return _transform
            End Get
        End Property

        Public Function GetChildren() As IEnumerable(Of ITreeElement(Of T2)) Implements ITreeElement(Of T2).GetChildElements
#If FrameworkNet35 Then
            Return _baseElement.GetChildElements.Select(Function(child) DirectCast(New TransformTreeElement(Of T1, T2)(child, Transform), ITreeElement(Of T2)))
#Else
            Return _baseElement.GetChildElements.Select(Function(child) New TransformTreeElement(Of T1, T2)(child, Transform))
#End If
        End Function

        Property Value As T2 Implements ITreeElement(Of T2).Value
            Get
                If _value Is Nothing Then
                    _value = Transform(_baseElement.Value)
                End If
                Return _value
            End Get
            Set(value As T2)
                Throw New InvalidOperationException("Cannot modify value of a TransformTreeNode")
            End Set
        End Property

    End Class
End Namespace
