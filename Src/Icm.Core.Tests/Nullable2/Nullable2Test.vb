Imports Icm

<TestFixture(), Category("Icm")>
Public Class Nullable2Test

    <Test(), Category("Icm")>
    Public Sub Test1()
        Dim nulInteger As Nullable2(Of Integer)
        Assert.IsFalse(nulInteger.HasValue)

        nulInteger = Nothing
        Assert.IsFalse(nulInteger.HasValue)

        nulInteger = 0
        Assert.IsTrue(nulInteger.HasValue)
        Assert.DoesNotThrow(
            Sub()
                Dim target = nulInteger.Value
            End Sub
        )

        nulInteger = Nothing
        Assert.IsFalse(nulInteger.HasValue)
        Assert.Throws(Of InvalidOperationException)(
            Sub()
                Dim target = nulInteger.Value
            End Sub
        )

        Dim nulString As Nullable2(Of String) = Nothing

        Assert.IsTrue(nulString.HasValue)
        nulString = Nothing
        Assert.IsTrue(nulString.HasValue)
        nulString = ""
        Assert.IsTrue(nulString.HasValue)
        nulString = "Abc"
        Assert.IsTrue(nulString.HasValue)

    End Sub

End Class
