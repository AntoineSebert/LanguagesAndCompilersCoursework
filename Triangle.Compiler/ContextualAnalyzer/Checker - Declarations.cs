using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.ContextualAnalyzer {
	public partial class Checker {
		public Void VisitConstDeclaration(ConstDeclaration ast, Void arg) {
			ast.Expression.Visit(this, null);
			idTable.Enter(ast.Identifier, ast);
			CheckAndReportError(!ast.Duplicated, "identifier \"%\" already declared", ast.Identifier, ast);
			return null;
		}
		public Void VisitVarDeclaration(VarDeclaration ast, Void arg) {
			ast.Type = ast.Type.Visit(this, null);
			idTable.Enter(ast.Identifier, ast);
			CheckAndReportError(!ast.Duplicated, "identifier \"%\" already declared", ast.Identifier, ast);
			return null;
		}
		public Void VisitInitDeclaration(InitDeclaration ast, Void arg) {
			ast.Type = ast.Type.Visit(this, null);
			ast.Expression.Visit(this, null);
			idTable.Enter(ast.Identifier, ast);
			CheckAndReportError(!ast.Duplicated, "identifier \"%\" already declared", ast.Identifier, ast);
			return null;
		}
		public Void VisitSequentialDeclaration(SequentialDeclaration ast, Void arg) {
			ast.FirstDeclaration.Visit(this, null);
			ast.SecondDeclaration.Visit(this, null);
			return null;
		}
	}
}