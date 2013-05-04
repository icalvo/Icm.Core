Imports res = My.Resources.CommandLineTools

Namespace Icm.CommandLineTools

    ''' <summary>
    '''  Command line option.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NamedParameter

        Private _shortName As String
        Private _longName As String
        Private ReadOnly _subarguments As List(Of SubArgument)
        Private ReadOnly _isRequired As Boolean
        Private _unlimitedSubargs As Boolean

        Public Sub New(
                ByVal sShortName As String,
                ByVal sLongName As String,
                ByVal sDescription As String,
                ByVal bRequired As Boolean,
                ByVal ParamArray subargs() As SubArgument)

            Debug.Assert(sShortName.Length <= sLongName.Length)
            ShortName = sShortName
            LongName = sLongName
            Description = sDescription
            _isRequired = bRequired
            _subarguments = New List(Of SubArgument)
            If subargs IsNot Nothing Then
                For Each subarg In subargs
                    AddSubArgument(subarg)
                Next
            End If
            MustReset = False
        End Sub

        ''' <summary>
        ''' If this property is True, it indicates that we must eliminate all the present
        ''' subarguments.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property MustReset As Boolean

        ''' <summary>
        '''  If true, the presence of this option in the command line makes the processor to
        ''' ignore the main arguments because they are unneeded.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property MakesMainArgumentsIrrelevant() As Boolean

        ''' <summary>
        ''' Does this option accept an unlimited list of subarguments?
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property AcceptsUnlimitedSubargs() As Boolean
            Get
                Return _unlimitedSubargs
            End Get
        End Property

        ReadOnly Property IsRequired As Boolean
            Get
                Return _isRequired
            End Get
        End Property

        ''' <summary>
        '''  Short option (for example -h)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property ShortName() As String
            Get
                Return _shortName
            End Get
            Set(ByVal Value As String)
                Debug.Assert(Value.Length >= 1)
                _shortName = Value
            End Set
        End Property

        ''' <summary>
        '''  Long option (for example --help)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property LongName() As String
            Get
                Return _longName
            End Get
            Set(ByVal Value As String)
                Debug.Assert(Value.Length > 1)
                _longName = Value
            End Set
        End Property

        ''' <summary>
        '''  Description of this option.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property Description() As String

        ReadOnly Property SubArguments() As List(Of SubArgument)
            Get
                Return _subarguments
            End Get
        End Property

        ''' <summary>
        '''  Adds a subargument.
        ''' </summary>
        ''' <param name="sa">Subargument to be added</param>
        ''' <remarks></remarks>
        Public Sub AddSubArgument(ByVal sa As SubArgument)
            ' require
            '   p /= Void
            '   parameters.Last.isOptional implies p.isOptional
            '   not parameters.Last.isList
            Debug.Assert(Not sa Is Nothing, res.S_ERR_VOID_SUBARGUMENT)

            If _subarguments.Count > 0 Then
                Dim lastsa As SubArgument = CType(_subarguments(_subarguments.Count - 1), SubArgument)
                Debug.Assert(Not lastsa.IsOptional OrElse sa.IsOptional, String.Format(res.S_ERR_NON_OPTIONAL_AFTER_OPTIONAL, sa.Name, lastsa.Name))
                Debug.Assert(Not lastsa.IsList, res.S_ERR_SUBARG_AFTER_LIST)
            End If
            If sa.IsList Then
                _unlimitedSubargs = True
            End If
            _subarguments.Add(sa)
        End Sub

    End Class
End Namespace
