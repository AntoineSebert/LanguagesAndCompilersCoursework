using System;
using System.Collections.Generic;
using System.IO;

namespace Triangle.Compiler.SyntacticAnalyzer {
	/// <summary>
	/// Wrapper for reading from a source code file
	/// </summary>
	public class SourceFile : IEnumerator<char> {
		/// <summary>
		/// The underlying file reader
		/// </summary>
		private StreamReader source;
		/// <summary>
		/// The currently buffered line
		/// </summary>
		private string buffer;
		/// <summary>
		/// The index in buffer that is currently being read
		/// </summary>
		private int index;
		/// <summary>
		/// The current line number
		/// </summary>
		private int lineNumber;
		/// <summary>
		/// The file being read from
		/// </summary>
		public string Name { get; }
		/// <summary>
		/// Whether the file can be read from
		/// </summary>
		public bool IsValid => source != null;
		/// <summary>
		/// The current character
		/// </summary>
		public char Current => buffer == null ? default(char) : buffer[index];
		/// <summary>
		/// The current character
		/// </summary>
		object System.Collections.IEnumerator.Current => Current;
		/// <summary>
		/// Gets the location of the current character
		/// </summary>
		public Location Location => new Location(lineNumber, index + 1);
		/// <summary>
		/// Skips forward to the beginning of the next line
		/// </summary>
		/// <returns></returns>
		public bool SkipRestOfLine() {
			index = buffer.Length;
			return MoveNext();
		}
		/// <summary>
		/// Moves to the next character
		/// </summary>
		/// <returns>True if there are still charcters to read or false otherwise</returns>
		public bool MoveNext() {
			if(buffer != null) {
				index++;
				if(index >= buffer.Length)
					ReadLine();
			}

			return buffer != null;
		}
		/// <summary>
		/// Resets the reader to the beginning of the file
		/// </summary>
		public void Reset() {
			if(source == null)
				throw new InvalidOperationException();
			if(!source.BaseStream.CanSeek)
				throw new NotSupportedException();

			source.BaseStream.Seek(0L, SeekOrigin.Begin);
			source.DiscardBufferedData();

			index = lineNumber = 0;

			ReadLine();
		}
		/// <summary>
		/// Reads the next line from the source file
		/// </summary>
		private void ReadLine() {
			buffer = source.ReadLine();
			if(buffer != null)
				buffer += "\n";

			index = 0;
			lineNumber++;
		}
		/// <summary>
		/// Creates a new source code file wrapper
		/// </summary>
		/// <param name="sourceFileName">The file to read from</param>
		public SourceFile(string sourceFileName) {
			Name = sourceFileName;
			try {
				source = new StreamReader(new FileStream(sourceFileName, FileMode.Open));
				Reset();
			}
			catch(FileNotFoundException) { source = null; }
		}
		/// <summary>
		/// Disposes of the file reader
		/// </summary>
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		/// <summary>
		/// Disposes of the file reader
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing) {
			if(disposing) {
				if(source != null) {
					source.Dispose();
					source = null;
				}
			}
		}
	}
}