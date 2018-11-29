using Triangle.AbstractMachine;
using Triangle.Compiler.CodeGenerator.Entities;

namespace Triangle.Compiler.CodeGenerator {
	public class Frame {
		public static readonly Frame Initial = new Frame(0, 0);

		public int Level { get; }
		public int Size { get; }

		private Frame(int level, int size) {
			Level = level;
			Size = size;
		}

		public Frame Expand(int sizeIncrement) { return new Frame(Level, Size + sizeIncrement); }

		public Frame Replace(int size) { return new Frame(Level, size); }

		public Frame Push(int size) { return new Frame((byte)(Level + 1), size); }

		/**
		 * Returns the display register appropriate for object code at the current static level to access a data object at the static level of the given address.
		 * @param	address	the address of the data object
		 * @return	the display register required for static addressing
		 */
		public Register DisplayRegister(ObjectAddress address) {
			if(address.Level == 0)
				return Register.SB;

			if(Level - address.Level <= 6)
				return (Register)(Level - address.Level); // LBr|L1r|...|L6r

			//errorReporter.ReportRestriction("can't access data more than 6 levels out");
			return Register.L6; // to allow code generation to continue
		}
	}
}