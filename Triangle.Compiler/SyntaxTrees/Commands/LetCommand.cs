using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Commands {
	public class LetCommand : Command {
		public Declaration Declaration { get; }
		public Command Command { get; }

		public LetCommand(Declaration declaration, Command command, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Declaration = declaration;
			Command = command;
		}

		public override TResult Visit<TArg, TResult>(ICommandVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitLetCommand(this, arg); }
	}
}