using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Parameters {
	public class ValueParameter : Parameter {
		public Expression Expression { get; }

		public ValueParameter(Expression expression, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Expression = expression;
		}

		public override TResult Visit<TArg, TResult>(IParameterVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitValueParameter(this, arg); }
	}
}