/**
 * @author Antoine/Anthony Sébert
 */

namespace Compiler {
	/**
	 * Encompasses all the kinds of token defined in the grammar, except the atomics 'Letter', 'Digit' and 'Graphic'.
	 * @see	Token
	 */
	public enum TokenKind {
		// non-terminals
		IntLiteral, Identifier, Operator, CharacterLiteral,
		// reserved words - terminals
		Begin, Const, Do, Else, End, If, In, Let, Then, Var, While, Skip,
		// punctuation - terminals
		Colon /* : */, Semicolon, Becomes /* := */, Is /* ~ */, LeftParenthese, RightParenthese, Comma,
		// special tokens
		EndOfText, Error
	}
}