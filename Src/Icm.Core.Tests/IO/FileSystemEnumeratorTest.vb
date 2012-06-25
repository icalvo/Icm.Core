Imports System.IO
Imports Icm.IO

'''<summary>
'''This is a test class for FileSystemEnumeratorTest and is intended
'''to contain all FileSystemEnumeratorTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class FileSystemEnumeratorTest

    Private tempDir_ As DirectoryInfo

    Public Shared Function GetTempDirectory() As DirectoryInfo
        Return New DirectoryInfo(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()))
    End Function

    <TestFixtureSetUp()>
    Public Sub SetUp()
        tempDir_ = GetTempDirectory()
        tempDir_.Create()
        tempDir_.GetFile("A.tmp").Create.Close()
    End Sub

    <TestFixtureTearDown()>
    Public Sub TearDown()
        tempDir_.Delete(recursive:=True)
    End Sub


    '''<summary>
    '''A test for Current
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub CurrentTest()
        Using target As New FileSystemEnumerator(tempDir_)
            Dim actual As FileSystemInfo
            target.MoveNext()
            actual = target.Current
            Assert.AreEqual(actual.Name, "A.tmp")
        End Using
    End Sub


End Class
