using Triangle.AbstractMachine;
using Triangle.Compiler.SyntaxTrees.Terminals;

namespace Triangle.Compiler.CodeGenerator.Entities {
	public class KnownAddress : AddressableEntity {
		public KnownAddress(int size, int level, int displacement) : base(size, level, displacement) {}

		public KnownAddress(int size, Frame frame) : base(size, frame) {}

		public override void EncodeAssign(Emitter emitter, Frame frame, int size, Identifier identifier) {
			emitter.Emit(OpCode.STORE, size, frame.DisplayRegister(Address), Address.Displacement);
		}

		public override void EncodeFetch(Emitter emitter, Frame frame, int size, Identifier identifier) {
			emitter.Emit(OpCode.LOAD, frame.DisplayRegister(Address), Address.Displacement);
		}

		public override void EncodeFetchAddress(Emitter emitter, Frame frame, Identifier identifier) {
			emitter.Emit(OpCode.LOADA, frame.DisplayRegister(Address), Address.Displacement);
		}
	}
}