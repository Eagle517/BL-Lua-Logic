datablock fxDTSBrickData(LogicGate_HalfSubtractor_Data)
{
	brickFile = $LuaLogic::Path @ "bricks/blb/HalfAdder.blb";
	category = "Logic Bricks";
	subCategory = "Math";
	uiName = "Half Subtractor";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Half Subtractor";
	logicUIDesc = "Subtracts B from A";

	logic =
"gate.ports[3]:setstate(bit.bxor(bool_to_int[gate.ports[1].state], bool_to_int[gate.ports[2].state]) == 1) " @
"gate.ports[4]:setstate(not gate.ports[1].state and gate.ports[2].state)";

	numLogicPorts = 4;

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

	logicPortType[2] = 0;
	logicPortPos[2] = "-1 0 0";
	logicPortDir[2] = 1;
	logicPortUIName[2] = "Difference";

	logicPortType[3] = 0;
	logicPortPos[3] = "-1 0 0";
	logicPortDir[3] = 0;
	logicPortUIName[3] = "Borrow";
};
lualogic_registergatedefinition("LogicGate_HalfSubtractor_Data");
