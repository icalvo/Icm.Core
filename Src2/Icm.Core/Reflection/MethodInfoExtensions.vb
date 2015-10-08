Imports System.Runtime.CompilerServices
Imports System.Reflection

Namespace Icm.Reflection

    Public Module MethodInfoExtensions

        ''' <summary>
        ''' Get the attributes of a given method.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="mi"></param>
        ''' <param name="inherit"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Function GetAttributes(Of T As Attribute)(ByVal mi As MethodInfo, ByVal inherit As Boolean) As T()
            Return DirectCast(mi.GetCustomAttributes(GetType(T), inherit), T())
        End Function


        ''' <summary>
        ''' Do the method have the attribute?
        ''' </summary>
        ''' <typeparam name="T">Attribute type to check</typeparam>
        ''' <param name="mi"></param>
        ''' <param name="inherit"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Function HasAttribute(Of T As Attribute)(ByVal mi As MethodInfo, ByVal inherit As Boolean) As Boolean
            Return DirectCast(mi.GetCustomAttributes(GetType(T), inherit), T()).Count > 0
        End Function

        ''' <summary>
        ''' Get the given attribute, that must be unique.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="mi"></param>
        ''' <param name="inherit"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Function GetAttribute(Of T As Attribute)(ByVal mi As MethodInfo, ByVal inherit As Boolean) As T
            Return DirectCast(mi.GetCustomAttributes(GetType(T), inherit), T()).Single
        End Function

    End Module

End Namespace
