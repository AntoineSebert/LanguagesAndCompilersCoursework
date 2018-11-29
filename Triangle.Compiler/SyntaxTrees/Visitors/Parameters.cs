using Triangle.Compiler.SyntaxTrees.Formals;
using Triangle.Compiler.SyntaxTrees.Parameters;

namespace Triangle.Compiler.SyntaxTrees.Visitors {
	public interface IParameterSequenceVisitor<TArg, TResult> {
		TResult VisitEmptyParameterSequence(EmptyParameterSequence ast, TArg arg);

		TResult VisitSingleParameterSequence(SingleParameterSequence ast, TArg arg);

		TResult VisitMultipleParameterSequence(MultipleParameterSequence ast, TArg arg);
	}

	public interface IParameterVisitor<TArg, TResult> {
		TResult VisitValueParameter(ValueParameter ast, TArg arg);

		TResult VisitVarParameter(VarParameter ast, TArg arg);
	}

	// This is only needed for the standard environment
	public interface IFormalParameterSequenceVisitor<TArg, TResult> {
		TResult VisitEmptyFormalParameterSequence(EmptyFormalParameterSequence ast, TArg arg);

		TResult VisitSingleFormalParameterSequence(SingleFormalParameterSequence ast, TArg arg);

		TResult VisitMultipleFormalParameterSequence(MultipleFormalParameterSequence ast, TArg arg);
	}
}