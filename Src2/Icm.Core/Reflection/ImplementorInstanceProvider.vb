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
    ''' constructor (no arguments), because it cannot be provided with the necesary arguments.</para>
    ''' </remarks>
    Public Class ImplementorInstanceProvider(Of T)
        ''' <summary>
        ''' A list of instance providers that are available.
        ''' </summary>
        Private ReadOnly implementorInstances_ As Dictionary(Of String, T)

        Public Sub New()
            implementorInstances_ = New Dictionary(Of String, T)
            CreateList()
            AddHandler AppDomain.CurrentDomain.AssemblyLoad, New AssemblyLoadEventHandler(AddressOf AssemblyLoaded)
        End Sub

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
            UpdateList(args.LoadedAssembly)
        End Sub
        ''' <summary>
        ''' Fills the list with currently loaded assemblies.
        ''' </summary>
        Private Sub CreateList()
            For Each assembly As Assembly In AppDomain.CurrentDomain.GetAssemblies()
                UpdateList(assembly)
            Next
        End Sub

        ''' <summary>
        ''' Updates the list of instance providers with the ones found in the given assembly.
        ''' </summary>
        ''' <param name="assembly">The assembly with which the list of instance providers will be updated.</param>
        Private Sub UpdateList(ByVal assembly As Assembly)
            Dim newInstances = GetInstanceDictionaryOfAllImplementors(Of T)(assembly)
            For Each inst In newInstances
                If implementorInstances_.ContainsKey(inst.Key) Then
                    implementorInstances_(inst.Key) = inst.Value
                Else
                    implementorInstances_.Add(inst.Key, inst.Value)
                End If
            Next
        End Sub

    End Class

End Namespace
