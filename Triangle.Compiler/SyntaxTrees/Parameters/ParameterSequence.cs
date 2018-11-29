using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Parameters {
	public abstract class ParameterSequence : AbstractSyntaxTree {
		protected ParameterSequence(SourcePosition position) : base(position) { Compiler.WriteDebuggingInfo($"Creating {GetType().Name}"); }

		public abstract TResult Visit<TArg, TResult>(IParameterSequenceVisitor<TArg, TResult> visitor, TArg arg);
	}
}