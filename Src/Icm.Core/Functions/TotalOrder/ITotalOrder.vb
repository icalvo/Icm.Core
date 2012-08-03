Namespace Icm

    ''' <summary>
    ''' IComparable types establish a total order relationship by defining a CompareTo function
    ''' defined for every pair of elements.
    ''' Total order relationships provide a Least element, a Greatest element and also the
    ''' possibility of establishing a bijection between the type and the real straight line, in
    ''' such a way that the order is preserved.
    ''' Functions Greatest, Least, T2Long and Long2T implement those operations.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks>
    ''' Invariant:
    ''' 
    ''' For each t IN T / t LE Greatest()
    ''' For each t IN T / Least() LE t
    ''' For each t1,t2 IN T / t1 LE t2 EQV T2Double(t1) LE T2Double(t2)
    ''' For each t IN T / Double2T(T2Double(t)) = t
    ''' </remarks>
    Public Interface ITotalOrder(Of T As IComparable(Of T))
        Inherits IComparer(Of T)

        Function Greatest() As T
        Function Least() As T
        Function T2Long(ByVal t As T) As Long
        Function Long2T(ByVal d As Long) As T
        Function [Next](ByVal t As T) As T
        Function Previous(ByVal t As T) As T
    End Interface

End Namespace
