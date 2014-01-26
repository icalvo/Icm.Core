Imports NUnit.Framework

<TestFixture>
Public Class MemoryRepositoryTest

    Private Class MyRegister
        Implements IEqualityComparer(Of MyRegister)

        Public Id As Integer
        Public Value As String

        Public Shared Function Create(id As Integer, val As String) As MyRegister
            Return New MyRegister With {.Id = id, .Value = val}
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            Return Equals(Me, DirectCast(obj, MyRegister))
        End Function

        Public Overloads Function Equals(x As MyRegister, y As MyRegister) As Boolean Implements IEqualityComparer(Of MyRegister).Equals
            Return x.Id = y.Id AndAlso x.Value = y.Value
        End Function

        Public Overloads Function GetHashCode(obj As MyRegister) As Integer Implements IEqualityComparer(Of MyRegister).GetHashCode
            Return 0
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("[{0}:{1}]", Id, Value)
        End Function
    End Class

    <Test>
    Public Sub ConstructorTest()
        Dim repo As New Icm.Data.MemoryRepository(Of MyRegister)(Function(reg) reg.Id)

        Assert.That(repo.Count, [Is].EqualTo(0))
    End Sub

    <Test>
    Public Sub Constructor2Test()
        Dim repo As New Icm.Data.MemoryRepository(Of MyRegister)(
            New HashSet(Of MyRegister) From {
                MyRegister.Create(1, "asdf"),
                MyRegister.Create(2, "qwer"),
                MyRegister.Create(3, "zxcv")
            },
            Function(reg) reg.Id)

        Assert.That(repo, [Is].EquivalentTo({
                MyRegister.Create(2, "qwer"),
                MyRegister.Create(1, "asdf"),
                MyRegister.Create(3, "zxcv")
            }))
    End Sub

    <Test>
    Public Sub LoadTest()
        Dim repo As New Icm.Data.MemoryRepository(Of MyRegister)(
            New HashSet(Of MyRegister) From {
                MyRegister.Create(1, "rtyu"),
                MyRegister.Create(2, "fghj"),
                MyRegister.Create(3, "vbnm")
            },
            Function(reg) reg.Id)
        repo.Load({
                MyRegister.Create(1, "asdf"),
                MyRegister.Create(2, "qwer"),
                MyRegister.Create(3, "zxcv")
            })
        Assert.That(repo, [Is].EquivalentTo({
                MyRegister.Create(1, "asdf"),
                MyRegister.Create(2, "qwer"),
                MyRegister.Create(3, "zxcv")
            }))
    End Sub

    <Test>
    Public Sub Test2()
        Dim repo As New Icm.Data.MemoryRepository(Of MyRegister)(
            New HashSet(Of MyRegister) From {
                MyRegister.Create(1, "rtyu"),
                MyRegister.Create(2, "fghj"),
                MyRegister.Create(3, "vbnm")
            },
            Function(reg) reg.Id)

        Dim item = repo.GetById(2)

        Assert.That(item, [Is].Not.Null)
        Assert.That(item.Id, [Is].EqualTo(2))
        Assert.That(item.Value, [Is].EqualTo("fghj"))
    End Sub
End Class
