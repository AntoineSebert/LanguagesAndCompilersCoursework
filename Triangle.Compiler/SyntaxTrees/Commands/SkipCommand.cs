using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Commands {
	public class SkipCommand : Command {
		public SkipCommand(SourcePosition position) : base(position) { Compiler.WriteDebuggingInfo($"Creating {GetType().Name}"); }

		public SkipCommand() : this(SourcePosition.Empty) { Compiler.WriteDebuggingInfo($"Creating {GetType().Name}"); }

		public override TResult Visit<TArg, TResult>(ICommandVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitSkipCommand(this, arg); }
	}
}