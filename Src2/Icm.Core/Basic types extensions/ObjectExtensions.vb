Imports System.Runtime.CompilerServices

Namespace Icm

    Public Module ObjectExtensions

        ''' <summary>
        ''' If a variable is Nothing or DBNull, return subst, else return the same variable
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="o"></param>
        ''' <param name="subst"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Function IfNothing(Of T)(ByVal o As Object, ByVal subst As T) As T
            If o Is Nothing Then
                Return subst
            ElseIf IsDBNull(o) Then
                Return subst
            Else
                Return CType(o, T)
            End If
        End Function

        ''' <summary>
        ''' If obj is not Nothing, execute action over obj
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="obj"></param>
        ''' <param name="act"></param>
        ''' <remarks></remarks>
        <Extension()>
        Sub DoIfAny(Of T)(ByVal obj As T, ByVal act As Action(Of T))
            If obj Is Nothing Then
                ' Do nothing
            Else
                act(obj)
            End If
        End Sub

        ''' <summary>
        ''' If o is not null, act applied to o; otherwise null.
        ''' </summary>
        ''' <typeparam name="TObject"></typeparam>
        ''' <typeparam name="TResult"></typeparam>
        ''' <param name="o"></param>
        ''' <param name="act"></param>
        ''' <returns>If o is not null, act applied to o; otherwise null.</returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function GetIfAny(Of TObject, TResult)(ByVal o As TObject, ByVal act As Func(Of TObject, TResult)) As TResult
            If o Is Nothing Then
                Return Nothing
            Else
                Return act(o)
            End If
        End Function

        ''' <summary>
        ''' Abbreviation of "IsNot Nothing".
        ''' </summary>
        ''' <param name="o"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Function IsSomething(ByVal o As Object) As Boolean
            Return o IsNot Nothing
        End Function

        ''' <summary>
        ''' Direct casting shortcut.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="o"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Casting shortcut. Use:
        ''' <code>
        ''' myObj = otherObj.As(Of DerivedType)
        ''' </code>
        ''' instead of:
        ''' <code>
        ''' myObj = DirectCast(otherObj, DerivedType)
        ''' myObj = CType(otherObj, DerivedType)
        ''' </code>
        ''' 
        ''' A little bit shorter than CType, and with a pleasant object syntax.
        ''' </remarks>
        <Extension()>
        Function [As](Of T)(ByVal o As Object) As T
            Return DirectCast(o, T)
        End Function

        ''' <summary>
        ''' Is the first parameter one of the rest of parameters?
        ''' </summary>
        ''' <param name="s"></param>
        ''' <param name="sa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function IsOneOf(Of T)(ByVal s As T, ByVal ParamArray sa As T()) As Boolean
            If sa Is Nothing Then
                Return False
            End If
            Return sa.Contains(s)
        End Function

    End Module
End Namespace
