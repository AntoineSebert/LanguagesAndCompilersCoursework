using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Expressions {
	public class TernaryExpression : Expression {
		public Expression Condition { get; }
		public Expression LeftExpression { get; }
		public Expression RightExpression { get; }

		public TernaryExpression(Expression condition, Expression leftExpression, Expression rightExpression, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Condition = condition;
			LeftExpression = leftExpression;
			RightExpression = rightExpression;
		}

		public override TResult Visit<TArg, TResult>(IExpressionVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitTernaryExpression(this, arg); }
	}
}