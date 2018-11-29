using Triangle.AbstractMachine;
using Triangle.Compiler.SyntaxTrees.Commands;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.CodeGenerator {
	public partial class Encoder {
		public Void VisitIfCommand(IfCommand ast, Frame frame) { 
			ast.Expression.Visit(this, frame);
			int jumpFalseAddress = emitter.Emit(OpCode.JUMPIF, Machine.FalseValue, Register.CB);
			ast.TrueCommand.Visit(this, frame);
			int jumpTrueAddr = emitter.Emit(OpCode.JUMP, Register.CB);
			emitter.Patch(jumpFalseAddress);
			ast.FalseCommand.Visit(this, frame);
			emitter.Patch(jumpTrueAddr);

			return null;
		}

		public Void VisitWhileCommand(WhileCommand ast, Frame frame) {
			int jumpAddr = emitter.Emit(OpCode.JUMP, Register.CB);
			int loopAddr = emitter.NextInstrAddr;
			ast.Command.Visit(this, frame);
			emitter.Patch(jumpAddr);
			ast.Expression.Visit(this, frame);
			emitter.Emit(OpCode.JUMPIF, Machine.TrueValue, Register.CB, loopAddr);

			return null;
		}

		public Void VisitCallCommand(CallCommand ast, Frame frame) {
			int argsSize = ast.Parameters.Visit(this, frame);
			ast.Identifier.Visit(this, frame.Replace(argsSize));

			return null;
		}

		public Void VisitSequentialCommand(SequentialCommand ast, Frame frame) {
			ast.FirstCommand.Visit(this, frame);
			ast.SecondCommand.Visit(this, frame);

			return null;
		}

		public Void VisitSkipCommand(SkipCommand ast, Frame frame) {
			// ... ?

			return null;
		}

		public Void VisitAssignCommand(AssignCommand ast, Frame frame) {
			int valSize = ast.Expression.Visit(this, frame);
			EncodeAssign(ast.Identifier, frame.Expand(valSize), valSize);

			return null;
		}

		public Void VisitLetCommand(LetCommand ast, Frame frame) {
			int extraSize = ast.Declaration.Visit(this, frame);
			ast.Command.Visit(this, frame.Expand(extraSize));
			if(extraSize > 0)
				emitter.Emit(OpCode.POP, (short)extraSize);
			return null;
		}
	}
}