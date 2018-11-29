using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Commands {
	public class IfCommand : Command {
		public Expression Expression { get; }
		public Command TrueCommand { get; }
		public Command FalseCommand { get; }

		public IfCommand(Expression expression, Command trueCommand, Command falseCommand, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Expression = expression;
			TrueCommand = trueCommand;
			FalseCommand = falseCommand;
		}

		public override TResult Visit<TArg, TResult>(ICommandVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitIfCommand(this, arg); }
	}
}