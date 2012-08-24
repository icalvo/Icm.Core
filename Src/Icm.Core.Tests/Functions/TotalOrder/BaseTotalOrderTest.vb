<TestFixture>
Public Class BaseTotalOrderTest

    Class FakeTotalOrder
        Inherits BaseTotalOrder(Of Long)

        Public Overrides Function Greatest() As Long
            Return 1783
        End Function

        Public Overrides Function Least() As Long
            Return -1761
        End Function

        Public Overrides Function Long2T(d As Long) As Long
            Return d
        End Function

        Public Overrides Function T2Long(t As Long) As Long
            Return t
        End Function
    End Class

    <TestCase(18, 17)>
    <TestCase(0, -1)>
    <TestCase(1, 0)>
    Public Sub Next_ForUsual_ReturnsNextConsecutive(expected As Long, argument As Long)
        Dim target As BaseTotalOrder(Of Long) = New FakeTotalOrder

        Dim actual = target.Next(argument)

        Assert.That(expected, [Is].EqualTo(actual))
    End Sub

    <Test>
    Public Sub Next_ForGreatest_ThrowsArgumentOutOfRange()
        Dim target As BaseTotalOrder(Of Long) = New FakeTotalOrder

        Assert.That(Sub() target.Next(target.Greatest), Throws.InstanceOf(Of ArgumentOutOfRangeException))
    End Sub

    <TestCase(18, 19)>
    <TestCase(-1, 0)>
    <TestCase(0, 1)>
    Public Sub Previous_ForUsual_ReturnsPreviousConsecutive(expected As Long, argument As Long)
        Dim target As BaseTotalOrder(Of Long) = New FakeTotalOrder

        Dim actual = target.Previous(argument)

        Assert.That(expected, [Is].EqualTo(actual))
    End Sub

    <Test>
    Public Sub Previous_ForLeast_ThrowsArgumentOutOfRange()
        Dim target As BaseTotalOrder(Of Long) = New FakeTotalOrder

        Assert.That(Sub() target.Previous(target.Least), Throws.InstanceOf(Of ArgumentOutOfRangeException))
    End Sub


End Class
