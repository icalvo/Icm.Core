Imports System.Runtime.CompilerServices
Imports System.Reflection

Namespace Icm.Reflection

    Public Module MethodInfoExtensions

        <Extension()>
        Function GetAttributes(Of T As Attribute)(ByVal mi As MethodInfo, ByVal inherit As Boolean) As T()
            Return DirectCast(mi.GetCustomAttributes(GetType(T), inherit), T())
        End Function

        <Extension()>
        Function HasAttribute(Of T As Attribute)(ByVal mi As MethodInfo, ByVal inherit As Boolean) As Boolean
            Return DirectCast(mi.GetCustomAttributes(GetType(T), inherit), T()).Count > 0
        End Function

        <Extension()>
        Function GetAttribute(Of T As Attribute)(ByVal mi As MethodInfo, ByVal inherit As Boolean) As T
            Return DirectCast(mi.GetCustomAttributes(GetType(T), inherit), T()).Single
        End Function

    End Module

End Namespace
