Imports System.Reflection

Namespace Icm.Reflection

    ''' <summary>
    ''' Provider of instances of a given type.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks>
    ''' <para>This class provides a list of instances of every type that implements T (one
    ''' for each concrete type).</para>
    ''' <para>In contrast to <see cref="ActivatorTools"></see>, the list autoupdates each 
    ''' time a new assembly is loaded in the current AppDomain.</para>
    ''' <para>However it has a limitation in that it only creates types with a default
    ''' constructor (no arguments).</para>
    ''' </remarks>
    Public Class ImplementorInstanceProvider(Of T)
        ''' <summary>
        ''' A list of instance providers that are available.
        ''' </summary>
        Private ReadOnly implementorInstances_ As Dictionary(Of String, T) = Initialize()

        ReadOnly Property ImplementorsInstances As List(Of T)
            Get
                Return implementorInstances_.Values.ToList
            End Get
        End Property

        ''' <summary>
        ''' Updates the list of instance providers with any found in the newly loaded assembly.
        ''' </summary>
        ''' <param name="sender">The object that sent the event.</param>
        ''' <param name="args">The event arguments.</param>
        Private Sub AssemblyLoaded(ByVal sender As Object, ByVal args As AssemblyLoadEventArgs)
            UpdateList(args.LoadedAssembly, implementorInstances_)
        End Sub
        ''' <summary>
        ''' Initializes the list of instance providers.
        ''' </summary>
        ''' <returns>A list of instance providers that are used by the Copyable framework.</returns>
        Private Function Initialize() As Dictionary(Of String, T)
            Dim providers As New Dictionary(Of String, T)
            For Each assembly As Assembly In AppDomain.CurrentDomain.GetAssemblies()
                UpdateList(assembly, providers)
            Next
            AddHandler AppDomain.CurrentDomain.AssemblyLoad, New AssemblyLoadEventHandler(AddressOf AssemblyLoaded)
            Return providers
        End Function

        ''' <summary>
        ''' Updates the list of instance providers with the ones found in the given assembly.
        ''' </summary>
        ''' <param name="assembly">The assembly with which the list of instance providers will be updated.</param>
        Private Sub UpdateList(ByVal assembly As Assembly, ByVal instanceList As Dictionary(Of String, T))
            Dim newInstances = GetInstanceDictionaryOfAllImplementors(Of T)(assembly)
            For Each inst In newInstances
                If instanceList.ContainsKey(inst.Key) Then
                    instanceList(inst.Key) = inst.Value
                Else
                    instanceList.Add(inst.Key, inst.Value)
                End If
            Next
        End Sub

    End Class

End Namespace
