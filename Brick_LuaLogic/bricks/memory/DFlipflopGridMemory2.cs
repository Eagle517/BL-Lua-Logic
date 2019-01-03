datablock fxDTSBrickData(LogicGate_DFlipflopGridMemory2_Data)
{
	brickFile = $LuaLogic::Path @ "bricks/blb/DFlipflopGridMemory2.blb";
	category = "Logic Bricks";
	subCategory = "Memory";
	uiName = "D Flipflop Grid Memory 2";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "D Flipflop Grid Memory 2";
	logicUIDesc = "D Flipflop where Clk = C & A & B, R = Q & A & B";

	logic =
"if gate.ports[3].state and gate.ports[4].state and gate.ports[6]:isrising() then " @
"	gate.ports[1]:setstate(gate.ports[5].state) " @
"end " @
"gate.ports[2]:setstate(gate.ports[3].state and gate.ports[4].state and gate.ports[1].state)";

	numLogicPorts = 6;

	logicPortType[0] = 0;
	logicPortPos[0] = "0 0 4";
	logicPortDir[0] = 4;
	logicPortUIName[0] = "Q";

	logicPortType[1] = 0;
	logicPortPos[1] = "0 0 4";
	logicPortDir[1] = 1;
	logicPortUIName[1] = "Readout";

	logicPortType[2] = 1;
	logicPortPos[2] = "0 0 0";
	logicPortDir[2] = 2;
	logicPortCauseUpdate[2] = true;
	logicPortUIName[2] = "A";

	logicPortType[3] = 1;
	logicPortPos[3] = "0 0 2";
	logicPortDir[3] = 1;
	logicPortCauseUpdate[3] = true;
	logicPortUIName[3] = "B";

	logicPortType[4] = 1;
	logicPortPos[4] = "0 0 -4";
	logicPortDir[4] = 1;
	logicPortCauseUpdate[4] = false;
	logicPortUIName[4] = "Data";

	logicPortType[5] = 1;
	logicPortPos[5] = "0 0 -2";
	logicPortDir[5] = 1;
	logicPortCauseUpdate[5] = true;
	logicPortUIName[5] = "Clock";
};
lualogic_registergatedefinition("LogicGate_DFlipflopGridMemory2_Data");
