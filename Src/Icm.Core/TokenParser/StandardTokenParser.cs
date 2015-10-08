using System;
using System.Collections.Generic;

namespace Icm
{

	/// <summary>
	/// Standard parser
	/// </summary>
	/// <remarks>
	/// The standard token parser separates tokens by spaces, but recognizes strings inside
	/// double quotes (") as single tokens and can also escape characters inside and outside quotes
	/// using the backslash (\).
	/// </remarks>
	public class StandardTokenParser : ITokenParser
	{

		private readonly List<ParseError> _errors = new List<ParseError>();

		private readonly List<string> _tokens = new List<string>();
		private enum ParserState
		{
			OutsideQuotesNotEscaping,
			OutsideQuotesEscaping,
			InsideQuotesNotEscaping,
			InsideQuotesEscaping
		}

		public void Parse(string line)
		{
			System.Text.StringBuilder currentToken = new System.Text.StringBuilder();
			int index = 0;
			ParserState state = ParserState.OutsideQuotesNotEscaping;
		    int errorStartIndex = 0;
			if (line == null) {
				return;
			}
			foreach (char character in line) {
				ParserState oldState = state;
				switch (state) {
					case ParserState.OutsideQuotesNotEscaping:
						// Outside quotes and not escaping
						if (char.IsWhiteSpace(character)) {
							if (currentToken.Length > 0) {
								_tokens.Add(currentToken.ToString());
								currentToken.Clear();
							}
						} else if (character == '\\') {
							state = ParserState.OutsideQuotesEscaping;
						} else if (character == '"') {
							state = ParserState.InsideQuotesNotEscaping;
						} else {
							currentToken.Append(character);
						}
						break;
					case ParserState.OutsideQuotesEscaping:
						// Outside quotes and escaping
						currentToken.Append(character);
						state = ParserState.OutsideQuotesNotEscaping;
						break;
					case ParserState.InsideQuotesNotEscaping:
						// Inside quotes and not escaping
                        if (character == '\\')
                        {
							state = ParserState.InsideQuotesEscaping;
						} else if (character == '"') {
							_tokens.Add(currentToken.ToString());
							currentToken.Clear();
							state = ParserState.OutsideQuotesNotEscaping;
						} else {
							currentToken.Append(character);
						}
						break;
					case ParserState.InsideQuotesEscaping:
						// Inside quotes and escaping
						currentToken.Append(character);
						state = ParserState.InsideQuotesNotEscaping;
						break;
					default:
						throw new Exception("Bad state! Cannot happen!");
				}
				if (oldState == ParserState.OutsideQuotesNotEscaping && state != ParserState.OutsideQuotesNotEscaping) {
					// transitions between initial state and any other mark a possible
					// cause for an error
					errorStartIndex = index;
				}

				index += 1;
			}
			if (currentToken.Length > 0) {
				_tokens.Add(currentToken.ToString());
			}
			if (state != ParserState.OutsideQuotesNotEscaping) {
				_errors.Add(new ParseError(1, index, errorStartIndex));
			}
		}


		public IEnumerable<ParseError> Errors {
			get { return _errors; }
		}

		public IEnumerable<string> Tokens {
			get { return _tokens; }
		}

		public void Initialize()
		{
			_tokens.Clear();
			_errors.Clear();
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
