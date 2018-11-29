using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.ContextualAnalyzer {
	public partial class Checker {
		// Type Denoters

		// Returns the expanded version of the TypeDenoter. Does not
		// use the given object.
		public TypeDenoter VisitAnyTypeDenoter(AnyTypeDenoter ast, Void arg) { return StandardEnvironment.AnyType; }
		public TypeDenoter VisitBoolTypeDenoter(BoolTypeDenoter ast, Void arg) { return StandardEnvironment.BooleanType; }
		public TypeDenoter VisitCharTypeDenoter(CharTypeDenoter ast, Void arg) { return StandardEnvironment.CharType; }
		public TypeDenoter VisitIntTypeDenoter(IntTypeDenoter ast, Void arg) { return StandardEnvironment.IntegerType; }
		public TypeDenoter VisitErrorTypeDenoter(ErrorTypeDenoter ast, Void arg) { return StandardEnvironment.ErrorType; }
		public TypeDenoter VisitSimpleTypeDenoter(SimpleTypeDenoter ast, Void arg) {
			Declaration binding = ast.Identifier.Visit(this, null);
			if(binding is TypeDeclaration decl)
				return decl.Type;
			else {
				ReportUndeclaredOrError(binding, ast.Identifier, "\"%\" is not a type identifier");
				return StandardEnvironment.ErrorType;
			}
		}
	}
}