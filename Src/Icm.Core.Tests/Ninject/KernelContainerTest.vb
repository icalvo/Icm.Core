<TestFixture>
Public Class KernelContainerTest

    <Test>
    Public Sub TryInstance_WithNoConfig_ThrowsNothing()
        Assert.That(Sub()
                        Dim c = Ninject.TryInstance(Of ICollection)()
                    End Sub,
                    Throws.Nothing)
        Assert.That(Sub()
                        Dim c = Ninject.TryInstance(Of ICollection)("name")
                    End Sub,
                    Throws.Nothing)
        Assert.That(Sub()
                        Dim c = Ninject.TryInstance(Of ICollection)(New Global.Ninject.Parameters.Parameter("a", "", False))
                    End Sub,
                    Throws.Nothing)
        Assert.That(Sub()
                        Dim c = Ninject.TryInstance(GetType(ICollection), "name")
                    End Sub,
                    Throws.Nothing)
        Assert.That(Sub()
                        Dim c = Ninject.TryInstance(GetType(ICollection), New Global.Ninject.Parameters.Parameter("a", "", False))
                    End Sub,
                    Throws.Nothing)
    End Sub

    <Test>
    Public Sub Instance_WithNoConfig_ThrowsActivationException()
        Assert.That(Sub()
                        Dim c = Ninject.Instance(Of ICollection)()
                    End Sub,
                    Throws.InstanceOf(Of Global.Ninject.ActivationException))
        Assert.That(Sub()
                        Dim c = Ninject.Instance(Of ICollection)("name")
                    End Sub,
                    Throws.InstanceOf(Of Global.Ninject.ActivationException))
        Assert.That(Sub()
                        Dim c = Ninject.Instance(Of ICollection)(New Global.Ninject.Parameters.Parameter("a", "", False))
                    End Sub,
                    Throws.InstanceOf(Of Global.Ninject.ActivationException))
        Assert.That(Sub()
                        Dim c = Ninject.Instance(GetType(ICollection), "name")
                    End Sub,
                    Throws.InstanceOf(Of Global.Ninject.ActivationException))
        Assert.That(Sub()
                        Dim c = Ninject.Instance(GetType(ICollection), New Global.Ninject.Parameters.Parameter("a", "", False))
                    End Sub,
                    Throws.InstanceOf(Of Global.Ninject.ActivationException))
    End Sub

    <Test>
    Public Sub Instance1_WithConfig_ReturnsExpected()
        Dim expected = {"a", "b"}
        Ninject.Kernel.Bind(Of ICollection).ToConstant(expected)

        Dim actual = Ninject.Instance(Of ICollection)()
        Assert.That(actual, [Is].EquivalentTo(expected))
    End Sub

    <Test>
    Public Sub Instance2_WithConfig_ReturnsExpected()
        Dim expected = {"a", "b"}
        Ninject.Kernel.Bind(Of ICollection).ToConstant(expected).Named("mybinding")

        Dim actual = Ninject.Instance(Of ICollection)("mybinding")
        Assert.That(actual, [Is].EquivalentTo(expected))
    End Sub

End Class
