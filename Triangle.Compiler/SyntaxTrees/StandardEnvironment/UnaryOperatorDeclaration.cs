using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Declarations {
	public class UnaryOperatorDeclaration : Declaration {
		public Operator Operator { get; }
		public TypeDenoter Argument { get; }
		public TypeDenoter Result { get; }

		public UnaryOperatorDeclaration(Operator op, TypeDenoter argument, TypeDenoter result) : base(SourcePosition.Empty) {
			Operator = op;
			Argument = argument;
			Result = result;
		}

		public override TResult Visit<TArg, TResult>(IDeclarationVisitor<TArg, TResult> visitor, TArg arg) {
			return visitor.VisitUnaryOperatorDeclaration(this, arg);
		}
	}
}