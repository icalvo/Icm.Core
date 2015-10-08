Imports Icm.CommandLineTools

'''<summary>
'''This is a test class for ValuesStore
'''</summary>
<TestFixture(), Category("Icm")>
Public Class ValuesStoreTest

    <TestCase(Nothing, "asdf", ExpectedException:=GetType(ArgumentNullException))>
    <TestCase("asdf", Nothing, ExpectedException:=GetType(ArgumentNullException))>
    <TestCase("a", "a", ExpectedException:=GetType(ArgumentException))>
    <TestCase("a", "asdf")>
    Public Sub AddTest(shortName As String, longName As String)
        Dim store As New ValuesStore

        store.AddParameter(shortName, longName)

        Assert.That(store.ContainsKey(shortName))
        Assert.That(store.ContainsKey(longName))
    End Sub

    <TestCase(Nothing, "asdf", ExpectedException:=GetType(ArgumentNullException))>
    <TestCase("asdf", Nothing, ExpectedException:=GetType(ArgumentNullException))>
    <TestCase("a", "a", ExpectedException:=GetType(ArgumentException))>
    <TestCase("asdf", "a", ExpectedException:=GetType(ArgumentException))>
    <TestCase("asdf", "qwer", ExpectedException:=GetType(ArgumentException))>
    <TestCase("a", "asdf")>
    Public Sub AddValue(shortName As String, longName As String)
        Dim store As New ValuesStore

        store.AddParameter(shortName, longName)

        Assert.That(store.ContainsKey(shortName))
        Assert.That(store.ContainsKey(longName))
    End Sub

End Class
