using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Compiler {
	class Scanner : IEnumerable<Token> {
		/* ATTRIBUTES */
			// public
				static public bool Debug { get; set; } = true;
			// private
				static bool atEndOfFile = false;
				static readonly char[] operators = { '+', '-', '*', '/', '=', '<', '>' }, specials = { '.', '!', '?', '_', ' ' };
				static SourceFile source = null;
				static StringBuilder currentSpelling = null;
				static readonly ImmutableDictionary<string, TokenKind> ReservedWords =
					Enumerable.Range((int)TokenKind.Begin, (int)TokenKind.While)
					.Cast<TokenKind>().ToImmutableDictionary(kind => kind.ToString().ToLower(), kind => kind);
		/* MEMBERS */
			// public 
				// constructor
					public Scanner(SourceFile _source) {
						source = _source;
						source.Reset();
						currentSpelling = new StringBuilder();
					}
				// interface
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

							if(Debug)
								Console.WriteLine(token);

							yield return token;
						}
					}
					IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
			// protected
				protected bool IsOperator(char c) { return Array.IndexOf(operators, c) != -1; }
				protected bool IsSpecial(char c) { return Array.IndexOf(special, c) != -1; }
				protected bool IsGraphic(char c) { return Char.IsLetterOrDigit(c) || IsOperator(c) || IsSpecial(c); }
				protected void IgnoreUseless() {
					while(source.Current == '!' || source.Current == ' ' || source.Current == '\t' || source.Current == '\n') {
						switch(source.Current) {
							case '!':
								source.SkipRestOfLine();
								break;
							case ' ':
							case '\t':
							case '\n':
								source.MoveNext();
								break;
						}
					}
				}
			// private
				private TokenKind ScanToken() {
				/*
					if(IsOperator(source.Current)) {
						TakeIt();
						if(IsOperator(source.Current))
							TakeIt();
						return TokenKind.Operator;
					}
					if(char.IsDigit(source.Current)) {
						TakeIt();
						return TokenKind.IntLiteral;
					}
					if(char.IsLetter(source.Current)) {
						do {
							TakeIt();
						}
						while(char.IsLetter(source.Current) || char.IsDigit(source.Current));
						if(ReservedWords.TryGetValue(currentSpelling.ToString(), out TokenKind reservedWordType))
							return reservedWordType;
						return TokenKind.Identifier;
					}
					*/
					switch(source.Current) {
				/*
						case default(char):
							return TokenKind.EndOfText;
						case ';':
							TakeIt();
							return TokenKind.Semicolon;
						case ',':
							TakeIt();
							return TokenKind.Colon;
						case '{':
							TakeIt();
							return TokenKind.LeftBracket;
						case '}':
							TakeIt();
							return TokenKind.RightBracket;
						case '(':
							TakeIt();
							return TokenKind.LeftParenthese;
						case ')':
							TakeIt();
							return TokenKind.RightParenthese;
						case ':':
							TakeIt();
							if(IsOperator(source.Current)) {
								TakeIt();
								return TokenKind.Operator;
							}
							return TokenKind.Becomes;
						case '\'':
							source.MoveNext();
							if(source.Current == '\'') {
								source.MoveNext();
								return TokenKind.CharacterLiteral;
							}
							else {
								TakeIt();
								if(source.Current == '\'') {
									source.MoveNext();
									return TokenKind.CharacterLiteral;
								}
								Console.WriteLine("Scanning error : ill-formed character literal");
								return TokenKind.Error;
							}
					*/
						default:
							TakeIt();
							return TokenKind.Error;
					}
				}
				private void TakeIt() {
					currentSpelling.Append(source.Current);
					source.MoveNext();
				}
	}
};