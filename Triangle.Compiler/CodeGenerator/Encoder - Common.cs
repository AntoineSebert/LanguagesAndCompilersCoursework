using Triangle.AbstractMachine;
using Triangle.Compiler.CodeGenerator.Entities;
using Triangle.Compiler.SyntaxTrees;
using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Terminals;

namespace Triangle.Compiler.CodeGenerator {
	public partial class Encoder : IEncoderVisitor {
		private ErrorReporter errorReporter;
		private Emitter emitter;

		public Encoder(ErrorReporter _errorReporter) {
			emitter = new Emitter(errorReporter = _errorReporter);
			ElaborateStdEnvironment();
		}

		public void EncodeRun(Program ast) {
			ast.Visit(this, Frame.Initial);
			emitter.Emit(OpCode.HALT);
		}

		public void SaveObjectProgram(string objectFileName) { emitter.SaveObjectProgram(objectFileName); }

		// REGISTERS

		/* 
		 * Generates code to fetch the value of a named constant or variable and push it on to the stack.
		 * @param	currentLevel	routine level where the vname occurs.
		 * @param	frameSize		anticipated size of the local stack frame when the constant or variable is fetched at run-time.
		 * @param	valSize			size of the constant or variable's value.
		*/
		private void EncodeAssign(Identifier identifier, Frame frame, int valSize) {
			// If indexed = true, code will have been generated to load an index value.
			if(valSize > 255) {
				errorReporter.ReportRestriction("can't store values larger than 255 words");
				valSize = 255; // to allow code generation to continue
			}
			(identifier.Declaration.Entity as AddressableEntity).EncodeAssign(emitter, frame, valSize, identifier);
		}
		// Generates code to fetch the value of a named constant or variable and push it on to the stack.
		// currentLevel is the routine level where the vname occurs.
		// frameSize is the anticipated size of the local stack frame when the constant or variable is fetched at run-time.
		// valSize is the size of the constant or variable's value.
		private void EncodeFetch(Identifier identifier, Frame frame, int valSize) {
			// If indexed = true, code will have been generated to load an index value.
			if(valSize > 255) {
				errorReporter.ReportRestriction("can't load values larger than 255 words");
				valSize = 255; // to allow code generation to continue
			}
			(identifier.Declaration.Entity as IFetchableEntity).EncodeFetch(emitter, frame, valSize, identifier);
		}
		// Generates code to compute and push the address of a named variable.
		// vname is the program phrase that names this variable.
		// currentLevel is the routine level where the vname occurs.
		// frameSize is the anticipated size of the local stack frame when/ the variable is addressed at run-time.
		private void EncodeFetchAddress(Identifier identifier, Frame frame) {
			// If indexed = true, code will have been generated to load an index value.
			(identifier.Declaration.Entity as AddressableEntity).EncodeFetchAddress(emitter, frame, identifier);
		}
		// Decides run-time representation of a standard constant.
		private void ElaborateStdConst(ConstDeclaration decl, int value) {
			decl.Entity = new KnownValue(decl.Expression.Type.Visit(this, null), value);
			WriteTableDetails(decl);
		}
		// Decides run-time representation of a standard routine.
		private void ElaborateStdPrimRoutine(Declaration routineDeclaration, Primitive primitive) {
			routineDeclaration.Entity = new PrimitiveRoutine(Machine.ClosureSize, primitive);
			WriteTableDetails(routineDeclaration);
		}

		private void ElaborateStdEqRoutine(Declaration routineDeclaration, Primitive primitive) {
			routineDeclaration.Entity = new EqualityProcedure(Machine.ClosureSize, primitive);
			WriteTableDetails(routineDeclaration);
		}

		private void ElaborateStdEnvironment() {
			ElaborateStdConst(StandardEnvironment.FalseDecl, Machine.FalseValue);
			ElaborateStdConst(StandardEnvironment.TrueDecl, Machine.TrueValue);
			ElaborateStdPrimRoutine(StandardEnvironment.AddDecl, Primitive.ADD);
			ElaborateStdPrimRoutine(StandardEnvironment.SubtractDecl, Primitive.SUB);
			ElaborateStdPrimRoutine(StandardEnvironment.MultiplyDecl, Primitive.MULT);
			ElaborateStdPrimRoutine(StandardEnvironment.DivideDecl, Primitive.DIV);
			ElaborateStdPrimRoutine(StandardEnvironment.LessDecl, Primitive.LT);
			ElaborateStdPrimRoutine(StandardEnvironment.GreaterDecl, Primitive.GT);
			ElaborateStdPrimRoutine(StandardEnvironment.ChrDecl, Primitive.ID);
			ElaborateStdPrimRoutine(StandardEnvironment.OrdDecl, Primitive.ID);
			ElaborateStdPrimRoutine(StandardEnvironment.EolDecl, Primitive.EOL);
			ElaborateStdPrimRoutine(StandardEnvironment.EofDecl, Primitive.EOF);
			ElaborateStdPrimRoutine(StandardEnvironment.GetDecl, Primitive.GET);
			ElaborateStdPrimRoutine(StandardEnvironment.PutDecl, Primitive.PUT);
			ElaborateStdPrimRoutine(StandardEnvironment.GetintDecl, Primitive.GETINT);
			ElaborateStdPrimRoutine(StandardEnvironment.PutintDecl, Primitive.PUTINT);
			ElaborateStdPrimRoutine(StandardEnvironment.PuteolDecl, Primitive.PUTEOL);
			ElaborateStdEqRoutine(StandardEnvironment.EqualDecl, Primitive.EQ);
		}

		private static void WriteTableDetails(AbstractSyntaxTree ast) {}
	}
}