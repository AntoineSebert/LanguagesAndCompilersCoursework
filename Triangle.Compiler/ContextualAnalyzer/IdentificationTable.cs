using System.Collections.Generic;
using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Terminals;

namespace Triangle.Compiler.ContextualAnalyzer {
	public sealed class IdentificationTable {
		private readonly Stack<Dictionary<string, Declaration>> scopes = null;
		public IdentificationTable() { scopes = new Stack<Dictionary<string, Declaration>>(1); }
		// Opens a new level in the identification table, 1 higher than the current topmost level.
		public void OpenScope() { scopes.Push(new Dictionary<string, Declaration>()); }
		// Closes the topmost level in the identification table, discarding all entries belonging to that level.
		public void CloseScope() { scopes.Pop(); }
		/**
		 * Makes a new entry in the identification table for the given terminal and attribute.
		 * The new entry belongs to the current level. duplicated is set to true if there is already an entry for the same identifier at the current level.
		 * @param	terminal	the terminal symbol
		 * @param	attr		the attribute
		 */
		public void Enter(Terminal terminal, Declaration attr) {
			if(scopes.Peek().ContainsKey(terminal.Spelling))
				attr.Duplicated = true;
			else
				scopes.Peek().Add(terminal.Spelling, attr);
			}
		/**
		 * Finds an entry for the given identifier in the identification table, if any.
		 * If there are several entries for that identifier, finds the entry at the highest level, in accordance with the scope rules.
		 * @param	id	the identifier of the declaration to retrieve
		 * @return	the matching declaration or null if none exists
		 */
		public Declaration Retrieve(string id) {
			foreach(Dictionary<string, Declaration> scope in scopes)
				if(scope.TryGetValue(id, out Declaration attr))
					return attr;
			return null;
		}
	}
}