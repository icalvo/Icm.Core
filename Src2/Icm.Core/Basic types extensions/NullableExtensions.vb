Imports System.Runtime.CompilerServices

Namespace Icm

    Public Module NullableExtensions

        ''' <summary>
        ''' Abbreviation of Nullable.Value
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="n"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function V(Of T As Structure)(ByVal n As T?) As T
            If n.HasValue Then
                Return n.Value
            Else
                Throw New InvalidOperationException
            End If
            Return n.Value
        End Function

        ''' <summary>
        ''' Abbreviation of "Not var.HasValue"
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="n"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function HasNotValue(Of T As Structure)(ByVal n As T?) As Boolean
            Return Not n.HasValue
        End Function


        ''' <summary>
        ''' If a Nullable has value, the value; otherwise, a provided substitution.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="o"></param>
        ''' <param name="subst"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Function IfNull(Of T As Structure)(ByVal o As T?, ByVal subst As Object) As Object
            If o.HasValue Then
                Return o.Value
            Else
                Return subst
            End If
        End Function

    End Module
End Namespace
