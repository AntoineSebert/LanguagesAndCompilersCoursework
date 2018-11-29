using Triangle.AbstractMachine;
using Triangle.Compiler.SyntaxTrees.Terminals;

namespace Triangle.Compiler.CodeGenerator.Entities {
	public class KnownValue : RuntimeEntity, IFetchableEntity {
		public int Value { get; }

		public KnownValue(int size, int value) : base(size) { Value = value; }

		public void EncodeFetch(Emitter emitter, Frame frame, int size, Identifier identifier) { emitter.Emit(OpCode.LOADL, 0, 0, Value); }
	}
}