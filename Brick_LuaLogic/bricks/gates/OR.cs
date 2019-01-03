datablock fxDTSBrickData(LogicGate_OR_Data)
{
	category = "Logic Bricks";
	subCategory = "Gates";
	uiName = "1x2f OR";
	iconName = $LuaLogic::Path @ "icons/OR";
	brickFile = $LuaLogic::Path @ "bricks/blb/1x2f_2i_1o.blb";
	hasPrint = 1;
	printAspectRatio = "Logic";

	isLogic = 1;
	isLogicGate = 1;
	logicUIName = "OR";
	logicUIDesc = "C is true if A or B are true";

	logic = "gate.ports[3]:setstate(gate.ports[1].state or gate.ports[2].state)";

	numLogicPorts = 3;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 1 0";
	logicPortDir[0] = "0";
	logicPortCauseUpdate[0] = true;
	logicPortUIName[0] = "A";

	logicPortType[1] = 1;
	logicPortPos[1] = "0 -1 0";
	logicPortDir[1] = "0";
	logicPortCauseUpdate[1] = true;
	logicPortUIName[1] = "B";

	logicPortType[2] = 0;
	logicPortPos[2] = "0 -1 0";
	logicPortDir[2] = "2";
	logicPortUIName[2] = "C";
};
lualogic_registergatedefinition("LogicGate_OR_Data");
