Namespace Icm.Reflection
    ''' <summary>
    ''' Utility functions to manage Activator class.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module ActivatorTools

        ''' <summary>
        ''' Creates an instance of a type.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="args"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' This is a shortcut for Activator.CreateInstance that returns a typed object.
        ''' Use:
        ''' <example>Dim obj = CreateInstance(Of MyClass)(arg1, arg2, ...)</example>
        ''' Instead of:
        ''' <example>Dim obj = DirectCast(Activator.CreateInstance(GetType(MyClass), arg1, arg2, ...), MyClass)</example>
        ''' </remarks>
        Public Function CreateInstance(Of T)(ByVal ParamArray args() As Object) As T
            Return DirectCast(Activator.CreateInstance(GetType(T), args), T)
        End Function

        ''' <summary>
        ''' Gets all the implementors of T  in the current AppDomain.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <returns></returns>
        ''' <remarks>
        ''' This method returns all the concrete classes that derive from T (including T) that 
        ''' can be found on the current AppDomain assemblies.
        ''' </remarks>
        Public Function GetAllImplementors(Of T)() As IEnumerable(Of Type)
            Return GetAllImplementors(Of T)(AppDomain.CurrentDomain.GetAssemblies)
        End Function

        ''' <summary>
        ''' Gets all the implementors of T in the current AppDomain.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="assemblies"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' This method returns all the concrete classes that derive from T (including T) that 
        ''' can be found on the passed assembly list.
        ''' </remarks>
        Public Function GetAllImplementors(Of T)(assemblies As IEnumerable(Of System.Reflection.Assembly)) As IEnumerable(Of Type)
            Dim type = GetType(T)
            Return _
                assemblies _
                .SelectMany(Function(s)
                                Try
                                    Return s.GetTypes()
                                Catch ex As Exception
                                    Return New Type() {}
                                End Try
                            End Function) _
                .Where(Function(p) type.IsAssignableFrom(p) AndAlso Not p.IsAbstract)
        End Function

        ''' <summary>
        ''' Gets all the implementors of T in a given assembly.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="assembly"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' This method returns all the concrete classes that derive from T (including T) that 
        ''' can be found on the passed assembly.
        ''' </remarks>
        Public Function GetAllImplementors(Of T)(assembly As System.Reflection.Assembly) As IEnumerable(Of Type)
            Return GetAllImplementors(Of T)({assembly})
        End Function

        ''' <summary>
        ''' Gets a list of instances of all implementors of T in the current AppDomain.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="args"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetInstanceListOfAllImplementors(Of T)(ByVal ParamArray args() As Object) As IEnumerable(Of T)
            Return GetInstanceDictionaryOfAllImplementors(Of T)(args).Values
        End Function

        ''' <summary>
        ''' Gets a dictionary (key: type name) of all implementors of T in the current AppDomain.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="args"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetInstanceDictionaryOfAllImplementors(Of T)(ByVal ParamArray args() As Object) As Dictionary(Of String, T)
            Dim types = GetAllImplementors(Of T)()

            Dim instances As New Dictionary(Of String, T)
            For Each type In types
                instances.Add(type.Name, DirectCast(Activator.CreateInstance(type, args), T))
            Next

            Return instances
        End Function

        ''' <summary>
        ''' Gets a list of instances of all implementors of T in the passed assembly.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="args"></param>
        ''' <param name="assembly"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetInstanceListOfAllImplementors(Of T)(assembly As System.Reflection.Assembly, ByVal ParamArray args() As Object) As IEnumerable(Of T)
            Return GetInstanceDictionaryOfAllImplementors(Of T)({assembly}, args).Values
        End Function

        ''' <summary>
        ''' Gets a list of instances of all implementors of T in the passed assembly list.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="args"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetInstanceListOfAllImplementors(Of T)(assemblies As IEnumerable(Of System.Reflection.Assembly), ByVal ParamArray args() As Object) As IEnumerable(Of T)
            Return GetInstanceDictionaryOfAllImplementors(Of T)(assemblies, args).Values
        End Function

        Public Function GetInstanceDictionaryOfAllImplementors(Of T)(assembly As System.Reflection.Assembly, ByVal ParamArray args() As Object) As Dictionary(Of String, T)
            Return GetInstanceDictionaryOfAllImplementors(Of T)({assembly}, args)
        End Function

        ''' <summary>
        ''' Gets a dictionary (key: type name) of all implementors of T in the passed assembly list.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="args"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetInstanceDictionaryOfAllImplementors(Of T)(assemblies As IEnumerable(Of System.Reflection.Assembly), ByVal ParamArray args() As Object) As Dictionary(Of String, T)
            Dim types = GetAllImplementors(Of T)(assemblies)

            Dim instances As New Dictionary(Of String, T)
            For Each type In types
                instances.Add(type.Name, DirectCast(Activator.CreateInstance(type, args), T))
            Next

            Return instances
        End Function


    End Module

End Namespace
