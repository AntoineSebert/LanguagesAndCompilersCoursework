# Element 1 - Scanner & Parser (40% of Coursework Grade)

The first submission will constitute the two primary components of the front end of your compiler.

## Scanner implementation – 15%

Your Scanner (Lexical Analyser) should be developed to read in a source file written in the given source language. The characters in the source file should be compiled into a sequence of recognised language tokens.

During this process your scanner should fulfil the following basic compiler requirements

- [x] Identify and remove whitespace.

- [x] Identify and remove language comments.

```csharp
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
```

- [x] Identify and produce errors for unknown characters in the language.

```csharp
/**
 * Determine the token kind to build from the characters processed. Reads the file stream to build the token.
 * @return	a token kind.
 * @see		TokenKind
 */
private TokenKind ScanToken() {
	/* valid characters */
	// ...
	switch(source.Current) {
		/* valid characters */
		// ...
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
```

- [x] Identify and produce errors for unterminated character literals.

```csharp
/**
 * Determine the token kind to build from the characters processed. Reads the file stream to build the token.
 * @return	a token kind.
 * @see		TokenKind
 */
private TokenKind ScanToken() {
	/* valid characters */
	// ...
	switch(source.Current) {
		/* other valid characters */
		// ...
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
		/* unknown characters */
		// ...
	}
}
```

## Parser implementation – 20%

Your Parser (Syntax Analyser) should work on the sequence of tokens created by the scanner. These tokens should be grouped into sentences, creating a set of instructions. The instruction set will represent the purpose of the program defined in the source code.

Your Parser should be able to identify Syntax Errors in the source program and produce errors for instructions that do not conform to the language definition.

On completion of the Parser stage your compiler should maintain an Internal Representation of the instruction set and write out an instruction set, in the correct order, to the console.

## Implementation Report – 5%

In addition to the two components above you should also submit an implementation report describing how you have implemented the compiler. Your report should a description of how you have covered the requirements of the Scanner and Parser detailed above, including how you have transferred data between the components. Your implementation report for this stage should be no more than 2000 words plus appendices.

As an appendix to your report you should include any source programs written in the given language that you used to test your Scanner and Parser.