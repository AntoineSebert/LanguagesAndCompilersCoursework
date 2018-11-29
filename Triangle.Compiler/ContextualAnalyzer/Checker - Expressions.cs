using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.ContextualAnalyzer {
	public partial class Checker {
		// Expressions
		// Set Type property for the expression and return this type
		public TypeDenoter VisitEmptyExpression(EmptyExpression ast, Void arg) { return (ast.Type = null); }
		public TypeDenoter VisitCharacterExpression(CharacterExpression ast, Void arg) {
			return (ast.Type = StandardEnvironment.CharType);
		}
		public TypeDenoter VisitIntegerExpression(IntegerExpression ast, Void arg) {
			return (ast.Type = StandardEnvironment.IntegerType);
		}
		public TypeDenoter VisitIdExpression(IdExpression ast, Void arg) {
			ast.Identifier.Visit(this, null);
			return (ast.Type = ast.Identifier.Declaration.Type);
		}
		public TypeDenoter VisitCallExpression(CallExpression ast, Void arg) {
			Declaration binding = ast.Identifier.Visit(this, null);
			if(binding is FuncDeclaration function) {
				ast.Parameters.Visit(this, function.Formals);
				ast.Type = function.Type;
			}
			else {
				ReportUndeclaredOrError(binding, ast.Identifier, "\"%\" is not a function identifier");
				ast.Type = StandardEnvironment.ErrorType;
			}
			return ast.Type;
		}
		public TypeDenoter VisitUnaryExpression(UnaryExpression ast, Void arg) {
			TypeDenoter expressionType = ast.Expression.Visit(this, null);
			Declaration binding = ast.Operator.Visit(this, null);
			if(binding is UnaryOperatorDeclaration ubinding) {
				CheckAndReportError(expressionType.Equals(ubinding.Argument), "wrong argument type for \"%\"", ast.Operator);
				ast.Type = ubinding.Result;
			}
			else {
				ReportUndeclaredOrError(binding, ast.Operator, "\"%\" is not a unary operator");
				ast.Type = StandardEnvironment.ErrorType;
			}
			return ast.Type;
		}
		public TypeDenoter VisitBinaryExpression(BinaryExpression ast, Void arg) {
			TypeDenoter e1Type = ast.LeftExpression.Visit(this, null);
			TypeDenoter e2Type = ast.RightExpression.Visit(this, null);
			Declaration binding = ast.Operation.Visit(this, null);

			if(binding is BinaryOperatorDeclaration bbinding) {
				if(bbinding.FirstArgument == StandardEnvironment.AnyType)
					CheckAndReportError(e1Type.Equals(e2Type), "incompatible argument types for \"%\"", ast.Operation, ast);
				else {
					CheckAndReportError(e1Type.Equals(bbinding.FirstArgument), "wrong argument type for \"%\"", ast.Operation, ast.LeftExpression);
					CheckAndReportError(e2Type.Equals(bbinding.SecondArgument), "wrong argument type for \"%\"", ast.Operation, ast.RightExpression);
				}
				ast.Type = bbinding.Result;
			}
			else {
				ReportUndeclaredOrError(binding, ast.Operation, "\"%\" is not a binary operator");
				ast.Type = StandardEnvironment.ErrorType;
			}
			return ast.Type;
		}
		public TypeDenoter VisitTernaryExpression(TernaryExpression ast, Void arg) {
			TypeDenoter e1Type = ast.Condition.Visit(this, null);
			TypeDenoter e2Type = ast.LeftExpression.Visit(this, null);
			TypeDenoter e3Type = ast.RightExpression.Visit(this, null);

			if(e1Type == StandardEnvironment.BooleanType) {
				if(e2Type == e3Type)
					ast.Type = StandardEnvironment.AnyType;
				else {
					CheckAndReportError(e2Type.Equals(e3Type), "ternary expression member \"%\" must have the same type", ast.LeftExpression);
					CheckAndReportError(e3Type.Equals(e2Type), "ternary expression member \"%\" must have the same type", ast.RightExpression);
					ast.Type = StandardEnvironment.ErrorType;
				}
			}
			else {
				CheckAndReportError(e1Type.Equals(StandardEnvironment.BooleanType), "argument type \"%\" is not a boolean", ast.Condition);
				ast.Type = StandardEnvironment.ErrorType;
			}

			return ast.Type;
		}
	}
}