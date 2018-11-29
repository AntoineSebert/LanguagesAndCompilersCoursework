using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Formals;
using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Types;

namespace Triangle.Compiler {
	public static class StandardEnvironment {
		public static readonly TypeDenoter BooleanType = new BoolTypeDenoter();
		public static readonly TypeDenoter CharType = new CharTypeDenoter();
		public static readonly TypeDenoter IntegerType = new IntTypeDenoter();
		public static readonly TypeDenoter AnyType = new AnyTypeDenoter();
		public static readonly TypeDenoter ErrorType = new ErrorTypeDenoter();

		public static readonly TypeDeclaration BooleanDecl = DeclareStdType("Boolean", BooleanType);
		public static readonly TypeDeclaration CharDecl = DeclareStdType("Char", CharType);
		public static readonly TypeDeclaration IntegerDecl = DeclareStdType("Integer", IntegerType);

		public static readonly ConstDeclaration FalseDecl = DeclareStdConst("false", BooleanType);
		public static readonly ConstDeclaration TrueDecl = DeclareStdConst("true", BooleanType);

		public static readonly BinaryOperatorDeclaration AddDecl = DeclareStdBinaryOp("+", IntegerType, IntegerType, IntegerType);
		public static readonly BinaryOperatorDeclaration SubtractDecl = DeclareStdBinaryOp("-", IntegerType, IntegerType, IntegerType);
		public static readonly BinaryOperatorDeclaration MultiplyDecl = DeclareStdBinaryOp("*", IntegerType, IntegerType, IntegerType);
		public static readonly BinaryOperatorDeclaration DivideDecl = DeclareStdBinaryOp("/", IntegerType, IntegerType, IntegerType);
		public static readonly BinaryOperatorDeclaration EqualDecl = DeclareStdBinaryOp("=", AnyType, AnyType, BooleanType);
		public static readonly BinaryOperatorDeclaration LessDecl = DeclareStdBinaryOp("<", IntegerType, IntegerType, BooleanType);
		public static readonly BinaryOperatorDeclaration GreaterDecl = DeclareStdBinaryOp(">", IntegerType, IntegerType, BooleanType);

		public static readonly ProcDeclaration GetDecl = DeclareStdProc("get", new SingleFormalParameterSequence(new VarFormalParameter(CharType)));
		public static readonly ProcDeclaration PutDecl = DeclareStdProc("put", new SingleFormalParameterSequence(new ConstFormalParameter(CharType)));
		public static readonly ProcDeclaration GetintDecl = DeclareStdProc("getint", new SingleFormalParameterSequence(new VarFormalParameter(IntegerType)));
		public static readonly ProcDeclaration PutintDecl = DeclareStdProc("putint", new SingleFormalParameterSequence(new ConstFormalParameter(IntegerType)));
		public static readonly ProcDeclaration PuteolDecl = DeclareStdProc("puteol", new EmptyFormalParameterSequence());

		public static readonly FuncDeclaration ChrDecl = DeclareStdFunc("chr", new SingleFormalParameterSequence(new ConstFormalParameter(IntegerType)), CharType);
		public static readonly FuncDeclaration OrdDecl = DeclareStdFunc("ord", new SingleFormalParameterSequence(new ConstFormalParameter(CharType)), IntegerType);
		public static readonly FuncDeclaration EolDecl = DeclareStdFunc("eol", new EmptyFormalParameterSequence(), BooleanType);
		public static readonly FuncDeclaration EofDecl = DeclareStdFunc("eof", new EmptyFormalParameterSequence(), BooleanType);
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