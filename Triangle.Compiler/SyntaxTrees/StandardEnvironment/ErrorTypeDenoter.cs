using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Types {
	public class ErrorTypeDenoter : TypeDenoter {
		public override int Size => 0;

		public ErrorTypeDenoter() : base(SourcePosition.Empty) {}

		public override TResult Visit<TArg, TResult>(ITypeDenoterVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitErrorTypeDenoter(this, arg); }
	}
}