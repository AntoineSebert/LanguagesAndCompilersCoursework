namespace Compiler {
	public enum TokenKind {
		// non-terminals
		IntLiteral, Identifier, Operator, CharacterLiteral, Graphic,
		// reserved words - terminals
		Begin, Const, Do, Else, End, If, In, Let, Then, Var, While, Skip,
		// punctuation - terminals
		Colon /* : */, Semicolon, Becomes /* := */, Is /* ~ */, LeftParenthese, RightParenthese,
		// special tokens
		EndOfText, Error
	}
}