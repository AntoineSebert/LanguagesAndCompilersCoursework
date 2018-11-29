using Triangle.AbstractMachine;
using Triangle.Compiler.CodeGenerator.Entities;
using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Expressions;

namespace Triangle.Compiler.CodeGenerator {
	public partial class Encoder {
		public int VisitConstDeclaration(ConstDeclaration ast, Frame frame) {
			int extraSize = 0;
			Expression expr = ast.Expression;
			switch(expr) {
				case CharacterExpression charExpression:
					ast.Entity = new KnownValue(charExpression.Type.Size, charExpression.Value);
					break;
				case IntegerExpression intExpression:
					ast.Entity = new KnownValue(intExpression.Type.Size, intExpression.Value);
					break;
				default:
					extraSize = expr.Visit(this, frame);
					ast.Entity = new UnknownValue(extraSize, frame);
					break;
			}
			WriteTableDetails(ast);
			return extraSize;
		}

		public int VisitVarDeclaration(VarDeclaration ast, Frame frame) {
			int extraSize = ast.Type.Visit(this, null);
			emitter.Emit(OpCode.PUSH, (short)extraSize);
			ast.Entity = new KnownAddress(Machine.AddressSize, frame);
			WriteTableDetails(ast);
			return extraSize;
		}

		public int VisitInitDeclaration(InitDeclaration ast, Frame frame) {
			int extraSize = ast.Expression.Visit(this, null);
			ast.Entity = new KnownAddress(Machine.AddressSize, frame);
			WriteTableDetails(ast);
			return extraSize;
		}

		public int VisitSequentialDeclaration(SequentialDeclaration ast, Frame frame) {
			int extraSize1 = ast.FirstDeclaration.Visit(this, frame);
			int extraSize2 = ast.SecondDeclaration.Visit(this, frame.Expand(extraSize1));
			return extraSize1 + extraSize2;
		}
	}
}