using Triangle.Compiler.SyntaxTrees.Terminals;

namespace Triangle.Compiler.CodeGenerator.Entities {
	public interface IFetchableEntity {
		void EncodeFetch(Emitter emitter, Frame frame, int size, Identifier identifer);
	}
}