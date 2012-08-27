Imports System.IO
Imports System.Text

Namespace Icm.Text

    '''<summary>
    ''' High performance string replacer.
    '''</summary>
    ''' <remarks><para>This class is a highly efficient string replacer. It replaces
    ''' tagged strings (that is, limited by configurable start and end tags)
    ''' "on the fly", that is, reading from an input stream and writing
    ''' to an output stream. Replacements can be simple strings and
    ''' also IEnumerator(Of String) instances. For example, one can create
    ''' a <see cref="AutoNumberGenerator"></see>, that produces
    ''' consecutive numbers, and replace each apparition of "__COUNTER__"
    ''' with consecutive numbers.</para>
    ''' <para>Warning: this class does NOT open the streams and only
    ''' performs forward reading and writing; it
    ''' is the responsibility of clients to pass to the constructor 
    ''' appropriately configured streams. Otherwise, I/O exceptions
    ''' will occur.</para>
    ''' </remarks>
    Public Class Replacer

        Private Enum State
            BeforeTag = 0
            TagStart = 1
            TagContent = 2
            TagEnd = 3
        End Enum

#Region " Attributes "
        Private ReadOnly replacements_ As New Dictionary(Of String, IEnumerator(Of String))()
        Private tagStart_ As String
        Private tagEnd_ As String
#End Region

        Public Sub New(ByVal tr As TextReader, ByVal tw As TextWriter, Optional ByVal tgstart As String = "__", Optional ByVal tgend As String = "__")
            If tr Is Nothing Then Throw New ArgumentNullException("tr")
            If tw Is Nothing Then Throw New ArgumentNullException("tw")
            Input = tr
            Output = tw
            TagStart = tgstart
            TagEnd = tgend
        End Sub

        Public Property TagStart() As String
            Get
                Return tagStart_
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing OrElse Value = "" Then
                    Throw New ArgumentException("Empty start tag not admitted", "Value")
                    Exit Property
                Else
                    tagStart_ = Value
                End If
            End Set
        End Property

        Public Property TagEnd() As String
            Get
                Return tagEnd_
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing OrElse Value = "" Then
                    Throw New ArgumentException("Empty start tag not admitted", "Value")
                    Exit Property
                Else
                    tagEnd_ = Value
                End If
            End Set
        End Property

        Property Input() As TextReader

        Property Output() As TextWriter

        Public Sub AddReplacement(ByVal search As String, ByVal replacement As String)
            AddReplacement(search, New PlainStringGenerator(replacement))
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="search"></param>
        ''' <param name="replacement"></param>
        ''' <remarks>
        ''' Warning: the IEnumerator is assumed to be pointing before the first element to be
        ''' used for replacement.
        ''' If you want to start with the first element of the enumeration, pass a freshly created 
        ''' IEnumerator or call IEnumerator.Reset before passing.
        ''' </remarks>
        Public Sub AddReplacement(ByVal search As String, ByVal replacement As IEnumerator(Of String))
            replacement.MoveNext()
            replacements_.Add(search, replacement)
        End Sub

        Public Sub ModifyReplacement(ByVal search As String, ByVal replacement As String)
            ModifyReplacement(search, New PlainStringGenerator(replacement))
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="search"></param>
        ''' <param name="replacement"></param>
        ''' <remarks>
        ''' Warning: the IEnumerator is assumed to be pointing before the first element to be
        ''' used for replacement.
        ''' If you want to start with the first element of the enumeration, pass a freshly created 
        ''' IEnumerator or call IEnumerator.Reset before passing.
        ''' </remarks>
        Public Sub ModifyReplacement(ByVal search As String, ByVal replacement As IEnumerator(Of String))
            replacement.MoveNext()
            replacements_(search) = replacement
        End Sub

        ''' <summary>
        ''' Performs replacement and leaves the streams opened.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ReplaceAndLeaveOpen()
            Dim tagStartP As Integer = 0
            Dim tagEndP As Integer = 0
            Dim bufferStart As New StringBuilder(tagStart_.Length - 1)
            Dim bufferEnd As New StringBuilder(tagEnd_.Length - 1)
            Dim bufferTag As New StringBuilder
            Dim lastchar As Char
            Dim st = State.BeforeTag

            Do Until Input.Peek = -1
                lastchar = ChrW(Input.Read)
                Select Case st
                    Case State.BeforeTag
                        If lastchar = tagStart_.Chars(0) Then
                            If tagStart_.Length = 1 Then
                                bufferTag.Length = 0
                                st = State.TagContent
                            Else
                                bufferStart.Length = 0
                                bufferStart.Append(lastchar)
                                tagStartP = 1
                                st = State.TagStart
                            End If
                        Else
                            Output.Write(lastchar)
                        End If
                    Case State.TagStart
                        If lastchar = tagStart_.Chars(tagStartP) Then
                            bufferStart.Append(lastchar)
                            tagStartP += 1
                            If tagStartP = tagStart_.Length Then
                                bufferTag.Length = 0
                                st = State.TagContent
                            End If
                        Else
                            st = State.BeforeTag
                            Output.Write(bufferStart.ToString)
                        End If
                    Case State.TagContent
                        If lastchar = tagEnd_.Chars(0) Then
                            If tagEnd_.Length = 1 Then
                                If replacements_.ContainsKey(bufferTag.ToString) Then
                                    replacements_(bufferTag.ToString).MoveNext()
                                    Output.Write(replacements_(bufferTag.ToString).Current)
                                Else
                                    Output.Write(tagStart_)
                                    Output.Write(bufferTag.ToString)
                                    Output.Write(tagEnd_)
                                End If
                                st = State.BeforeTag
                            Else
                                bufferEnd.Length = 0
                                bufferEnd.Append(lastchar)
                                tagEndP = 1
                                st = State.TagEnd
                            End If
                        Else
                            bufferTag.Append(lastchar)
                        End If
                    Case State.TagEnd
                        bufferEnd.Append(lastchar)
                        If lastchar = tagEnd_.Chars(tagEndP) Then
                            tagEndP += 1
                            If tagEndP = tagEnd_.Length Then
                                If replacements_.ContainsKey(bufferTag.ToString) Then
                                    replacements_(bufferTag.ToString).MoveNext()
                                    Output.Write(replacements_(bufferTag.ToString).Current)
                                Else
                                    Output.Write(tagStart_)
                                    Output.Write(bufferTag.ToString)
                                    Output.Write(tagEnd_)
                                End If
                                st = State.BeforeTag
                            End If
                        Else
                            st = State.TagContent
                            bufferTag.Append(bufferEnd.ToString)
                        End If
                End Select
            Loop
            If st = State.TagContent Then
                If replacements_.ContainsKey(bufferTag.ToString) Then
                    replacements_(bufferTag.ToString).MoveNext()
                    Output.Write(replacements_(bufferTag.ToString).Current)
                Else
                    Output.Write(tagStart_)
                    Output.Write(bufferTag.ToString)
                    ' Don't output tag end because it is not present in the input
                End If
                st = State.BeforeTag
            End If
            If st <> State.BeforeTag Then
                Throw New InvalidOperationException("Unclosed tag")
            End If
        End Sub

        ''' <summary>
        ''' Performs replacement and closes the streams.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ReplaceAndClose()
            ReplaceAndLeaveOpen()
            Close()
        End Sub

        ''' <summary>
        ''' Closes both streams.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Close()
            Input.Close()
            Output.Close()
        End Sub
    End Class
End Namespace
