using System.IO;
using Triangle.Compiler.CodeGenerator;
using Triangle.Compiler.ContextualAnalyzer;
using Triangle.Compiler.SyntacticAnalyzer;
using Triangle.Compiler.SyntaxTrees;

namespace Triangle.Compiler {
	public class Compiler {
		private const bool DEBUG = true;
		/// The error reporter.
		private ErrorReporter ErrorReporter { get; }
		/// The source file to compile.
		private SourceFile Source { get; }
		/// The lexical analyzer.
		private Scanner Scanner { get; }
		/// The syntactic analyzer.
		private Parser Parser { get; }

		private Checker Checker { get; }

		private Encoder Encoder { get; }

		private string source_file = null;
		/// Creates a compiler for the given source file.
		/// param	sourceFileName	a File that specifies the source program
		public Compiler(string sourceFileName, ErrorReporter errorReporter) {
			source_file = sourceFileName;
			ErrorReporter = errorReporter;
			Source = new SourceFile(sourceFileName);
			Scanner = new Scanner(Source);
			Parser = new Parser(Scanner, ErrorReporter);
			Checker = new Checker(ErrorReporter);
			Encoder = new Encoder(ErrorReporter);
		}
		/// <summary>
		/// Compiles the source program to TAM machine code.
		/// </summary>
		/// <param name="showingTable">
		/// a boolean that determines if the object description details are to
		/// be displayed during code generation (not currently implemented)
		/// </param>
		/// <returns>
		/// true if the source program is free of compile-time errors, otherwise false
		/// </returns>
		public bool CompileProgram() {
			ErrorReporter.ReportMessage("********** Triangle Compiler **********");
			if(!Source.IsValid) {
				ErrorReporter.ReportMessage($"Cannot access source file \"{Source.Name}\".");
				return false;
			}
			// 1st pass
			ErrorReporter.ReportMessage("Syntactic Analysis ...");
			Program program = Parser.ParseProgram();
			if(ErrorReporter.HasErrors) {
				ErrorReporter.ReportMessage("Compilation was unsuccessful.");
				return false;
			}
			// 2nd pass
			ErrorReporter.ReportMessage("Contextual Analysis ...");
			Checker.Check(program);
			if(ErrorReporter.HasErrors) {
				ErrorReporter.ReportMessage("Compilation was unsuccessful.");
				return false;
			}
			// 3rd pass
			ErrorReporter.ReportMessage("Code Generation...");
			Encoder.EncodeRun(program);
			if(ErrorReporter.HasErrors) {
				ErrorReporter.ReportMessage("Compilation was unsuccessful.");
				return false;
			}
			Encoder.SaveObjectProgram(source_file + ".tam");
			System.Console.WriteLine(program);
			ErrorReporter.ReportMessage("Compilation was successful.");

			System.Console.ReadLine();

			return true;
		}
		/// <summary>
		/// Triangle compiler main program.
		/// </summary>
		/// <param name="args">
		/// a string array containing the command-line arguments. This must
		/// be a single string specifying the source filename.
		/// </param>
		public static void Main(string[] args) {
			ErrorReporter errorReporter = new ErrorReporter();
			if(args.Length != 1)
				errorReporter.ReportMessage("Usage: Compiler.exe source");
			else {
				string sourceFileName = args[0];
				if(File.Exists(sourceFileName)) {
					if(sourceFileName != null) {
						Compiler compiler = new Compiler(sourceFileName, errorReporter);
						compiler.CompileProgram();
					}
				}
				else
					errorReporter.ReportMessage("File does not exist");
			}
		}
		public static void WriteDebuggingInfo(object message) {
			if(DEBUG)
				System.Console.WriteLine(message.ToString());
		}
	}
}