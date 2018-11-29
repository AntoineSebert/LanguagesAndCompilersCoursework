using System;
using static System.Console;

namespace Triangle.Compiler {
	public class ErrorReporter {
		public int ErrorCount { get; private set; } = 0;
		public bool HasErrors => ErrorCount > 0;
		public void ReportError(string message, string tokenName, SourcePosition pos) {
			WriteLine($"ERROR: {message.Replace("%", tokenName)} {pos}");
			ErrorCount++;
		}
		public void ReportMessage(string message) { WriteLine(message); }

		internal void ReportRestriction(string v) {
			throw new NotImplementedException();
		}
	}
}