namespace Triangle.Compiler.SyntacticAnalyzer {
	public enum TokenKind {
		// non-terminals
		IntLiteral, Identifier, Operator, CharLiteral,
		// reserved words - terminals
		Begin, Const, Do, Else, End, If, In, Let, Skip, Then, Var, While,
		// punctuation - terminals (Becomes is for assignment (:=) , Is is for constants (~))
		Colon, Semicolon, Becomes, Is, LeftBracket, RightBracket, Comma, QuestionMark,
		// special tokens
		EndOfText, Error
	}
}