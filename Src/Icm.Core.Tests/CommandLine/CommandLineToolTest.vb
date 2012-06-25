Imports Icm

'''<summary>
'''This is a test class for CommandLineToolTest and is intended
'''to contain all CommandLineToolTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class CommandLineToolTest

    Private Shared Function GetCommandLineExample() As CommandLine
        Dim cln As New CommandLine

        cln _
        .Required("b", "base", "Base of the logarithm", SubArgument.Required("base")) _
        .Optional("e", "exponent", "Exponent of the logarithm", SubArgument.Required("exponent")) _
        .Optional("x", "extensions", "Extensions", SubArgument.List("extensions")) _
        .MainParametersExactly({New UnnamedParameter("number", "Number which logarithm is extracted")})

        Return cln
    End Function

    Private Shared Function GetCommandLineExample2() As CommandLine
        Dim cln As New CommandLine

        cln _
        .Required("b", "base", "Base of the logarithm", SubArgument.Required("base")) _
        .Optional("e", "exponent", "Exponent of the logarithm", SubArgument.Required("exponent"))

        Return cln
    End Function

    <Test(), Category("Icm")>
    Public Sub IsPresent_FailsWithUndefinedOptionTest()
        Dim cln = GetCommandLineExample()

        cln.ProcessArguments("example.exe", "-b", "2", "32")

        Assert.Throws(Of UndefinedOptionException)(Sub()
                                                       Dim res = cln.IsPresent("f")
                                                   End Sub)

        Assert.Throws(Of UndefinedOptionException)(Sub()
                                                       Dim res = cln.IsPresent("foo")
                                                   End Sub)
    End Sub


    <Test(), Category("Icm")>
    Public Sub GetValue_FailsWithUndefinedOptionTest()
        Dim cln = GetCommandLineExample()

        cln.ProcessArguments("example.exe", "-b", "2", "32")

        Assert.Throws(Of UndefinedOptionException)(Sub()
                                                       Dim res = cln.GetValue("f")
                                                   End Sub)

        Assert.Throws(Of UndefinedOptionException)(Sub()
                                                       Dim res = cln.GetValue("foo")
                                                   End Sub)

    End Sub


    <Test(), Category("Icm")>
    Public Sub GetValues_FailsWithUndefinedOptionTest()
        Dim cln = GetCommandLineExample()

        cln.ProcessArguments("example.exe", "-b", "2", "32")

        Assert.Throws(Of UndefinedOptionException)(Sub()
                                                       Dim res = cln.GetValues("f")
                                                   End Sub)

        Assert.Throws(Of UndefinedOptionException)(Sub()
                                                       Dim res = cln.GetValues("foo")
                                                   End Sub)
    End Sub


    '''<summary>
    '''A test for MainArgumentsConfig
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub ProcessArguments_SeparatesMainArgumentsTest()

        Dim cln = GetCommandLineExample()

        cln.ProcessArguments("example.exe", "-b", "2", "32")

        Assert.IsFalse(cln.HasErrors)
    End Sub

    <Test(), Category("Icm")>
    Public Sub IsPresent_TrueWithShortTest()
        Dim cln = GetCommandLineExample()

        cln.ProcessArguments("example.exe", "-b", "2", "32")

        Assert.IsTrue(cln.IsPresent("b"))
        Assert.IsTrue(cln.IsPresent("base"))
    End Sub

    <Test(), Category("Icm")>
    Public Sub IsPresent_TrueWithLongTest()
        Dim cln = GetCommandLineExample()

        cln.ProcessArguments("example.exe", "--base", "2", "32")

        Assert.IsTrue(cln.IsPresent("b"))
        Assert.IsTrue(cln.IsPresent("base"))
    End Sub

    <Test(), Category("Icm")>
    Public Sub IsPresent_FalseWithLongTest()
        Dim cln = GetCommandLineExample()

        cln.ProcessArguments("example.exe", "-b", "2", "32")

        Assert.IsFalse(cln.IsPresent("e"))
        Assert.AreEqual(cln.GetValue("e"), Nothing)
        Assert.IsFalse(cln.IsPresent("exponent"))
        Assert.AreEqual(cln.GetValue("exponent"), Nothing)
    End Sub

    <Test(), Category("Icm")>
    Public Sub MainArgumentTest()
        Dim cln = GetCommandLineExample()

        cln.ProcessArguments("example.exe", "-b", "2", "32")

        Assert.AreEqual(cln.MainValue, "32")
        Assert.AreEqual(cln.MainValues.Count, 1)
    End Sub

    <Test(), Category("Icm")>
    Public Sub MainArgumentsTest()
        Dim arg = New UnnamedParameter("arg", "")
        Dim cln = GetCommandLineExample2().MainParametersAtMost({arg, arg, arg})

        cln.ProcessArguments("example.exe", "-b", "2", "32", "asdf")

        Assert.AreEqual(cln.MainValue, "32")
        Assert.AreEqual(cln.MainValues.Count, 2)
        Assert.AreEqual(cln.MainValues(0), "32")
        Assert.AreEqual(cln.MainValues(1), "asdf")

    End Sub

    <Test(), Category("Icm")>
    Public Sub MainAtLeast1Test()
        Dim arg = New UnnamedParameter("arg", "")
        Dim cln = GetCommandLineExample2().MainParametersAtLeast(arg, {arg, arg, arg})

        cln.ProcessArguments("example.exe", "-b", "2", "32", "asdf")

        Assert.IsTrue(cln.HasErrors())
    End Sub

    <Test(), Category("Icm")>
    Public Sub MainAtLeast2Test()
        Dim arg = New UnnamedParameter("arg", "")
        Dim cln = GetCommandLineExample2().MainParametersAtLeast(arg, {arg, arg, arg})

        cln.ProcessArguments("example.exe", "-b", "2", "32", "asdf", "qwer")

        Assert.IsFalse(cln.HasErrors())
    End Sub


    <Test(), Category("Icm")>
    Public Sub MainAtMost1Test()
        Dim arg = New UnnamedParameter("arg", "")
        Dim cln = GetCommandLineExample2().MainParametersAtMost({arg, arg})

        cln.ProcessArguments("example.exe", "-b", "2", "32", "asdf")

        Assert.IsFalse(cln.HasErrors())
    End Sub

    <Test(), Category("Icm")>
    Public Sub MainAtMost2Test()
        Dim arg = New UnnamedParameter("arg", "")
        Dim cln = GetCommandLineExample2().MainParametersAtMost({arg, arg})

        cln.ProcessArguments("example.exe", "-b", "2", "32", "asdf", "qwer")

        Assert.IsTrue(cln.HasErrors())
    End Sub

    <Test(), Category("Icm")>
    Public Sub GetValuesTest()

        Dim cln = GetCommandLineExample()

        cln.ProcessArguments("example.exe", "-b", "4", "-x", "*.vb", "*.asdf", "-", "32")

        Assert.IsFalse(cln.HasErrors)
        Assert.IsTrue(cln.IsPresent("x"))
        Assert.AreEqual(2, cln.GetValues("x").Count)
        Assert.IsTrue(cln.GetValues("x").Contains("*.vb"))
        Assert.IsTrue(cln.GetValues("x").Contains("*.asdf"))
        Assert.IsTrue(cln.MainValue = "32")
    End Sub

    <Test(), Category("Icm")>
    Public Sub GetValues2Test()

        Dim cln = GetCommandLineExample()

        cln.ProcessArguments("example.exe", "-b", "4", "-x", "-", "32")

        Assert.IsFalse(cln.HasErrors)
        Assert.IsTrue(cln.IsPresent("x"))
        Assert.AreEqual(0, cln.GetValues("x").Count)
        Assert.IsTrue(cln.MainValue = "32")
    End Sub
End Class
