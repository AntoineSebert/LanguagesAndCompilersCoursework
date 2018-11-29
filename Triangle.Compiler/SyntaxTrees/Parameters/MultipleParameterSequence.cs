using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Parameters {
	public class MultipleParameterSequence : ParameterSequence {
		public Parameter Parameter { get; }
		public ParameterSequence Parameters { get; }

		public MultipleParameterSequence(Parameter parameter, ParameterSequence parameters, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Parameter = parameter;
			Parameters = parameters;
		}

		public override TResult Visit<TArg, TResult>(IParameterSequenceVisitor<TArg, TResult> visitor, TArg arg) {
			return visitor.VisitMultipleParameterSequence(this, arg);
		}
	}
}