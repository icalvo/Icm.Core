Namespace Icm.CommandLineTools
    ''' <summary>
    '''  Types of subarguments.
    ''' </summary>
    ''' <remarks>
    ''' <list>
    ''' <item>Required: The subargument is required</item>
    ''' <item>Optional: The subargument is optional</item>
    ''' <item>List: An indefinite list of subarguments is admitted (including the empty list).</item>
    ''' </list></remarks>

    Public Enum SubArgumentType
        Required = 1
        [Optional] = 2
        List = 3
    End Enum
End Namespace
