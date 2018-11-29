using System.Collections.Generic;

namespace Triangle.Compiler.SyntacticAnalyzer {
	public partial class Parser {
		private Scanner scanner;
		private ErrorReporter errorReporter;
		private IEnumerator<Token> tokens;
		public Parser(Scanner scanner, ErrorReporter errorReporter) {
			this.scanner = scanner;
			this.errorReporter = errorReporter;
			tokens = this.scanner.GetEnumerator();
		}
		/// <summary>
		/// Checks that the kind of the current token matches the expected kind, and
		/// fetches the next token from the source file, if not it throws a
		/// {@link SyntaxError}.
		/// </summary>
		/// <param name="expectedKind">
		/// the TokenKind expected
		/// </param>
		/// <returns>
		/// the current token
		/// </returns>
		/// <throws type="SyntaxError">
		/// a syntactic error
		/// </throws>
		private Token Accept(TokenKind expectedKind) {
			if(tokens.Current.Kind == expectedKind) {
				Compiler.WriteDebuggingInfo($"Accepted {tokens.Current}");
				Token token = tokens.Current;
				tokens.MoveNext();
				return token;
			}
			else {
				RaiseSyntacticError("\"%\" expected here", expectedKind);
				return null;
			}
		}
		/// <summary>
		/// Fetches the next token from the source file.
		/// </summary>
		private void AcceptIt() {
			Compiler.WriteDebuggingInfo($"Accepted {tokens.Current}");
			tokens.MoveNext();
		}
		/// <summary>
		/// Reports an error and then throws a {@link SyntaxError} exception. The error
		/// is reported by calling
		/// {@link Parser#reportError(String, String, SourcePosition)} using the given
		/// template and quoted token, and the current source position. A new
		/// {@link SyntaxError} exception is then thrown to end the parsing of the
		/// source program.
		/// </summary>
		/// <param name="messageTemplate">
		/// the message template
		/// </param>
		/// <param name="quotedToken">
		/// the quoted token
		/// </param>
		/// <throws type="SyntaxError">
		/// a syntactic error
		/// </throws>
		private void RaiseSyntacticError(string messageTemplate, string quotedToken) {
			SourcePosition pos = tokens.Current.Position;
			errorReporter.ReportError(messageTemplate, quotedToken, pos);
			throw new SyntaxError();
		}
		/// <summary>
		/// Reports an error and then throws a {@link SyntaxError} exception. It calls
		/// {@link #raiseSyntacticError(String, String)} using the spelling of the
		/// given token or the empty string if the token is null.
		/// </summary>
		/// <param name="messageTemplate">
		/// the message template
		/// </param>
		/// <param name="token">
		/// the token
		/// </param>
		/// <throws type="SyntaxError">
		/// a syntactic error
		/// </throws>
		private void RaiseSyntacticError(string messageTemplate, Token token) { RaiseSyntacticError(messageTemplate, token == null ? string.Empty : token.Spelling); }
		/// <summary>
		/// Reports an error and then throws a {@link SyntaxError} exception. It calls
		/// {@link #RaiseSyntacticError(string, string)} using the spelling of the
		/// given token kind, or the empty string if the token kind is null.
		/// </summary>
		/// <param name="messageTemplate">
		/// the message template
		/// </param>
		/// <param name="kind">
		/// the token kind
		/// </param>
		/// <throws type="SyntaxError">
		/// a syntactic error
		/// </throws>
		private void RaiseSyntacticError(string messageTemplate, TokenKind kind) { RaiseSyntacticError(messageTemplate, kind.ToString()); }
	}
}