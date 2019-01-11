datablock fxDTSBrickData(LogicGate_FullAdder_Data)
{
	brickFile = $LuaLogic::Path @ "bricks/blb/FullAdder.blb";
	category = "Logic Bricks";
	subCategory = "Math";
	uiName = "Full Adder";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Full Adder";
	logicUIDesc = "Adds A and B with carry in";

	logic =
"local a, b, c = bool_to_int[gate.ports[1].state], bool_to_int[gate.ports[2].state], bool_to_int[gate.ports[3].state] " @
"gate.ports[4]:setstate(bit.bxor(bit.bxor(a, b), c) == 1) " @
"gate.ports[5]:setstate(bit.bor(bit.bor(bit.band(b, c), bit.band(a, c)), bit.band(a, b)) == 1)";

	numLogicPorts = 5;

	logicPortType[0] = 1;
	logicPortPos[0] = "-1 0 0";
	logicPortDir[0] = 3;
	logicPortCauseUpdate[0] = true;
	logicPortUIName[0] = "A";

	logicPortType[1] = 1;
	logicPortPos[1] = "1 0 0";
	logicPortDir[1] = 3;
	logicPortCauseUpdate[1] = true;
	logicPortUIName[1] = "B";

	logicPortType[2] = 1;
	logicPortPos[2] = "1 0 0";
	logicPortDir[2] = 2;
	logicPortCauseUpdate[2] = true;
	logicPortUIName[2] = "Carry In";

	logicPortType[3] = 0;
	logicPortPos[3] = "-1 0 0";
	logicPortDir[3] = 1;
	logicPortUIName[3] = "Sum";

	logicPortType[4] = 0;
	logicPortPos[4] = "-1 0 0";
	logicPortDir[4] = 0;
	logicPortUIName[4] = "Carry Out";
};
lualogic_registergatedefinition("LogicGate_FullAdder_Data");
