namespace Triangle.Compiler.SyntaxTrees.Visitors {
	public interface IProgramVisitor<TArg, TResult> {
		TResult VisitProgram(Program ast, TArg arg);
	}
}