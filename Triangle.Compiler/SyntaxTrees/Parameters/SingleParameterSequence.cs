using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Parameters {
	public class SingleParameterSequence : ParameterSequence {
		public Parameter Parameter { get; }

		public SingleParameterSequence(Parameter parameter, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Parameter = parameter;
		}

		public override TResult Visit<TArg, TResult>(IParameterSequenceVisitor<TArg, TResult> visitor, TArg arg) {
			return visitor.VisitSingleParameterSequence(this, arg);
		}
	}
}