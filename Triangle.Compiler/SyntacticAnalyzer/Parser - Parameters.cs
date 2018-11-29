using Triangle.Compiler.SyntaxTrees.Parameters;

namespace Triangle.Compiler.SyntacticAnalyzer {
	public partial class Parser {
		///////////////////////////////////////////////////////////////////////////////
		// PARAMETERS
		///////////////////////////////////////////////////////////////////////////////
		private ParameterSequence ParseParameters() {
			Compiler.WriteDebuggingInfo("Parsing Parameters");
			Location startLocation = tokens.Current.Start;
			if(tokens.Current.Kind == TokenKind.RightBracket)
				return new EmptyParameterSequence(new SourcePosition(startLocation, tokens.Current.Finish));
			ParameterSequence ps = new SingleParameterSequence(ParseParameter(), new SourcePosition(startLocation, tokens.Current.Finish));
			while(tokens.Current.Kind == TokenKind.Comma) {
				AcceptIt();
				ps = new MultipleParameterSequence(ParseParameter(), ps, new SourcePosition(startLocation, tokens.Current.Finish));
			}
			return ps;
		}
		private Parameter ParseParameter() {
			Compiler.WriteDebuggingInfo("Parsing Parameter");
			Location startLocation = tokens.Current.Position.Start;
			switch(tokens.Current.Kind) {
				case TokenKind.Identifier:
				case TokenKind.IntLiteral:
				case TokenKind.CharLiteral:
				case TokenKind.Operator:
				case TokenKind.LeftBracket: {
					Compiler.WriteDebuggingInfo("Parsing Value Parameter");
					AcceptIt();
					return new ValueParameter(ParseExpression(), new SourcePosition(startLocation, tokens.Current.Position.Finish));
				}
				case TokenKind.Var: {
					Compiler.WriteDebuggingInfo("Parsing Variable Parameter");
					AcceptIt();
					return new VarParameter(ParseIdentifier(), new SourcePosition(startLocation, tokens.Current.Position.Finish));
				}
				default: {
					RaiseSyntacticError("\"%\" cannot start a parameter", tokens.Current);
					return null;
				}
			}
		}
	}
}