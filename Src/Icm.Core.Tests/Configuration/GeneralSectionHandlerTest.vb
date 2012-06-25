Imports System.Configuration
Imports System.Collections.Generic
Imports System.Xml
Imports Icm.Configuration



'''<summary>
'''This is a test class for GeneralSectionHandlerTest and is intended
'''to contain all GeneralSectionHandlerTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
<Ignore()>
Public Class GeneralSectionHandlerTest


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
    '''A test for ManageSection
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub ManageSectionTest()
        Assert.Inconclusive()
        Dim target As GeneralSectionHandler = New GeneralSectionHandler
        Dim section As XmlNode = Nothing
        Dim expected As Object = Nothing
        Dim actual As Object
        actual = target.ManageSection(section)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive()

    End Sub

    '''<summary>
    '''A test for Create
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub CreateTest()
        Assert.Inconclusive()
        Dim target As IConfigurationSectionHandler = New GeneralSectionHandler ' TODO: Initialize to an appropriate value
        Dim parent As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim configContext As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim section As XmlNode = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Object
        actual = target.Create(parent, configContext, section)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive()


    End Sub

    '''<summary>
    '''A test for BuildHash
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub BuildHashTest()
        Assert.Inconclusive()
        Dim target As GeneralSectionHandler = New GeneralSectionHandler ' TODO: Initialize to an appropriate value
        Dim section As XmlNode = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Dictionary(Of String, Object) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Dictionary(Of String, Object)
        actual = target.BuildHash(section)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive()


    End Sub

    '''<summary>
    '''A test for BuildArray
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub BuildArrayTest()
        Assert.Inconclusive()
        Dim target As GeneralSectionHandler = New GeneralSectionHandler ' TODO: Initialize to an appropriate value
        Dim section As XmlNode = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As List(Of Object) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of Object)
        actual = target.BuildArray(section)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive()


    End Sub

End Class
