using Triangle.Compiler.CodeGenerator.Entities;

namespace Triangle.Compiler.SyntaxTrees {
	public abstract class AbstractSyntaxTree {
		public SourcePosition Position { get; }
		public Location Start => Position.Start;
		public Location Finish => Position.Finish;
		public RuntimeEntity Entity { get; set; }

		protected AbstractSyntaxTree(SourcePosition position) { Position = position; }
	}
}