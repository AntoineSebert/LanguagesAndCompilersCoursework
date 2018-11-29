using Triangle.Compiler.SyntaxTrees.Terminals;

namespace Triangle.Compiler.CodeGenerator.Entities {
	public abstract class AddressableEntity : RuntimeEntity, IFetchableEntity {
		public ObjectAddress Address { get; }

		protected AddressableEntity(int size, int level, int displacement) : base(size) { Address = new ObjectAddress(level, displacement); }

		protected AddressableEntity(int size, Frame frame) : this(size, frame.Level, frame.Size) { }

		public abstract void EncodeAssign(Emitter emitter, Frame frame, int size, Identifier identifier);

		public abstract void EncodeFetchAddress(Emitter emitter, Frame frame, Identifier identifier);

		public abstract void EncodeFetch(Emitter emitter, Frame frame, int size, Identifier identifier);
	}
}