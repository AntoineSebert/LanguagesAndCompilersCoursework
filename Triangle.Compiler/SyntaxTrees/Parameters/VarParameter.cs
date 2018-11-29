using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.SyntaxTrees.Parameters {
	public class VarParameter : Parameter {
		public Identifier Identifier { get; }

		public VarParameter(Identifier identifier, SourcePosition position) : base(position) {
			Compiler.WriteDebuggingInfo($"Creating {GetType().Name}");
			Identifier = identifier;
		}

		public override TResult Visit<TArg, TResult>(IParameterVisitor<TArg, TResult> visitor, TArg arg) { return visitor.VisitVarParameter(this, arg); }
	}
}