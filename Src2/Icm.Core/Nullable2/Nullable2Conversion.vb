Imports System.Runtime.CompilerServices
Namespace Icm

    ''' <summary>
    ''' Conversions of Nullable2 from/to Nullable
    ''' </summary>
    ''' <remarks>
    ''' Unfortunately, the Nullable restriction to struct types forbids a direct solution via
    ''' conversion operators (conversion operators don't accept generic parameters). 
    ''' </remarks>
    Public Module Nullable2Conversion

        <Extension()>
        Public Function ToNullable2(Of T As Structure)(other As T?) As Nullable2(Of T)
            ' Optimizator: don't do the following because it returns 0 for a null 'other':
            ' If(other.HasValue, other.Value, Nothing)
            If other.HasValue Then
                Return other.Value
            Else
                Return Nothing
            End If
        End Function

        <Extension()>
        Public Function ToNullable(Of T As Structure)(other As Nullable2(Of T)) As T?
            ' Optimizator: don't do the following because it returns 0 for a null 'other':
            ' If(other.HasValue, other.Value, Nothing)
            If other.HasValue Then
                Return other.Value
            Else
                Return Nothing
            End If
        End Function

    End Module

End Namespace
