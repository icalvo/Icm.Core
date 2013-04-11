Imports System.Text
Imports res = My.Resources.CommandLineTools
Imports Icm.Collections
Imports Icm.Reflection

Namespace Icm.CommandLineTools

    ''' <summary>
    '''  This class abstracts a command line following a GNU-like syntax.
    ''' </summary>
    ''' <remarks>
    ''' <para>This class performs a full processing
    ''' of command line arguments. It assumes the following general syntax:</para>
    ''' <para>program -namedparam1 subarg1a subarg1b -namedparam2 subarg2a subarg2b [-] unnamedparam1 unnamedparam2</para>
    ''' <para>
    ''' To use it, create an instance, configure named and unnamed parameters, and process a command line
    ''' with <see cref="CommandLine.ProcessArguments"></see>, which must be passed a String array like
    ''' the one provided by <see cref="Environment.GetCommandLineArgs"></see> (In fact, calling <see cref="CommandLine.ProcessArguments"></see>
    ''' without arguments will process the result of <see cref="Environment.GetCommandLineArgs"></see>). After the processing,
    ''' <see cref="CommandLine.ErrorList"></see> will provide the errors found. If no error is found,
    ''' <see cref="CommandLine.IsPresent"></see> will say if a named parameter is present,
    ''' <see cref="CommandLine.GetValue"></see> and <see cref="CommandLine.GetValues"></see> will
    ''' return subarguments of some named parameter, and
    ''' <see cref="CommandLine.MainValue"></see> and <see cref="CommandLine.MainValues"></see> will
    ''' return the unnamed parameters.
    ''' </para>
    ''' <para>NOTE: By default, a <see cref="CommandLine"></see> will feature a --help option.</para>
    ''' <para><see cref="CommandLineExtensions.Instructions"></see> paints a colorized help text, based on the configured parameters.</para>
    ''' <para><see cref="CommandLine"></see> only includes validation of optional/required arguments
    ''' or subarguments, existence of parameters, and proper syntax (including double hyphen for
    ''' long parameter name, single hyphen for single parameter name, and optional single hyphen alone
    ''' for separating the named parameter part from the unnamed arguments.</para>
    ''' <para>For any more advanced or specific validations, and of course for actually doing something with
    ''' the arguments, the client will have to do it herself.</para>
    ''' <para>Also, <see cref="CommandLine.ProcessArguments"></see> do NOT fail, so the client will have to use 
    ''' <see cref="CommandLine.HasErrors"></see> and <see cref="CommandLine.ErrorList"></see> properties and react accordingly.</para>
    ''' <para>For an example, see Icm.CommandLine.Sample package.</para>
    ''' </remarks>
    ''' <history>
    ''' 	[icalvo]	31/03/2005	Created
    ''' </history>
    Public Class CommandLine

#Region " Attributes "

        ' Space-splitted command line arguments
        Private arguments_() As String

        ' List of generated errors
        Private ReadOnly errorList_ As New List(Of String)()

        ' Defined named parameters
        Private ReadOnly namedParameters_ As New List(Of NamedParameter)()

        ' Named parameters hash table, for finding a parameter given a name. For example, "h" and
        ' "help" are keys and they point to the same named parameter.
        Private ReadOnly namedParameterDictionary_ As New Dictionary(Of String, NamedParameter)()

        ' Values for each of the named and unnamed parameters
        Private ReadOnly valuesStore_ As New ValuesStore()

        Private passedParametersCount_ As Integer

        ' Length of the greatest long name. Used to format instructions.
        Private parameterLongNamesMaxLength_ As Integer

        ' Length of the greatest short name. Used to format instructions.
        Private parameterShortNamesMaxLength_ As Integer

        ' Configuration for unnamed (main) parameters
        Private unnamedParametersConfig_ As New UnnamedParametersConfig(0, New UnnamedParameter("args", ""), {})

        ' Some named parameters can change the configuration for unnamed parameters, 
        ' rendering them optional. This variable will hold either the original, user provided configuration
        ' if none of those named parameters is found, or MainArgumentsConfig.None.
        Private realMainArgumentConfig_ As UnnamedParametersConfig
#End Region

        Public Sub New()
            AddHelpOption()
        End Sub

#Region " Query "

#Region " Before process "


        Default ReadOnly Property GetOption(ByVal s As String) As NamedParameter
            Get
                If namedParameterDictionary_.ContainsKey(s) Then
                    Return namedParameterDictionary_(s)
                Else
                    Return Nothing
                End If
            End Get
        End Property

        ReadOnly Property IsDeclaredHelp As Boolean
            Get
                Return IsPresent(res.S_DEFAULT_HELP_LONG)
            End Get
        End Property

        ReadOnly Property DeclaredOptions As Integer
            Get
                Return passedParametersCount_
            End Get
        End Property

#End Region

#Region " After process "

        ReadOnly Property IsPresent(ByVal s As String) As Boolean
            Get
                If namedParameterDictionary_.ContainsKey(s) Then
                    Return valuesStore_.ContainsKey(s)
                Else
                    Throw New UndefinedOptionException(s)
                End If
            End Get
        End Property

        ReadOnly Property ErrorList() As List(Of String)
            Get
                Return errorList_
            End Get
        End Property

        ReadOnly Property MainValues As ICollection(Of String)
            Get
                Return valuesStore_.MainValues
            End Get
        End Property

        ReadOnly Property MainValue As String
            Get
                If valuesStore_.MainValues.Count = 0 Then
                    Return Nothing
                Else
                    Return valuesStore_.MainValues(0)
                End If
            End Get
        End Property

        ReadOnly Property GetValues(ByVal s As String) As ICollection(Of String)
            Get
                If namedParameterDictionary_.ContainsKey(s) Then
                    Return valuesStore_.Values(s)
                Else
                    Throw New UndefinedOptionException(s)
                End If
            End Get
        End Property

        ReadOnly Property GetValue(ByVal s As String) As String
            Get
                ' Suppose we declare (only) named parameter -e [subarg1] and we find these examples:
                ' 1. example.exe
                ' 2. example.exe -e
                ' 3. example.exe -e 123
                ' 3. example.exe -e 123 456
                '
                ' Suppose we DON'T declare named parameter -e and we find these examples:
                ' 4. example.exe -e
                ' 5. example.exe -e 123
                If namedParameterDictionary_.ContainsKey(s) Then
                    If valuesStore_.ContainsKey(s) Then
                        If valuesStore_.Values(s).Count = 0 Then
                            ' Case 2: There is -e but without subarguments
                            Return Nothing
                        Else
                            ' Case 3: There is -e with subarguments, we return the first one (123)
                            Return valuesStore_.Values(s)(0)
                        End If
                    Else
                        ' Case 1: No -e
                        Return Nothing
                    End If
                Else
                    'Cases 4 and 5: -e has not been declared
                    Throw New UndefinedOptionException(s)
                End If
            End Get
        End Property

        Public Function HasErrors() As Boolean
            Return errorList_.Count > 0
        End Function

#End Region

#End Region

#Region " Fluid configuration "

#Region " Main parameters "

        ReadOnly Property MainParameters As UnnamedParametersConfig
            Get
                Return unnamedParametersConfig_
            End Get
        End Property

        ''' <summary>
        ''' Establishes that unnamed parameters are optional. This is the default value.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function MainParametersOptional(unboundArgumentsName As UnnamedParameter) As CommandLine
            unnamedParametersConfig_ = New UnnamedParametersConfig(0, unboundArgumentsName, {})
            Return Me
        End Function

        ''' <summary>
        ''' Establishes that the number of unnamed parameters must be between min and max.
        ''' </summary>
        ''' <param name="min"></param>
        ''' <param name="argList">Names and descriptions of all arguments be</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function MainParametersBetween(min As Integer, argList As IEnumerable(Of UnnamedParameter)) As CommandLine
            unnamedParametersConfig_ = New UnnamedParametersConfig(min, argList.Count, argList)
            Return Me
        End Function

        ''' <summary>
        ''' Establishes that at least min unnamed parameters will be required.
        ''' </summary>
        ''' <param name="unboundArgument"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function MainParametersAtLeast(unboundArgument As UnnamedParameter, ByVal argList As IEnumerable(Of UnnamedParameter)) As CommandLine
            unnamedParametersConfig_ = New UnnamedParametersConfig(argList.Count, unboundArgument, argList)
            Return Me
        End Function

        ''' <summary>
        ''' Establishes that at most max unnamed parameters will be required.
        ''' </summary>
        ''' <param name="argList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function MainParametersAtMost(argList As IEnumerable(Of UnnamedParameter)) As CommandLine
            unnamedParametersConfig_ = New UnnamedParametersConfig(0, argList.Count, argList)
            Return Me
        End Function

        ''' <summary>
        ''' Establishes that exactly num unnamed parameters will be required.
        ''' </summary>
        ''' <param name="argList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function MainParametersExactly(argList As IEnumerable(Of UnnamedParameter)) As CommandLine
            unnamedParametersConfig_ = New UnnamedParametersConfig(argList.Count, argList.Count, argList)
            Return Me
        End Function

        ''' <summary>
        ''' Establishes that at least one main parameter is required.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function MainParametersRequired(requiredArg As UnnamedParameter, unboundArgument As UnnamedParameter) As CommandLine
            unnamedParametersConfig_ = New UnnamedParametersConfig(1, unboundArgument, {requiredArg})
            Return Me
        End Function

        ''' <summary>
        ''' Establishes that at least one main parameter is required.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function MainParametersRequired(arg As UnnamedParameter) As CommandLine
            unnamedParametersConfig_ = New UnnamedParametersConfig(1, arg, {arg})
            Return Me
        End Function

        ''' <summary>
        ''' Establishes that no unnamed parameters will be admitted.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function MainParametersNone() As CommandLine
            unnamedParametersConfig_ = New UnnamedParametersConfig()
            Return Me
        End Function

#End Region

#Region " Named parameters "

        ReadOnly Property NamedParameters As List(Of NamedParameter)
            Get
                Return namedParameters_
            End Get
        End Property

        ''' <summary>
        ''' Adds an optional named parameter.
        ''' </summary>
        ''' <param name="sShortName"></param>
        ''' <param name="sLongName"></param>
        ''' <param name="sDescription"></param>
        ''' <param name="subargs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function [Optional]( _
                  ByVal sShortName As String,
                  ByVal sLongName As String,
                  ByVal sDescription As String,
                  ByVal ParamArray subargs() As SubArgument) As CommandLine

            Dim o As New NamedParameter(sShortName, sLongName, sDescription, False, subargs)

            NamedParameter(o)

            Return Me
        End Function

        ''' <summary>
        ''' Adds a required named parameter.
        ''' </summary>
        ''' <param name="sShortName"></param>
        ''' <param name="sLongName"></param>
        ''' <param name="sDescription"></param>
        ''' <param name="subargs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Required( _
                ByVal sShortName As String,
                ByVal sLongName As String,
                ByVal sDescription As String,
                ByVal ParamArray subargs() As SubArgument) As CommandLine

            Dim o As New NamedParameter(sShortName, sLongName, sDescription, True, subargs)

            NamedParameter(o)

            Return Me
        End Function

        ''' <summary>
        ''' Adds a named parameter.
        ''' </summary>
        ''' <param name="o"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NamedParameter(ByVal o As NamedParameter) As CommandLine
            namedParameterDictionary_.Add(o.ShortName.ToLower, o)
            namedParameterDictionary_.Add(o.LongName.ToLower, o)
            If o.ShortName.Length > parameterShortNamesMaxLength_ Then
                parameterShortNamesMaxLength_ = o.ShortName.Length
            End If
            If o.LongName.Length > parameterLongNamesMaxLength_ Then
                parameterLongNamesMaxLength_ = o.LongName.Length
            End If
            namedParameters_.Add(o)

            Return Me
        End Function

#End Region

#End Region

#Region " Processing "

        ''' <summary>
        ''' Process arguments from the application settings
        ''' </summary>
        ''' <param name="settings"></param>
        ''' <remarks></remarks>
        Public Sub ProcessArguments(ByVal settings As System.Configuration.ApplicationSettingsBase)
            ProcessArguments(settings, Nothing)
        End Sub

        ''' <summary>
        ''' Process arguments from an array of strings
        ''' </summary>
        ''' <param name="args"></param>
        ''' <remarks></remarks>
        Public Sub ProcessArguments(ByVal ParamArray args() As String)
            ProcessArguments(Nothing, args)
        End Sub

        ''' <summary>
        ''' Process arguments from the application settings and an array of strings
        ''' </summary>
        ''' <param name="args"></param>
        ''' <remarks>
        ''' <para>If args is nothing, <see cref="Environment.GetCommandLineArgs"></see> result will used.</para>
        ''' <para>Application settings can be also null and in that case they will be ignored.</para>
        ''' <para>The args array takes precedence over the application settings.</para>
        ''' </remarks>
        Public Sub ProcessArguments(ByVal settings As System.Configuration.ApplicationSettingsBase, ByVal args() As String)
            Dim i As Integer
            Dim optStr As String

            ' First we process application settings. They can only provide named arguments.
            If settings IsNot Nothing Then
                For Each opt In namedParameters_
                    If settings.HasProp(opt.LongName) Then
                        ProcessSettingsOption(opt, StringifySetting(settings.GetProp(opt.LongName)))
                    End If
                Next
            End If

            ' Now we process command line arguments, which will override application settings.


            For Each o In namedParameterDictionary_.Values
                o.MustReset = True
            Next
            If args Is Nothing Then
                arguments_ = Environment.GetCommandLineArgs
            Else
                arguments_ = args
            End If

            realMainArgumentConfig_ = DirectCast(unnamedParametersConfig_.Clone, UnnamedParametersConfig)
            If UBound(arguments_) >= 1 Then
                i = 1
                Do
                    optStr = arguments_(i)
                    If optStr.StartsWith("-", StringComparison.Ordinal) Then

                        ' Possible option
                        Select Case optStr.Length
                            Case 1
                                'Empty long option (-): Not admitted
                                errorList_.Add(res.S_ERR_EMPTY_SHORT_OPTION & " (-)")
                            Case 2
                                If optStr = "--" Then
                                    'Empty long option (--): main arguments start
                                    i += 1

                                    ProcessMain(i)
                                Else
                                    'Short option (-o); strip one dash and process
                                    ProcessOption(optStr.Substring(1), i)
                                End If
                            Case Else
                                If optStr.StartsWith("--", StringComparison.Ordinal) Then
                                    'Long option (--longoption); strip two dashes and process
                                    ProcessOption(optStr.Substring(2), i)
                                Else
                                    'Short option with more than one letter (-option); strip one dash and process
                                    ProcessOption(optStr.Substring(1), i)
                                End If
                        End Select
                    Else
                        ' Main arguments found
                        If realMainArgumentConfig_.Maximum.HasValue AndAlso realMainArgumentConfig_.Maximum.V = 0 Then
                            ' No main arguments configured
                            errorList_.Add(res.S_ERR_ARGS_WHEN_NOARGS)
                        Else
                            ProcessMain(i)
                        End If
                    End If
                Loop Until i >= arguments_.Length OrElse HasErrors()

            End If

            If valuesStore_.MainValues.Count < realMainArgumentConfig_.Minimum OrElse
               valuesStore_.MainValues.Count > realMainArgumentConfig_.Maximum Then
                ' Incorrect number of main arguments
                errorList_.Add(res.S_ERR_NOARGS_WHEN_REQUIRED)
            End If

            ' Required named arguments check
            For Each par In namedParameters_
                If par.IsRequired AndAlso Not valuesStore_.ContainsKey(par.ShortName) Then
                    errorList_.Add(String.Format(res.S_ERR_ARG_REQUIRED, par.LongName, par.ShortName))
                End If
            Next
        End Sub

#End Region

#Region " Auxiliary "

        Protected Sub AddHelpOption()
            Dim helpopt As New NamedParameter(res.S_DEFAULT_HELP_SHORT, res.S_DEFAULT_HELP_LONG, res.S_DEFAULT_HELP_DESC, False) With {.MakesMainArgumentsIrrelevant = True}
            NamedParameter(helpopt)
        End Sub

        Public Sub Clean()
            errorList_.Clear()
            passedParametersCount_ = 0
            parameterLongNamesMaxLength_ = 0
            parameterShortNamesMaxLength_ = 0
        End Sub

        ''' <summary>
        ''' Just adds the rest of the tokens to the collection of main arguments
        ''' </summary>
        ''' <param name="i"></param>
        ''' <remarks></remarks>
        Private Sub ProcessMain(ByRef i As Integer)
            Do Until i >= arguments_.Length
                valuesStore_.AddMainValue(arguments_(i))
                i += 1
            Loop
        End Sub

        ''' <summary>
        ''' A setting can return in general an array of strings that corresponds to the
        ''' subarguments of a named parameter. This functions returns that array from the
        ''' object value.
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function StringifySetting(obj As Object) As IEnumerable(Of String)
            Dim result As New List(Of String)
            If TypeOf obj Is Specialized.StringCollection Then
                For Each cad In DirectCast(obj, Specialized.StringCollection)
                    result.Add(cad)
                Next
            ElseIf TypeOf obj Is Boolean Then
                If CBool(obj) Then
                    Dim empty As String() = {}
                    Return empty
                Else
                    Return Nothing
                End If
            Else
                result.Add(obj.ToString)
            End If
            Return result
        End Function

        Private Sub ProcessSettingsOption(o As NamedParameter, ByVal settingValue As IEnumerable(Of String))
            Dim opSubargValues As New List(Of String)

            If settingValue Is Nothing Then
                Return
            End If

            If valuesStore_.ContainsKey(o.ShortName) Then
                ' We admit more than one instance of a named parameter.
            Else
                valuesStore_.AddParameter(o.ShortName, o.LongName)
            End If

            passedParametersCount_ += 1
            ' If this option makes the main arguments irrelevant, we must ignore
            ' them. To achieve this, we just change the configuration we will use later to check them.
            If o.MakesMainArgumentsIrrelevant Then
                realMainArgumentConfig_ = New UnnamedParametersConfig(0, New UnnamedParameter("args", ""), {})
            End If

            ' Fill the subarguments to be processed.
            Dim i = 0
            Do Until i >= settingValue.Count OrElse settingValue(i).Chars(0) = "-" OrElse _
                (opSubargValues.Count = o.SubArguments.Count AndAlso Not o.AcceptsUnlimitedSubargs)
                opSubargValues.Add(settingValue(i))
                i += 1
            Loop

            ProcessSubargs(o, opSubargValues)
        End Sub


        ''' <summary>
        '''   Process a single option.
        ''' </summary>
        ''' <param name="op">Option to be analyzed, without hyphens ('-').</param>
        ''' <param name="i">Current position of the </param>
        ''' <remarks>
        ''' <c>i</c> is pointing to the CL arg next to the
        ''' current option, and will be updated to point to the next option (string 
        ''' starting with hyphen).
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	22/12/2005	Created
        ''' </history>
        Private Sub ProcessOption(op As String, ByRef i As Integer)
            Dim o As NamedParameter
            Dim opSubargValues As New List(Of String)
            i += 1
            If Not namedParameterDictionary_.ContainsKey(op.ToLower) Then
                'inexistent option
                errorList_.Add(String.Format("{0}: {1}", res.S_ERR_OPTION_NOT_EXIST, op))
                Exit Sub
            End If
            o = namedParameterDictionary_(op)

            ' We must eat subargument values for this option until:
            ' + We find the end of the arguments_.
            ' + We find a 'value' that starts with '-';
            ' + The option does not accept unlimited values and we have
            '   reached the count of defined subarguments for this option; or
            ' One more token:
            Do Until _
                i > UBound(arguments_) OrElse _
                arguments_(i).Chars(0) = "-" OrElse _
                (opSubargValues.Count = o.SubArguments.Count AndAlso Not o.AcceptsUnlimitedSubargs)

                ' Fill the subarguments to be processed.
                opSubargValues.Add(arguments_(i))
                i += 1
            Loop
            ProcessSettingsOption(o, opSubargValues)
        End Sub

        Private Sub ProcessSubargs(ByVal par As NamedParameter, ByVal subargValues As List(Of String))
            Dim i As Integer = 0
            Dim subarg As SubArgument
            Dim value As String

            ' Match required subarguments.

            ' Obtain subarg and value
            If par.SubArguments.Count > i Then
                subarg = par.SubArguments(i)
            Else
                subarg = Nothing
            End If
            If subargValues.Count > i Then
                value = subargValues(i)
            Else
                value = Nothing
            End If

            ' Loop until a null subarg, null value or not-required subarg is found.
            Do Until _
                subarg Is Nothing OrElse _
                value Is Nothing OrElse _
                Not subarg.IsRequired

                ' Add the value
                valuesStore_.AddValue(par.ShortName, par.LongName, value)

                ' obtain next subarg and value
                i += 1
                If par.SubArguments.Count > i Then
                    subarg = par.SubArguments(i)
                Else
                    subarg = Nothing
                End If
                If subargValues.Count > i Then
                    value = subargValues(i)
                Else
                    value = Nothing
                End If
            Loop

            ' Check current situation: Is there a mismatch between required
            ' subargs and values?
            If subarg Is Nothing Then
                If value IsNot Nothing Then
                    ErrorList.AppendFormat(res.S_ERR_TOO_MANY_SUBARGS, par.LongName)
                End If
                Exit Sub
            ElseIf subarg.IsRequired Then
                ErrorList.AppendFormat(res.S_ERR_NOT_ENOUGH_SUBARGS, par.LongName)
                Exit Sub
            Else
                If value Is Nothing Then
                    ' There are no more values
                    Exit Sub
                End If
            End If

            ' Match optional / list subarguments
            If subarg.IsOptional Then
                Do Until _
                    subarg Is Nothing OrElse _
                    value Is Nothing

                    valuesStore_.AddValue(par.ShortName, par.LongName, value)
                    i += 1
                    subarg = If(par.SubArguments.Count > i, par.SubArguments(i), Nothing)
                    value = If(subargValues.Count > i, subargValues(i), Nothing)
                Loop

                If subarg Is Nothing AndAlso Not value Is Nothing Then
                    ErrorList.AppendFormat(res.S_ERR_TOO_MANY_SUBARGS, par.LongName)
                End If
            Else
                valuesStore_.AddValues(par.ShortName, par.LongName, subargValues.Subrange(i))
            End If
        End Sub

#End Region

    End Class

End Namespace

