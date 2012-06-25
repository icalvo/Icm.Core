Imports System.Collections
Imports System.Collections.Generic
Imports System.IO
Imports Icm.IO

'''<summary>
'''This is a test class for FileSystemEnumerableTest and is intended
'''to contain all FileSystemEnumerableTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class FileSystemEnumerableTest


#Region "Additional test attributes"
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    'End Sub
    '
    'Use ClassCleanup to run code after all tests in a class have run
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    'Use TestInitialize to run code before running each test
    '<TestInitialize()>  _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    'Use TestCleanup to run code after each test has run
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region


    '''<summary>
    '''A test for GetEnumerator1
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub GetEnumeratorTest1()

        Dim target As IEnumerable(Of FileSystemInfo)
        Dim expected As New List(Of FileSystemInfo)
        Dim actual As New List(Of FileSystemInfo)
        'System.IO.Path.GetTempPath
        Dim diraiz As New DirectoryInfo(Path.Combine(System.IO.Path.GetTempPath, "test"))

        'ESTRUCTURA
        diraiz.Create()
        Dim di2 = diraiz.CreateSubdirectory("carpeta1")
        expected.Add(di2)
        Dim d3 = di2.CreateSubdirectory("Carpetita")
        expected.Add(d3)
        Dim fi = New FileInfo(Path.Combine(di2.FullName, "prueba.txt"))
        fi.CreateText.Close()
        expected.Add(fi)
        Dim fi2 = New FileInfo(Path.Combine(d3.FullName, "otraprueba.txt"))
        fi2.CreateText.Close()
        expected.Add(fi2)
        target = New FileSystemEnumerable(diraiz)

        For Each fso In target
            actual.Add(fso)
        Next
        Assert.IsTrue(actual.All(Function(actualfso) expected.Any(Function(expfso) expfso.FullName = actualfso.FullName)))
        diraiz.Delete(recursive:=True)


    End Sub


End Class
