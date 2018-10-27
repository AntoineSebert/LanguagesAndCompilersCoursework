using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Compiler {
	public class SourceFile : IEnumerator<char> {
		/* ATTRIBUTES */
			object IEnumerator.Current { get { return Current; } }
			// public
				public string Name { get; } = "";
				public char Current { get { return buffer == null ? default : buffer[(int)index]; } }
				public bool IsValid { get { return source != null; } }
				public Location _Location { get { return new Location(lineNumber, index); } }
			// private
				private uint index = 0, lineNumber = 0;
				private string buffer = "";
				private StreamReader source = null;
		/* MEMBERS */
			// public
				// constructor
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
					public bool MoveNext() {
						if(buffer != null) {
							index++;
							if(buffer.Length <= index)
								ReadLine();
						}
						return buffer != null;
					}
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
					public void Dispose() {
						Dispose(true);
						GC.SuppressFinalize(this);
					}
					public bool SkipRestOfLine() {
						index = (uint)buffer.Length;
						return MoveNext();
					}
			// protected
				protected virtual void Dispose(bool disposing) {
					if(disposing && source != null) {
						source.Dispose();
						source = null;
					}
				}
			// private
				private void ReadLine() {
					buffer = source.ReadLine();
					if(buffer != null)
						buffer += '\n';

					index = 0;
					lineNumber++;
				}
	}
}