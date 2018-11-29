using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Expressions {
	public class CharacterExpression : Expression {
		public CharacterLiteral CharacterLiteral { get; }
		public char Value => CharacterLiteral.Value;

		public CharacterExpression(CharacterLiteral characterLiteral, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			CharacterLiteral = characterLiteral;
		}

		public override TResult Visit<TArg, TResult>(IExpressionVisitor<TArg, TResult> visitor, TArg arg) {
			return visitor.VisitCharacterExpression(this, arg);
		}
	}
}