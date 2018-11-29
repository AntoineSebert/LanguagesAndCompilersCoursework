using Triangle.AbstractMachine;
using Triangle.Compiler.CodeGenerator.Entities;
using Triangle.Compiler.SyntaxTrees.Types;

namespace Triangle.Compiler.CodeGenerator {
	public partial class Encoder {
		public int VisitSimpleTypeDenoter(SimpleTypeDenoter ast, Frame frame) { return 0; }

		public int VisitAnyTypeDenoter(AnyTypeDenoter ast, Frame frame) { return 0; }

		public int VisitBoolTypeDenoter(BoolTypeDenoter ast, Frame frame) {
			if(ast.Entity == null) {
				ast.Entity = new TypeRepresentation(Machine.BooleanSize);
				WriteTableDetails(ast);
			}
			return Machine.BooleanSize;
		}

		public int VisitIntTypeDenoter(IntTypeDenoter ast, Frame frame) {
			if(ast.Entity == null) {
				ast.Entity = new TypeRepresentation(Machine.IntegerSize);
				WriteTableDetails(ast);
			}
			return Machine.IntegerSize;
		}

		public int VisitCharTypeDenoter(CharTypeDenoter ast, Frame frame) {
			if(ast.Entity == null) {
				ast.Entity = new TypeRepresentation(Machine.IntegerSize);
				WriteTableDetails(ast);
			}
			return Machine.IntegerSize;
		}

		public int VisitErrorTypeDenoter(ErrorTypeDenoter ast, Frame frame) { return 0; }
	}
}