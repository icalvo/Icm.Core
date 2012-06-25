Imports System.Runtime.CompilerServices

Namespace Icm.Reflection

    Public Module TypeExtensions

        <Extension()>
        Function GetAttributes(Of T As Attribute)(ByVal type As Type, ByVal inherit As Boolean) As T()
            Return DirectCast(type.GetCustomAttributes(GetType(T), inherit), T())
        End Function

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


        <Extension()>
        Public Function IsNullable(type As Type) As Boolean
            Return type.IsGenericType AndAlso type.GetGenericTypeDefinition().Equals(GetType(Nullable(Of )))
        End Function


    End Module

End Namespace