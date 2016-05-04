using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Icm.IO
{

	/// <summary>
	/// TextWriter composed by other TextWriters. Every write
	/// is transferred to all the TextWriters.
	/// </summary>
	/// <remarks>
	/// </remarks>
	/// <history>
	/// 	[icalvo]	03/12/2005	Created
	/// </history>
	public class CompositeWriter : TextWriter
	{


		private readonly List<TextWriter> _textWriters = new List<TextWriter>();
		protected IList<TextWriter> TextWriters => _textWriters;

	    public CompositeWriter(params TextWriter[] tws) : base(CultureInfo.CurrentCulture)
		{
			Add(tws);
		}

		public void Add(params TextWriter[] tws)
		{
			foreach (var tw in tws) {
				if (tw == null) {
					throw new ArgumentNullException(nameof(tws));
				}

				TextWriters.Add(tw);
			}
		}

		public override void Write(char value)
		{
			foreach (TextWriter tw in TextWriters) {
				tw.Write(value);
			}
		}

		public override void Write(string value)
		{
			foreach (TextWriter tw in TextWriters) {
				tw.Write(value);
			}
		}

		public override void Flush()
		{
			foreach (TextWriter tw in TextWriters) {
				tw.Flush();
			}
		}

		public override void Close()
		{
			foreach (TextWriter tw in TextWriters) {
				tw.Close();
			}
		}

		public override void WriteLine(string value)
		{
			foreach (TextWriter tw in TextWriters) {
				tw.WriteLine(value);
			}
		}

		public override System.Text.Encoding Encoding {
			get {
				throw new InvalidOperationException("CompositeWriter does not have a single encoding because it is composed of several child TextWriters.");
			}
		}
	}

}