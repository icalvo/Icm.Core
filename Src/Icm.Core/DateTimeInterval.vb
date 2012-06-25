Namespace Icm

    ''' <summary>
    ''' Defines a non-negative interval between two given dates.
    ''' </summary>
    ''' <remarks>
    ''' Every modification that produces a negative interval will throw an exception.
    ''' </remarks>
    <DebuggerDisplay("{Description()}")> _
    Public Class DateTimeInterval

        Private start_ As Date
        Private end_ As Date

        ReadOnly Property Start() As Date
            Get
                Return start_
            End Get
        End Property


        Property [End]() As Date
            Get
                Return end_
            End Get
            Set(ByVal value As Date)
                SetInterval(start_, value)
            End Set
        End Property
        ReadOnly Property Inicio() As Date
            Get
                Return start_
            End Get
        End Property


        Property Fin() As Date
            Get
                Return end_
            End Get
            Set(ByVal value As Date)
                SetInterval(start_, value)
            End Set
        End Property
        Public Sub New(ByVal i As Date, ByVal f As Date)
            SetInterval(i, f)
        End Sub

        Public Sub New(ByVal i As Date, ByVal dur As TimeSpan)
            SetInterval(i, dur)
        End Sub

        ReadOnly Property Duration() As TimeSpan
            Get
                Return [End].Subtract(Start)
            End Get
        End Property

        Public Sub StartWith(ByVal newStart As Date)
            Offset(newStart.Subtract(Start))
        End Sub

        Public Sub Offset(ByVal offsetSpan As TimeSpan)
            Dim newEnd As Date
            If Date.MaxValue.Subtract(end_) > offsetSpan Then
                newEnd = end_.Add(offsetSpan)
            Else
                newEnd = Date.MaxValue
            End If
            SetInterval(start_.Add(offsetSpan), newEnd)
        End Sub

        Public Sub SetDuration(ByVal newDuration As TimeSpan)
            Debug.Assert(newDuration >= TimeSpan.Zero)
            SetInterval(start_, start_.Add(newDuration))
        End Sub

        ''' <summary>
        ''' This is the only method that actually modifies the values of the interval.
        ''' Every other modification method use it, directly or indirectly.
        ''' </summary>
        ''' <param name="startDate"></param>
        ''' <param name="endDate"></param>
        ''' <remarks></remarks>
        Public Sub SetInterval(ByVal startDate As Date, ByVal endDate As Date)
            Debug.Assert(startDate <= endDate)
            Debug.Assert(startDate < Date.MaxValue)
            start_ = startDate
            end_ = endDate
        End Sub

        Public Sub SetInterval(ByVal startDate As Date, ByVal dur As TimeSpan)
            Debug.Assert(dur >= TimeSpan.Zero)
            SetInterval(startDate, startDate.Add(dur))
        End Sub

        Public Sub SetPoint(ByVal pointDate As Date)
            SetInterval(pointDate, pointDate)
        End Sub

        Public Overridable Function Description() As String
            If [End] = Date.MaxValue Then
                Return String.Format("from {0:dd/MM/yyyy HH:mm:ss}, indefinitely", Start)
            ElseIf [End] = Date.MinValue Then
                Return String.Format("{0:dd/MM/yyyy HH:mm:ss}", Start)
            Else
                Return String.Format("between {0:dd/MM/yyyy HH:mm:ss} and {1:dd/MM/yyyy HH:mm:ss} ({2})", Start, [End], Duration.ToAbbrev)
            End If
        End Function

        Public Function Contains(ByVal aDate As Date) As Boolean
            Return Start <= aDate AndAlso aDate < [End]
        End Function

        ' -------------

        ReadOnly Property Duración() As TimeSpan
            Get
                Return [End].Subtract(Start)
            End Get
        End Property

        Public Sub EmpezarEn(ByVal newStart As Date)
            StartWith(newStart)
        End Sub

        Public Sub Establecer(ByVal newStart As Date)
            StartWith(newStart)
        End Sub
        Public Sub Desplazar(ByVal offsetSpan As TimeSpan)
            Offset(offsetSpan)
        End Sub

        Public Sub CambiarTamaño(ByVal newDuration As TimeSpan)
            SetDuration(newDuration)
        End Sub

        ''' <summary>
        ''' This is the only method that actually modifies the values of the interval.
        ''' Every other modification method use it, directly or indirectly.
        ''' </summary>
        ''' <param name="startDate"></param>
        ''' <param name="endDate"></param>
        ''' <remarks></remarks>
        Public Sub Establecer(ByVal startDate As Date, ByVal endDate As Date)
            Debug.Assert(startDate <= endDate)
            Debug.Assert(startDate < Date.MaxValue)
            start_ = startDate
            end_ = endDate
        End Sub

        Public Sub Establecer(ByVal startDate As Date, ByVal dur As TimeSpan)
            Debug.Assert(dur >= TimeSpan.Zero)
            SetInterval(startDate, startDate.Add(dur))
        End Sub

        Public Overridable Function Descripción() As String
            Return Description()
        End Function

        Public Function Contiene(ByVal aDate As Date) As Boolean
            Return Contains(aDate)
        End Function

    End Class
End Namespace