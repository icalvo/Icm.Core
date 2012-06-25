Imports System.Reflection

Namespace Icm.Reflection
    ''' <summary>
    ''' This class is an abstract base class that can be used as a really simple way of making an object
    ''' copyable.
    ''' 
    ''' To make an object copyable, simply inherit from this class, and call the base constructor from
    ''' your constructor, with the same arguments as your constructor.
    ''' </summary>
    ''' <example>
    '''     public class ACopyable : Copyable
    '''     {
    '''       private ACopyable _friend;
    '''       
    '''       public ACopyable(ACopyable friend)
    '''         : base(friend)
    '''       {
    '''         this._friend = friend;
    '''       }
    '''     }
    ''' </example>
    Public MustInherit Class BaseCopyable
        Implements ICopyable

        Private ReadOnly constructor As ConstructorInfo
        Private constructorArgs As Object()

        Protected Sub New(ByVal ParamArray args As Object())
            Dim frame As New StackFrame(1, True)

            Dim method = frame.GetMethod()

            If Not method.IsConstructor Then
                Throw New InvalidOperationException("Copyable cannot be instantiated directly; use a subclass.")
            End If

            Dim parameters As ParameterInfo() = method.GetParameters()

            If args.Length > parameters.Length Then
                Throw New InvalidOperationException("Copyable constructed with more arguments than the constructor of its subclass.")
            End If

            Dim constructorTypeArgs As New List(Of Type)()
            Dim i As Integer = 0

            While i < args.Length
                If Not parameters(i).ParameterType.IsAssignableFrom(args(i).[GetType]()) Then
                    Throw New InvalidOperationException(String.Format("Copyable constructed with invalid type {0} for argument #{2} (should be {1})", args(i).[GetType](), parameters(i).ParameterType, i))
                End If
                constructorTypeArgs.Add(parameters(i).ParameterType)
                i += 1
            End While
            While i < parameters.Length
                If Not parameters(i).IsOptional Then
                    Throw New InvalidOperationException("Copyable constructed with too few arguments.")
                End If
                constructorTypeArgs.Add(parameters(i).ParameterType)
                i += 1
            End While

            constructor = [GetType]().GetConstructor(constructorTypeArgs.ToArray())
            constructorArgs = args
        End Sub

        Public Function CreateInstanceForCopy() As Object Implements ICopyable.CreateInstanceForCopy
            Return constructor.Invoke(constructorArgs)
        End Function
    End Class
End Namespace

