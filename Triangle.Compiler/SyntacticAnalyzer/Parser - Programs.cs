using Triangle.Compiler.SyntaxTrees;
using Triangle.Compiler.SyntaxTrees.Commands;

namespace Triangle.Compiler.SyntacticAnalyzer {
	public partial class Parser {
		///////////////////////////////////////////////////////////////////////////////
		//
		// PROGRAMS
		//
		///////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Parses a Triangle program, and constructs an AST to represent it.
		/// </summary>
		/// <returns>
		/// a {@link triangle.compiler.syntax.trees.Program} or null if there
		/// is a syntactic error
		/// </returns>
		public Program ParseProgram() {
			try {
				Compiler.WriteDebuggingInfo("Parsing Program");
				tokens.MoveNext();
				Location startLocation = tokens.Current.Start;
				Command command = ParseCommand();
				SourcePosition pos = new SourcePosition(startLocation, tokens.Current.Finish);
				Program program = new Program(command, pos);
				if(tokens.Current.Kind != TokenKind.EndOfText)
					RaiseSyntacticError("\"%\" not expected after end of program", tokens.Current);
				return program;
			}
			catch(SyntaxError) {
				return null;
			}
		}
	}
}