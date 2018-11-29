using Triangle.Compiler.SyntaxTrees.Commands;
using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Parameters;
using Triangle.Compiler.SyntaxTrees.Terminals;

namespace Triangle.Compiler.SyntacticAnalyzer {
	public partial class Parser {
		///////////////////////////////////////////////////////////////////////////////
		// COMMANDS
		///////////////////////////////////////////////////////////////////////////////
		/// Parses the command, and constructs an AST to represent its phrase structure.
		/// @return	a <link>Triangle.SyntaxTrees.Commands.Command</link>
		/// @throw	SyntaxError
		private Command ParseCommand() {
			Compiler.WriteDebuggingInfo("Parsing Command");
			Location startLocation = tokens.Current.Start;
			Command command = ParseSingleCommand();
			while(tokens.Current.Kind == TokenKind.Semicolon) {
				AcceptIt();
				Command command2 = ParseSingleCommand();
				SourcePosition commandPosition = new SourcePosition(startLocation, tokens.Current.Finish);
				command = new SequentialCommand(command, command2, commandPosition);
			}
			return command;
		}
		/// Parses the single command, and constructs an AST to represent its phrase structure.
		/// @returns	a {@link triangle.compiler.syntax.trees.commands.Command}
		/// @throw		syntaxError
		private Command ParseSingleCommand() {
			Compiler.WriteDebuggingInfo("Parsing Single Command");
			Location startLocation = tokens.Current.Start;

			switch(tokens.Current.Kind) {
				case TokenKind.Identifier: {
					Compiler.WriteDebuggingInfo("Parsing Assignment Command or Call Command");
					Identifier identifier = ParseIdentifier();
					if(tokens.Current.Kind == TokenKind.LeftBracket) {
						Compiler.WriteDebuggingInfo("Parsing Call Command");
						AcceptIt();
						ParameterSequence parameters = ParseParameters();
						Accept(TokenKind.RightBracket);
						return new CallCommand(identifier, parameters,  new SourcePosition(startLocation, tokens.Current.Finish));
					}
					else if(tokens.Current.Kind == TokenKind.Becomes) {
						Compiler.WriteDebuggingInfo("Parsing Assignment Command");
						Accept(TokenKind.Becomes);
						Expression expression = ParseExpression();
						return new AssignCommand(identifier, expression,  new SourcePosition(startLocation, tokens.Current.Finish));
					}
					else {
						RaiseSyntacticError("Expected either an assignment or function call but found a \"%\"", tokens.Current);
						return null;
					}
				}
				case TokenKind.Begin: {
					Compiler.WriteDebuggingInfo("Parsing Begin Command");
					AcceptIt();
					Command command = ParseCommand();
					Accept(TokenKind.End);
					return command;
				}
				case TokenKind.Let: {
					Compiler.WriteDebuggingInfo("Parsing Let Command");
					AcceptIt();
					Declaration declaration = ParseDeclaration();
					Accept(TokenKind.In);
					Command c = ParseSingleCommand();
					SourcePosition commandPosition = new SourcePosition(startLocation, tokens.Current.Finish);
					return c;
				}
				case TokenKind.If: {
					Compiler.WriteDebuggingInfo("Parsing If Command");
					AcceptIt();
					ParseExpression();
					Accept(TokenKind.Then);
					ParseSingleCommand();
					Accept(TokenKind.Else);
					ParseSingleCommand();
					SourcePosition commandPosition = new SourcePosition(startLocation, tokens.Current.Finish);
					return null;
				}
				case TokenKind.While: {
					Compiler.WriteDebuggingInfo("Parsing While Command");
					AcceptIt();
					ParseExpression();
					Accept(TokenKind.Do);
					ParseSingleCommand();
					SourcePosition commandPosition = new SourcePosition(startLocation, tokens.Current.Finish);
					return null;
				}
				case TokenKind.Skip: {
					Compiler.WriteDebuggingInfo("Parsing Skip Command");
					AcceptIt();
					SourcePosition commandPosition = new SourcePosition(startLocation, tokens.Current.Finish);
					return null;
				}
				default:
					RaiseSyntacticError("\"%\" cannot start a command", tokens.Current);
					return null;
			}
		}
	}
}