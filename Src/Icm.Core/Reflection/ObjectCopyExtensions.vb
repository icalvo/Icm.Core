Imports System.Runtime.CompilerServices

Namespace Icm.Reflection

    Public Module ObjectCopyExtensions

        ''' <summary>
        ''' Copy (shallow) the properties of an object to another.
        ''' </summary>
        ''' <param name="objSource"></param>
        ''' <param name="objDest"></param>
        ''' <param name="excludedMembers"></param>
        ''' <param name="excludedTypes"></param>
        ''' <remarks></remarks>
        <Extension()>
        Public Sub CopyTo(objSource As Object, objDest As Object, Optional excludedMembers As IEnumerable(Of String) = Nothing, Optional excludedTypes As IEnumerable(Of String) = Nothing)
            CopyToAux(objSource, objDest, excludedMembers, excludedTypes)
        End Sub

        ''' <summary>
        ''' Copy (shallow) the properties of an object to another.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="objSource"></param>
        ''' <param name="objDest"></param>
        ''' <param name="excludedMembers"></param>
        ''' <param name="excludedTypes"></param>
        ''' <remarks></remarks>
        <Extension()>
        Public Sub CopyTo(Of T)(objSource As T, objDest As T, Optional excludedMembers As IEnumerable(Of String) = Nothing, Optional excludedTypes As IEnumerable(Of String) = Nothing)
            CopyToAux(objSource, objDest, excludedMembers, excludedTypes)
        End Sub

        ''' <summary>
        ''' Clone an object with a shallow copy.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="obj"></param>
        ''' <param name="excludedMembers"></param>
        ''' <param name="excludedTypes"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function Clone(Of T As New)(obj As T, Optional excludedMembers As IEnumerable(Of String) = Nothing, Optional excludedTypes As IEnumerable(Of String) = Nothing) As T
            Dim result As New T

            CopyToAux(obj, result, excludedMembers, excludedTypes)

            Return result
        End Function

        ''' <summary>
        ''' Copy (shallow) an Entity Framework proxy object.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="objSource">Source object</param>
        ''' <param name="objDest">Target object</param>
        ''' <param name="destKeyProperties"></param>
        ''' <remarks></remarks>
        <Extension()>
        Public Sub CopyEntityTo(Of T)(objSource As T, objDest As T, ParamArray destKeyProperties() As String)
            CopyTo(
                objSource,
                objDest,
                excludedMembers:={"EntityKey", "EntityState", "RelationshipManager"}.Union(destKeyProperties),
                excludedTypes:={"ICollection"})
        End Sub

        ''' <summary>
        ''' Copy (shallow) an Entity Framework proxy object.
        ''' </summary>
        ''' <param name="objSource"></param>
        ''' <param name="objDest"></param>
        ''' <param name="destKeyProperties"></param>
        ''' <remarks></remarks>
        <Extension()>
        Public Sub CopyEntityTo(objSource As Object, objDest As Object, ParamArray destKeyProperties() As String)
            CopyTo(
                objSource,
                objDest,
                excludedMembers:={"EntityKey", "EntityState", "RelationshipManager"}.Union(destKeyProperties),
                excludedTypes:={"ICollection"})
        End Sub

        ''' <summary>
        ''' Copy a property of an object to the same named property of another object
        ''' </summary>
        ''' <param name="objSource"></param>
        ''' <param name="objDest"></param>
        ''' <param name="prop"></param>
        ''' <param name="excludedProperties"></param>
        ''' <param name="excludedTypes"></param>
        ''' <remarks></remarks>
        Private Sub CopyProp(ByVal objSource As Object, ByVal objDest As Object, ByVal prop As System.Reflection.PropertyInfo, excludedProperties As IEnumerable(Of String), excludedTypes As IEnumerable(Of String))
            Dim propName As String = prop.Name
            If excludedProperties.Contains(propName) Then
                Debug.Print("---- Property {0} excluded", propName)
            ElseIf excludedTypes.Any(Function(exclType) prop.PropertyType.Name.StartsWith(exclType)) Then
                Debug.Print("---- Property {0} excluded for being of type {1}", propName, prop.PropertyType.Name)
            ElseIf HasProp(objSource, propName) Then
                Debug.Print("-- Copying property {0} with value [{1}] (old: [{2}])",
                            propName,
                           GetProp(objSource, propName),
                           GetProp(objDest, propName))
                SetProp(objDest, propName, GetProp(objSource, propName))
            Else
                Debug.Print("---- Property {0} does not exist at source", propName)
            End If
        End Sub


        ''' <summary>
        ''' Copy a property of an object to the same named property of another object
        ''' </summary>
        ''' <param name="objSource"></param>
        ''' <param name="objDest"></param>
        ''' <param name="field"></param>
        ''' <param name="excludedFields"></param>
        ''' <param name="excludedTypes"></param>
        ''' <remarks></remarks>
        Private Sub CopyField(ByVal objSource As Object, ByVal objDest As Object, ByVal field As System.Reflection.FieldInfo, excludedFields As IEnumerable(Of String), excludedTypes As IEnumerable(Of String))
            Dim fieldName As String = field.Name
            If excludedFields.Contains(fieldName) Then
                Debug.Print("---- Field {0} excluded", fieldName)
            ElseIf excludedTypes.Any(Function(exclType) field.FieldType.Name.StartsWith(exclType)) Then
                Debug.Print("---- Field {0} excluded for being of type {1}", fieldName, field.FieldType.Name)
            ElseIf HasField(objSource, fieldName) Then
                Debug.Print("-- Copying field {0} with value [{1}] (old: [{2}])",
                            fieldName,
                           GetField(objSource, fieldName),
                           GetField(objDest, fieldName))
                SetField(objDest, fieldName, GetField(objSource, fieldName))
            Else
                Debug.Print("---- Field {0} does not exist at source", fieldName)
            End If
        End Sub

        Private Sub CopyToAux(objSource As Object, objDest As Object, excludedMembers As IEnumerable(Of String), excludedTypes As IEnumerable(Of String))
            Dim destFields = objDest.GetType.GetFields
            excludedMembers = If(excludedMembers, {})
            excludedTypes = If(excludedTypes, {})
            For Each destField In destFields
                CopyField(objSource, objDest, destField, excludedMembers, excludedTypes)
            Next
            Dim destProps = objDest.GetType.GetProperties()
            For Each destProp In destProps
                CopyProp(objSource, objDest, destProp, excludedMembers, excludedTypes)
            Next
        End Sub

    End Module

End Namespace
