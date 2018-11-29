using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Types {
	public abstract class TypeDenoter : AbstractSyntaxTree {
		protected TypeDenoter(SourcePosition position) : base(position) { Compiler.WriteDebuggingInfo($"Creating {GetType().Name}"); }

		public abstract int Size { get; }

		public abstract TResult Visit<TArg, TResult>(ITypeDenoterVisitor<TArg, TResult> visitor, TArg arg);
	}
}