namespace Compiler {
	class SourcePosition {
		/* ATTRIBUTES */
			public Location Start = null, End = null;
		/* MEMBERS */
			public SourcePosition(Location _Start, Location _End) {
				Start = _Start;
				End = _End;
			}
			public override string ToString() { return $"{Start} -- {End}"; }
	}
}