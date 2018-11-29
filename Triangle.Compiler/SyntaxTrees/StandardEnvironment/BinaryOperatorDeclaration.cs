using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Declarations {
	public class BinaryOperatorDeclaration : Declaration {
		public Operator Operator { get; }
		public TypeDenoter FirstArgument { get; }
		public TypeDenoter SecondArgument { get; }
		public TypeDenoter Result { get; }

		public BinaryOperatorDeclaration(Operator op, TypeDenoter firstArgument, TypeDenoter secondArgument, TypeDenoter result) : base(SourcePosition.Empty) {
			Operator = op;
			FirstArgument = firstArgument;
			SecondArgument = secondArgument;
			Result = result;
		}

		public override TResult Visit<TArg, TResult>(IDeclarationVisitor<TArg, TResult> visitor, TArg arg) {
			return visitor.VisitBinaryOperatorDeclaration(this, arg);
		}
	}
}