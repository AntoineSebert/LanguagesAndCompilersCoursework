using Triangle.Compiler.SyntaxTrees.Parameters;
using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Expressions {
	public class CallExpression : Expression {
		public Identifier Identifier { get; }
		public ParameterSequence Parameters { get; }

		public CallExpression(Identifier identifier, ParameterSequence parameters, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Identifier = identifier;
			Parameters = parameters;
		}

		public override TResult Visit<TArg, TResult>(IExpressionVisitor<TArg, TResult> visitor, TArg arg) {
			return visitor.VisitCallExpression(this, arg);
		}
	}
}