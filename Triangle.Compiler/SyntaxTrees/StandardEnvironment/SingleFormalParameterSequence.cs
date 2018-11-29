using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Formals {
	public class SingleFormalParameterSequence : FormalParameterSequence {
		public FormalParameter Formal { get; }

		public SingleFormalParameterSequence(FormalParameter formal, SourcePosition position) : base(position) { Formal = formal; }

		public SingleFormalParameterSequence(FormalParameter formal) : this(formal, SourcePosition.Empty) {}

		public override TResult Visit<TArg, TResult>(IFormalParameterSequenceVisitor<TArg, TResult> visitor, TArg arg) {
			return visitor.VisitSingleFormalParameterSequence(this, arg);
		}
	}
}