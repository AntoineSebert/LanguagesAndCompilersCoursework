using Triangle.Compiler.SyntaxTrees.Commands;
using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.ContextualAnalyzer {
	public partial class Checker {
		public Void VisitAssignCommand(AssignCommand ast, Void arg) {
			TypeDenoter identifierType = ast.Identifier.Visit(this, null).Type;
			TypeDenoter expressionType = ast.Expression.Visit(this, null);
			CheckAndReportError(ast.Identifier.IsVariable, "LHS of assignment is not a variable", ast.Identifier);
			CheckAndReportError(expressionType.Equals(identifierType), "assignment incompatibilty", ast);
			return null;
		}
		public Void VisitCallCommand(CallCommand ast, Void arg) {
			Declaration binding = ast.Identifier.Visit(this, null);
			if(binding is ProcDeclaration procedure)
				ast.Parameters.Visit(this, procedure.Formals);
			else if(binding is FuncDeclaration function)
				ast.Parameters.Visit(this, function.Formals);
			else
				ReportUndeclaredOrError(binding, ast.Identifier, "\"%\" is not a procedure identifier");
			return null;
		}
		public Void VisitSkipCommand(SkipCommand ast, Void arg) { return null; }
		public Void VisitIfCommand(IfCommand ast, Void arg) {
			TypeDenoter expressionType = ast.Expression.Visit(this, null);
			CheckAndReportError(expressionType.Equals(StandardEnvironment.BooleanType), "expression type must be boolean", ast);
			ast.TrueCommand.Visit(this, null);
			ast.FalseCommand.Visit(this, null);
			return null;
		}
		public Void VisitLetCommand(LetCommand ast, Void arg) {
			idTable.OpenScope();
			ast.Declaration.Visit(this, null);
			ast.Command.Visit(this, null);
			idTable.CloseScope();
			return null;
		}
		public Void VisitSequentialCommand(SequentialCommand ast, Void arg) { throw new System.NotImplementedException(); }
		public Void VisitWhileCommand(WhileCommand ast, Void arg) { throw new System.NotImplementedException(); }
	}
}