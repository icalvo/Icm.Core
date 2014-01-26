Namespace Icm.Data

    Public Class MemoryEntityRepository(Of TEntity As IEntity(Of TKey), TKey)
        Implements IRepository(Of TEntity, TKey)

        Protected store As Dictionary(Of TKey, TEntity)

        ''' <summary>
        ''' Initializes a new instance of the MemoryRepository class.
        ''' </summary>
        Public Sub New()
            store = New Dictionary(Of TKey, TEntity)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MemoryRepository class.
        ''' </summary>
        ''' <param name="items"></param>
        Public Sub New(ByVal items As ICollection(Of TEntity))
            Load(items)
        End Sub

        Public Sub Load(items As IEnumerable(Of TEntity))
            Me.store = New Dictionary(Of TKey, TEntity)
            For Each item In items
                Add(item)
            Next
        End Sub

        Public Sub Add(entity As TEntity) Implements IRepository(Of TEntity, TKey).Add
            store.Add(entity.Id, entity)
        End Sub

        Public Sub Delete(entity As TEntity) Implements IRepository(Of TEntity, TKey).Delete
            store.Remove(entity.Id)
        End Sub

        Public Overridable Function GetById(id As TKey) As TEntity Implements IRepository(Of TEntity, TKey).GetById
            Return store(id)
        End Function

        Public Function GetEnumerator() As IEnumerator(Of TEntity) Implements IEnumerable(Of TEntity).GetEnumerator
            Return store.Values.GetEnumerator
        End Function

        Public Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return store.Values.GetEnumerator
        End Function

        Public Function Contains(entity As TEntity) As Boolean Implements IRepository(Of TEntity, TKey).Contains
            Return store.ContainsKey(entity.Id)
        End Function

        Public Function ContainsId(key As TKey) As Boolean Implements IRepository(Of TEntity, TKey).ContainsId
            Return store.ContainsKey(key)
        End Function
    End Class

End Namespace
