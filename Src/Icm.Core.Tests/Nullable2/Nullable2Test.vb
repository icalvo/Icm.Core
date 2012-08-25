Imports Icm

<TestFixture(), Category("Icm")>
Public Class Nullable2Test

    <Test()>
    Public Sub HasValueTest()
        Dim nulInteger As Nullable2(Of Integer)
        Assert.IsFalse(nulInteger.HasValue)

        nulInteger = Nothing
        Assert.IsFalse(nulInteger.HasValue)

        nulInteger = 0
        Assert.That(nulInteger.HasValue)

        nulInteger = Nothing
        Assert.IsFalse(nulInteger.HasValue)

        Dim nulString As Nullable2(Of String) = Nothing

        Assert.That(nulString.HasValue)
        nulString = Nothing
        Assert.That(nulString.HasValue)
        nulString = ""
        Assert.That(nulString.HasValue)
        nulString = "Abc"
        Assert.That(nulString.HasValue)

    End Sub

    <Test()>
    Public Sub ValueTest()
        Dim nulInteger As Nullable2(Of Integer)

        nulInteger = 0
        Assert.DoesNotThrow(
            Sub()
                Dim target = nulInteger.Value
            End Sub
        )

        nulInteger = Nothing
        Assert.Throws(Of InvalidOperationException)(
            Sub()
                Dim target = nulInteger.Value
            End Sub
        )

        Dim nulString As Nullable2(Of String) = Nothing

        Assert.DoesNotThrow(
            Sub()
                Dim target = nulString.Value
            End Sub
        )

        nulString = "Abc"
        Assert.DoesNotThrow(
            Sub()
                Dim target = nulString.Value
            End Sub
        )

    End Sub

    <Test()>
    Public Sub HasSomethingTest()
        Dim nulInteger As Nullable2(Of Integer)
        Assert.IsFalse(nulInteger.HasSomething)

        nulInteger = Nothing
        Assert.IsFalse(nulInteger.HasSomething)

        nulInteger = 0
        Assert.That(nulInteger.HasSomething)

        nulInteger = Nothing
        Assert.IsFalse(nulInteger.HasSomething)

        Dim nulString As Nullable2(Of String) = Nothing

        Assert.IsFalse(nulString.HasSomething)
        nulString = Nothing
        Assert.IsFalse(nulString.HasSomething)
        nulString = ""
        Assert.That(nulString.HasSomething)
        nulString = "Abc"
        Assert.That(nulString.HasSomething)

    End Sub

End Class
