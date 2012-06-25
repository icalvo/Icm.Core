Imports System.Runtime.Serialization

Namespace Icm.Reflection
    Friend Class VisitedGraph
        Inherits Dictionary(Of Object, Object)
        Public Sub New()

        End Sub
        Public Sub New(ByVal capacity As Integer)
            MyBase.New(capacity)

        End Sub
        Public Sub New(ByVal comparer As IEqualityComparer(Of Object))
            MyBase.New(comparer)

        End Sub
        Public Sub New(ByVal capacity As Integer, ByVal comparer As IEqualityComparer(Of Object))
            MyBase.New(capacity, comparer)

        End Sub
        Public Sub New(ByVal dictionary As IDictionary(Of Object, Object))
            MyBase.New(dictionary)

        End Sub
        Public Sub New(ByVal dictionary As IDictionary(Of Object, Object), ByVal comparer As IEqualityComparer(Of Object))
            MyBase.New(dictionary, comparer)

        End Sub
        Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
            MyBase.New(info, context)

        End Sub

        Public Shadows Function ContainsKey(ByVal key As Object) As Boolean
            If key Is Nothing Then
                Return True
            End If
            Return MyBase.ContainsKey(key)
        End Function

        Default Public Shadows ReadOnly Property Item(ByVal key As Object) As Object
            Get
                If key Is Nothing Then
                    Return Nothing
                End If
                Return MyBase.Item(key)
            End Get
        End Property
    End Class
End Namespace

