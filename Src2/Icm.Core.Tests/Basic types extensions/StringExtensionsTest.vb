Imports Icm

<TestFixture(), Category("Icm")>
Public Class StringExtensionsTest

    <TestCase("bLioNNpPA", Result:="BLioNNpPA")>
    <TestCase("BaMBu", Result:="BaMBu")>
    <TestCase("áNGeL", Result:="ÁNGeL")>
    <TestCase("78a", Result:="78a")>
    <TestCase("a1", Result:="A1")>
    <TestCase("", Result:="")>
    <TestCase(Nothing, Result:="")>
    Public Function ToUpperFirst_Test(s As String) As String
        Return s.ToUpperFirst()
    End Function

    <TestCase("aa", 3, Result:="aaaaaa")>
    <TestCase("", 3, Result:="")>
    <TestCase("aa", 0, Result:="")>
    <TestCase("aa", -5, ExpectedException:=GetType(ArgumentOutOfRangeException))>
    <TestCase(Nothing, 3, ExpectedException:=GetType(NullReferenceException))>
    Public Function Repeat_Test(s As String, count As Integer) As String
        Return s.Repeat(count)
    End Function


    <TestCase("Maria", 3, Result:="Mar")>
    <TestCase("M12a", 3, Result:="M12")>
    <TestCase("Maria", 0, Result:="")>
    <TestCase("Maria", 12, ExpectedException:=GetType(ArgumentOutOfRangeException))>
    <TestCase("Maria", -12, ExpectedException:=GetType(ArgumentOutOfRangeException))>
    Public Function Left_Test(target As String, length As Integer) As String
        Return target.Left(length)
    End Function

    <TestCase("ViernesLunes", 2, 8, Result:="ernesLu")>
    <TestCase("ViernesLunes", 0, 0, Result:="V")>
    <TestCase("ViernesLunes", 5, 4, Result:="")>
    <TestCase("ViernesLunes", 0, -1, Result:="")>
    <TestCase("ViernesLunes", 5, 3, ExpectedException:=GetType(ArgumentOutOfRangeException))>
    <TestCase("ViernesLunes", -4, 6, ExpectedException:=GetType(ArgumentOutOfRangeException))>
    <TestCase("ViernesLunes", 4, -6, ExpectedException:=GetType(ArgumentOutOfRangeException))>
    <TestCase("ViernesLunes", 20, 21, ExpectedException:=GetType(ArgumentOutOfRangeException))>
    Public Function Med_Test(target As String, startIdx As Integer, endIdx As Integer) As String
        Return target.Med(startIdx, endIdx)
    End Function

    <TestCase("LunesMartes", 1, 3, Result:="unesMar")>
    <TestCase("LunesMartes", 0, 0, Result:="LunesMartes")>
    <TestCase("LunesMartes", 5, 6, Result:="")>
    <TestCase("LunesMartes", 5, 7, ExpectedException:=GetType(ArgumentOutOfRangeException), Description:="Overlapped lengths")>
    <TestCase("LunesMartes", -1, 3, ExpectedException:=GetType(ArgumentOutOfRangeException))>
    <TestCase("LunesMartes", 1, -3, ExpectedException:=GetType(ArgumentOutOfRangeException))>
    <TestCase("LunesMartes", 20, 3, ExpectedException:=GetType(ArgumentOutOfRangeException))>
    <TestCase("LunesMartes", 2, 23, ExpectedException:=GetType(ArgumentOutOfRangeException))>
    Public Function SkipBoth_Test(target As String, startLength As Integer, endLength As Integer) As String
        Return target.SkipBoth(startLength, endLength)
    End Function

    <TestCase("Maria", 3, Result:="ria")>
    <TestCase("M12a", 3, Result:="12a")>
    <TestCase("Maria", 0, Result:="")>
    <TestCase("Maria", 12, ExpectedException:=GetType(ArgumentOutOfRangeException))>
    <TestCase("Maria", -12, ExpectedException:=GetType(ArgumentOutOfRangeException))>
    Public Function Right_Test(target As String, length As Integer) As String
        Return target.Right(length)
    End Function

    '''<summary>
    '''A test for SurroundedBy
    '''</summary>
    <TestCase("LaCasaBonita", "La", "Bonita", Result:=True)>
    <TestCase("LaCasaBonita", "Li", "Bonita", Result:=False)>
    <TestCase("LaCasaBonita", "La", "Fea", Result:=False)>
    Public Function SurroundedBy_Test(target As String, startString As String, endString As String) As Boolean
        Return target.SurroundedBy(startString, endString)
    End Function

    <TestCase("qwer", "subst", Result:="qwer")>
    <TestCase("", "subst", Result:="subst")>
    <TestCase(Nothing, "subst", Result:=Nothing)>
    Public Function IfEmpty_Test(target As String, ifEmptyString As String) As String
        Return target.IfEmpty(ifEmptyString)
    End Function

    <TestCase("qwer", Result:=False)>
    <TestCase("", Result:=True)>
    <TestCase(Nothing, Result:=False)>
    Public Function IsEmpty_Test(target As String) As Boolean
        Return target.IsEmpty()
    End Function
End Class
