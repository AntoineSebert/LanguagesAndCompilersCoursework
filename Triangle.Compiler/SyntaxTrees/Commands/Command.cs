using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Commands {
	public abstract class Command : AbstractSyntaxTree {
		protected Command(SourcePosition position) : base(position) { Compiler.WriteDebuggingInfo($"Creating {GetType().Name}"); }

		public abstract TResult Visit<TArg, TResult>(ICommandVisitor<TArg, TResult> visitor, TArg arg);
	}
}