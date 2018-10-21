# Element 1 - Scanner & Parser (40% of Coursework Grade)

The first submission will constitute the two primary components of the front end of your compiler.

## Scanner implementation – 15%

Your Scanner (Lexical Analyser) should be developed to read in a source file written in the given source language. The characters in the source file should be compiled into a sequence of recognised language tokens.

During this process your scanner should fulfil the following basic compiler requirements
* Identify and remove whitespace.
* Identify and remove language comments
* Identify and produce errors for unknown characters in the language
* Identify and produce errors for unterminated character literals

## Parser implementation – 20%

Your Parser (Syntax Analyser) should work on the sequence of tokens created by the scanner. These tokens should be grouped into sentences, creating a set of instructions. The instruction set will represent the purpose of the program defined in the source code.

Your Parser should be able to identify Syntax Errors in the source program and produce errors for instructions that do not conform to the language definition.

On completion of the Parser stage your compiler should maintain an Internal Representation of the instruction set and write out an instruction set, in the correct order, to the console.

## Implementation Report – 5%

In addition to the two components above you should also submit an implementation report describing how you have implemented the compiler. Your report should a description of how you have covered the requirements of the Scanner and Parser detailed above, including how you have transferred data between the components. Your implementation report for this stage should be no more than 2000 words plus appendices.

As an appendix to your report you should include any source programs written in the given language that you used to test your Scanner and Parser.