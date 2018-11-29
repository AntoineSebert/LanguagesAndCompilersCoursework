using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Declarations {
	public abstract class Declaration : AbstractSyntaxTree {
		public bool Duplicated { get; set; }
		public virtual TypeDenoter Type { get; set; }

		protected Declaration(SourcePosition pos) : this(pos, null) { Compiler.WriteDebuggingInfo($"Creating {GetType().Name}"); }

		protected Declaration(SourcePosition pos, TypeDenoter type) : base(pos) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Type = type;
		}

		public abstract TResult Visit<TArg, TResult>(IDeclarationVisitor<TArg, TResult> visitor, TArg arg);
	}
}