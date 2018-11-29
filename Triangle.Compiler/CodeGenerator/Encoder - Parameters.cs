using Triangle.AbstractMachine;
using Triangle.Compiler.SyntaxTrees.Parameters;

namespace Triangle.Compiler.CodeGenerator {
	public partial class Encoder {
		public int VisitValueParameter(ValueParameter ast, Frame frame) { return ast.Expression.Visit(this, frame); }

		public int VisitVarParameter(VarParameter ast, Frame frame) {
			EncodeFetchAddress(ast.Identifier, frame);
			return Machine.AddressSize;
		}

		public int VisitEmptyParameterSequence(EmptyParameterSequence ast, Frame frame) { return 0; }

		public int VisitSingleParameterSequence(SingleParameterSequence ast, Frame frame) { return ast.Parameter.Visit(this, frame); }

		public int VisitMultipleParameterSequence(MultipleParameterSequence ast, Frame frame) {
			int argsSize1 = ast.Parameter.Visit(this, frame);
			Frame frame1 = frame.Expand(argsSize1);
			int argsSize2 = ast.Parameters.Visit(this, frame1);
			return argsSize1 + argsSize2;
		}
	}
}