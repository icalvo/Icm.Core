Imports Icm.Reflection
Imports System.Reflection

<TestFixture()>
Public Class ObjectReflectionExtensionsTest

    Dim GetFieldTestCases As Object() = {
        New TestCaseData(Nothing, "myfield").Throws(GetType(ArgumentNullException)),
        New TestCaseData(Nothing, "inexistent").Throws(GetType(ArgumentNullException)),
        New TestCaseData(New MyExample, "inexistent").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "mynumber").Throws(GetType(InvalidCastException)),
        New TestCaseData(New MyExample, "myfield").Returns("mytest"),
        New TestCaseData(New MyExample, "MyProp").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "DictPropStr[key1]").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "DictPropStr[key2]").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "DictPropStr[key3]").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "DictPropInt[1234]").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "DictPropInt[4567]").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "DictPropInt[4568]").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "DictPropInt[qwer]").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "DictPropDate[2013-04-07]").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "IndexedProp[7]").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "IndexedProp2[19, asdf]").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "ArrayProp[0]").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "ArrayProp[1]").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "ArrayProp[2]").Throws(GetType(ArgumentException))
    }

    <TestCaseSource("GetFieldTestCases")>
    Public Function GetField_Test(obj As MyExample, fieldName As String) As String
        Return obj.GetField(Of String)(fieldName)
    End Function

    Dim SetFieldTestCases As Object() = {
        New TestCaseData(Nothing, "myfield", "NEWVALUE").Throws(GetType(ArgumentNullException)),
        New TestCaseData(Nothing, "inexistent", "NEWVALUE").Throws(GetType(ArgumentNullException)),
        New TestCaseData(New MyExample, "inexistent", "NEWVALUE").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "myfield", 345).Returns("345"),
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
        New TestCaseData(New MyExample, "myfield").Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "MyProp").Returns("mytestp"),
        New TestCaseData(New MyExample, "DictPropStr[key1]").Returns("value1"),
        New TestCaseData(New MyExample, "DictPropStr[key2]").Returns("value2"),
        New TestCaseData(New MyExample, "DictPropStr[key3]").Throws(GetType(KeyNotFoundException)),
        New TestCaseData(New MyExample, "DictPropInt[1234]").Returns("value1i"),
        New TestCaseData(New MyExample, "DictPropInt[4567]").Returns("value2i"),
        New TestCaseData(New MyExample, "DictPropInt[4568]").Throws(GetType(KeyNotFoundException)),
        New TestCaseData(New MyExample, "DictPropInt[qwer]").Throws(GetType(FormatException)),
        New TestCaseData(New MyExample, "DictPropDate[2013-04-07]").Returns("value2d"),
        New TestCaseData(New MyExample, "IndexedProp[7]").Returns("<<7>>"),
        New TestCaseData(New MyExample, "IndexedProp2[19, asdf]").Returns("<<19;asdf>>"),
        New TestCaseData(New MyExample, "ArrayProp[0]").Returns("a"),
        New TestCaseData(New MyExample, "ArrayProp[1]").Returns("b"),
        New TestCaseData(New MyExample, "ArrayProp[2]").Throws(GetType(IndexOutOfRangeException))
    }

    <TestCaseSource("GetPropTestCases")>
    Public Function GetProp_Test(obj As MyExample, fieldName As String) As String
        Return obj.GetProp(Of String)(fieldName)
    End Function


    Dim GetMemberTestCases As Object() = {
        New TestCaseData(Nothing, "myfield", Nothing).Throws(GetType(ArgumentNullException)),
        New TestCaseData(Nothing, "inexistent", Nothing).Throws(GetType(ArgumentNullException)),
        New TestCaseData(New MyExample, "inexistent", Nothing).Throws(GetType(ArgumentException)),
        New TestCaseData(New MyExample, "MyPropNumber", Nothing).Throws(GetType(InvalidCastException)),
        New TestCaseData(New MyExample, "MyProp", Nothing).Returns("mytestp"),
        New TestCaseData(New MyExample, "mynumber", Nothing).Throws(GetType(InvalidCastException)),
        New TestCaseData(New MyExample, "myfield", Nothing).Returns("mytest"),
        New TestCaseData(New MyExample, "myfield.Substring", New Object() {1, 3}).Returns("yte"),
        New TestCaseData(New MyExample, "myfield.Length.ToString", Nothing).Returns("6"),
        New TestCaseData(New MyExample, "myfield.Length.ToString", {"00"}).Returns("06"),
        New TestCaseData(New MyExample, "DictPropStr[key1]", Nothing).Returns("value1"),
        New TestCaseData(New MyExample, "DictPropStr[key2]", Nothing).Returns("value2"),
        New TestCaseData(New MyExample, "DictPropStr[key3]", Nothing).Throws(GetType(KeyNotFoundException)),
        New TestCaseData(New MyExample, "DictPropInt[1234]", Nothing).Returns("value1i"),
        New TestCaseData(New MyExample, "DictPropInt[4567]", Nothing).Returns("value2i"),
        New TestCaseData(New MyExample, "DictPropInt[4568]", Nothing).Throws(GetType(KeyNotFoundException)),
        New TestCaseData(New MyExample, "DictPropInt[qwer]", Nothing).Throws(GetType(FormatException)),
        New TestCaseData(New MyExample, "DictPropDate[2013-04-07]", Nothing).Returns("value2d"),
        New TestCaseData(New MyExample, "IndexedProp[7]", Nothing).Returns("<<7>>"),
        New TestCaseData(New MyExample, "IndexedProp2[19, asdf]", Nothing).Returns("<<19;asdf>>"),
        New TestCaseData(New MyExample, "ArrayProp[0]", Nothing).Returns("a"),
        New TestCaseData(New MyExample, "ArrayProp[1]", Nothing).Returns("b"),
        New TestCaseData(New MyExample, "ArrayProp[2]", Nothing).Throws(GetType(IndexOutOfRangeException))
    }

    <TestCaseSource("GetMemberTestCases")>
    Public Function GetMember_Test(obj As MyExample, fieldName As String, ParamArray args() As Object) As String
        Return obj.GetMember(Of String)(fieldName, args)
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
