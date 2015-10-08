Imports Icm.Reflection
Imports System.Linq.Expressions

Namespace Icm.Data

    Public Class MemoryRepository(Of TEntity)
        Inherits MemoryRepository(Of TEntity, Integer)
        Implements IRepository(Of TEntity)

        ''' <summary>
        ''' Initializes a new instance of the MemoryRepository class.
        ''' </summary>
        ''' <param name="idFunction"></param>
        Public Sub New(ByVal idFunction As Expression(Of Func(Of TEntity, Integer)))
            MyBase.New(idFunction)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MemoryRepository class.
        ''' </summary>
        ''' <param name="store"></param>
        ''' <param name="idFunction"></param>
        Public Sub New(ByVal store As HashSet(Of TEntity), ByVal idFunction As Expression(Of Func(Of TEntity, Integer)))
            MyBase.New(store, idFunction)
        End Sub

    End Class

End Namespace
