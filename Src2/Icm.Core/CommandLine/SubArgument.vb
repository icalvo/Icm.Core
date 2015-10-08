
Namespace Icm.CommandLineTools
    ''' <summary>
    '''  Subargument of a command line option.
    ''' </summary>
    ''' <remarks></remarks>

    Public Class SubArgument

        Private ReadOnly _type As SubArgumentType
        Private ReadOnly _description As String
        Private ReadOnly _name As String

        ReadOnly Property Name() As String
            Get
                Return _name
            End Get
        End Property

        ReadOnly Property Description() As String
            Get
                Return _description
            End Get
        End Property

        ReadOnly Property Type() As SubArgumentType
            Get
                Return _type
            End Get
        End Property

        Private Sub New(ByVal sName As String, eType As SubArgumentType)
            MyClass.New(sName, "", eType)
        End Sub

        Private Sub New(ByVal sName As String, ByVal sDescription As String, eType As SubArgumentType)
            _name = sName
            _description = sDescription
            _type = eType
        End Sub

        Public Shared Function List(ByVal sName As String) As SubArgument
            Return New SubArgument(sName, SubArgumentType.List)
        End Function

        Public Shared Function [Optional](ByVal sName As String) As SubArgument
            Return New SubArgument(sName, SubArgumentType.Optional)
        End Function

        Public Shared Function Required(ByVal sName As String) As SubArgument
            Return New SubArgument(sName, SubArgumentType.Required)
        End Function

        Public Shared Function List(ByVal sName As String, ByVal sDescription As String) As SubArgument
            Return New SubArgument(sName, sDescription, SubArgumentType.List)
        End Function

        Public Shared Function [Optional](ByVal sName As String, ByVal sDescription As String) As SubArgument
            Return New SubArgument(sName, sDescription, SubArgumentType.Optional)
        End Function

        Public Shared Function Required(ByVal sName As String, ByVal sDescription As String) As SubArgument
            Return New SubArgument(sName, sDescription, SubArgumentType.Required)
        End Function

        Public Function IsList() As Boolean
            Return Type = SubArgumentType.List
        End Function

        Public Function IsOptional() As Boolean
            Return Type = SubArgumentType.Optional
        End Function

        Public Function IsRequired() As Boolean
            Return Type = SubArgumentType.Required
        End Function
    End Class

End Namespace
