namespace Triangle.Compiler.CodeGenerator.Entities {
	public class Field : RuntimeEntity {
		public int FieldOffset { get; }

		public Field(int size, int fieldOffset) : base(size) { FieldOffset = fieldOffset; }
	}
}