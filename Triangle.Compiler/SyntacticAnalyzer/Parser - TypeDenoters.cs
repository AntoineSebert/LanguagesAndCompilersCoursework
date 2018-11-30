using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Types;

namespace Triangle.Compiler.SyntacticAnalyzer {
	public partial class Parser {
		// /////////////////////////////////////////////////////////////////////////////
		// TYPE-DENOTERS
		// /////////////////////////////////////////////////////////////////////////////
		/**
		 * Parses the type denoter, and constructs an AST to represent its phrase structure.
		 * @return	a {@link triangle.compiler.syntax.trees.types.TypeDenoter}
		 * @throws	SyntaxError	a syntactic error
		 */
		private TypeDenoter ParseTypeDenoter() {
			Compiler.WriteDebuggingInfo("Parsing Type Denoter");
			Location startLocation = tokens.Current.Start;

			switch(tokens.Current.Kind) {
				case TokenKind.Identifier: {
					Identifier i = ParseIdentifier();
					SourcePosition typePosition = new SourcePosition(startLocation, tokens.Current.Finish);
					return new SimpleTypeDenoter(i, typePosition);
				}
				default: {
					RaiseSyntacticError("\"%\" cannot start a type denoter", tokens.Current);
					return null;
				}
			}
		}
	}
}