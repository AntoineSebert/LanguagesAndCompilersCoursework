using Triangle.Compiler.SyntaxTrees.Parameters;
using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Commands {
	public class CallCommand : Command {

		public Identifier Identifier { get; }
		public ParameterSequence Parameters { get; }

		public CallCommand(Identifier identifier, ParameterSequence parameters, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Identifier = identifier;
			Parameters = parameters;
		}

		public override TResult Visit<TArg, TResult>(ICommandVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitCallCommand(this, arg); }
	}
}