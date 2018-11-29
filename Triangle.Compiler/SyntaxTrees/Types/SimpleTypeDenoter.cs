using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Types {
	public class SimpleTypeDenoter : TypeDenoter {
		public Identifier Identifier { get; }
		public override int Size => 0;

		public SimpleTypeDenoter(Identifier identifier, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Identifier = identifier;
		}

		public override TResult Visit<TArg, TResult>(ITypeDenoterVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitSimpleTypeDenoter(this, arg); }
	}
}