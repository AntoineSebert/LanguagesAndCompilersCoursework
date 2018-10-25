using System;
using System.Collections.Generic;

namespace Compiler {
	class Parser {
		/* ATTRIBUTES */
			private Scanner scanner = null;
			private IEnumerator<Token> tokens = null;
		/* MEMBERS */
			// public
				// constructor
					/**
					 * Build a Parser instance.
					 * @param _scanner	Scanner from which to perform the parsing process.
					 */
					public Parser(Scanner _scanner) {
						scanner = _scanner;
						//errorReporter = errorReporter;
						//previousLocation = Location.Empty;
						tokens = scanner.GetEnumerator();
					}
					/**
					 * Parses a program from the beginning to the end.
					 */
				// program parsing
					public void ParseProgram() {
						Console.WriteLine("parsing Program");
						tokens.MoveNext();
						var startLocation = tokens.Current.Position.Start;
						ParseCommand();
					}
			// protected
				// ...
					/**
					 * Checks that the given token matches the current stream of tokens, if not prints an error.
					 * @param expectedKinds	an array of expected token kinds.
					 */
					protected void Accept(params TokenKind[] expectedKinds) {
						for(uint i = 0; i < expectedKinds.Length; i++) {
							if(tokens.Current.Kind == expectedKinds[i]) {
								//previousLocation = tokens.Current.Position.Start;
								tokens.MoveNext();
							}
							else
								Console.WriteLine($"error : {expectedKinds[i]} expected, {tokens.Current.Kind} found");
						}
					}
					/**
					 * Just Fetches the next token from the source file.
					 */
					protected void AcceptIt() {
						//previousLocation = tokens.Current.Position.End;
						tokens.MoveNext();
					}
				// value-or-variable nae parsing
					protected void ParseVname() {
						Console.WriteLine("parsing variable name");
						ParseIdentifier();
					}
				// terminals parsing
					// Parses an identifier, and constructs a leaf AST to represent it.
					protected void ParseIdentifier() {
						Console.WriteLine("parsing identifier");
						Accept(TokenKind.Identifier);
					}
					protected void ParseOperator() {
						Console.WriteLine("parsing operator");
						switch(tokens.Current.Spelling) {
							case "+":
							case "-":
							case "/":
							case "*":
							case "<":
							case ">":
								AcceptIt();
								break;
							default:
								Console.WriteLine($"error : '{tokens.Current.Spelling}' found");
								break;
						}
					}
					protected void ParseIntLiteral() {
						Console.WriteLine("parsing identifier");
					}
				// command parsing
					/// Parses the command
					protected void ParseCommand() {
						Console.WriteLine("parsing command");
						ParseSingleCommand();
						while(tokens.Current.Kind == TokenKind.Semicolon) {
							AcceptIt();
							ParseSingleCommand();
						}
					}
					protected void ParseExpression() {
						Console.WriteLine("parsing expression");
						ParsePrimaryExpression();
						while(tokens.Current.Kind == TokenKind.Operator) {
							AcceptIt();
							ParsePrimaryExpression();
						}
					}
					protected void ParsePrimaryExpression() {
						Console.WriteLine("parsing primary expression");
						switch(tokens.Current.Kind) {
							case TokenKind.IntLiteral:
								AcceptIt();
								break;
							case TokenKind.Identifier:
								AcceptIt();
								ParseVname();
								break;
							case TokenKind.Operator:
								AcceptIt();
								ParsePrimaryExpression();
								break;
							case TokenKind.LeftParenthese:
								AcceptIt();
								ParseExpression();
								Accept(TokenKind.RightParenthese);
								break;
							default:
								Console.WriteLine("error : " + tokens.Current.Kind + " found");
								break;
						}
					}
					protected void ParseDeclaration() {
						Console.WriteLine("parsing declaration");
						ParsingSingleDeclaration();
						while(tokens.Current.Kind == TokenKind.Semicolon) {
							AcceptIt();
							ParsingSingleDeclaration();
						}
					}
					protected void ParsingSingleDeclaration() {
						Console.WriteLine("parsing single declaration");
						switch(tokens.Current.Kind) {
							case TokenKind.Const:
								AcceptIt();
								ParseIdentifier();
								if(tokens.Current.Spelling == "~")
									AcceptIt();
								ParseExpression();
								break;
							case TokenKind.Var:
								AcceptIt();
								ParseIdentifier();
								if(tokens.Current.Spelling == ":")
									AcceptIt();
								ParseIdentifier();
								break;
							default:
								Console.WriteLine("error : " + tokens.Current.Kind + " found");
								break;
						}
					}
					// Parses the single command
					protected void ParseSingleCommand() {
						Console.WriteLine("parsing single command");
						switch(tokens.Current.Kind) {
							case TokenKind.Identifier: {
								AcceptIt();
								ParseVname();
								Accept(TokenKind.Becomes);
								ParseExpression();
								Accept(TokenKind.LeftParenthese);
								ParseExpression();
								Accept(TokenKind.RightParenthese);
								break;
							}
							case TokenKind.Begin:
								AcceptIt();
								ParseCommand();
								Accept(TokenKind.End);
								break;
							case TokenKind.If:
								AcceptIt();
								ParseExpression();
								Accept(TokenKind.Then);
								ParseSingleCommand();
								Accept(TokenKind.Else);
								ParseSingleCommand();
								break;
							case TokenKind.While:
								AcceptIt();
								ParseExpression();
								Accept(TokenKind.Do);
								ParseSingleCommand();
								break;
							case TokenKind.Let:
								AcceptIt();
								ParseDeclaration();
								Accept(TokenKind.In);
								ParseSingleCommand();
								break;
							default:
								Console.WriteLine("error : " + tokens.Current.Kind + " found");
								break;
						}
				}
	}
}
