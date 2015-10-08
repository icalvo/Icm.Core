Imports res = My.Resources.CommandLineTools

Namespace Icm.CommandLineTools
    Public Class UnnamedParametersConfig
        Implements ICloneable

#Region "Attributes"

        Private ReadOnly _paramDictionary As New Dictionary(Of String, UnnamedParameter)
        Private ReadOnly _argumentNameList As New List(Of UnnamedParameter)()
        Private ReadOnly _unboundArgumentsName As UnnamedParameter
        Private ReadOnly _minimum As Integer
        Private ReadOnly _maximum As Integer?

#End Region

        ''' <summary>
        ''' Constructor for cloning
        ''' </summary>
        ''' <param name="minimum"></param>
        ''' <param name="maximum"></param>
        ''' <param name="unboundArgument"></param>
        ''' <param name="argNameList"></param>
        ''' <remarks></remarks>
        Private Sub New(ByVal minimum As Integer, maximum As Integer?, unboundArgument As UnnamedParameter, Optional ByVal argNameList As IEnumerable(Of UnnamedParameter) = Nothing)
            _minimum = minimum
            _maximum = maximum
            If unboundArgument IsNot Nothing Then
                _paramDictionary.Add(unboundArgument.Name, unboundArgument)
            End If
            _unboundArgumentsName = unboundArgument
            StoreArgList(argNameList)
        End Sub

        Private Sub StoreArgList(ByVal argNameList As IEnumerable(Of UnnamedParameter))
            If argNameList IsNot Nothing Then
                For Each arg In argNameList
                    If _paramDictionary.ContainsKey(arg.Name) Then
                        If _paramDictionary(arg.Name) IsNot arg Then
                            Throw New ArgumentException(String.Format("You have passed two Unnamed Parameters with the same name ({0}). If they are of the same kind, use the same UnnamedParameter object for both. Otherwise, use different names", arg.Name))
                        End If
                    Else
                        _paramDictionary.Add(arg.Name, arg)
                    End If
                Next
                _argumentNameList.AddRange(argNameList)
            End If

        End Sub

        Public Sub New()
            _minimum = 0
            _maximum = 0
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the UnnamedParametersConfig class.
        ''' </summary>
        ''' <param name="minimum">Minimum number of arguments</param>
        ''' <param name="unboundArgumentsName">Name for the optional arguments from minimum+1 beyond</param>
        ''' <param name="argNameList">Names for the required arguments between 0 and minimum</param>
        Public Sub New(ByVal minimum As Integer, unboundArgumentsName As UnnamedParameter, ByVal argNameList As IEnumerable(Of UnnamedParameter))
            If argNameList.Count <> minimum Then
                Throw New ArgumentException(res.S_ERR_REQUIRED_MAIN_ARGUMENTS_WITHOUT_NAME)
            End If
            _minimum = minimum
            _maximum = Nothing
            _unboundArgumentsName = unboundArgumentsName
            StoreArgList(argNameList)
        End Sub

        Public Sub New(ByVal minimum As Integer, ByVal maximum As Integer, ByVal argNameList As IEnumerable(Of UnnamedParameter))
            If argNameList.Count <> maximum Then
                Throw New ArgumentException(res.S_ERR_REQUIRED_MAIN_ARGUMENTS_WITHOUT_NAME)
            End If
            _minimum = minimum
            _maximum = maximum
            StoreArgList(argNameList)
        End Sub

        Public ReadOnly Property Minimum As Integer
            Get
                Return _minimum
            End Get
        End Property

        Public ReadOnly Property Maximum As Integer?
            Get
                Return _maximum
            End Get
        End Property

        Public ReadOnly Property UnboundArgumentsName As UnnamedParameter
            Get
                Return _unboundArgumentsName
            End Get
        End Property

        ReadOnly Property ParamDictionary As Dictionary(Of String, UnnamedParameter)
            Get
                Return _paramDictionary
            End Get
        End Property

        ReadOnly Property GetArgumentName(ByVal i As Integer) As UnnamedParameter
            Get
                Return _argumentNameList(i - 1)
            End Get
        End Property

        Public Function Clone() As Object Implements ICloneable.Clone
            Return New UnnamedParametersConfig(_minimum, _maximum, _unboundArgumentsName, _argumentNameList)
        End Function
    End Class

End Namespace
