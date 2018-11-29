using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Formals;
using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.ContextualAnalyzer {
	internal interface ICheckerVisitor :
		ICommandVisitor<Void, Void>,
		IDeclarationVisitor<Void, Void>,
		IExpressionVisitor<Void, TypeDenoter>,
		IParameterVisitor<FormalParameter, Void>,
		IParameterSequenceVisitor<FormalParameterSequence, Void>,
		IFormalParameterSequenceVisitor<Void, Void>,
		IProgramVisitor<Void, Void>,
		ILiteralVisitor<Void, TypeDenoter>,
		ITerminalVisitor<Void, Declaration>,
		ITypeDenoterVisitor<Void, TypeDenoter> {}
}
