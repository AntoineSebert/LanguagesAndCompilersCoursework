/**
 * @author Antoine/Anthony Sébert
 */

namespace Compiler {
	/**
	 * Represents the start and end positions of an element in the file.
	 * @see	Location
	 */
	class SourcePosition {
		/* ATTRIBUTES */
			/**
			 * Represents the start position of the element.
			 */
			public Location Start = null, 
			/**
			 * Represents the end position of the element.
			 */
				End = null;
		/* MEMBERS */
			/**
			 * Builds a {@code SourcePosition} object from two {@code Location} objects.
			 * @param	_Start	the start position
			 * @param	_End	the end position
			 */
			public SourcePosition(Location _Start, Location _End) {
				Start = _Start;
				End = _End;
			}
			/**
			 * Overrides the cast of a {@code SourcePosition} object to a {@code string}.
			 */
			public override string ToString() { return $"{Start} -- {End}"; }
	}
}