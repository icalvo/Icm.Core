Namespace Icm

    ''' <summary>
    ''' Represents a nullable entity that, unlike Nullable(Of T), can enclose either a class or a struct.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks>
    ''' <para>For enclosed structs, Nullable2 works exactly as Nullable(Of T).</para>
    ''' <para>For enclosed class, HasValue always returns True, since Nothing is a valid "state" for
    ''' a class. You may need to HasSomething instead, which returns the same as HasValue for struct-T
    ''' but returns False for class-T if the value is Nothing.</para>
    ''' </remarks>
    Public Structure Nullable2(Of T)

        Private value_ As T
        Private hasStructValue_ As Boolean
        Private Shared ReadOnly isClass_ As Boolean

        Shared Sub New()
            isClass_ = GetType(T).IsClass
        End Sub

        Property Value As T
            Get
                If HasValue Then
                    Return value_
                Else
                    Throw New InvalidOperationException("Null value")
                End If
            End Get
            Set(value As T)
                value_ = value
                hasStructValue_ = True
            End Set
        End Property

        Property V As T
            Get
                Return Value
            End Get
            Set(value As T)
                Me.Value = value
            End Set
        End Property

        ReadOnly Property HasValue As Boolean
            Get
                Return isClass_ OrElse hasStructValue_
            End Get
        End Property


        ReadOnly Property HasSomething As Boolean
            Get
                If isClass_ Then
                    Return value_ IsNot Nothing
                Else
                    Return hasStructValue_
                End If
            End Get
        End Property

        Public Shared Widening Operator CType(ByVal d As Nullable2(Of T)) As T
            Return d.Value
        End Operator

        Public Shared Widening Operator CType(ByVal b As T) As Nullable2(Of T)
            Return New Nullable2(Of T) With {.Value = b}
        End Operator

    End Structure

End Namespace
