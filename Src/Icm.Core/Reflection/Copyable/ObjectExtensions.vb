Imports System.Collections.Generic
Imports System.Reflection
Imports System.Runtime.CompilerServices

Namespace Icm.Reflection

    ''' <summary>
    ''' This class defines all the extension methods provided by the Copyable framework 
    ''' on the <see cref="System.Object"/> type.
    ''' </summary>
    Public Module ObjectExtensions

        ''' <summary>
        ''' Creates an object copier for the object.
        ''' </summary>
        ''' <param name="instance">The object to be copied.</param>
        ''' <returns>A deep copy of the object.</returns>
        <Extension()>
        Public Function GetCopier(Of T)(ByVal instance As T) As ObjectCopier(Of T)
            Return New ObjectCopier(Of T)(instance)
        End Function

        ''' <summary>
        ''' Creates a copy of the object.
        ''' </summary>
        ''' <param name="instance">The object to be copied.</param>
        ''' <returns>A deep copy of the object.</returns>
        <Extension()>
        Public Function GetCopy(Of T)(ByVal instance As T) As T
            Dim copier = New ObjectCopier(Of T)(instance)
            Return copier.Clone
        End Function

        <Extension()>
        Public Sub CopyTo(Of T)(ByVal instance As T, target As T)
            Dim copier = New ObjectCopier(Of T)(instance)
            copier.CopyTo(target)
        End Sub

    End Module
End Namespace
