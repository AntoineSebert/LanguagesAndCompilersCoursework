using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Formals;
using Triangle.Compiler.SyntaxTrees.Parameters;
using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.ContextualAnalyzer {
	public partial class Checker {
		// Actual Parameters
		// Always returns null. Uses the given FormalParameter.
		public Void VisitValueParameter(ValueParameter ast, FormalParameter arg) {
			TypeDenoter expressionType = ast.Expression.Visit(this, null);
			if(arg is ConstFormalParameter param)
				CheckAndReportError(expressionType.Equals(param.Type), "wrong type for const actual parameter", ast.Expression);
			else
				ReportError("const actual parameter not expected here", ast);
			return null;
		}
		public Void VisitVarParameter(VarParameter ast, FormalParameter arg) {
			Declaration actualType = ast.Identifier.Visit(this, null);
			if(!ast.Identifier.IsVariable)
				ReportError("parameter is not a variable", ast.Identifier);
			else if(arg is VarFormalParameter parameter)
				CheckAndReportError(actualType.Equals(parameter.Type), "wrong type for var parameter", ast.Identifier);
			else
				ReportError("var parameter not expected here", ast.Identifier);
			return null;
		}
		public Void VisitEmptyParameterSequence(EmptyParameterSequence ast, FormalParameterSequence arg) {
			CheckAndReportError(arg is EmptyFormalParameterSequence, "too few actual parameters", ast);
			return null;
		}
		public Void VisitMultipleParameterSequence(MultipleParameterSequence ast, FormalParameterSequence arg) {
			if(arg is MultipleFormalParameterSequence formals) {
				ast.Parameter.Visit(this, formals.Formal);
				ast.Parameters.Visit(this, formals.Formals);
			}
			else
				ReportError("too many actual parameters", ast);
			return null;
		}
		public Void VisitSingleParameterSequence(SingleParameterSequence ast, FormalParameterSequence arg) {
			if(arg is SingleFormalParameterSequence formal)
				ast.Parameter.Visit(this, formal.Formal);
			else
				ReportError("incorrect number of actual parameters", ast);
			return null;
		}
	}
}