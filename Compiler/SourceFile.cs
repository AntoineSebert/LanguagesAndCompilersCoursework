using System;
using System.Collections.Generic;
using System.IO;

namespace Compiler {
	public class SourceFile : IEnumerator<char> {
		private StreamReader source;
		private string buffer;
		private int index;
		private int lineNumber;
		public string Name { get; }
		public bool IsValid { get { return source != null; } }
		public char Current { get { return buffer == null ? default : buffer[index]; } }
		object System.Collections.IEnumerator.Current { get { return Current; } }
		public Location Location { get { return new Location((uint)lineNumber, (uint)index); } }
		public bool SkipRestOfLine() {
			index = buffer.Length;
			return MoveNext();
		}
		public bool MoveNext() {
			if(buffer != null) {
				index++;
				if(index >= buffer.Length)
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

			index = 0;
			lineNumber = 0;

			ReadLine();
		}
		private void ReadLine() {
			buffer = source.ReadLine();
			if(buffer != null)
				buffer += "\n";

			index = 0;
			lineNumber++;
		}
		public SourceFile(string sourceFileName) {
			Name = sourceFileName;
			try {
				source = new StreamReader(new FileStream(sourceFileName, FileMode.Open));
				Reset();
			}
			catch(FileNotFoundException) {
				source = null;
			}
		}
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}
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