using Triangle.AbstractMachine;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Types {
	public class IntTypeDenoter : TypeDenoter {
		public override int Size => Machine.IntegerSize;

		public IntTypeDenoter() : base(SourcePosition.Empty) {}

		public override TResult Visit<TArg, TResult>(ITypeDenoterVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitIntTypeDenoter(this, arg); }
	}
}