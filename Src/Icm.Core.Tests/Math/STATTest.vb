

Imports Icm.MathTools



'''<summary>
'''This is a test class for STATTest and is intended
'''to contain all STATTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class STATTest




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
    '''A test for Uniform01
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub Uniform01Test()

        Dim actual As Double
        Dim media As Double
        Dim expected As Double = 0
        Dim elemento As New ArrayList()
        For i = 0 To 500
            actual = STAT.Uniform01
            media = media + actual
            elemento.Add(actual)
        Next
        media = media / 500
        Debug.WriteLine(media)

        Assert.IsTrue(media > 0.40000000000000002 And media < 0.59999999999999998)
    End Sub

    '''<summary>
    '''A test for Uniform
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub UniformTest()

        'Caso 1
        Dim min As Double = 0
        Dim max As Double = 0
        Dim expected As Double = 0.0
        Dim actual As Double
        actual = STAT.Uniform(min, max)
        Assert.AreEqual(expected, actual)



    End Sub


End Class
