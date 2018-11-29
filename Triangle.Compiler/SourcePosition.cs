namespace Triangle.Compiler {
	public struct SourcePosition {
		public static SourcePosition Empty { get; } = new SourcePosition(Location.Empty, Location.Empty);
		public Location Start { get; }
		public Location Finish { get; }
		public SourcePosition(Location start, Location finish) {
			Start = start;
			Finish = finish;
		}
		public override string ToString() { return $"{Start}..{Finish}"; }
	}
}