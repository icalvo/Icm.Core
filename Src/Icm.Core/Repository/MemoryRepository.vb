Imports Icm.Reflection

Namespace Proes.Data

    Public Class MemoryRepository(Of TType As New, TKey As Structure)
        Implements IEntityRepository(Of TType, TKey)

        Private _store As ISet(Of TType)
        Private ReadOnly _idFunction As Func(Of TType, TKey)

        ''' <summary>
        ''' Initializes a new instance of the MemoryRepository class.
        ''' </summary>
        ''' <param name="idFunction"></param>
        Public Sub New(ByVal idFunction As Func(Of TType, TKey))
            _store = New HashSet(Of TType)
            _idFunction = idFunction
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MemoryRepository class.
        ''' </summary>
        ''' <param name="store"></param>
        ''' <param name="idFunction"></param>
        Public Sub New(ByVal store As ISet(Of TType), ByVal idFunction As Func(Of TType, TKey))
            _store = store
            _idFunction = idFunction
        End Sub

        Public Sub Load(entities As IEnumerable(Of TType))
            _store = New HashSet(Of TType)(entities)
        End Sub

        Public Sub Add(entity As TType) Implements IEntityRepository(Of TType, TKey).Add
            _store.Add(entity)
        End Sub

        Public Function Copy(other As TType) As TType Implements IEntityRepository(Of TType, TKey).Copy
            Dim newObject = Create()
            other.CopyEntityTo(newObject, "id")
            Add(newObject)

            Return newObject
        End Function

        Public Function Create() As TType Implements IEntityRepository(Of TType, TKey).Create
            Return New TType
        End Function

        Public Sub Delete(entity As TType) Implements IEntityRepository(Of TType, TKey).Delete
            _store.Remove(entity)
        End Sub

        Public Function DeleteById(id As TKey) As TType Implements IEntityRepository(Of TType, TKey).DeleteById
            Delete(GetById(id))
        End Function

        Public Function GetById(id As TKey?, Optional bypassCache As Boolean = False, Optional includePath As String = Nothing) As TType Implements IEntityRepository(Of TType, TKey).GetById
            If id.HasValue Then
                Return _store.SingleOrDefault(Function(obj) _idFunction(obj).Equals(id.Value))
            Else
                Return Nothing
            End If
        End Function

        Public Function GetByIdOrCreate(id As TKey?, Optional bypassCache As Boolean = False, Optional includePath As String = Nothing) As TType Implements IEntityRepository(Of TType, TKey).GetByIdOrCreate
            If id.HasValue Then
                Return GetById(id, bypassCache, includePath)
            Else
                Return Nothing
            End If
        End Function

        Public Sub Save() Implements IEntityRepository(Of TType, TKey).Save

        End Sub

        Public Function GetEnumerator() As IEnumerator(Of TType) Implements IEnumerable(Of TType).GetEnumerator
            Return _store.GetEnumerator
        End Function

        Public Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return _store.GetEnumerator
        End Function
    End Class

    Public Class MemoryRepository(Of TType As New)
        Inherits MemoryRepository(Of TType, Integer)
        Implements IEntityRepository(Of TType)

        ''' <summary>
        ''' Initializes a new instance of the MemoryRepository class.
        ''' </summary>
        ''' <param name="idFunction"></param>
        Public Sub New(ByVal idFunction As Func(Of TType, Integer))
            MyBase.New(idFunction)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MemoryRepository class.
        ''' </summary>
        ''' <param name="store"></param>
        ''' <param name="idFunction"></param>
        Public Sub New(ByVal store As HashSet(Of TType), ByVal idFunction As Func(Of TType, Integer))
            MyBase.New(store, idFunction)
        End Sub

    End Class

End Namespace
