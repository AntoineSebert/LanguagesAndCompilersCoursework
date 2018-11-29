using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.CodeGenerator {
	internal interface IEncoderVisitor :
		ICommandVisitor<Frame, Void>,
		IDeclarationVisitor<Frame, int>,
		IExpressionVisitor<Frame, int>,
		IParameterVisitor<Frame, int>,
		IParameterSequenceVisitor<Frame, int>,
		IFormalParameterSequenceVisitor<Frame, int>,
		IProgramVisitor<Frame, Void>,
		ILiteralVisitor<Frame, Void>,
		ITerminalVisitor<Frame, Void>,
		ITypeDenoterVisitor<Frame, int> {}
}
