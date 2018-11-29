using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Expressions {
	public class UnaryExpression : Expression {
		public Operator Operator { get; }
		public Expression Expression { get; }

		public UnaryExpression(Operator op, Expression expression, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Operator = op;
			Expression = expression;
		}

		public override TResult Visit<TArg, TResult>(IExpressionVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitUnaryExpression(this, arg); }
	}
}