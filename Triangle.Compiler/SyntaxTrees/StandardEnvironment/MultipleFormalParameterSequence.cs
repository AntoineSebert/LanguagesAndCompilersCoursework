using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Formals {
	public class MultipleFormalParameterSequence : FormalParameterSequence {
		public FormalParameter Formal { get; }
		public FormalParameterSequence Formals { get; }

		public MultipleFormalParameterSequence(FormalParameter formal, FormalParameterSequence formals, SourcePosition position) : base(position) {
			Formal = formal;
			Formals = formals;
		}

		public override TResult Visit<TArg, TResult>(IFormalParameterSequenceVisitor<TArg, TResult> visitor, TArg arg) {
			return visitor.VisitMultipleFormalParameterSequence(this, arg);
		}
	}
}