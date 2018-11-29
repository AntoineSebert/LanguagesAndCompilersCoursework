using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Declarations {
	public class VarDeclaration : Declaration {
		public Identifier Identifier { get; }

		public VarDeclaration(Identifier identifier, TypeDenoter type, SourcePosition position) : base(position, type) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Identifier = identifier;
		}

		public override TResult Visit<TArg, TResult>(IDeclarationVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitVarDeclaration(this, arg); }
	}
}