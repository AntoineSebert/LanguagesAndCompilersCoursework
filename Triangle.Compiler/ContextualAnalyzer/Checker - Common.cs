using Triangle.Compiler.SyntaxTrees;
using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Terminals;

namespace Triangle.Compiler.ContextualAnalyzer {
	public partial class Checker : ICheckerVisitor {
		private ErrorReporter errorReporter;
		private IdentificationTable idTable;
		public Checker(ErrorReporter _errorReporter) {
			errorReporter = _errorReporter;
			idTable = new IdentificationTable();
			EstablishStdEnvironment();
		}
		public void Check(Program ast) { ast.Visit(this, null); }
		// Reports that the identifier or operator used at a leaf of the AST has not been declared.
		private void ReportUndeclaredOrError(Declaration binding, Terminal leaf, string message) {
			ReportError((binding == null ? "\"%\" is not declared" : message), leaf);
		}
		private void ReportError(string message, Terminal ast) { ReportError(message, ast, ast); }
		private void ReportError(string message, Terminal spellingNode, AbstractSyntaxTree positionNode) {
			errorReporter.ReportError(message, spellingNode.Spelling, positionNode.Position);
		}
		private void ReportError(string message, AbstractSyntaxTree positionNode) { errorReporter.ReportError(message, string.Empty, positionNode.Position); }
		private void CheckAndReportError(bool condition, string message, string token, SourcePosition position) {
			if(!condition)
				errorReporter.ReportError(message, token, position);
		}
		private void CheckAndReportError(bool condition, string message, Terminal ast) { CheckAndReportError(condition, message, ast, ast); }
		private void CheckAndReportError(bool condition, string message, Terminal spellingNode, AbstractSyntaxTree positionNode) {
			CheckAndReportError(condition, message, spellingNode.Spelling, positionNode.Position);
		}
		private void CheckAndReportError(bool condition, string message, AbstractSyntaxTree positionNode) {
			CheckAndReportError(condition, message, string.Empty, positionNode.Position);
		}
		// Creates small ASTs to represent the standard types.
		// Creates small ASTs to represent "declarations" of standard types, constants, procedures, functions, and operators.
		// Enters these "declarations" in the identification table.
		private void EnterStdDeclaration(Terminal terminal, Declaration declaration) { idTable.Enter(terminal, declaration); }
		private void EnterStdDeclaration(TypeDeclaration declaration) { EnterStdDeclaration(declaration.Identifier, declaration); }
		private void EnterStdDeclaration(ConstDeclaration declaration) { EnterStdDeclaration(declaration.Identifier, declaration); }
		private void EnterStdDeclaration(FuncDeclaration declaration) { EnterStdDeclaration(declaration.Identifier, declaration); }
		private void EnterStdDeclaration(ProcDeclaration declaration) { EnterStdDeclaration(declaration.Identifier, declaration); }
		private void EnterStdDeclaration(BinaryOperatorDeclaration declaration) { EnterStdDeclaration(declaration.Operator, declaration); }
		private void EstablishStdEnvironment() {
			EnterStdDeclaration(StandardEnvironment.BooleanDecl);
			EnterStdDeclaration(StandardEnvironment.FalseDecl);
			EnterStdDeclaration(StandardEnvironment.TrueDecl);
			EnterStdDeclaration(StandardEnvironment.IntegerDecl);
			EnterStdDeclaration(StandardEnvironment.AddDecl);
			EnterStdDeclaration(StandardEnvironment.SubtractDecl);
			EnterStdDeclaration(StandardEnvironment.MultiplyDecl);
			EnterStdDeclaration(StandardEnvironment.DivideDecl);
			EnterStdDeclaration(StandardEnvironment.LessDecl);
			EnterStdDeclaration(StandardEnvironment.GreaterDecl);
			EnterStdDeclaration(StandardEnvironment.EqualDecl);
			EnterStdDeclaration(StandardEnvironment.CharDecl);
			EnterStdDeclaration(StandardEnvironment.ChrDecl);
			EnterStdDeclaration(StandardEnvironment.OrdDecl);
			EnterStdDeclaration(StandardEnvironment.EofDecl);
			EnterStdDeclaration(StandardEnvironment.EolDecl);
			EnterStdDeclaration(StandardEnvironment.GetDecl);
			EnterStdDeclaration(StandardEnvironment.PutDecl);
			EnterStdDeclaration(StandardEnvironment.GetintDecl);
			EnterStdDeclaration(StandardEnvironment.PutintDecl);
			EnterStdDeclaration(StandardEnvironment.PuteolDecl);
		}
	}
}