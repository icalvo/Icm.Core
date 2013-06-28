Imports System.Runtime.CompilerServices

Namespace Icm.Functions

    Public Module ThresholdTypeExtensions

        ''' <summary>
        '''  Opposed threshold type
        ''' </summary>
        ''' <param name="tu"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function Opposed(ByVal tu As ThresholdType) As ThresholdType
            Select Case tu
                Case ThresholdType.LeftOpen
                    Return ThresholdType.RightClosed
                Case ThresholdType.LeftClosed
                    Return ThresholdType.RightOpen
                Case ThresholdType.RightClosed
                    Return ThresholdType.LeftOpen
                Case ThresholdType.RightOpen
                    Return ThresholdType.LeftClosed
                Case Else
                    Throw New ArgumentException
            End Select
        End Function

        ''' <summary>
        ''' Converts an operator string into a threshold type.
        ''' </summary>
        ''' <param name="op"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' <para>The equivalent operator for a threshold type is OP, where:</para>
        ''' <code>X OP THRESHOLD</code>
        ''' </remarks>
        Public Function Operator2ThresholdType(ByVal op As String) As ThresholdType
            Select Case op
                Case "<"
                    Return ThresholdType.LeftClosed
                Case "<="
                    Return ThresholdType.LeftOpen
                Case ">"
                    Return ThresholdType.RightClosed
                Case ">="
                    Return ThresholdType.RightOpen
                Case Else
                    Throw New ArgumentException(String.Format("Unknown operator: {0}", op), "op")
            End Select
        End Function

        ''' <summary>
        ''' Converts a threshold type into its equivalent operator string.
        ''' </summary>
        ''' <param name="tu"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' <para>The equivalent operator for a threshold type is OP, where:</para>
        ''' <code>X OP THRESHOLD</code>
        ''' </remarks>
        <Extension()>
        Public Function GetOperator(ByVal tu As ThresholdType) As String
            Select Case tu
                Case ThresholdType.LeftClosed
                    Return "<"
                Case ThresholdType.LeftOpen
                    Return "<="
                Case ThresholdType.RightClosed
                    Return ">"
                Case ThresholdType.RightOpen
                    Return ">="
                Case Else
                    Throw New InvalidOperationException(String.Format("Unknown threshold type: {0}", CInt(tu)))
            End Select
        End Function

    End Module

End Namespace
