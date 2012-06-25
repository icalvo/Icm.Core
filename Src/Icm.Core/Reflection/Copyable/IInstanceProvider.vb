Namespace Icm.Reflection
    ''' <summary>
    ''' An interface defining an instance provider, i.e. an object that can create instances of a specific type.
    ''' If an instance of a class cannot be deduced automatically by the Copyable framework, and the class
    ''' cannot implement <see cref="ICopyable" />, then creating an instance provider is the preferred
    ''' way of making the class copyable.
    ''' </summary>
    Public Interface IInstanceProvider
        ''' <summary>
        ''' The type for which the provider provides instances.
        ''' </summary>
        ReadOnly Property Provided() As Type
        ''' <summary>
        ''' Creates a new instance.
        ''' </summary>
        ''' <param name="toBeCopied">The object to be copied.</param>
        ''' <returns>The created instance.</returns>
        Function CreateCopy(ByVal toBeCopied As Object) As Object
    End Interface

    ''' <summary>
    ''' The generic version of <see cref="IInstanceProvider" />, defining a strongly typed way of providing instances.
    ''' </summary>
    ''' <typeparam name="T">The type of the instances provided by the implementor.</typeparam>
    Public Interface IInstanceProvider(Of T)
        Inherits IInstanceProvider
        ''' <summary>
        ''' Creates a new typed instance.
        ''' </summary>
        ''' <returns></returns>
        Function CreateTypedCopy(ByVal toBeCopied As T) As T
    End Interface
End Namespace

