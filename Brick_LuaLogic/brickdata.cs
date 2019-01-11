//Wires
exec("./bricks/wires.cs");

//Gates
exec("./bricks/gates/diode.cs");
exec("./bricks/gates/NOT.cs");
exec("./bricks/gates/OR.cs");
exec("./bricks/gates/AND.cs");
exec("./bricks/gates/NOR.cs");
exec("./bricks/gates/NAND.cs");
exec("./bricks/gates/XOR.cs");
exec("./bricks/gates/XNOR.cs");

	//Vertical
exec("./bricks/gates/verticalDiode.cs");
exec("./bricks/gates/verticalNOT.cs");

//Bus
exec("./bricks/bus/8BitEnabler.cs");
exec("./bricks/bus/8BitDFlipFlop.cs");

//Inputs
exec("./bricks/inputs/switch.cs");

//Math
	//Addition
exec("./bricks/math/HalfAdder.cs");
exec("./bricks/math/FullAdder.cs");
exec("./bricks/math/8bitAdder.cs");

	//Subtraction
exec("./bricks/math/HalfSubtractor.cs");
exec("./bricks/math/FullSubtractor.cs");
exec("./bricks/math/8bitSubtractor.cs");

	//Multiplication
exec("./bricks/math/8bitMultiplier.cs");

	//Division
exec("./bricks/math/8bitDivisor.cs");

//Memory
exec("./bricks/memory/DFlipFlop.cs");
exec("./bricks/memory/DFlipflopGridMemory2.cs");

//Special
exec("./bricks/special/pixel.cs");
exec("./bricks/special/HorizontalPixel.cs");
