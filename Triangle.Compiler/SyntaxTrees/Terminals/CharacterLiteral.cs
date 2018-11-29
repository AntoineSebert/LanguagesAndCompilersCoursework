using Triangle.Compiler.SyntacticAnalyzer;

namespace Triangle.Compiler.SyntaxTrees.Terminals {
	public class CharacterLiteral : Terminal {
		public char Value => Spelling[1];

		public CharacterLiteral(string spelling, SourcePosition position) : base(spelling, position) { Compiler.WriteDebuggingInfo($"Creating {GetType().Name}"); }

		public CharacterLiteral(Token token) : this(token.Spelling, token.Position) { Compiler.WriteDebuggingInfo($"Creating {GetType().Name}"); }
	}
}