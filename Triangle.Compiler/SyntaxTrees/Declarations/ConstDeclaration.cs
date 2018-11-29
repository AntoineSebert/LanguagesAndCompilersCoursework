using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Declarations {
	public class ConstDeclaration : Declaration {
		public Identifier Identifier { get; }
		public Expression Expression { get; }
		public override TypeDenoter Type => Expression.Type;

		public ConstDeclaration(Identifier identifier, Expression expression, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Identifier = identifier;
			Expression = expression;
		}

		public ConstDeclaration(Identifier identifier, Expression expression) : this(identifier, expression, SourcePosition.Empty) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
		}

		public override TResult Visit<TArg, TResult>(IDeclarationVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitConstDeclaration(this, arg); }
	}
}