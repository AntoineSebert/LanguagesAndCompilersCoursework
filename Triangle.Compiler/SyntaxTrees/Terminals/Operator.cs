using Triangle.Compiler.SyntacticAnalyzer;
using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Terminals {
	public class Operator : Terminal {
		public Declaration Declaration { get; set; }

		public Operator(string spelling, SourcePosition position) : base(spelling, position) { Compiler.WriteDebuggingInfo($"Creating {GetType().Name}"); }

		public Operator(Token token) : this(token.Spelling, token.Position) { Compiler.WriteDebuggingInfo($"Creating {GetType().Name}"); }

		public Operator(string spelling) : this(spelling, SourcePosition.Empty) { Compiler.WriteDebuggingInfo($"Creating {GetType().Name}"); }

		public TResult Visit<TArg, TResult>(ITerminalVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitOperator(this, arg); }
	}
}