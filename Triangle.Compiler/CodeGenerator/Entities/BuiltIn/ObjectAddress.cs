namespace Triangle.Compiler.CodeGenerator.Entities {
	public sealed class ObjectAddress {
		public int Level { get; }
		public int Displacement { get; }

		public ObjectAddress(int level, int displacement) {
			Level = level;
			Displacement = displacement;
		}
	}
}