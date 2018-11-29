using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Formals {
	public class VarFormalParameter : FormalParameter {
		public Identifier Identifier { get; }

		public VarFormalParameter(Identifier identifier, TypeDenoter type, SourcePosition position) : base(position, type) { Identifier = identifier; }

		public VarFormalParameter(TypeDenoter type) : this(Identifier.Empty, type, SourcePosition.Empty) {}

		public override TResult Visit<TArg, TResult>(IDeclarationVisitor<TArg, TResult> visitor, TArg arg) {
			return visitor.VisitVarFormalParameter(this, arg);
		}
	}
}