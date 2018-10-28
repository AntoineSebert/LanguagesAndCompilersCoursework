# Implementation report

[Anthony Sébert](mailto:antoine.sb@orange.fr)[^1] , 28 october 2018

## Documentation

The documentation has been generated with [Doxygen](file:///D:/Program%20Files/doxygen/html/index.html) from the in-code documentation.
It has been generated in several formats, as listed : HTML, LaTeX, man-pages, rtf and xml;
The "main page" of the HTML version is located at [docs/html/annotated.html](docs/html/index.html) (the real main page is empty, for I had nothing interesting to put there).

## Scanner implementation

Your Scanner (Lexical Analyser) should be developed to read in a source file written in the given source language. The characters in the source file should be compiled into a sequence of recognised language tokens.

During this process your scanner should fulfill the following basic compiler requirements

- [x] [Identify and remove whitespace](#Fragment 1)

- [x] [Identify and remove language comments](#Fragment 1)

- [x] [Identify and produce errors for unknown characters in the language](#Fragment 2)

- [x] [Identify and produce errors for unterminated character literals](#Fragment 3)

## Parser implementation

including how you have transferred data between the components.

- [x] [Your Parser (Syntax Analyser) should work on the sequence of tokens created by the scanner. These tokens should be grouped into sentences, creating a set of instructions. The instruction set will represent the purpose of the program defined in the source code](file:///D:/Users/i/Desktop/LanguagesAndCompilersCoursework/docs/html/df/dc2/class_compiler_1_1_parser.html)

- [x] [Your Parser should be able to identify Syntax Errors in the source program and produce errors for instructions that do not conform to the language definition](#Fragment 4)

- [x] On completion of the Parser stage your compiler should maintain an Internal Representation of the instruction set and write out an instruction set, in the correct order, to the console.

## Appendix

### Scanner

#### Fragment 1

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

#### Fragment 2

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

#### Fragment 3

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

### Parser

#### Fragment 4

```csharp
/**
 * Checks that the given token matches the current stream of tokens, if not prints an error.
 * @param expectedKinds	an array of expected token kinds.
 */
protected void Accept(TokenKind expectedKind) {
	Location previousLocation = null;
	if(tokens.Current.Kind == expectedKind)
		previousLocation = tokens.Current.Position.Start;
	else
		Compiler.Error(typeof(Parser).Name, 2, new string[]{
			tokens.Current.Position.Start.LineNumber.ToString(),
			tokens.Current.Position.Start.RowNumber.ToString(),
			tokens.Current.Kind.ToString(),
			expectedKind.ToString()
		});
	tokens.MoveNext();
}
```

#### Fragment 5

#### Fragment 6


[^1]: Computer Science student at [The Robert Gordon university](https://www.rgu.ac.uk/) (Garthdee House, Garthdee Road, Aberdeen, AB10 7QB, Scotland, United Kingdom)