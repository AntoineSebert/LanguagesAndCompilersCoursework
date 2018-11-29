using Triangle.Compiler.SyntaxTrees.Commands;
using Triangle.Compiler.SyntaxTrees.Formals;
using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Declarations {
	public class ProcDeclaration : Declaration {
		public Identifier Identifier { get; }
		public FormalParameterSequence Formals { get; }
		public Command Command { get; }

		public ProcDeclaration(Identifier identifier, FormalParameterSequence formals, Command command, SourcePosition position) : base(position) {
			Identifier = identifier;
			Formals = formals;
			Command = command;
		}

		public ProcDeclaration(Identifier identifier, FormalParameterSequence formals) : this(identifier, formals, new SkipCommand(), SourcePosition.Empty) {}

		public override TResult Visit<TArg, TResult>(IDeclarationVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitProcDeclaration(this, arg); }
	}
}