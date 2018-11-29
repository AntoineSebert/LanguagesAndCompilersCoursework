using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Formals;

namespace Triangle.Compiler.SyntaxTrees.Visitors {
	public interface IDeclarationVisitor<TArg, TResult> {
		TResult VisitConstDeclaration(ConstDeclaration ast, TArg arg);

		TResult VisitSequentialDeclaration(SequentialDeclaration ast, TArg arg);

		TResult VisitVarDeclaration(VarDeclaration ast, TArg arg);

		TResult VisitInitDeclaration(InitDeclaration ast, TArg arg);

		// Things below here are needed only for the standard environment
		TResult VisitUnaryOperatorDeclaration(UnaryOperatorDeclaration ast, TArg arg);

		TResult VisitBinaryOperatorDeclaration(BinaryOperatorDeclaration ast, TArg arg);

		TResult VisitFuncDeclaration(FuncDeclaration ast, TArg arg);

		TResult VisitProcDeclaration(ProcDeclaration ast, TArg arg);

		TResult VisitTypeDeclaration(TypeDeclaration ast, TArg arg);

		TResult VisitConstFormalParameter(ConstFormalParameter ast, TArg arg);

		TResult VisitVarFormalParameter(VarFormalParameter ast, TArg arg);
	}
}