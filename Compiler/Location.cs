namespace Compiler {
	class Location {
		/* ATTRIBUTES */
			private uint LineNumber { get; } = 0;
			private uint RowNumber { get; } = 0;
		/* METHODS */
			public Location(uint _LineNumber, uint _RowNumber) {
				LineNumber = _LineNumber;
				RowNumber = _RowNumber;
			}
			public override string ToString() { return $"Line {LineNumber}, Column {RowNumber}"; }
	}
}