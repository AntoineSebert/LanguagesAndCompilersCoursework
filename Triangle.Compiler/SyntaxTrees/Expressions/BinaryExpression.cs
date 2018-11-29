using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Expressions {
	public class BinaryExpression : Expression {
		public Operator Operation { get; }
		public Expression LeftExpression { get; }
		public Expression RightExpression { get; }

		public BinaryExpression(Expression leftExpression, Operator operation, Expression rightExpression, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Operation = operation;
			LeftExpression = leftExpression;
			RightExpression = rightExpression;
		}

		public override TResult Visit<TArg, TResult>(IExpressionVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitBinaryExpression(this, arg); }
	}
}