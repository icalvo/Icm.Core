Imports Icm.Localization

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

        Public Sub New(ByVal i As Date, ByVal f As Date)
            SetInterval(i, f)
        End Sub

        Public Sub New(ByVal i As Date, ByVal dur As TimeSpan)
            SetInterval(i, dur)
        End Sub

        Public Sub New(ByVal point As Date)
            SetInterval(point, point)
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
            If newDuration < TimeSpan.Zero Then Throw New ArgumentOutOfRangeException("Duration must not be negative")

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
            If startDate > endDate Then Throw New ArgumentException("Start date must be less or equal than end date")
            If startDate = Date.MaxValue Then Throw New ArgumentOutOfRangeException("Start date must not be Date.MaxValue")

            start_ = startDate
            end_ = endDate
        End Sub

        Public Sub SetInterval(ByVal startDate As Date, ByVal dur As TimeSpan)
            If dur < TimeSpan.Zero Then Throw New ArgumentOutOfRangeException("Duration must not be negative")
            SetInterval(startDate, startDate.Add(dur))
        End Sub

        Public Sub SetPoint(ByVal pointDate As Date)
            SetInterval(pointDate, pointDate)
        End Sub

        Public Overridable Function Description() As Phrase
            If [End] = Date.MaxValue Then
                Return PhrF("from {0:dd/MM/yyyy HH:mm:ss}, indefinitely", Start)
            ElseIf [End] = Date.MinValue Then
                Return PhrF("{0:dd/MM/yyyy HH:mm:ss}", Start)
            Else
                Return PhrF("between {0:dd/MM/yyyy HH:mm:ss} and {1:dd/MM/yyyy HH:mm:ss} ({2})", Start, [End], Duration.ToAbbrev)
            End If
        End Function

        Public Function Contains(ByVal aDate As Date) As Boolean
            Return Start <= aDate AndAlso aDate < [End]
        End Function

    End Class
End Namespace