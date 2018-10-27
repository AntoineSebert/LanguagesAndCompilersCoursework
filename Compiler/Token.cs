/**
 * @author Antoine/Anthony Sébert
 */

namespace Compiler {
	/**
	 * Represents a token ans holds all the relevant information about it.
	 * @see	TokenKind
	 * @see	SourcePosition
	 */
	internal class Token {
		/* ATTRIBUTES */
			/**
			 * Contains the spelling of the token.
			 */
			public string Spelling { get; } = "";
			/**
			 * Contains the kind of the token.
			 * @see	TokenKind
			 */
			public TokenKind Kind { get; } = 0;
			/**
			 * Contains the position of the token in the source file.
			* @see	SourcePosition
			 */
			public SourcePosition Position { get; } = null;
		/* MEMBERS */
			/**
			 * Builds a {@code Token} object from the given parameters.
			 * @param	kind		the kind of the token
			 * @param	_Spelling	the spelling of the token
			 * @param	_Position	the position of the token
			 */
			public Token(TokenKind kind, string _Spelling, SourcePosition _Position) {
				Spelling = _Spelling;
				Kind = kind;
				Position = _Position;
			}
			/**
			 * Overrides the cast of a {@code Token} object to a {@code string}.
			 */
			public override string ToString() { return string.Format($"{Position.ToString()}-Kind = {Kind}, spelling = \"{Spelling}\""); }
	}
}