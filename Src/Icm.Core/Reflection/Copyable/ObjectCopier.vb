Imports System.Reflection

Namespace Icm.Reflection

    Public Class ObjectCopier(Of T)
        Private ReadOnly rootInstance As T
        Private ReadOnly cloneFilters As New List(Of Func(Of Object, Boolean))()
        Private visited As VisitedGraph

        Friend Sub New(ByVal _instance As T)
            rootInstance = _instance
        End Sub
        Public Function TypeFilter(Of TDeriv)() As ObjectCopier(Of T)
            cloneFilters.Add(Function(obj) TypeOf obj Is TDeriv)
            Return Me
        End Function
        Public Function TypeFilter(Of TDeriv)(ByVal additional As Func(Of TDeriv, Boolean)) As ObjectCopier(Of T)
            cloneFilters.Add(Function(obj) TypeOf obj Is TDeriv AndAlso additional(DirectCast(obj, TDeriv)))
            Return Me
        End Function
        ''' <summary>
        ''' Creates a clone of the object.
        ''' </summary>
        ''' <returns>A deep clone of the object.</returns>
        Public Function Clone() As T
            If rootInstance Is Nothing Then
                Return Nothing
            End If
            Return CopyTo(DirectCast(CreateCopy(rootInstance), T))
        End Function
        ''' <summary>
        ''' Creates a deep copy of the object using the supplied object as a target for the copy operation.
        ''' </summary>
        ''' <returns>A deep copy of the object.</returns>
        Public Function CopyTo(Of TCopy As T)(ByVal objTarget As TCopy) As TCopy
            If rootInstance Is Nothing Then
                Return Nothing
            End If
            If objTarget Is Nothing Then
                Throw New ArgumentNullException("The copy instance cannot be null")
            End If
            visited = New VisitedGraph()
            Return DirectCast(SetProperties(rootInstance, objTarget), TCopy)
        End Function

        Private Shared ReadOnly implementorInstanceProvider As New ImplementorInstanceProvider(Of IInstanceProvider)

        ''' <summary>
        ''' A list of instance providers that are available.
        ''' </summary>
        Private Shared ReadOnly Property Providers As IEnumerable(Of IInstanceProvider)
            Get
                Return implementorInstanceProvider.ImplementorsInstances
            End Get
        End Property

        ''' <summary>
        ''' Creates a deep copy of an object using the supplied dictionary of visited objects as 
        ''' a source of objects already encountered in the copy traversal. The dictionary of visited 
        ''' objects is used for holding objects that have already been copied, to avoid erroneous 
        ''' duplication of parts of the object graph.
        ''' </summary>
        ''' <param name="instance">The object to be copied.</param>
        ''' <returns></returns>
        Private Function Clone(ByVal instance As Object) As Object
            If instance Is Nothing Then
                Return Nothing
            End If
            Dim instanceType = instance.GetType()
            If instanceType.IsValueType OrElse instanceType Is GetType(String) Then
                Return instance
                ' Value types and strings are immutable
            ElseIf instanceType.IsArray Then
                Dim length As Integer = DirectCast(instance, Array).Length
                Dim copied = DirectCast(Activator.CreateInstance(instanceType, length), Array)
                visited.Add(instance, copied)
                For i As Integer = 0 To length - 1
                    copied.SetValue(Clone(DirectCast(instance, Array).GetValue(i)), i)
                Next
                Return copied
            ElseIf cloneFilters.Any(Function(filt) filt(instance)) Then
                Return instance
            Else
                Return SetProperties(instance, CreateCopy(instance))
            End If
        End Function

        Private Function SetProperties(ByVal instance As Object, ByVal copy As Object) As Object
            visited.Add(instance, copy)
            For Each field As FieldInfo In instance.[GetType]().GetFields(BindingFlags.[Public] Or BindingFlags.NonPublic Or BindingFlags.Instance)
                Dim value As Object = field.GetValue(instance)
                If visited.ContainsKey(value) Then
                    field.SetValue(copy, visited(value))
                Else
                    field.SetValue(copy, Clone(value))
                End If
            Next
            Return copy
        End Function

        Private Shared Function CreateCopy(ByVal instance As Object) As Object
            Dim instanceType = instance.GetType()
            If GetType(ICopyable).IsAssignableFrom(instanceType) Then
                Return DirectCast(instance, ICopyable).CreateInstanceForCopy()
            End If

            For Each provider As IInstanceProvider In Providers
                If provider.Provided Is instanceType Then
                    Return provider.CreateCopy(instance)
                End If
            Next

            Try
                Return Activator.CreateInstance(instanceType)
            Catch
                Throw New ArgumentException(String.Format("Object of type {0} cannot be cloned because it does not have a parameterless constructor. Supply an instance to copy data to, or create a parameterless constructor.", instanceType.FullName))
            End Try
        End Function
    End Class
End Namespace
