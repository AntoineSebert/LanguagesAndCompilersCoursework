using System;

/**
 * @author Antoine/Anthony Sébert
 */
namespace Compiler {
	/**
	 * Holds a scanner and a parser, is responsible for the whole process.
	 */
	class Compiler {
		/* ATTRIBUTES */
			readonly Scanner scanner = null;
			readonly Parser parser = null;
			readonly SourceFile source = null;
		/* MEMBERS */
			// constructor
				/**
				 * Build a Compiler instance.
				 * @param filename	name of the file to compile
				 */
				public Compiler(string fileName) {
					source = new SourceFile(fileName);
					scanner = new Scanner(source);
					parser = new Parser(scanner);
				}
			// entry point
				/**
				 * Build a Compiler instance.
				 * @param args	command-line one and only argument, the source code file
				 */
				public static void Main(string[] args) {
					if(args.Length != 1) {
						Console.WriteLine("Must provide exactly one argument - the source code file");
						return;
					}
					string sourceFileName = args[0];
					if(sourceFileName != null) {
						var compiler = new Compiler(sourceFileName);
						foreach(var token in compiler.scanner)
							Console.WriteLine(token);
						//compiler.source.Reset(); // uncomment to reset source code
						compiler.parser.ParseProgram();
					}
				}
	}
}
