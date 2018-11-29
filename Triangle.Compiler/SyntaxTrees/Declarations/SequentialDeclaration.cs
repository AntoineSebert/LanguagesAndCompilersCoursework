using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Declarations {
	public class SequentialDeclaration : Declaration {
		public Declaration FirstDeclaration { get; }
		public Declaration SecondDeclaration { get; }

		public SequentialDeclaration(Declaration firstDeclaration, Declaration secondDeclaration, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			FirstDeclaration = firstDeclaration;
			SecondDeclaration = secondDeclaration;
		}

		public override TResult Visit<TArg, TResult>(IDeclarationVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitSequentialDeclaration(this, arg); }
	}
}