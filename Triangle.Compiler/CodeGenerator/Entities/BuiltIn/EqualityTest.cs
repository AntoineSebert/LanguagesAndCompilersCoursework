using Triangle.AbstractMachine;

namespace Triangle.Compiler.CodeGenerator.Entities {
	public class EqualityProcedure : RuntimeEntity, IProcedureEntity {
		public Primitive Primitive { get; }

		public EqualityProcedure(int size, Primitive primitive) : base(size) { Primitive = primitive; }

		public void EncodeCall(Emitter emitter, Frame frame) {
			emitter.Emit(OpCode.LOADL, frame.Size / 2);
			emitter.Emit(OpCode.CALL, Register.SB, Register.PB, Primitive);
		}

		public void EncodeFetch(Emitter emitter, Frame frame) {
			emitter.Emit(OpCode.LOADA, 0, Register.SB, 0);
			emitter.Emit(OpCode.LOADA, 0, Register.PB, Primitive);
		}
	}
}