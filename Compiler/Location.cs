/**
 * @author Antoine/Anthony Sébert
 */

namespace Compiler {
	/**
	 * Represents a position in the file.
	 * @see	SourceFile
	 */
	public class Location {
		/* ATTRIBUTES */
			/**
			 * Holds the line number.
			 */
			public uint LineNumber { get; } = 0;
			/**
			 * Holds the row number.
			 */
			public uint RowNumber { get; } = 0;
		/* METHODS */
			/**
			 * Builds a {@code Location} object from two {@code uint}.
			 * @param	_LineNumber	the line number
			 * @param	_RowNumber	the row number
			 */
			public Location(uint _LineNumber, uint _RowNumber) {
				LineNumber = _LineNumber;
				RowNumber = _RowNumber;
			}
			/**
			 * Overrides the cast of a {@code Location} object to a {@code string}.
			 */
			public override string ToString() { return $"{LineNumber}:{RowNumber}"; }
	}
}