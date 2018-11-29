using Triangle.AbstractMachine;
using Triangle.Compiler.SyntaxTrees.Terminals;

namespace Triangle.Compiler.CodeGenerator.Entities {
	public class UnknownValue : RuntimeEntity, IFetchableEntity {
		public ObjectAddress Address { get; }

		public UnknownValue(int size, int level, int displacement) : base(size) { Address = new ObjectAddress(level, displacement); }

		public UnknownValue(int size, Frame frame) : this(size, frame.Level, frame.Size) {}

		public void EncodeFetch(Emitter emitter, Frame frame, int size, Identifier identifier) {
			emitter.Emit(OpCode.LOAD, size, frame.DisplayRegister(Address), Address.Displacement);
		}
	}
}