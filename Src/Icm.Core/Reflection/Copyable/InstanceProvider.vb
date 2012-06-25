Namespace Icm.Reflection
    ''' <summary>
    ''' Abstract class that implements the <see cref="IInstanceProvider"/> interface,
    ''' and can be used as a base class for an instance provider. The class simplifies
    ''' implementation by partially implementing the interface, leaving the implementation
    ''' of the CreateTypedCopy method to the concrete subclass.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    Public MustInherit Class InstanceProvider(Of T)
        Implements IInstanceProvider(Of T)
#Region "IInstanceProvider Members"

        Public ReadOnly Property Provided() As Type Implements IInstanceProvider(Of T).Provided
            Get
                Return GetType(T)
            End Get
        End Property

        Public Function CreateCopy(ByVal toBeCopied As Object) As Object Implements IInstanceProvider(Of T).CreateCopy
            Return CreateTypedCopy(DirectCast(toBeCopied, T))
        End Function

        Public MustOverride Function CreateTypedCopy(ByVal toBeCopied As T) As T Implements IInstanceProvider(Of T).CreateTypedCopy

#End Region
    End Class
End Namespace

