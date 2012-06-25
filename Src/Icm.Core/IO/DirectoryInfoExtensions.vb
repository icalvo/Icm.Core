Imports System.IO
Imports System.Runtime.CompilerServices

Namespace Icm.IO
    Public Module DirectoryInfoExtensions

        ''' <summary>
        ''' Builds a FieldInfo representing a file inside a DirectoryInfo object.
        ''' </summary>
        ''' <param name="di"></param>
        ''' <param name="relativeFilename"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function GetFile(ByVal di As DirectoryInfo, ByVal relativeFilename As String) As FileInfo
            Return New FileInfo(Path.Combine(di.FullName, relativeFilename))
        End Function

    End Module
End Namespace
