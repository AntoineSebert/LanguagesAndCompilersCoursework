using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Commands {
	public class AssignCommand : Command {
		public Identifier Identifier { get; }
		public Expression Expression { get; }

		public AssignCommand(Identifier identifier, Expression expression, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Identifier = identifier;
			Expression = expression;
		}

		public override TResult Visit<TArg, TResult>(ICommandVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitAssignCommand(this, arg); }
	}
}