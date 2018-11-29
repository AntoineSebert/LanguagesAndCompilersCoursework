using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Declarations {
	public class InitDeclaration : Declaration {
		public Identifier Identifier { get; }
		public Expression Expression { get; }

		public InitDeclaration(Identifier identifier, TypeDenoter type, Expression expression, SourcePosition position) : base(position, type) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Identifier = identifier;
			Expression = expression;
		}

		public override TResult Visit<TArg, TResult>(IDeclarationVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitInitDeclaration(this, arg); }
	}
}