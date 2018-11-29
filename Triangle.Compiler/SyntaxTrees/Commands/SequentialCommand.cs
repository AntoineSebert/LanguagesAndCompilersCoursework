using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Commands {
	public class SequentialCommand : Command {
		public Command FirstCommand { get; }
		public Command SecondCommand { get; }

		public SequentialCommand(Command firstCommand, Command secondCommand, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			FirstCommand = firstCommand;
			SecondCommand = secondCommand;
		}

		public override TResult Visit<TArg, TResult>(ICommandVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitSequentialCommand(this, arg); }
	}
}