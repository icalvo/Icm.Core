Imports System.Runtime.CompilerServices

Namespace Icm.Data
    Public Module IRepositoryExtensions

        <Extension>
        Public Function GetByIdOptional(Of TEntity, TKey As Structure)(repo As IRepository(Of TEntity, TKey), key As TKey?) As TEntity
            If key.HasValue Then
                Return repo.GetById(key.Value)
            Else
                Return Nothing
            End If
        End Function
    End Module
End Namespace
