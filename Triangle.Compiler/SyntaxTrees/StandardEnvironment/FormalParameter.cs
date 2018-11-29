using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Types;

namespace Triangle.Compiler.SyntaxTrees.Formals {
	public abstract class FormalParameter : Declaration {
		protected FormalParameter(SourcePosition position) : base(position) {}

		protected FormalParameter(SourcePosition position, TypeDenoter type) : base(position, type) {}
	}
}