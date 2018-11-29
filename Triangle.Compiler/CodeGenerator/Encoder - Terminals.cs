using Triangle.Compiler.CodeGenerator.Entities;
using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.CodeGenerator {
	public partial class Encoder {
		public Void VisitCharacterLiteral(CharacterLiteral ast, Frame frame) { return null; }

		public Void VisitIntegerLiteral(IntegerLiteral ast, Frame frame) { return null; }

		public Void VisitIdentifier(Identifier ast, Frame frame) {
			IProcedureEntity routine = ast.Declaration.Entity as IProcedureEntity;
			routine.EncodeCall(emitter, frame);
			return null;
		}

		public Void VisitOperator(Operator ast, Frame frame) {
			IProcedureEntity routine = ast.Declaration.Entity as IProcedureEntity;
			routine.EncodeCall(emitter, frame);
			return null;
		}
	}
}