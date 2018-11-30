# Implementation report

[Anthony Sébert](mailto:antoine.sb@orange.fr)[^1] , 31/11/2018

## Prerequisites

Microsoft .NET Core 2.x, available at https://dotnet.microsoft.com/download

## Getting started

Decompress the folder, then open a terminal.

```
cd path/to/folder/Compiler
dotnet build
```

*Note : if the terminal starts in another drive, just type the name of the drive where the project folder is located, i.e. `D:`*

## Compiling a file

Once you are in the **Compiler** folder, run the program (a sample [source code file](Triangle.Compiler/test/verify.tri) is already provided).

```
dotnet run path/to/myfile.tri
```

And voilà !

## Code generation

The code generation part works from the results of the lexing & parsing steps.



## Appendix

*Note : irrelevant parts of the fragments have been omitted so as to lead the eye of the reader to the essential.*



[^1]: Computer Science student at [The Robert Gordon university](https://www.rgu.ac.uk/) (Garthdee House, Garthdee Road, Aberdeen, AB10 7QB, Scotland, United Kingdom)