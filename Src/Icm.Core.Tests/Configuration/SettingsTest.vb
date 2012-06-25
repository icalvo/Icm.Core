

Imports Icm.Configuration
Imports System.Configuration


'''<summary>
'''This is a test class for SettingsTest and is intended
'''to contain all SettingsTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class SettingsTest


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
    '''A test for GetCfg
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub GetCfgTest()
        Dim key As String = "system.data"
        Dim actual As Object
        actual = Settings.GetCfg(key)
        Assert.IsInstanceOf(Of System.Data.DataSet)(actual)
    End Sub
End Class
