
Namespace Icm.CommandLineTools
    ''' <summary>
    '''  Unnamed parameter.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UnnamedParameter

        Private ReadOnly _description As String
        Private ReadOnly _name As String

        Public Sub New( _
                ByVal name As String,
                ByVal description As String)
            _name = name
            _description = description
        End Sub

        ''' <summary>
        '''  Long option (for example --help)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property Name() As String
            Get
                Return _name
            End Get
        End Property

        ''' <summary>
        ''' Description of this option.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property Description() As String
            Get
                Return _description
            End Get
        End Property

    End Class

End Namespace
