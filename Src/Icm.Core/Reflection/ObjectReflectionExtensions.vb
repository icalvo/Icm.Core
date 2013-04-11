Imports System.Runtime.CompilerServices
Imports System.Reflection

Namespace Icm.Reflection

    Public Module ObjectReflectionExtensions

        ''' <summary>
        ''' Gets a field value of an object, given a call chain on another object that ends up in that field.
        ''' </summary>
        ''' <typeparam name="TField">Type of the field</typeparam>
        ''' <param name="obj">Object</param>
        ''' <param name="callChain">Name of the field</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function GetField(Of TField)(ByVal obj As Object, ByVal callChain As String) As TField
            Return DirectCast(GetMemberAux(obj, callChain, MemberTypes.Field), TField)
        End Function

        ''' <summary>
        ''' Gets a field value of an object, given a call chain on another object that ends up in that field.
        ''' </summary>
        ''' <param name="obj">Object</param>
        ''' <param name="callChain">Name of the property</param>
        ''' <returns>Value of the final element of the call chain</returns>
        ''' <remarks>
        ''' <example>
        ''' <code>
        ''' Dim str As String = "hello"
        ''' Dim fmtlen As String
        ''' fmtlen = str.GetField("")
        ''' </code>
        ''' </example>
        ''' This overload return an untyped result. Use it only when you do not know or do not need it.
        ''' </remarks>
        <Extension()>
        Public Function GetField(ByVal obj As Object, ByVal callChain As String) As Object
            Return GetMemberAux(obj, callChain, MemberTypes.Field)
        End Function

        ''' <summary>
        ''' Sets a field of an object, given a call chain on another object that ends up in that field.
        ''' </summary>
        ''' <typeparam name="TField">Type of the object</typeparam>
        ''' <param name="obj">Object</param>
        ''' <param name="callChain">Name of the field</param>
        ''' <param name="value">New value</param>
        ''' <remarks></remarks>
        <Extension()>
        Public Sub SetField(Of TField)(ByVal obj As TField, ByVal callChain As String, ByVal value As Object)
            SetMemberAux(obj, callChain, value, MemberTypes.Field)
        End Sub

        ''' <summary>
        ''' Has the object a field with the given name?
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <param name="TMember"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function HasField(ByVal obj As Object, ByVal TMember As String) As Boolean
            If obj Is Nothing Then Return False
            Dim fi = obj.GetType.GetField(TMember)
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
            Return DirectCast(GetMemberAux(obj, propName, MemberTypes.Property, Nothing), TProp)
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
            Return DirectCast(GetMemberAux(obj, propName, MemberTypes.Property, index), TProp)
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
            Return GetMemberAux(obj, propName, MemberTypes.Property)
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
            Return GetMemberAux(obj, propName, MemberTypes.Property, index)
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
            SetMemberAux(obj, propName, value, MemberTypes.Property)
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
            SetMemberAux(obj, propName, value, MemberTypes.Property, index)
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
            SetMemberAux(obj, propName, value, MemberTypes.Property, {index1, index2})
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


        ''' <summary>
        ''' Gets a property value of an object, given the property name
        ''' </summary>
        ''' <typeparam name="TProp">Type of the property</typeparam>
        ''' <param name="obj">Object</param>
        ''' <param name="propName">Name of the property</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function GetMember(Of TProp)(ByVal obj As Object, ByVal propName As String) As TProp
            Return DirectCast(GetMemberAux(obj, propName, Nothing), TProp)
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
        Public Function GetMember(Of TProp)(ByVal obj As Object, ByVal propName As String, ByVal ParamArray args() As Object) As TProp
            Return DirectCast(GetMemberAux(obj, propName, MemberTypes.All, args), TProp)
        End Function

        ''' <summary>
        ''' Gets an indexed property value of an object, given the property name
        ''' </summary>
        ''' <param name="obj">Object</param>
        ''' <param name="propName">Name of the property</param>
        ''' <param name="args">Index</param>
        ''' <returns>Value of property with name propName for the object obj</returns>
        ''' <remarks>
        ''' This overload return an untyped result. Use it only when you do not know or do not need it.
        ''' </remarks>
        <Extension()>
        Public Function GetMember(ByVal obj As Object, ByVal propName As String, ByVal ParamArray args() As Object) As Object
            Return GetMemberAux(obj, propName, MemberTypes.All, args)
        End Function

        ''' <summary>
        ''' Sets a member of an object, given a call chain on another object that ends up in that member.
        ''' </summary>
        ''' <typeparam name="TMember">Type of the object</typeparam>
        ''' <param name="obj">Object</param>
        ''' <param name="callChain">Name of the field</param>
        ''' <param name="value">New value</param>
        ''' <remarks></remarks>
        Public Sub SetMember(Of TMember)(ByVal obj As TMember, ByVal callChain As String, ByVal value As Object, ParamArray args() As Object)
            SetMemberAux(obj, callChain, value, MemberTypes.All, args)
        End Sub

        Private Sub SetMemberAux(ByVal obj As Object, ByVal propName As String, value As Object, reqMemberType As MemberTypes, ByVal ParamArray args() As Object)
            If obj Is Nothing Then Throw New ArgumentNullException("obj")

            Dim callChain = propName.Split("."c)

            Dim result As Object = obj
            For Each callItem In callChain.Take(callChain.Length - 1)
                result = GetSingleMember(result, callItem, MemberTypes.All)
            Next
            ' For the last call item, use the args
            SetSingleMember(result, callChain.Last, value, args)
        End Sub

        Private Function GetMemberAux(ByVal obj As Object, ByVal propName As String, reqMemberType As MemberTypes, ByVal ParamArray args() As Object) As Object
            If obj Is Nothing Then Throw New ArgumentNullException("obj")

            Dim callChain = propName.Split("."c)

            Dim result As Object = obj
            For Each callItem In callChain.Take(callChain.Length - 1)
                result = GetSingleMemberWithArrayAccess(result, callItem, MemberTypes.All)
            Next
            ' For the last call item, use the args
            Return GetSingleMemberWithArrayAccess(result, callChain.Last, reqMemberType, args)
        End Function

        Private Function GetSingleMemberWithArrayAccess(obj As Object, callItem As String, reqMemberType As MemberTypes, ParamArray args() As Object) As Object
            If callItem.EndsWith("]") Then
                Dim callItemClean As String
                Dim index As String
                Dim idxOpen = callItem.IndexOf("[")
                callItemClean = callItem.Substring(0, idxOpen)

                If reqMemberType.HasFlag(MemberTypes.Property) Then
                    Dim piCallItem = obj.GetType.GetProperty(callItemClean)

                    index = callItem.Substring(idxOpen + 1, callItem.Length - idxOpen - 2)

                    If piCallItem IsNot Nothing Then
                        Dim indices = index.Split(","c)
                        Dim propParams = piCallItem.GetIndexParameters
                        If propParams.Count = indices.Count Then
                            Return GetSingleMember(
                                obj,
                                callItemClean,
                                MemberTypes.Property,
                                propParams.Zip(indices, Function(parinfo, idx) Convert.ChangeType(idx.Trim, parinfo.ParameterType)).ToArray
                            )
                        End If
                    End If

                End If

                Dim result = GetSingleMember(obj, callItemClean, reqMemberType, args)

                If result Is Nothing Then
                    Throw New NullReferenceException("[] accessor on null reference")
                End If

                Dim type = result.GetType
                ' Case 1: Array
                If type.IsArray Then
                    Return GetSingleMember(result, "GetValue", MemberTypes.Method, CInt(index))
                End If
                ' Case 2: Default property
                Dim defPropAttr = DirectCast(type.GetCustomAttribute(GetType(DefaultMemberAttribute)), DefaultMemberAttribute)
                If defPropAttr IsNot Nothing Then
                    Dim propName = defPropAttr.MemberName
                    Dim defProp = type.GetProperty(propName)
                    Dim propParams = defProp.GetIndexParameters()
                    Dim indices = index.Split(","c)
                    Return GetSingleMember(
                        result,
                        propName,
                        MemberTypes.Property,
                        propParams.Zip(indices, Function(parinfo, idx) Convert.ChangeType(idx.Trim, parinfo.ParameterType)).ToArray
                    )
                End If
                ' Case 3: IEnumerable
                Dim list = TryCast(result, IEnumerable)
                If list IsNot Nothing Then
                    Return list(CInt(index))
                End If
                Throw New ArgumentException(String.Format("Cannot apply [] operator to {0}", callItemClean))
            Else
                Return GetSingleMember(obj, callItem, reqMemberType, args)
            End If
        End Function

        Private Function GetSingleMember(obj As Object, callItem As String, reqMemberType As MemberTypes, ParamArray args() As Object) As Object
            Try
                If reqMemberType.HasFlag(MemberTypes.Property) Then
                    Dim pi = obj.GetType.GetProperty(callItem)
                    If pi IsNot Nothing Then
                        Return pi.GetValue(obj, args)
                    End If
                End If
                If reqMemberType.HasFlag(MemberTypes.Field) Then
                    Dim fi = obj.GetType.GetField(callItem)
                    If fi IsNot Nothing Then
                        Return fi.GetValue(obj)
                    End If
                End If
                If reqMemberType.HasFlag(MemberTypes.Method) Then
                    Dim fni As MethodInfo
                    If args Is Nothing OrElse args.Count = 0 Then
                        fni = obj.GetType.GetMethod(callItem, {})
                    Else
                        fni = obj.GetType.GetMethod(callItem, args.Select(Function(arg) arg.GetType).ToArray)
                    End If
                    If fni IsNot Nothing Then
                        Return fni.Invoke(obj, args)
                    End If
                End If
            Catch ex As TargetInvocationException
                If ex.InnerException Is Nothing Then
                    Throw
                Else
                    Throw ex.InnerException
                End If
            End Try

            Throw New ArgumentException(String.Format(My.Resources.Reflection.PropertyX0DoesNotExistInTypeX1, callItem, obj.GetType.Name), "propName")
        End Function

        Private Sub SetSingleMember(result As Object, callItem As String, value As Object, ParamArray args() As Object)
            Dim pi = result.GetType.GetProperty(callItem)
            If pi IsNot Nothing Then
                Dim targetType = If(pi.PropertyType.IsNullable, Nullable.GetUnderlyingType(pi.PropertyType), pi.PropertyType)
                Dim convertedValue As Object
                If targetType.IsEnum Then
                    convertedValue = [Enum].ToObject(targetType, value)
                Else
                    convertedValue = If(value Is Nothing, Nothing, Convert.ChangeType(value, targetType))
                End If
                pi.SetValue(result, convertedValue, args)
                Exit Sub
            End If
            Dim fi = result.GetType.GetField(callItem)
            If fi IsNot Nothing Then
                Dim targetType = If(fi.FieldType.IsNullable, Nullable.GetUnderlyingType(fi.FieldType), fi.FieldType)
                Dim convertedValue As Object
                If targetType.IsEnum Then
                    convertedValue = [Enum].ToObject(targetType, value)
                Else
                    convertedValue = If(value Is Nothing, Nothing, Convert.ChangeType(value, targetType))
                End If
                fi.SetValue(result, convertedValue)
                Exit Sub
            End If
            Dim fni = result.GetType.GetMethod(callItem)
            If fni IsNot Nothing Then
                fni.Invoke(result, {value}.Union(args).ToArray)
                Exit Sub
            End If
            Throw New ArgumentException(String.Format(My.Resources.Reflection.PropertyX0DoesNotExistInTypeX1, callItem, result.GetType.Name), "propName")
        End Sub


    End Module
End Namespace
