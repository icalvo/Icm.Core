Imports Ninject

Namespace Icm.Ninject
    Public Module KernelContainer

        Private _kernel As IKernel

        ReadOnly Property Kernel As IKernel
            Get
                If _kernel Is Nothing Then
                    _kernel = New StandardKernel
                End If
                Return _kernel
            End Get
        End Property

        Public Function Instance(Of T)(ParamArray args() As Parameters.IParameter) As T
            Return Kernel.Get(Of T)(args)
        End Function

        Public Function Instance(type As Type, ParamArray args() As Parameters.IParameter) As Object
            Return Kernel.Get(type, args)
        End Function

        Public Function Instance(Of T)(name As String) As T
            Return Kernel.Get(Of T)(name)
        End Function

        Public Function Instance(type As Type, name As String) As Object
            Return Kernel.Get(type, name)
        End Function

        Public Function TryInstance(Of T)(ParamArray args() As Parameters.IParameter) As T
            Return Kernel.TryGet(Of T)(args)
        End Function

        Public Function TryInstance(type As Type, ParamArray args() As Parameters.IParameter) As Object
            Return Kernel.TryGet(type, args)
        End Function

        Public Function TryInstance(Of T)(name As String) As T
            Return Kernel.TryGet(Of T)(name)
        End Function

        Public Function TryInstance(type As Type, name As String) As Object
            Return Kernel.TryGet(type, name)
        End Function


        Public Function Instances(Of T)(ParamArray args() As Parameters.IParameter) As IEnumerable(Of T)
            Return Kernel.GetAll(Of T)(args)
        End Function

        Public Function Instances(Of T)(constraint As Func(Of Global.Ninject.Planning.Bindings.IBindingMetadata, Boolean), ParamArray args() As Parameters.IParameter) As IEnumerable(Of T)
            Return Kernel.GetAll(Of T)(constraint, args)
        End Function

        Public Function Instances(type As Type, ParamArray args() As Parameters.IParameter) As IEnumerable(Of Object)
            Return Kernel.GetAll(type, args)
        End Function

        Public Function Instances(Of T)(name As String) As IEnumerable(Of T)
            Return Kernel.GetAll(Of T)(name)
        End Function

        Public Function Instances(type As Type, name As String) As IEnumerable(Of Object)
            Return Kernel.GetAll(type, name)
        End Function
    End Module
End Namespace

