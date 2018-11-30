using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Types;

namespace Triangle.Compiler.SyntacticAnalyzer {
	public partial class Parser {
		///////////////////////////////////////////////////////////////////////////////
		// DECLARATIONS
		///////////////////////////////////////////////////////////////////////////////
		/**
		 * Parses the declaration, and constructs an AST to represent its phrase structure.
		 * @return	a {@link triangle.compiler.syntax.trees.declarations.Declaration}
		 * @throws	SyntaxError	a syntactic error
		 */
		private Declaration ParseDeclaration() {
			Compiler.WriteDebuggingInfo("Parsing Declaration");
			Location startLocation = tokens.Current.Start;
			Declaration declaration = ParseSingleDeclaration();
			while(tokens.Current.Kind == TokenKind.Semicolon) {
				AcceptIt();
				Declaration sd = ParseSingleDeclaration();
				SourcePosition declarationPosition = new SourcePosition(startLocation, tokens.Current.Finish);
				declaration = new SequentialDeclaration(declaration, sd, declarationPosition);
			}
			return declaration;
		}
		/**
		 * Parses the single declaration, and constructs an AST to represent its phrase structure.
		 * @return	a {@link triangle.compiler.syntax.trees.declarations.Declaration}
		 * @throws	SyntaxError	a syntactic error
		 */
		private Declaration ParseSingleDeclaration() {
			Compiler.WriteDebuggingInfo("Parsing Single Declaration");
			Location startLocation = tokens.Current.Start;
			switch(tokens.Current.Kind) {
				case TokenKind.Const: {
					AcceptIt();
					Identifier i = ParseIdentifier();
					Accept(TokenKind.Is);
					Expression e = ParseExpression();
					SourcePosition declarationPosition = new SourcePosition(startLocation, tokens.Current.Finish);
					return new ConstDeclaration(i, e, declarationPosition);
				}
				case TokenKind.Var: {
					AcceptIt();
					Identifier i = ParseIdentifier();
					Accept(TokenKind.Colon);
					TypeDenoter t = ParseTypeDenoter();
					Expression e = null;
					if(tokens.Current.Kind == TokenKind.Becomes) {
						AcceptIt();
						e = ParseExpression();
						return new InitDeclaration(i, t, e, new SourcePosition(startLocation, tokens.Current.Finish));
					}
					SourcePosition declarationPosition = new SourcePosition(startLocation, tokens.Current.Finish);
					return new VarDeclaration(i, t, declarationPosition);
				}
				default:
					RaiseSyntacticError("\"%\" cannot start a declaration", tokens.Current);
					return null;
			}
		}
	}
}