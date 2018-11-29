using Triangle.AbstractMachine;
using Triangle.Compiler.CodeGenerator.Entities;
using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Formals;

namespace Triangle.Compiler.CodeGenerator {
	public partial class Encoder {
		public int VisitFuncDeclaration(FuncDeclaration ast, Frame frame) {
			int argsSize = 0, valSize = 0;
			int jumpAddr = emitter.Emit(OpCode.JUMP, Register.CB);
			ast.Entity = new KnownProcedure(Machine.ClosureSize, frame.Level, emitter.NextInstrAddr);
			WriteTableDetails(ast);
			if(frame.Level == Machine.MaximumRoutineLevel)
				errorReporter.ReportRestriction("can't nest routines more than 7 deep");
			else {
				argsSize = ast.Formals.Visit(this, frame.Push(0));
				valSize = ast.Expression.Visit(this, frame.Push(Machine.LinkDataSize));
			}
			emitter.Emit(OpCode.RETURN, (byte)valSize, argsSize);
			emitter.Patch(jumpAddr);
			return 0;
		}

		public int VisitProcDeclaration(ProcDeclaration ast, Frame frame) {
			int argsSize = 0, jumpAddr = emitter.Emit(OpCode.JUMP, Register.CB);
			ast.Entity = new KnownProcedure(Machine.ClosureSize, frame.Level, emitter.NextInstrAddr);
			WriteTableDetails(ast);
			if(frame.Level == Machine.MaximumRoutineLevel)
				errorReporter.ReportRestriction("can't nest routines so deeply");
			else {
				argsSize = ast.Formals.Visit(this, frame.Push(0));
				ast.Command.Visit(this, frame.Push(Machine.LinkDataSize));
			}
			emitter.Emit(OpCode.RETURN, argsSize);
			emitter.Patch(jumpAddr);
			return 0;
		}

		public int VisitConstFormalParameter(ConstFormalParameter ast, Frame frame) {
			int valSize = ast.Type.Visit(this, null);
			ast.Entity = new UnknownValue(valSize, frame.Level, -frame.Size - valSize);
			WriteTableDetails(ast);
			return valSize;
		}

		public int VisitVarFormalParameter(VarFormalParameter ast, Frame frame) {
			ast.Type.Visit(this, null);
			int argSize = Machine.AddressSize;
			ast.Entity = new UnknownAddress(argSize, frame.Level, -frame.Size - argSize);
			WriteTableDetails(ast);
			return Machine.AddressSize;
		}

		public int VisitEmptyFormalParameterSequence(EmptyFormalParameterSequence ast, Frame frame) { return 0; }

		public int VisitMultipleFormalParameterSequence(MultipleFormalParameterSequence ast, Frame frame) {
			int argsSize1 = ast.Formals.Visit(this, frame);
			Frame frame1 = frame.Expand(argsSize1);
			int argsSize2 = ast.Formal.Visit(this, frame1);
			return argsSize1 + argsSize2;
		}

		public int VisitSingleFormalParameterSequence(SingleFormalParameterSequence ast, Frame frame) { return ast.Formal.Visit(this, frame); }

		public int VisitTypeDeclaration(TypeDeclaration ast, Frame frame) {
			// just to ensure the type's representation is decided
			ast.Type.Visit(this, null);
			return 0;
		}

		public int VisitUnaryOperatorDeclaration(UnaryOperatorDeclaration ast, Frame frame) { return 0; }

		public int VisitBinaryOperatorDeclaration(BinaryOperatorDeclaration ast, Frame frame) { return 0; }
	}
}