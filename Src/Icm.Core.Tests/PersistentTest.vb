Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.Serialization
Imports Icm.Persistance
Imports System.IO



'''<summary>
'''This is a test class for PersistentTest and is intended
'''to contain all PersistentTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class PersistentTest

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
    '''A test for WriteXml
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub WriteAndReadXML()

        'WRITE XML
        Dim DirRaizXml As New DirectoryInfo(Path.Combine(System.IO.Path.GetTempPath, "XML"))

        DirRaizXml.Create()
        DirRaizXml.CreateSubdirectory("CarpetaXML")
        Using myXmlTextWriter = XmlTextWriter.Create(Path.Combine(System.IO.Path.GetTempPath, "XML\CarpetaXML\prueba.xml"))
            myXmlTextWriter.WriteStartDocument(False)
            myXmlTextWriter.WriteStartElement("PRUEBAXML")
            myXmlTextWriter.WriteStartElement("XML")
            myXmlTextWriter.WriteStartElement("TITULOXML")
            myXmlTextWriter.WriteString("Esto es una prueba de escritura en xml")
            myXmlTextWriter.WriteEndElement()
            myXmlTextWriter.WriteEndElement()
            myXmlTextWriter.WriteEndElement()
            myXmlTextWriter.Flush()
            myXmlTextWriter.Close()
        End Using

        'READ XML

        Using reader As New XmlTextReader(Path.Combine(System.IO.Path.GetTempPath, "XML\CarpetaXML\prueba.xml"))
            reader.Read()
            Assert.AreEqual("xml", reader.LocalName)
            reader.Read()
            Assert.AreEqual("PRUEBAXML", reader.LocalName)
            reader.Read()
            Assert.AreEqual("XML", reader.LocalName)
            reader.Close()
        End Using
        DirRaizXml.Delete(recursive:=True)

    End Sub

    '''<summary>
    '''A test for GetSchema
    '''</summary>
    <Test(), Category("Icm")>
    <Ignore()>
    Public Sub GetSchemaTest()
        Assert.Inconclusive()

        Dim target As IXmlSerializable = Nothing
        Dim expected As XmlSchema = Nothing
        Dim actual As XmlSchema
        actual = target.GetSchema
        Assert.AreEqual(expected, actual)
    End Sub
End Class
