using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Commands {
	public class WhileCommand : Command {
		public Expression Expression { get; }
		public Command Command { get; }

		public WhileCommand(Expression expression, Command command, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Command = command;
			Expression = expression;
		}

		public override TResult Visit<TArg, TResult>(ICommandVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitWhileCommand(this, arg); }
	}
}