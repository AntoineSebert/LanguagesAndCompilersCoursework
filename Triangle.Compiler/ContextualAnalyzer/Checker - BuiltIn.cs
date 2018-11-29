using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Formals;
using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.ContextualAnalyzer {
	public partial class Checker {
		// This is just for the standard environment
		public Void VisitFuncDeclaration(FuncDeclaration ast, Void arg) {
			ast.Type = ast.Type.Visit(this, null);
			idTable.Enter(ast.Identifier, ast);
			CheckAndReportError(!ast.Duplicated, "identifier \"%\" already declared", ast.Identifier, ast);
			idTable.OpenScope();
			ast.Formals.Visit(this, null);
			TypeDenoter expressionType = ast.Expression.Visit(this, null);
			idTable.CloseScope();
			CheckAndReportError(ast.Type.Equals(expressionType), "body of function \"%\" has wrong type", ast.Identifier, ast.Expression);
			return null;
		}
		public Void VisitProcDeclaration(ProcDeclaration ast, Void arg) {
			idTable.Enter(ast.Identifier, ast);
			CheckAndReportError(!ast.Duplicated, "identifier \"%\" already declared", ast.Identifier, ast);
			idTable.OpenScope();
			ast.Formals.Visit(this, null);
			ast.Command.Visit(this, null);
			idTable.CloseScope();
			return null;
		}
		public Void VisitConstFormalParameter(ConstFormalParameter ast, Void arg) {
			ast.Type = ast.Type.Visit(this, null);
			idTable.Enter(ast.Identifier, ast);
			CheckAndReportError(!ast.Duplicated, "duplicated formal parameter \"%\"", ast.Identifier, ast);
			return null;
		}
		public Void VisitVarFormalParameter(VarFormalParameter ast, Void arg) {
			ast.Type = ast.Type.Visit(this, null);
			idTable.Enter(ast.Identifier, ast);
			CheckAndReportError(!ast.Duplicated, "duplicated formal parameter \"%\"", ast.Identifier, ast);
			return null;
		}
		public Void VisitEmptyFormalParameterSequence(EmptyFormalParameterSequence ast, Void arg) { return null; }
		public Void VisitSingleFormalParameterSequence(SingleFormalParameterSequence ast, Void arg) {
			ast.Formal.Visit(this, null);
			return null;
		}
		public Void VisitMultipleFormalParameterSequence(MultipleFormalParameterSequence ast, Void arg) {
			ast.Formal.Visit(this, null);
			ast.Formals.Visit(this, null);
			return null;
		}
		public Void VisitTypeDeclaration(TypeDeclaration ast, Void arg) {
			ast.Type = ast.Type.Visit(this, null);
			idTable.Enter(ast.Identifier, ast);
			CheckAndReportError(!ast.Duplicated, "identifier \"%\" already declared", ast.Identifier, ast);
			return null;
		}
		public Void VisitUnaryOperatorDeclaration(UnaryOperatorDeclaration ast, Void arg) { return null; }
		public Void VisitBinaryOperatorDeclaration(BinaryOperatorDeclaration ast, Void arg) { return null; }
	}
}