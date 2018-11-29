using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Triangle.Compiler.SyntacticAnalyzer {
	/// <summary>
	/// Scanner for the triangle language
	/// </summary>
	public class Scanner : IEnumerable<Token> {
		/// <summary>
		/// The file being read from
		/// </summary>
		private SourceFile source;
		/// <summary>
		/// The characters currently in the token being constructed
		/// </summary>
		private StringBuilder currentSpelling;
		/// <summary>
		/// Whether the reader has reached the end of the source file
		/// </summary>
		private bool atEndOfFile = false;
		/// <summary>
		/// Lookup table of reserved words used to screen tokens
		/// </summary>
		private static ImmutableDictionary<string, TokenKind> ReservedWords { get; } =
			Enumerable.Range((int)TokenKind.Begin, (int)TokenKind.While)
			.Cast<TokenKind>()
			.ToImmutableDictionary(kind => kind.ToString().ToLower(), kind => kind);
		/// <summary>
		/// Creates a new scanner
		/// </summary>
		/// <param name="source">The file to read the characters from</param>
		public Scanner(SourceFile _source) {
			source = _source;
			source.Reset();
			currentSpelling = new StringBuilder();
		}
		/// <summary>
		/// Returns the tokens in the source file
		/// </summary>
		/// <returns>The sequence of tokens that are found in the source code</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
		/// <summary>
		/// Returns the tokens in the source file
		/// </summary>
		/// <returns>The sequence of tokens that are found in the source code</returns>
		public IEnumerator<Token> GetEnumerator() {
			while(!atEndOfFile) {
				currentSpelling.Clear();
				ScanWhiteSpace();

				Location startLocation = source.Location;
				TokenKind kind = ScanToken();
				SourcePosition position = new SourcePosition(startLocation, source.Location);

				Token token = new Token(kind, currentSpelling.ToString(), position);

				if(kind == TokenKind.EndOfText)
					atEndOfFile = true;

				Compiler.WriteDebuggingInfo($"Scanned {token}");

				yield return token;
			}
		}
		/// <summary>
		/// Skips over any whitespace
		/// </summary>
		private void ScanWhiteSpace() {
			while(source.Current == '!' || source.Current == ' ' || source.Current == '\t' || source.Current == '\n')
				ScanSeparator();
		}
		/// <summary>
		/// Skips a single separator
		/// </summary>
		private void ScanSeparator() {
			if(source.Current == '!')
				source.SkipRestOfLine();
			else
				source.MoveNext();
		}
		/// <summary>
		/// Gets the next token in the file
		/// </summary>
		/// <returns>The type of the next token</returns>
		private TokenKind ScanToken() {
			if(char.IsLetter(source.Current)) {
				do { TakeIt(); } while(IsLetterOrDigitOrUnderscore(source.Current));

				if(ReservedWords.TryGetValue(currentSpelling.ToString(), out TokenKind reservedKind))
					return reservedKind;
				else
					return TokenKind.Identifier;
			}
			else if(char.IsDigit(source.Current)) {
				do { TakeIt(); } while(char.IsDigit(source.Current));
				return TokenKind.IntLiteral;
			}
			else if(IsOperator(source.Current)) {
				// Matched an operator
				TakeIt();
				return TokenKind.Operator;
			}
			else {
				switch(source.Current) {
					case '\'':
						// Matching a character literal
						TakeIt();
						if(IsGraphic(source.Current)) {
							TakeIt();
							if(source.Current == '\'') {
								TakeIt();
								return TokenKind.CharLiteral;
							}
							else {
								// Found something that wasn't a ' as 3rd character
								TakeIt();
								return TokenKind.Error;
							}
						}
						else {
							// Found something that wasn't a graphic as 2nd character
							TakeIt();
							return TokenKind.Error;
						}
					case ';':
						TakeIt();
						return TokenKind.Semicolon;
					case ':':
						TakeIt();
						if(source.Current == '=') {
							TakeIt();
							return TokenKind.Becomes;
						}
						else
							return TokenKind.Colon;
					case '(':
						TakeIt();
						return TokenKind.LeftBracket;
					case ')':
						TakeIt();
						return TokenKind.RightBracket;
					case '~':
						TakeIt();
						return TokenKind.Is;
					case ',':
						TakeIt();
						return TokenKind.Comma;
					case '?':
						TakeIt();
						return TokenKind.QuestionMark;
					case default(char):
						// We have reached the end of the file
						return TokenKind.EndOfText;
					default:
						// We encountered something we weren't expecting
						TakeIt();
						return TokenKind.Error;
				}
			}
		}
		/// <summary>
		/// Appends the current character to the current token, and gets the next character from the source program
		/// </summary>
		private void TakeIt() {
			currentSpelling.Append(source.Current);
			source.MoveNext();
		}
		/// <summary>
		/// Checks whether a character is a letter or digit or underscore
		/// </summary>
		/// <param name="c">The character to check</param>
		/// <returns>True if and only if c is a letter or digit or underscore</returns>
		private static bool IsLetterOrDigitOrUnderscore(char c) { return char.IsLetter(c) || char.IsDigit(c) || c == '_'; }
		/// <summary>
		/// Checks whether a charcter is a graphic
		/// </summary>
		/// <param name="c">The character to check</param>
		/// <returns>True if and only if the character is a graphic</returns>
		private static bool IsGraphic(char c) { return char.IsLetter(c) || char.IsDigit(c) || IsOperator(c) || c == '.' || c == '!' || c == '?' || c == '_' || c == ' '; }
		/// <summary>
		/// Checks whether a character is an operator
		/// </summary>
		/// <param name="c">The character to check</param>
		/// <returns>True if and only if the character is an operator in the language</returns>
		private static bool IsOperator(char c) {
			switch(c) {
				case '+':
				case '-':
				case '*':
				case '/':
				case '<':
				case '>':
				case '=':
					return true;
				default:
					return false;
			}
		}
	}
}