namespace Compiler {
	public enum TokenKind {
		// non-terminals
		IntLiteral, Identifier, Operator, CharacterLiteral,
		// reserved words - terminals
		Begin, Const, Do, Else, End, If, In, Let, Then, Var, While,
		// punctuation - terminals
		Colon, Semicolon, Becomes, Is, LeftBracket, RightBracket, LeftParenthese, RightParenthese,
		// special tokens
		EndOfText, Error
	}
}