Namespace Icm.Data

    Public Interface IRepository(Of TEntity, TKey)
        Inherits IEnumerable(Of TEntity)

        Sub Add(ByVal entity As TEntity)
        Function GetById(key As TKey) As TEntity

        Function Contains(entity As TEntity) As Boolean

        Function ContainsId(key As TKey) As Boolean

        Sub Delete(ByVal entity As TEntity)

    End Interface

End Namespace
