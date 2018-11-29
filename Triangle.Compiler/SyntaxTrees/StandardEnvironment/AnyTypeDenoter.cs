using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Types {
	public class AnyTypeDenoter : TypeDenoter {
		public override int Size => 0;

		public AnyTypeDenoter() : base(SourcePosition.Empty) {}

		public override TResult Visit<TArg, TResult>(ITypeDenoterVisitor<TArg, TResult> visitor, TArg arg) {
			return visitor.VisitAnyTypeDenoter(this, arg);
		}
	}
}