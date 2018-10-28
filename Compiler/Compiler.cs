﻿/**
 * @author	Antoine/Anthony Sébert
 */

using System;
using System.Collections.Generic;

namespace Compiler {
	/**
	 * Holds a scanner, a parser, and the source file is responsible for the whole process.
	 * @see	Scanner
	 * @see	Parser
	 */
	class Compiler {
		/* ATTRIBUTES */
			/**
			 * Holds a Scanner object, initialised in #constructor(string)
			 * @see	Scanner
			 */
			readonly Scanner scanner = null;
			/**
			 * Holds a Parser object, initialised in #constructor(string)
			 * @see	Parser
			 */
			readonly Parser parser = null;
			/**
			 * Holds the source code file, initialised in #constructor(string)
			 * @see	SourceFile
			 */
			readonly SourceFile source = null;
			/**
			 * A collection representing the source code as tokens.
			 * @see	SourceFile
			 */
			private List<Token> collection = null;
		/* MEMBERS */
			// constructor
				/**
				 * Builds a Compiler instance, containing a {@code SourceFile}, a  {@code Scanner} and a  {@code Parser}.
				 * @param	filename	name of the file to compile
				 */
				public Compiler(string fileName) {
					source = new SourceFile(fileName);
					scanner = new Scanner(source);
					parser = new Parser(scanner);
				}
			// entry point
				/**
				 * Builds a {@code Compiler} instance. Launches the scanning and the compilation. Prints the tokens representing the source file if the previous operations succeeded.
				 * @param	args		command-line one and only argument, the source code file.
				 * @see		collection
				 */
				public static void Main(string[] args) {
					Console.ResetColor();
					if(args.Length == 0) {
						Error(typeof(Compiler).Name, 4, null);
						Environment.Exit(1);
					}
					else if(1 < args.Length) {
						Error(typeof(Compiler).Name, 5, null);
						Environment.Exit(1);
					}

					if(args[0] != null) {
						var compiler = new Compiler(args[0]);
						compiler.collection = compiler.parser.ParseProgram();

						if(0 < compiler.collection.Count) {
							foreach(var element in compiler.collection)
								Info(typeof(Compiler).Name, element.Kind.ToString());
						}
					}
				}
			// output
				/**
				 * Displays an error. If an error occurs, the compilation is automatically stopped and all the data discarded.
				 * @param	origin	class name of the object sender.
				 * @param	code	error code.
				 * @param	msg		arguments for the error message. Can be empty.
				 */
				public static void Error(string origin, uint code, string[] msg) {
					Console.ForegroundColor = ConsoleColor.Red;
					string error_message = "[ERROR]";

					switch(origin) {
						case "Compiler":
							error_message += $"[{origin}]";
							switch(code) {
								case 0:
									error_message += $"[{code}] : ";
									error_message += "sample error";
									break;
								case 1:
									if(msg.Length == 0) {
										Error(origin, 3, new string[]{ code.ToString() });
										return;
									}
									error_message += $"[{code}] : ";
									error_message += $"unknown error origin, got : {msg[0]}";
									break;
								case 2:
									if(msg.Length < 2) {
										Error(origin, 3, new string[]{ code.ToString() });
										return;
									}
									error_message += $"[{code}] : ";
									error_message += $"unknown error code from {msg[0]} , got : {msg[1]}";
									break;
								case 3:
									if(msg.Length == 0) {
										Error(origin, 3, new string[]{ code.ToString() });
										return;
									}
									error_message += $"[{code}] : ";
									error_message += $"the error from {origin} with the error code {msg[0]} does not carry enough parameters";
									break;
								case 4:
									error_message += $"[{code}] : ";
									error_message += $"no source file specified";
									break;
								case 5:
									error_message += $"[{code}] : ";
									error_message += $"too much arguments provided";
									break;
								case 6:
									if(msg.Length == 0) {
										Error(origin, 3, new string[]{ code.ToString() });
										return;
									}
									error_message += $"[{code}] : ";
									error_message += $"the source file {msg[0]} is empty";
									break;
								case 7:
									if(msg.Length < 2) {
										Error(origin, 3, new string[]{ code.ToString() });
										return;
									}
									error_message += $"[{code}] : ";
									error_message += $"the source file {msg[0]} cannot be read : {msg[1]}";
									break;
								default:
									Error(origin, 2, new string[]{ origin, code.ToString() });
									break;
							}
							break;
						case "Scanner":
							error_message += $"[{origin}]";
							switch(code) {
								case 0:
									if(msg.Length < 3) {
										Error(origin, 3, new string[]{ code.ToString() });
										return;
									}
									error_message += $"[{code}] : ";
									error_message += $"unknown token at line {msg[0]}, column {msg[1]}, got : {msg[2]}";
									break;
								case 1:
									if(msg.Length < 3) {
										Error(origin, 3, new string[]{ code.ToString() });
										return;
									}
									error_message += $"[{code}] : ";
									error_message += $"ill-formed character literal at line {msg[0]}, column {msg[1]} , got : {msg[2]}";
									break;
								case 2:
									if(msg.Length < 3) {
										Error(origin, 3, new string[]{ code.ToString() });
										return;
									}
									error_message += $"[{code}] : ";
									error_message += $"invalid operator at line  {msg[0]}, column {msg[1]} , got : {msg[2]}";
									break;
								default:
									Error(origin, 2, new string[]{ origin, code.ToString() });
									return;
							}
							break;
						case "Parser":
							error_message += $"[{origin}]";
							switch(code) {
								case 0:
									if(msg.Length < 3) {
										Error(origin, 3, new string[]{ code.ToString() });
										return;
									}
									error_message += $"[{code}] : ";
									error_message += $"unknown token at line {msg[0]}, column {msg[1]}, got : {msg[3]}";
									break;
								case 1:
									if(msg.Length < 3) {
										Error(origin, 3, new string[]{ code.ToString() });
										return;
									}
									error_message += $"[{code}] : ";
									error_message += $"invalid operator at line {msg[0]}, column {msg[1]}, got : {msg[2]}";
									break;
								case 2:
									if(msg.Length < 4) {
										Error(origin, 3, new string[]{ code.ToString() });
										return;
									}
									error_message += $"[{code}] : ";
									error_message += $"unexpected token found at line {msg[0]}, column {msg[1]}, got {msg[2]}, expected ";
									for(uint i = 3; i < msg.Length; ++i)
										error_message += msg[i] + ' ';
									break;
								default:
									Error(origin, 2, new string[]{ origin, code.ToString() });
									return;
							}
							break;
						default:
							Error(typeof(Compiler).Name, 1, new string[]{ origin });
							return;
					}
					Console.WriteLine(error_message);
					Console.ResetColor();
				}
				/**
				 * Displays an error.
				 * @param	origin	class name of the object sender.
				 * @param	msg		body of the info message.
				 */
				public static void Info(string origin, string msg) {
					Console.ForegroundColor = ConsoleColor.Cyan;
					string message = "[INFO]";

					switch(origin) {
						case "Compiler":
						case "Scanner":
						case "Parser":
							message += $"[{origin}] : ";
							message += msg;
							break;
						default:
							Error(typeof(Compiler).Name, 1, new string[]{ origin });
							return;
					}
					Console.WriteLine(message);
					Console.ResetColor();
				}
			// compilation error
				public static void Abort() {
					
				}
	}
}
