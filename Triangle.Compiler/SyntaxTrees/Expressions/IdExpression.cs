using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Expressions {
	public class IdExpression : Expression {
		public Identifier Identifier { get; }

		public IdExpression(Identifier identifier, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Identifier = identifier;
		}

		public override TResult Visit<TArg, TResult>(IExpressionVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitIdExpression(this, arg); }
	}
}