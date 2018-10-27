namespace Compiler {
	public class Location {
		/* ATTRIBUTES */
			public uint LineNumber { get; } = 0;
			public uint RowNumber { get; } = 0;
		/* METHODS */
			public Location(uint _LineNumber, uint _RowNumber) {
				LineNumber = _LineNumber;
				RowNumber = _RowNumber;
			}
			public override string ToString() { return $"{LineNumber}:{RowNumber}"; }
	}
}