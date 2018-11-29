namespace Triangle.Compiler.SyntaxTrees.Visitors {
	// What's this?
	// The visitor methods all have an argument and a return type.
	// However, we often don't want to take an argument or return a value, so we need a "nothing" type.
	// That is what Void is.
	// It is impossible to create a void object (the constructor is private and it impossible to create subclasses).
	// Therefore the only allowable item of type Void is null.
	public sealed class Void { private Void() { } }
}