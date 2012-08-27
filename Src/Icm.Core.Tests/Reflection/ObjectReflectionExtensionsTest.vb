Imports Icm.Reflection

<TestFixture()>
Public Class ObjectReflectionExtensionsTest

    Class MyExample
        Public myfield As String = "mytest"
        Public mynumber As Integer = 7

        Property MyProp As String = "mytestp"
        Property MyPropNumber As Integer = 11

    End Class

    Dim GetFieldTestCases As Object() = {
        New TestCaseData(Nothing, "myfield").Throws(GetType(ArgumentNullException)),
        New TestCaseData(Nothing, "inexistent").Throws(GetType(ArgumentNullException)),
        New TestCaseData(New MyExample, "inexistent").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "mynumber").Throws(GetType(InvalidCastException)),
        New TestCaseData(New MyExample, "myfield").Returns("mytest")
    }

    <TestCaseSource("GetFieldTestCases")>
    Public Function GetField_Test(obj As MyExample, fieldName As String) As String
        Return obj.GetField(Of String)(fieldName)
    End Function

    Dim SetFieldTestCases As Object() = {
        New TestCaseData(Nothing, "myfield", "NEWVALUE").Throws(GetType(ArgumentNullException)),
        New TestCaseData(Nothing, "inexistent", "NEWVALUE").Throws(GetType(ArgumentNullException)),
        New TestCaseData(New MyExample, "inexistent", "NEWVALUE").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "myfield", 345).Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "myfield", "NEWVALUE").Returns("NEWVALUE")
    }

    <TestCaseSource("SetFieldTestCases")>
    Public Function SetField_Test(obj As MyExample, fieldName As String, value As Object) As String
        obj.SetField(fieldName, value)
        Return obj.myfield
    End Function

    Dim HasFieldTestCases As Object() = {
        New TestCaseData(Nothing, "myfield").Returns(False),
        New TestCaseData(Nothing, "inexistent").Returns(False),
        New TestCaseData(New MyExample, "myfield").Returns(True),
        New TestCaseData(New MyExample, "inexistent").Returns(False)
    }

    <TestCaseSource("HasFieldTestCases")>
    Public Function HasField_Test(obj As MyExample, fieldName As String) As Boolean
        Return obj.HasField(fieldName)
    End Function
    
    Dim GetPropTestCases As Object() = {
        New TestCaseData(Nothing, "MyProp").Throws(GetType(ArgumentNullException)),
        New TestCaseData(Nothing, "inexistent").Throws(GetType(ArgumentNullException)),
        New TestCaseData(New MyExample, "inexistent").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "MyPropNumber").Throws(GetType(InvalidCastException)),
        New TestCaseData(New MyExample, "MyProp").Returns("mytestp")
    }

    <TestCaseSource("GetPropTestCases")>
    Public Function GetProp_Test(obj As MyExample, fieldName As String) As String
        Return obj.GetProp(Of String)(fieldName)
    End Function
    
    Dim SetPropTestCases As Object() = {
        New TestCaseData(Nothing, "MyProp", "NEWVALUE").Throws(GetType(ArgumentNullException)),
        New TestCaseData(Nothing, "inexistent", "NEWVALUE").Throws(GetType(ArgumentNullException)),
        New TestCaseData(New MyExample, "inexistent", "NEWVALUE").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "MyProp", 345).Returns("345"),
        New TestCaseData(New MyExample, "MyProp", "NEWVALUE").Returns("NEWVALUE")
    }

    <TestCaseSource("SetPropTestCases")>
    Public Function SetProp_Test(obj As MyExample, fieldName As String, value As Object) As String
        obj.SetProp(fieldName, value)
        Return obj.MyProp
    End Function

    Dim HasPropTestCases As Object() = {
        New TestCaseData(Nothing, "MyProp").Returns(False),
        New TestCaseData(Nothing, "inexistent").Returns(False),
        New TestCaseData(New MyExample, "MyProp").Returns(True),
        New TestCaseData(New MyExample, "inexistent").Returns(False)
    }

    <TestCaseSource("HasPropTestCases")>
    Public Function HasProp_Test(obj As MyExample, fieldName As String) As Boolean
        Return obj.HasProp(fieldName)
    End Function

    <Test()>
    Public Sub TypeHasPropTest()
        Dim obj As New MyExample

        Assert.That(TypeHasProp(Of MyExample)("MyProp"))
        Assert.That(Not TypeHasProp(Of MyExample)("myother"))
    End Sub
End Class
