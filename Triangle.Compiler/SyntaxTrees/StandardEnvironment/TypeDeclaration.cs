using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Declarations {
	public class TypeDeclaration : Declaration {
		public Identifier Identifier { get; }

		public TypeDeclaration(Identifier identifier, TypeDenoter type, SourcePosition position) : base(position, type) { Identifier = identifier; }

		public TypeDeclaration(Identifier identifier, TypeDenoter type) : this(identifier, type, SourcePosition.Empty) {}

		public override TResult Visit<TArg, TResult>(IDeclarationVisitor<TArg, TResult> visitor, TArg arg) {
			return visitor.VisitTypeDeclaration(this, arg);
		}
	}
}