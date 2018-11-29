using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Parameters {
	public abstract class Parameter : AbstractSyntaxTree {
		protected Parameter(SourcePosition position) : base(position) { Compiler.WriteDebuggingInfo($"Creating {GetType().Name}"); }

		public abstract TResult Visit<TArg, TResult>(IParameterVisitor<TArg, TResult> visitor, TArg arg);
	}
}