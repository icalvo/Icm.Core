Imports Icm.Collections

<TestFixture(), Category("Icm")>
Public Class ArrayExtensionsTest

    Shared ReadOnly MultiGetRow_NormalStringTestCases As Object() = {
        New Object() {
            New String(,) {{"maria", "rafa"}, {"juan", "pepe"}},
            0,
            New Integer() {1},
            New String() {"rafa", "pepe"}
        }
    }

    Shared ReadOnly MultiGetRow_NormalIntegerTestCases As Object() = {
        New Object() {
            New Integer(,) {{1, 5}, {2, 7}, {3, 9}},
            1,
            New Integer() {1},
            New Integer() {2, 7}
        },
        New Object() {
            New Integer(,,) {{{11, 15, 14}, {12, 17, 16}, {13, 19, 18}},
                             {{21, 25, 24}, {22, 27, 26}, {23, 29, 28}},
                             {{31, 35, 34}, {32, 37, 36}, {33, 39, 38}}},
            0,
            New Integer() {1, 1},
            New Integer() {17, 27, 37}
        },
        New Object() {
            New Integer(,,) {{{11, 15, 14}, {12, 17, 16}, {13, 19, 18}},
                             {{21, 25, 24}, {22, 27, 26}, {23, 29, 28}},
                             {{31, 35, 34}, {32, 37, 36}, {33, 39, 38}}},
            1,
            New Integer() {1, 2},
            New Integer() {24, 26, 28}
        }
    }


    Shared ReadOnly MultiGetRow_ExceptionalStringTestCases As Object() = {
        New Object() {
            New String(,) {{"", ""}, {"", ""}},
            0,
            New Integer() {2},
            GetType(IndexOutOfRangeException)
        },
        New Object() {
            New String(,) {{"", ""}, {"", ""}},
            -1,
            New Integer() {1},
            GetType(IndexOutOfRangeException)
        },
        New Object() {
            New String(,) {{"", ""}, {"", ""}},
            0,
            New Integer() {-1},
            GetType(IndexOutOfRangeException)
        },
        New Object() {
            New String(,) {{"", ""}, {"", ""}},
            5,
            New Integer() {1},
            GetType(IndexOutOfRangeException)
        },
        New Object() {
            New String(,,) {{{"", ""}, {"", ""}}, {{"", ""}, {"", ""}}},
            0,
            New Integer() {1},
            GetType(ArgumentException)
        }
    }


    <Test()>
    <TestCaseSource("MultiGetRow_NormalStringTestCases")>
    Public Sub MultiGetRow_ReturnsExpected(target(,) As String, iteratingDimension As Integer, fixedDimensionValues As Integer(), expected As String())
        Dim actual = target.MultiGetRow(Of String)(iteratingDimension, fixedDimensionValues)
        Assert.That(actual, [Is].EquivalentTo(expected))
    End Sub

    <Test()>
    <TestCaseSource("MultiGetRow_NormalIntegerTestCases")>
    Public Sub MultiGetRow_ReturnsExpected(target As Array, iteratingDimension As Integer, fixedDimensionValues As Integer(), expected As Integer())
        Dim actual = target.MultiGetRow(Of Integer)(iteratingDimension, fixedDimensionValues)
        Assert.That(actual, [Is].EquivalentTo(expected))
    End Sub

    <Test()>
    <TestCaseSource("MultiGetRow_ExceptionalStringTestCases")>
    Public Sub MultiGetRow_ThrowsIndexOutOfRange(target As Array, iteratingDimension As Integer, fixedDimensionValues As Integer(), exceptionType As Type)
        Assert.That(Sub() target.MultiGetRow(Of String)(iteratingDimension, fixedDimensionValues), Throws.TypeOf(exceptionType))
    End Sub

End Class
