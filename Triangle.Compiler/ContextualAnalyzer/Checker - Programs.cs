using Triangle.Compiler.SyntaxTrees;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.ContextualAnalyzer {
	public partial class Checker {
		public Void VisitProgram(Program ast, Void arg) { return ast.Command.Visit(this, null); }
	}
}