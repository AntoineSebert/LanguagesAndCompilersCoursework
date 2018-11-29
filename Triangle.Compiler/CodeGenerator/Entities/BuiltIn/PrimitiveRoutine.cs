using Triangle.AbstractMachine;

namespace Triangle.Compiler.CodeGenerator.Entities {
	public class PrimitiveRoutine : RuntimeEntity, IProcedureEntity {
		public Primitive Primitive { get; }

		public PrimitiveRoutine(int size, Primitive primitive) : base(size) { Primitive = primitive; }

		public void EncodeCall(Emitter emitter, Frame frame) {
			if(Primitive != Primitive.ID)
				emitter.Emit(OpCode.CALL, Register.SB, Register.PB, Primitive);
		}

		public void EncodeFetch(Emitter emitter, Frame frame) {
			emitter.Emit(OpCode.LOADA, 0, Register.SB, 0);
			emitter.Emit(OpCode.LOADA, 0, Register.PB, Primitive);
		}
	}
}