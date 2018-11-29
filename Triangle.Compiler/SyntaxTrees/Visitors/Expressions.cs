using Triangle.Compiler.SyntaxTrees.Expressions;

namespace Triangle.Compiler.SyntaxTrees.Visitors {
	public interface IExpressionVisitor<TArg, TResult> {
		TResult VisitBinaryExpression(BinaryExpression ast, TArg arg);

		TResult VisitCallExpression(CallExpression ast, TArg arg);

		TResult VisitCharacterExpression(CharacterExpression ast, TArg arg);

		TResult VisitEmptyExpression(EmptyExpression ast, TArg arg);

		TResult VisitIdExpression(IdExpression ast, TArg arg);

		TResult VisitIntegerExpression(IntegerExpression ast, TArg arg);

		TResult VisitTernaryExpression(TernaryExpression ast, TArg arg);

		TResult VisitUnaryExpression(UnaryExpression ast, TArg arg);
	}
}