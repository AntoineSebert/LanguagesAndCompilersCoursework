namespace Triangle.Compiler {
	public struct Location {
		public static Location Empty { get; } = new Location(0, 0);
		public int Line { get; }
		public int Column { get; }
		public Location(int line, int column) {
			Line = line;
			Column = column;
		}
		public override string ToString() { return $"({Line},{Column})"; }
	}
}