namespace Compiler {
	internal class Token {
		/* ATTRIBUTES */
			public string Spelling { get; } = "";
			public TokenKind Kind { get; } = 0;
			public SourcePosition Position { get; } = null;
		/* MEMBERS */
			public Token(TokenKind kind, string _Spelling, SourcePosition _Position) {
				Spelling = _Spelling;
				Kind = kind;
				Position = _Position;
			}
			public override string ToString() { return string.Format($"{Position.ToString()}-Kind = {Kind}, spelling = \"{Spelling}\""); }
	}
}