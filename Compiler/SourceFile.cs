/**
 * @author Antoine/Anthony Sébert
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Compiler {
	/**
	 * Holds the source file content as a collection of characters. Reads the file line by line.
	 */
	public class SourceFile : IEnumerator<char> {
		/* ATTRIBUTES */
			/**
			 * Current character being read.
			 */
			object IEnumerator.Current { get { return Current; } }
			// public
				/**
				 * Last character of the buffer, if the buffer is not {@code null}.
				 */
				public char Current { get { return buffer == null ? default : buffer[(int)index]; } }
				/**
				 * Name of the souce code file.
				 */
				public string Name { get; } = "";
				/**
				 * Location of the current character in the file.
				 */
				public Location _Location { get { return new Location(lineNumber, index); } }
			// private
				/**
				 * Index of the current character in the buffer.
				 */
				private uint index = 0,
				/**
				 * Current line number.
				 */
					lineNumber = 0;
				/**
				 * Contains the characters of the current line.
				 */
				private string buffer = "";
				/**
				 * Reads the content of the source code file.
				 */
				private StreamReader source = null;
		/* MEMBERS */
			// public
				// constructor
					/**
					 * Builds a {@code SourceFile} instance.
					 * @param	sourceFileName	string representing the file name of the file to open.
					 */
					public SourceFile(string sourceFileName) {
						Name = sourceFileName;
						try {
							if(new FileInfo(sourceFileName).Length == 0) {
								Compiler.Error(typeof(Compiler).Name, 6, new string[]{ Name }, 1);
								Environment.Exit(1);
							}
							source = new StreamReader(new FileStream(sourceFileName, FileMode.Open));
							Reset();
						}
						catch(Exception e) {
							Compiler.Error(typeof(Compiler).Name, 7, new string[]{ Name, e.Message }, 1);
							Environment.Exit(1);
						}
					}
				// other
					/**
					 * Moves the index to the next character. If the index has reached the end of the line, a new line is read.
					 * @return	{@code true} if the buffer is not full, {@code false} otherwise.
					 */
					public bool MoveNext() {
						if(buffer != null) {
							index++;
							if(buffer.Length <= index)
								ReadLine();
						}
						return buffer != null;
					}
					/**
					 * Resets the reading of the source code file.
					 * @throws	InvalidOperationException	This exception is thrown if the source code file is {@code null}.
					 * @throws	NotSupportedException		This exception is thrown to mark the seeking of the current stream as not supported yet.
					 */
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
					/**
					 * Destroys the current object.
					 * @deprecated
					 */
					public void Dispose() {
						Dispose(true);
						GC.SuppressFinalize(this); // breaks single responsability principle. Ugly.
					}
					/**
					 * Destroys the current object.
					 * @return	If the end of the file has been reached, returns {@code true}, otherwise {@code false}.
					 */
					public bool SkipRestOfLine() {
						index = (uint)buffer.Length;
						return MoveNext();
					}
			// protected
				/**
				 * Destroys the {@code StreamReader} if the source is not {@code null}.
				 * @param	disposing	If this variable is set to {@code false}, the {@code StreamReader} will not be destroyed.
				 */
				protected virtual void Dispose(bool disposing) {
					if(disposing && source != null) {
						source.Dispose();
						source = null;
					}
				}
			// private
				/**
				 * Copy the next line in the stream into the buffer, resets the index. If the buffer is not {@code null}, appends a new line to it.
				 */
				private void ReadLine() {
					buffer = source.ReadLine();
					if(buffer != null)
						buffer += '\n';

					index = 0;
					lineNumber++;
				}
	}
}