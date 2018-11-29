using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Expressions {
	public class EmptyExpression : Expression {
		public EmptyExpression() : base(SourcePosition.Empty) { Compiler.WriteDebuggingInfo($"Creating {GetType().Name}"); }

		public override TResult Visit<TArg, TResult>(IExpressionVisitor<TArg, TResult> visitor, TArg arg) {
			return visitor.VisitEmptyExpression(this, arg);
		}
	}
}