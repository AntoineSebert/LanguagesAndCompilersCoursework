using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Formals;
using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Types;

namespace Triangle.Compiler {
	public static class StandardEnvironment {
		public static readonly TypeDenoter BooleanType = new BoolTypeDenoter(),
			CharType = new CharTypeDenoter(),
			IntegerType = new IntTypeDenoter(),
			AnyType = new AnyTypeDenoter(),
			ErrorType = new ErrorTypeDenoter();

		public static readonly TypeDeclaration BooleanDecl = DeclareStdType("Boolean", BooleanType),
			CharDecl = DeclareStdType("Char", CharType),
			IntegerDecl = DeclareStdType("Integer", IntegerType);

		public static readonly ConstDeclaration FalseDecl = DeclareStdConst("false", BooleanType),
			TrueDecl = DeclareStdConst("true", BooleanType);

		public static readonly BinaryOperatorDeclaration AddDecl = DeclareStdBinaryOp("+", IntegerType, IntegerType, IntegerType),
			SubtractDecl = DeclareStdBinaryOp("-", IntegerType, IntegerType, IntegerType),
			MultiplyDecl = DeclareStdBinaryOp("*", IntegerType, IntegerType, IntegerType),
			DivideDecl = DeclareStdBinaryOp("/", IntegerType, IntegerType, IntegerType),
			EqualDecl = DeclareStdBinaryOp("=", AnyType, AnyType, BooleanType),
			LessDecl = DeclareStdBinaryOp("<", IntegerType, IntegerType, BooleanType),
			GreaterDecl = DeclareStdBinaryOp(">", IntegerType, IntegerType, BooleanType);

		public static readonly ProcDeclaration GetDecl = DeclareStdProc("get", new SingleFormalParameterSequence(new VarFormalParameter(CharType))),
			PutDecl = DeclareStdProc("put", new SingleFormalParameterSequence(new ConstFormalParameter(CharType))),
			GetintDecl = DeclareStdProc("getint", new SingleFormalParameterSequence(new VarFormalParameter(IntegerType))),
			PutintDecl = DeclareStdProc("putint", new SingleFormalParameterSequence(new ConstFormalParameter(IntegerType))),
			PuteolDecl = DeclareStdProc("puteol", new EmptyFormalParameterSequence());

		public static readonly FuncDeclaration ChrDecl = DeclareStdFunc("chr", new SingleFormalParameterSequence(new ConstFormalParameter(IntegerType)), CharType),
			OrdDecl = DeclareStdFunc("ord", new SingleFormalParameterSequence(new ConstFormalParameter(CharType)), IntegerType),
			EolDecl = DeclareStdFunc("eol", new EmptyFormalParameterSequence(), BooleanType),
			EofDecl = DeclareStdFunc("eof", new EmptyFormalParameterSequence(), BooleanType);
		private static TypeDeclaration DeclareStdType(string id, TypeDenoter typedenoter) { return new TypeDeclaration(new Identifier(id), typedenoter); }
		private static ConstDeclaration DeclareStdConst(string id, TypeDenoter constType) {
			return new ConstDeclaration(new Identifier(id), new EmptyExpression { Type = constType });
		}
		private static ProcDeclaration DeclareStdProc(string id, FormalParameterSequence fps) { return new ProcDeclaration(new Identifier(id), fps); }
		private static FuncDeclaration DeclareStdFunc(string id, FormalParameterSequence fps, TypeDenoter resultType) {
			return new FuncDeclaration(new Identifier(id), fps, resultType);
		}
		private static BinaryOperatorDeclaration DeclareStdBinaryOp(string op, TypeDenoter arg1Type, TypeDenoter arg2Type, TypeDenoter resultType) {
			return new BinaryOperatorDeclaration(new Operator(op, SourcePosition.Empty), arg1Type, arg2Type, resultType);
		}
	}
}