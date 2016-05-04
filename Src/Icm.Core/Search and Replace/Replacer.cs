using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Icm.Text
{

	///<summary>
	/// High performance string replacer.
	///</summary>
	/// <remarks><para>This class is a highly efficient string replacer. It replaces
	/// tagged strings (that is, limited by configurable start and end tags)
	/// "on the fly", that is, reading from an input stream and writing
	/// to an output stream. Replacements can be simple strings and
	/// also IEnumerator(Of String) instances. For example, one can create
	/// a <see cref="AutoNumberGenerator"></see>, that produces
	/// consecutive numbers, and replace each apparition of "__COUNTER__"
	/// with consecutive numbers.</para>
	/// <para>Warning: this class does NOT open the streams and only
	/// performs forward reading and writing; it
	/// is the responsibility of clients to pass to the constructor 
	/// appropriately configured streams. Otherwise, I/O exceptions
	/// will occur.</para>
	/// </remarks>
	public class Replacer
	{

		private enum State
		{
			BeforeTag = 0,
			TagStart = 1,
			TagContent = 2,
			TagEnd = 3
		}

	    private readonly Dictionary<string, IEnumerator<string>> _replacements = new Dictionary<string, IEnumerator<string>>();
		private string _tagStart;

	    private string _tagEnd;

		public Replacer(TextReader tr, TextWriter tw, string tgstart = "__", string tgend = "__")
		{
			if (tr == null)
				throw new ArgumentNullException(nameof(tr));
			if (tw == null)
				throw new ArgumentNullException(nameof(tw));
			Input = tr;
			Output = tw;
			TagStart = tgstart;
			TagEnd = tgend;
		}

		public string TagStart {
			get { return _tagStart; }
			set
			{
			    if (value == null || string.IsNullOrEmpty(value)) {
					throw new ArgumentException("Empty start tag not admitted", nameof(value));
				}

			    _tagStart = value;
			}
		}

		public string TagEnd {
			get { return _tagEnd; }
			set
			{
			    if (value == null || string.IsNullOrEmpty(value)) {
					throw new ArgumentException("Empty start tag not admitted", nameof(value));
				}

			    _tagEnd = value;
			}
		}

		public TextReader Input { get; set; }

		public TextWriter Output { get; set; }

		public void AddReplacement(string search, string replacement)
		{
			AddReplacement(search, new PlainStringGenerator(replacement));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="search"></param>
		/// <param name="replacement"></param>
		/// <remarks>
		/// Warning: the IEnumerator is assumed to be pointing before the first element to be
		/// used for replacement.
		/// If you want to start with the first element of the enumeration, pass a freshly created 
		/// IEnumerator or call IEnumerator.Reset before passing.
		/// </remarks>
		public void AddReplacement(string search, IEnumerator<string> replacement)
		{
			replacement.MoveNext();
			_replacements.Add(search, replacement);
		}

		public void ModifyReplacement(string search, string replacement)
		{
			ModifyReplacement(search, new PlainStringGenerator(replacement));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="search"></param>
		/// <param name="replacement"></param>
		/// <remarks>
		/// Warning: the IEnumerator is assumed to be pointing before the first element to be
		/// used for replacement.
		/// If you want to start with the first element of the enumeration, pass a freshly created 
		/// IEnumerator or call IEnumerator.Reset before passing.
		/// </remarks>
		public void ModifyReplacement(string search, IEnumerator<string> replacement)
		{
			replacement.MoveNext();
			_replacements[search] = replacement;
		}

		/// <summary>
		/// Performs replacement and leaves the streams opened.
		/// </summary>
		/// <remarks></remarks>
		public void ReplaceAndLeaveOpen()
		{
			int tagStartP = 0;
			int tagEndP = 0;
			StringBuilder bufferStart = new StringBuilder(_tagStart.Length - 1);
			StringBuilder bufferEnd = new StringBuilder(_tagEnd.Length - 1);
			StringBuilder bufferTag = new StringBuilder();
		    var st = State.BeforeTag;

			while (Input.Peek() != -1)
			{
			    var lastchar = (char)Input.Read();
			    switch (st) {
					case State.BeforeTag:
						if (lastchar == _tagStart[0]) {
							if (_tagStart.Length == 1) {
								bufferTag.Length = 0;
								st = State.TagContent;
							} else {
								bufferStart.Length = 0;
								bufferStart.Append(lastchar);
								tagStartP = 1;
								st = State.TagStart;
							}
						} else {
							Output.Write(lastchar);
						}
						break;
					case State.TagStart:
						if (lastchar == _tagStart[tagStartP]) {
							bufferStart.Append(lastchar);
							tagStartP += 1;
							if (tagStartP == _tagStart.Length) {
								bufferTag.Length = 0;
								st = State.TagContent;
							}
						} else {
							st = State.BeforeTag;
							Output.Write(bufferStart.ToString());
						}
						break;
					case State.TagContent:
						if (lastchar == _tagEnd[0]) {
							if (_tagEnd.Length == 1) {
								if (_replacements.ContainsKey(bufferTag.ToString())) {
									_replacements[bufferTag.ToString()].MoveNext();
									Output.Write(_replacements[bufferTag.ToString()].Current);
								} else {
									Output.Write(_tagStart);
									Output.Write(bufferTag.ToString());
									Output.Write(_tagEnd);
								}
								st = State.BeforeTag;
							} else {
								bufferEnd.Length = 0;
								bufferEnd.Append(lastchar);
								tagEndP = 1;
								st = State.TagEnd;
							}
						} else {
							bufferTag.Append(lastchar);
						}
						break;
					case State.TagEnd:
						bufferEnd.Append(lastchar);
						if (lastchar == _tagEnd[tagEndP]) {
							tagEndP += 1;
							if (tagEndP == _tagEnd.Length) {
								if (_replacements.ContainsKey(bufferTag.ToString())) {
									_replacements[bufferTag.ToString()].MoveNext();
									Output.Write(_replacements[bufferTag.ToString()].Current);
								} else {
									Output.Write(_tagStart);
									Output.Write(bufferTag.ToString());
									Output.Write(_tagEnd);
								}
								st = State.BeforeTag;
							}
						} else {
							st = State.TagContent;
							bufferTag.Append(bufferEnd);
						}
						break;
				}
			}
		    if (st == State.TagContent) {
				if (_replacements.ContainsKey(bufferTag.ToString())) {
					_replacements[bufferTag.ToString()].MoveNext();
					Output.Write(_replacements[bufferTag.ToString()].Current);
				} else {
					Output.Write(_tagStart);
					Output.Write(bufferTag.ToString());
					// Don't output tag end because it is not present in the input
				}
				st = State.BeforeTag;
			}
			if (st != State.BeforeTag) {
				throw new InvalidOperationException("Unclosed tag");
			}
		}

		/// <summary>
		/// Performs replacement and closes the streams.
		/// </summary>
		/// <remarks></remarks>
		public void ReplaceAndClose()
		{
			ReplaceAndLeaveOpen();
			Close();
		}

		/// <summary>
		/// Closes both streams.
		/// </summary>
		/// <remarks></remarks>
		public void Close()
		{
			Input.Close();
			Output.Close();
		}
	}
}