using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Parameters;
using Triangle.Compiler.SyntaxTrees.Terminals;

namespace Triangle.Compiler.SyntacticAnalyzer {
	public partial class Parser {
		///////////////////////////////////////////////////////////////////////////////
		//
		// EXPRESSIONS
		//
		///////////////////////////////////////////////////////////////////////////////
		/// Parses the expression, and constructs an AST to represent its phrase structure
		/// @return	an {@link Triangle.SyntaxTrees.Expressions.Expression}
		/// @throws	SyntaxError
		private Expression ParseExpression() {
			Compiler.WriteDebuggingInfo("Parsing Expression");
			Location startLocation = tokens.Current.Start;
			Expression expression = ParseSecondaryExpression();
			if(tokens.Current.Kind == TokenKind.QuestionMark) {
				Compiler.WriteDebuggingInfo("Parsing Ternary Expression");
				AcceptIt();
				Expression exp_2 = ParseExpression();
				Accept(TokenKind.Colon);
				Expression exp_3 = ParseExpression();
				SourcePosition position = new SourcePosition(startLocation, tokens.Current.Finish);
				expression = new TernaryExpression(expression, exp_2, exp_3, position);
			}
			return expression;
		}
		/// Parses the secondary expression, and constructs an AST to represent its phrase structure.
		/// @return	an {@link Triangle.SyntaxTrees.Expressions.Expression}
		/// @throws	SyntaxError
		private Expression ParseSecondaryExpression() {
			Compiler.WriteDebuggingInfo("Parsing Secondary Expression");
			Location startLocation = tokens.Current.Start;
			Expression primaryExpression = ParsePrimaryExpression();
			while(tokens.Current.Kind == TokenKind.Operator) {
				Operator op = ParseOperator();
				Expression p_exp = ParsePrimaryExpression();
				SourcePosition expressionPos = new SourcePosition(startLocation, tokens.Current.Finish);
				primaryExpression = new BinaryExpression(primaryExpression, op, p_exp, expressionPos);
			}
			return primaryExpression;
		}
		/// Parses the primary expression, and constructs an AST to represent its phrase structure.
		/// @return	an {@link Triangle.SyntaxTrees.Expressions.Expression}
		/// @throws	SyntaxError
		private Expression ParsePrimaryExpression() {
			Compiler.WriteDebuggingInfo("Parsing Primary Expression");
			Location startLocation = tokens.Current.Start;
			switch(tokens.Current.Kind) {
				case TokenKind.IntLiteral: {
					Compiler.WriteDebuggingInfo("Parsing Int Expression");
					IntegerLiteral il =  ParseIntegerLiteral();
					SourcePosition expressionPos = new SourcePosition(startLocation, tokens.Current.Finish);
					return new IntegerExpression(il, expressionPos);
				}
				case TokenKind.CharLiteral: {
					Compiler.WriteDebuggingInfo("Parsing Char Expression");
					CharacterLiteral cl = ParseCharacterLiteral();
					SourcePosition expressionPos = new SourcePosition(startLocation, tokens.Current.Finish);
					return new CharacterExpression(cl, expressionPos);
				}
				case TokenKind.Identifier: {
					Compiler.WriteDebuggingInfo("Parsing Call Expression or Identifier Expression");
					Identifier id = ParseIdentifier();
					if(tokens.Current.Kind == TokenKind.LeftBracket) {
						Compiler.WriteDebuggingInfo("Parsing Call Expression");
						AcceptIt();
						ParameterSequence ps = ParseParameters();
						Accept(TokenKind.RightBracket);
						SourcePosition expressionPos = new SourcePosition(startLocation, tokens.Current.Finish);
						return new CallExpression(id, ps, expressionPos);
					}
					else {
						Compiler.WriteDebuggingInfo("Parsing Identifier Expression");
						SourcePosition expressionPos = new SourcePosition(startLocation, tokens.Current.Finish);
						return new IdExpression(id, expressionPos);
					}
				}
				case TokenKind.Operator: {
					Compiler.WriteDebuggingInfo("Parsing Unary Expression");
					Operator op = ParseOperator();
					Expression p_exp = ParsePrimaryExpression();
					SourcePosition expressionPos = new SourcePosition(startLocation, tokens.Current.Finish);
					return new UnaryExpression(op, p_exp, expressionPos);
				}
				case TokenKind.LeftBracket: {
					Compiler.WriteDebuggingInfo("Parsing Bracket Expression");
					AcceptIt();
					Expression exp = ParseExpression();
					Accept(TokenKind.RightBracket);
					return  exp;
				}
				default: {
					RaiseSyntacticError("\"%\" cannot start an expression", tokens.Current);
					return null;
				}
			}
		}
	}
}