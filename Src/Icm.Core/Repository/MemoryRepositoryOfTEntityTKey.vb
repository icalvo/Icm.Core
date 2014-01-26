Imports Icm.Reflection
Imports System.Linq.Expressions

Namespace Icm.Data
    Public Class MemoryRepository(Of TEntity, TKey)
        Implements IRepository(Of TEntity, TKey)

        Protected store As ICollection(Of TEntity)
        Private ReadOnly _idFunction As Expression(Of Func(Of TEntity, TKey))

        ''' <summary>
        ''' Initializes a new instance of the MemoryRepository class.
        ''' </summary>
        ''' <param name="idFunction"></param>
        Public Sub New(ByVal idFunction As Expression(Of Func(Of TEntity, TKey)))
            store = New HashSet(Of TEntity)
            _idFunction = idFunction
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MemoryRepository class.
        ''' </summary>
        ''' <param name="store"></param>
        ''' <param name="idFunction"></param>
        Public Sub New(ByVal store As ICollection(Of TEntity), ByVal idFunction As Expression(Of Func(Of TEntity, TKey)))
            Me.store = store
            _idFunction = idFunction
        End Sub

        Public Sub Load(entities As IEnumerable(Of TEntity))
            store = New HashSet(Of TEntity)(entities)
        End Sub

        Public Sub Add(entity As TEntity) Implements IRepository(Of TEntity, TKey).Add
            store.Add(entity)
        End Sub

        Public Sub Delete(entity As TEntity) Implements IRepository(Of TEntity, TKey).Delete
            store.Remove(entity)
        End Sub

        Private Function IdEqualsExpression(key As TKey) As Expression(Of Func(Of TEntity, Boolean))
            ' Going from _idFunction, that has the form "Function(x) x.Id",
            ' to "Function(x) x.Id = VALUE" (where VALUE is the supplied key)

            ' So first we build the equality expression "x.Id = VALUE":
            Dim equalExpr = Expression.Equal(_idFunction.Body, Expression.Constant(key))

            ' Then we create a new lambda with the equality expression as body and the same parameter of _idFunction.
            Return Expression.Lambda(Of Func(Of TEntity, Boolean))(equalExpr, _idFunction.Parameters.First)
        End Function

        Public Overridable Function GetById(key As TKey) As TEntity Implements IRepository(Of TEntity, TKey).GetById
            Return store.AsQueryable().SingleOrDefault(IdEqualsExpression(key))
        End Function

        Public Function GetEnumerator() As IEnumerator(Of TEntity) Implements IEnumerable(Of TEntity).GetEnumerator
            Return store.GetEnumerator
        End Function

        Public Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return store.GetEnumerator
        End Function

        Public Function Contains(entity As TEntity) As Boolean Implements IRepository(Of TEntity, TKey).Contains
            Return store.Contains(entity)
        End Function

        Public Function ContainsId(key As TKey) As Boolean Implements IRepository(Of TEntity, TKey).ContainsId
            Return store.AsQueryable().Any(IdEqualsExpression(key))
        End Function
    End Class

End Namespace
