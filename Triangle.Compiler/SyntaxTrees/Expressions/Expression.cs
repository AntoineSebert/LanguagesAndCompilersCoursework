using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Expressions {
	public abstract class Expression : AbstractSyntaxTree {
		public TypeDenoter Type { get; set; }

		protected Expression(SourcePosition position) : base(position) { Compiler.WriteDebuggingInfo($"Creating {GetType().Name}"); }

		public abstract TResult Visit<TArg, TResult>(IExpressionVisitor<TArg, TResult> visitor, TArg arg);
	}
}