<TestFixture()>
Public Class NullableExtensionsTest

    <TestCase(3)>
    <TestCase(Nothing, ExpectedException:=GetType(InvalidOperationException))>
    Public Sub V_Test(obj As Nullable(Of Integer))
        Assert.That(obj.V, [Is].EqualTo(obj.Value))
    End Sub

    <TestCase(3)>
    <TestCase(Nothing)>
    Public Sub HasNotValue_Test(obj As Nullable(Of Integer))
        Assert.That(obj.HasNotValue, [Is].EqualTo(Not obj.HasValue))
    End Sub

End Class
