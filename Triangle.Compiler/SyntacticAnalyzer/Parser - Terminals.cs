using Triangle.Compiler.SyntaxTrees.Terminals;

namespace Triangle.Compiler.SyntacticAnalyzer {
	public partial class Parser {
		///////////////////////////////////////////////////////////////////////////////
		//
		// TERMINALS
		//
		///////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Parses an integer-literal, and constructs a leaf AST to represent it.
		/// </summary>
		/// <returns>
		/// an <link>Triangle.SyntaxTrees.Terminals.IntegerLiteral</link>
		/// </returns>
		/// <throws type="SyntaxError">
		/// a syntactic error
		/// </throws>
		private IntegerLiteral ParseIntegerLiteral() {
			Compiler.WriteDebuggingInfo("Parsing Integer Literal");
			return new IntegerLiteral(Accept(TokenKind.IntLiteral));
		}
		/**
		 * Parses a character-literal, and constructs a leaf AST to represent it.
		 * @return	a {@link triangle.compiler.syntax.trees.terminals.CharacterLiteral}
		 * @throws	SyntaxError	a syntactic error
		 */
		private CharacterLiteral ParseCharacterLiteral() {
			Compiler.WriteDebuggingInfo("Parsing Character Literal");
			return new CharacterLiteral(Accept(TokenKind.CharLiteral));
		}
		/**
		 * Parses an identifier, and constructs a leaf AST to represent it.
		 * @return	an {@link triangle.compiler.syntax.trees.terminals.Identifier}
		 * @throws	SyntaxError	a syntactic error
		 */
		private Identifier ParseIdentifier() {
			Compiler.WriteDebuggingInfo("Parsing Identifier");
			return new Identifier(Accept(TokenKind.Identifier));
		}
		/**
		 * Parses an operator, and constructs a leaf AST to represent it.
		 * @return an {@link triangle.compiler.syntax.trees.terminals.Operator}
		 * @throws	SyntaxError	a syntactic error
		 */
		private Operator ParseOperator() {
			Compiler.WriteDebuggingInfo("Parsing Operator");
			return new Operator(Accept(TokenKind.Operator));
		}
	}
}