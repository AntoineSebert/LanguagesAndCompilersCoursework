using Triangle.Compiler.SyntaxTrees.Commands;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees {
	public class Program : AbstractSyntaxTree {
		public Command Command { get; }

		public Program(Command command, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Command = command;
		}

		public TResult Visit<TArg, TResult>(IProgramVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitProgram(this, arg); }
	}
}