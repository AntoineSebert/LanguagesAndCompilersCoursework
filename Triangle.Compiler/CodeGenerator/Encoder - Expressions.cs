using Triangle.AbstractMachine;
using Triangle.Compiler.SyntaxTrees.Expressions;

namespace Triangle.Compiler.CodeGenerator {
	public partial class Encoder {
		public int VisitCharacterExpression(CharacterExpression ast, Frame frame) {
			int valSize = ast.Type.Visit(this, null);
			emitter.Emit(OpCode.LOADL, ast.Value);

			return valSize;
		}

		public int VisitIntegerExpression(IntegerExpression ast, Frame frame) {
			int valSize = ast.Type.Visit(this, null);
			emitter.Emit(OpCode.LOADL, ast.Value);

			return valSize;
		}

		public int VisitEmptyExpression(EmptyExpression ast, Frame frame) { return 0; }

		public int VisitIdExpression(IdExpression ast, Frame frame) {
			int valSize = ast.Type.Visit(this, null);
			EncodeFetch(ast.Identifier, frame, valSize);

			return valSize;
		}

		public int VisitCallExpression(CallExpression ast, Frame frame) {
			int valSize = ast.Type.Visit(this, null);
			int argsSize = ast.Parameters.Visit(this, frame);
			ast.Identifier.Visit(this, frame.Replace(argsSize));

			return valSize;
		}

		public int VisitUnaryExpression(UnaryExpression ast, Frame frame) {
			int valSize = ast.Type.Visit(this, null);
			ast.Expression.Visit(this, frame);
			ast.Operator.Visit(this, frame.Replace(valSize));

			return valSize;
		}

		public int VisitBinaryExpression(BinaryExpression ast, Frame frame) {
			int valSize = ast.Type.Visit(this, null);
			int valSize1 = ast.LeftExpression.Visit(this, frame);
			Frame frame1 = frame.Expand(valSize1);
			int valSize2 = ast.RightExpression.Visit(this, frame1);
			Frame frame2 = frame1.Replace(valSize1 + valSize2);
			ast.Operation.Visit(this, frame2);

			return valSize;
		}

		public int VisitTernaryExpression(TernaryExpression ast, Frame frame) {
			ast.Type.Visit(this, null);
			ast.Condition.Visit(this, frame);
			int jumpifAddr = emitter.Emit(OpCode.JUMPIF, Machine.FalseValue, Register.CB);
			int valSize = ast.LeftExpression.Visit(this, frame);
			int jumpAddr = emitter.Emit(OpCode.JUMP, Register.CB);
			emitter.Patch(jumpifAddr);
			ast.RightExpression.Visit(this, frame);
			emitter.Patch(jumpAddr);

			return valSize;
		}
	}
}