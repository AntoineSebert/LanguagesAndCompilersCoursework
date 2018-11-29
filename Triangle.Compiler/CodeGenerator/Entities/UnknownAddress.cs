using Triangle.AbstractMachine;
using Triangle.Compiler.SyntaxTrees.Terminals;

namespace Triangle.Compiler.CodeGenerator.Entities {
	public class UnknownAddress : AddressableEntity {
		public UnknownAddress(int size, int level, int displacement) : base(size, level, displacement) {}

		public override void EncodeAssign(Emitter emitter, Frame frame, int size, Identifier identifier) {
			emitter.Emit(OpCode.LOAD, Machine.AddressSize, frame.DisplayRegister(Address), Address.Displacement);
			emitter.Emit(OpCode.STOREI, size, 0, 0);
		}

		public override void EncodeFetch(Emitter emitter, Frame frame, int size, Identifier identifier) {
			emitter.Emit(OpCode.LOAD, Machine.AddressSize, frame.DisplayRegister(Address));
			emitter.Emit(OpCode.LOADI, size);
		}

		public override void EncodeFetchAddress(Emitter emitter, Frame frame, Identifier identifiere) {
			emitter.Emit(OpCode.LOAD, Machine.AddressSize, frame.DisplayRegister(Address), Address.Displacement);
		}
	}
}