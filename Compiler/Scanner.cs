/**
 * @author Antoine/Anthony Sébert
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Compiler {
	/**
	 * Split the source file into tokens, checks the validity of the stream on the fly. Does not perform syntax checks.
	 * @see	Token
	 * @see	TokenKind
	 */
	class Scanner : IEnumerable<Token> {
		/* ATTRIBUTES */
			// public
				/**
				 * Holds a boolean set to true if the Scanner runs in debug mode. In that case the process is more verbose.
				 */
				static public bool Debug { get; set; } = true;
			// private
				/**
				 * Holds a boolean set to true if the end of the source file has been reached.
				 */
				static bool atEndOfFile = false;
				/**
				 * Contains the atomic operators.
				 */
				static readonly char[] operators = { '+', '-', '*', '/', '=', '<', '>' },
				/**
				 * Contains the characters or sequences to be ignored by the compiler.
				 */
					ignored = { '!', ' ', '\t', '\n' },
				/**
				 * Contains the special characters.
				 */
					specials = { '.', '!', '?', '_', ' ' }; // is the space character really needed ? looks like it will be trimmed everytime
				/**
				 * Holds a reference to the source file.
				 * @see	SourceFile
				 */
				static SourceFile source = null;
				/**
				 * Contains the characters being processed in order to determine which token to create.
				 */
				static StringBuilder currentSpelling = null;
				/**
				 * Contains the reserved keywords as strings, generated on the fly when the instance is created.
				 * @see	TokenKind
				 */
				static readonly ImmutableDictionary<string, TokenKind> ReservedWords =
					Enumerable.Range((int)TokenKind.Begin, (int)TokenKind.While)
					.Cast<TokenKind>().ToImmutableDictionary(kind => kind.ToString().ToLower(), kind => kind);
		/* MEMBERS */
			// public 
				// constructor
					/**
					 * Builds a {@code Scanner} instance.
					 * @param	_source	reference to the source file to scan.
					 * @see		SourceFile
					 */
					public Scanner(SourceFile _source) {
						source = _source;
						source.Reset();
						currentSpelling = new StringBuilder();
					}
				// interface
					/**
					 * Responsible for the main process of creating a collection of tokens from the source file.
					 * @return	a collection of tokens of type {@code IEnumerator<Token>}.
					 * @see		Token
					 * @see		SourceFile
					 */
					public IEnumerator<Token> GetEnumerator() {
						Location start = null;
						Token token = null;
						TokenKind kind = 0;
						while(!atEndOfFile) {
							currentSpelling.Clear();
							IgnoreUseless();
							start = source._Location;
							kind = ScanToken();
							token = new Token(kind, currentSpelling.ToString(), new SourcePosition(start, source._Location));

							if(kind == TokenKind.EndOfText)
								atEndOfFile = true;
							else if(kind == TokenKind.Error)
								Environment.Exit(1);
							/*
							if(Debug)
								Compiler.Info(typeof(Scanner).Name, token.ToString(), 1);
							*/

							yield return token;
						}
					}
					/**
					 * Returns the collection of tokens built from the source file.
					 */
					IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
			// protected
				/**
				 * Test if a character is an operator.
				 * @param	c	the character to test.
				 * @return	{@code true} if the character matches an operator, {@code false} otherwise.
				 * @see		operators
				 */
				protected bool IsOperator(char c) { return Array.IndexOf(operators, c) != -1; }
				/**
				 * Test if a character or a sequence is to be ignored by the compiler.
				 * @param	c	the character to test.
				 * @return	{@code true} if the character matches an ignored character, {@code false} otherwise.
				 * @see		ignored
				 */
				protected bool IsIgnored(char c) { return Array.IndexOf(ignored, c) != -1; }
				/**
				 * Test if a character is a special character.
				 * @param	c	the character to test.
				 * @return	{@code true} if the character matches a special character, {@code false} otherwise.
				 * @see		specials
				 */
				protected bool IsSpecial(char c) { return Array.IndexOf(specials, c) != -1; }
				/**
				 * Test if a character is a graphic character. It includes digits, letters, operators and special characters.
				 * @param	c	the character to test.
				 * @return	{@code true} if the character is a graphic character, {@code false} otherwise.
				 */
				protected bool IsGraphic(char c) { return char.IsLetterOrDigit(c) || IsOperator(c) || IsSpecial(c); }
				/**
				 * Skips whitespaces and comments in the source file.
				 */
				protected void IgnoreUseless() {
					while(IsIgnored(source.Current)) {
						switch(source.Current) {
							case ' ':
							case '\t':
							case '\n':
								source.MoveNext();
								break;
							default:
								source.SkipRestOfLine();
								break;
						}
					}
				}
			// private
				/**
				 * Determine the token kind to build from the characters processed. Reads the file stream to build the token.
				 * @return	a token kind.
				 * @see		TokenKind
				 */
				private TokenKind ScanToken() {
					// operators + two-characters operators
					if(IsOperator(source.Current)) {
						TakeIt();
						if(IsOperator(source.Current)) {
							if(source.Current == '=')
								TakeIt();
							else {
								TakeIt();
								Compiler.Error(typeof(Scanner).Name, 2, new string[]{
									source._Location.LineNumber.ToString(),
									source._Location.RowNumber.ToString(),
									currentSpelling.ToString()
								}, 1);
							}
						}
						return TokenKind.Operator;
					}
					// integer literal
					if(char.IsDigit(source.Current)) {
						do { TakeIt(); } while(char.IsDigit(source.Current));
						return TokenKind.IntLiteral;
					}
					// identifier
					if(char.IsLetter(source.Current)) {
						do { TakeIt(); } while(char.IsLetter(source.Current) || char.IsDigit(source.Current) || source.Current == '_');
						if(ReservedWords.TryGetValue(currentSpelling.ToString(), out TokenKind reservedWordType))
							return reservedWordType;
						return TokenKind.Identifier;
					}
					switch(source.Current) {
						case default(char):
							return TokenKind.EndOfText;
						case ';':
							TakeIt();
							return TokenKind.Semicolon;
						case ',':
							TakeIt();
							return TokenKind.Colon;
						case '(':
							TakeIt();
							return TokenKind.LeftParenthese;
						case ')':
							TakeIt();
							return TokenKind.RightParenthese;
						case '~':
							TakeIt();
							return TokenKind.Is;
						case ':':
							TakeIt();
							if(source.Current == '=') {
								TakeIt();
								return TokenKind.Becomes;
							}
							return TokenKind.Colon;
						case '\'':
							TakeIt();
							if(source.Current == '\'') {
								TakeIt();
								return TokenKind.CharacterLiteral;
							}
							else {
								if(IsGraphic(source.Current)) {
									TakeIt();
									if(source.Current == '\'') {
										TakeIt();
										return TokenKind.CharacterLiteral;
									}
								}
								TakeIt();
								Compiler.Error(typeof(Scanner).Name, 1, new string[]{
									source._Location.LineNumber.ToString(),
									source._Location.RowNumber.ToString(),
									currentSpelling.ToString()
								}, 1);
								return TokenKind.Error;
							}
						default:
							TakeIt();
							Compiler.Error(typeof(Scanner).Name, 0, new string[]{
								source._Location.LineNumber.ToString(),
								source._Location.RowNumber.ToString(),
								currentSpelling.ToString()
							}, 1);
							return TokenKind.Error;
					}
				}
				/**
				 * Append the current character to the buffer and move to the next character.
				 */
				private void TakeIt() {
					currentSpelling.Append(source.Current);
					source.MoveNext();
				}
	}
};