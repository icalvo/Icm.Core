Imports System.Runtime.CompilerServices

Namespace Icm.Reflection

    Public Module TypeExtensions

        ''' <summary>
        ''' Get attributes of a type
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="type"></param>
        ''' <param name="inherit"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Function GetAttributes(Of T As Attribute)(ByVal type As Type, ByVal inherit As Boolean) As T()
            Return DirectCast(type.GetCustomAttributes(GetType(T), inherit), T())
        End Function

        ''' <summary>
        ''' Has the given type an attribute?
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="type"></param>
        ''' <param name="inherit"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Function HasAttribute(Of T As Attribute)(ByVal type As Type, ByVal inherit As Boolean) As Boolean
            Return DirectCast(type.GetCustomAttributes(GetType(T), inherit), T()).Count > 0
        End Function

        ''' <summary>
        ''' Gets an attribute that must appear a single time, or nothing if it does not appear.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="type"></param>
        ''' <param name="inherit"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Function GetAttribute(Of T As Attribute)(ByVal type As Type, ByVal inherit As Boolean) As T
            Return DirectCast(type.GetCustomAttributes(GetType(T), inherit), T()).SingleOrDefault
        End Function

        ''' <summary>
        ''' Is type of the form Nullable(Of T)?
        ''' </summary>
        ''' <param name="type"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function IsNullable(type As Type) As Boolean
            Return type.IsGenericType AndAlso type.GetGenericTypeDefinition().Equals(GetType(Nullable(Of )))
        End Function


    End Module

End Namespace