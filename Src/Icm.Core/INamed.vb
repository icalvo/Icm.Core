Namespace Icm

    ''' <summary>
    ''' Objects with a name or, if the name is null, a prefix with which a new name
    ''' can be generated (adding a unique number to the prefix)
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface INamed
        Property Name() As String
    End Interface

End Namespace
