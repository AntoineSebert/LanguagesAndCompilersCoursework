namespace Triangle.Compiler.CodeGenerator.Entities {
	public abstract class RuntimeEntity {
		public int Size { get; }

		protected RuntimeEntity(int size) { Size = size; }
	}
}