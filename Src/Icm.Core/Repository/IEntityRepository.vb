Namespace Icm.Data

    Public Interface IEntityRepository(Of TEntity, TKey As Structure)
        Inherits IEnumerable(Of TEntity)

        Sub Add(ByVal entity As TEntity)
        Sub Delete(ByVal entity As TEntity)
        Function Create() As TEntity
        Function Copy(other As TEntity) As TEntity
        Sub Save()

    End Interface

    Public Interface IEntityRepository(Of TEntity)
        Inherits IEntityRepository(Of TEntity, Integer)

    End Interface

End Namespace
