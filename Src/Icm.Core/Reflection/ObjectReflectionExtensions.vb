Imports System.Runtime.CompilerServices

Namespace Icm.Reflection

    Public Module ObjectReflectionExtensions

        ''' <summary>
        ''' Gets a field value of an object, given the field name
        ''' </summary>
        ''' <typeparam name="TField">Type of the field</typeparam>
        ''' <param name="obj">Object</param>
        ''' <param name="fieldName">Name of the field</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function GetField(Of TField)(ByVal obj As Object, ByVal fieldName As String) As TField
            If obj Is Nothing Then Throw New ArgumentNullException("obj")
            Dim fi = obj.GetType.GetField(fieldName)
            If fi Is Nothing Then Throw New ArgumentException(String.Format(My.Resources.Reflection.FieldX0DoesNotExistInTypeX1, fieldName, obj.GetType.Name), "fieldName")
            Return DirectCast(fi.GetValue(obj), TField)
        End Function

        ''' <summary>
        ''' Sets a field of an object, given the field name
        ''' </summary>
        ''' <typeparam name="T">Type of the object</typeparam>
        ''' <param name="obj">Object</param>
        ''' <param name="fieldName">Name of the field</param>
        ''' <param name="value">New value</param>
        ''' <remarks></remarks>
        <Extension()>
        Public Sub SetField(Of T)(ByVal obj As T, ByVal fieldName As String, ByVal value As Object)
            If obj Is Nothing Then Throw New ArgumentNullException("obj")
            Dim fi = obj.GetType.GetField(fieldName)
            If fi Is Nothing Then Throw New ArgumentException(String.Format(My.Resources.Reflection.FieldX0DoesNotExistInTypeX1, fieldName, GetType(T).Name), "fieldName")
            fi.SetValue(obj, value)
        End Sub

        ''' <summary>
        ''' Has the object a field with the given name?
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <param name="fieldName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function HasField(ByVal obj As Object, ByVal fieldName As String) As Boolean
            If obj Is Nothing Then Return False
            Dim fi = obj.GetType.GetField(fieldName)
            Return fi IsNot Nothing
        End Function

        ''' <summary>
        ''' Has the type a field with the given name?
        ''' </summary>
        ''' <param name="fieldName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function HasField(Of T)(ByVal fieldName As String) As Boolean
            Dim fi = GetType(T).GetField(fieldName)
            Return fi IsNot Nothing
        End Function

        ''' <summary>
        ''' Gets a property value of an object, given the property name
        ''' </summary>
        ''' <typeparam name="TProp">Type of the property</typeparam>
        ''' <param name="obj">Object</param>
        ''' <param name="propName">Name of the property</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function GetProp(Of TProp)(ByVal obj As Object, ByVal propName As String) As TProp
            If obj Is Nothing Then Throw New ArgumentNullException("obj")
            Dim pi = obj.GetType.GetProperty(propName)
            If pi Is Nothing Then Throw New ArgumentException(String.Format(My.Resources.Reflection.PropertyX0DoesNotExistInTypeX1, propName, obj.GetType.Name), "propName")
            Return DirectCast(pi.GetValue(obj, Nothing), TProp)
        End Function

        ''' <summary>
        ''' Gets a property value of an object, given the property name
        ''' </summary>
        ''' <typeparam name="TProp">Type of the property</typeparam>
        ''' <param name="obj">Object</param>
        ''' <param name="propName">Name of the property</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function GetProp(Of TProp)(ByVal obj As Object, ByVal propName As String, ByVal index As Object) As TProp
            If obj Is Nothing Then Throw New ArgumentNullException("obj")
            Dim pi = obj.GetType.GetProperty(propName)
            If pi Is Nothing Then Throw New ArgumentException(String.Format(My.Resources.Reflection.PropertyX0DoesNotExistInTypeX1, propName, obj.GetType.Name), "propName")
            Return DirectCast(pi.GetValue(obj, {index}), TProp)
        End Function
        ''' <summary>
        ''' Gets a property value of an object, given the property name
        ''' </summary>
        ''' <param name="obj">Object</param>
        ''' <param name="propName">Name of the property</param>
        ''' <returns>Value of property with name propName for the object obj</returns>
        ''' <remarks>
        ''' This overload return an untyped result. Use it only when you do not know or do not need it.
        ''' </remarks>
        <Extension()>
        Public Function GetProp(ByVal obj As Object, ByVal propName As String) As Object
            If obj Is Nothing Then Throw New ArgumentNullException("obj")
            Dim pi = obj.GetType.GetProperty(propName)
            If pi Is Nothing Then Throw New ArgumentException(String.Format(My.Resources.Reflection.PropertyX0DoesNotExistInTypeX1, propName, obj.GetType.Name), "propName")
            Return pi.GetValue(obj, Nothing)
        End Function
        ''' <summary>
        ''' Gets an indexed property value of an object, given the property name
        ''' </summary>
        ''' <param name="obj">Object</param>
        ''' <param name="propName">Name of the property</param>
        ''' <param name="index">Index</param>
        ''' <returns>Value of property with name propName for the object obj</returns>
        ''' <remarks>
        ''' This overload return an untyped result. Use it only when you do not know or do not need it.
        ''' </remarks>
        <Extension()>
        Public Function GetProp(ByVal obj As Object, ByVal propName As String, ByVal index As Object) As Object
            If obj Is Nothing Then Throw New ArgumentNullException("obj")
            Dim pi = obj.GetType.GetProperty(propName)
            If pi Is Nothing Then Throw New ArgumentException(String.Format(My.Resources.Reflection.PropertyX0DoesNotExistInTypeX1, propName, obj.GetType.Name), "propName")
            Return pi.GetValue(obj, {index})
        End Function
        ''' <summary>
        ''' Sets a property of an object, given the property name
        ''' </summary>
        ''' <typeparam name="T">Type of the object</typeparam>
        ''' <param name="obj">Object</param>
        ''' <param name="propName">Name of the property</param>
        ''' <param name="value">New value</param>
        ''' <remarks></remarks>
        <Extension()>
        Public Sub SetProp(Of T)(ByVal obj As T, ByVal propName As String, ByVal value As Object)
            obj.SetPropAux(propName, Nothing, value)
        End Sub

        ''' <summary>
        ''' Sets an indexed property of an object, given the property name
        ''' </summary>
        ''' <typeparam name="T">Type of the object</typeparam>
        ''' <param name="obj">Object</param>
        ''' <param name="propName">Name of the property</param>
        ''' <param name="index">Index</param>
        ''' <param name="value">New value</param>
        ''' <remarks></remarks>
        <Extension()>
        Public Sub SetProp(Of T)(ByVal obj As T, ByVal propName As String, ByVal index As Object, ByVal value As Object)
            obj.SetPropAux(propName, {index}, value)
        End Sub

        <Extension()>
        Private Sub SetPropAux(Of T)(ByVal obj As T, ByVal propName As String, ByVal indexes() As Object, ByVal value As Object)
            If obj Is Nothing Then Throw New ArgumentNullException("obj")
            Dim pi = obj.GetType.GetProperty(propName)
            If pi Is Nothing Then Throw New ArgumentException(String.Format(My.Resources.Reflection.PropertyX0DoesNotExistInTypeX1, propName, GetType(T).Name), "propName")
            Dim targetType = If(pi.PropertyType.IsNullable, Nullable.GetUnderlyingType(pi.PropertyType), pi.PropertyType)
            Dim convertedValue As Object
            If targetType.IsEnum Then
                convertedValue = [Enum].ToObject(targetType, value)
            Else
                convertedValue = If(value Is Nothing, Nothing, Convert.ChangeType(value, targetType))
            End If

            pi.SetValue(obj, convertedValue, indexes)
        End Sub

        ''' <summary>
        ''' Sets a double-indexed property of an object, given the property name
        ''' </summary>
        ''' <typeparam name="T">Type of the object</typeparam>
        ''' <param name="obj">Object</param>
        ''' <param name="propName">Name of the property</param>
        ''' <param name="index1">First index</param>
        ''' <param name="index2">Second index</param>
        ''' <param name="value">New value</param>
        ''' <remarks></remarks>
        <Extension()>
        Public Sub SetProp(Of T)(ByVal obj As T, ByVal propName As String, ByVal index1 As Object, ByVal index2 As Object, ByVal value As Object)
            obj.SetPropAux(propName, {index1, index2}, value)
        End Sub


        ''' <summary>
        ''' Has the type a property with the given name?
        ''' </summary>
        ''' <param name="propName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function HasProp(ByVal obj As Object, ByVal propName As String) As Boolean
            If IsNothing(obj) Then
                Return False
            Else
                Dim pi = obj.GetType.GetProperty(propName)
                Return pi IsNot Nothing
            End If
        End Function

        ''' <summary>
        ''' Is the type's property with the given name readable?
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="propName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function IsPropReadable(Of T)(ByVal obj As T, ByVal propName As String) As Boolean
            Return IsPropReadable(Of T)(propName)
        End Function

        ''' <summary>
        ''' Is the type's property with the given name readable?
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="propName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function IsPropWritable(Of T)(ByVal obj As T, ByVal propName As String) As Boolean
            Return IsPropWritable(Of T)(propName)
        End Function

        ''' <summary>
        ''' Has the type a property with the given name?
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="propName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function TypeHasProp(Of T)(ByVal propName As String) As Boolean
            Dim pi = GetType(T).GetProperty(propName)
            Return pi IsNot Nothing
        End Function

        ''' <summary>
        ''' Is the type's property with the given name readable?
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="propName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsPropReadable(Of T)(ByVal propName As String) As Boolean
            Dim pi = GetType(T).GetProperty(propName)
            If pi Is Nothing Then Throw New ArgumentException(String.Format(My.Resources.Reflection.PropertyX0DoesNotExistInTypeX1, propName, GetType(T).Name), "propName")
            Return pi.CanRead
        End Function

        ''' <summary>
        ''' Is the type's property with the given name readable?
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="propName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsPropWritable(Of T)(ByVal propName As String) As Boolean
            Dim pi = GetType(T).GetProperty(propName)
            If pi Is Nothing Then Throw New ArgumentException(String.Format(My.Resources.Reflection.PropertyX0DoesNotExistInTypeX1, propName, GetType(T).Name), "propName")
            Return pi.CanWrite
        End Function

        ''' <summary>
        ''' Call the given Sub (C#: void function) with the given parameters.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="obj"></param>
        ''' <param name="subName"></param>
        ''' <param name="params"></param>
        ''' <remarks></remarks>
        <Extension()>
        Public Sub CallSub(Of T)(ByVal obj As T, ByVal subName As String, ParamArray params As Object())
            ' TODO: Allow overloads. The same algorithm for method call matching
            ' TODO: Allow shared subs
            If obj Is Nothing Then Throw New ArgumentNullException("No se puede obtener un método de un objeto nulo")
            Dim mi = obj.GetType.GetMethod(subName)
            If mi Is Nothing Then Throw New ArgumentException(String.Format("El método {0} no existe en el tipo {1}", subName, obj.GetType.Name))
            mi.Invoke(obj, params)
        End Sub

        ''' <summary>
        ''' Has obj the given method's name?
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="obj"></param>
        ''' <param name="subName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function HasMethod(Of T)(ByVal obj As T, ByVal subName As String) As Boolean
            ' TODO: Allow overloads. The same algorithm for method call matching
            If obj Is Nothing Then Throw New ArgumentNullException("No se puede obtener un método de un objeto nulo")
            Dim mi = obj.GetType.GetMethod(subName)
            Return mi Is Nothing
        End Function

        ''' <summary>
        ''' Call the function with given name on an object
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="obj"></param>
        ''' <param name="funcName"></param>
        ''' <param name="params"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function CallFunc(Of T)(ByVal obj As T, ByVal funcName As String, ParamArray params As Object()) As Object
            ' TODO: Allow overloads. The same algorithm for method call matching
            ' TODO: Allow shared funcs
            If obj Is Nothing Then Throw New ArgumentNullException("Cannot obtain a method from a null object")
            Dim mi = obj.GetType.GetMethod(funcName)
            If mi Is Nothing Then Throw New ArgumentException(String.Format("Method {0} does not exist in type {1}", funcName, obj.GetType.Name))
            Return mi.Invoke(obj, params)
        End Function
    End Module
End Namespace
