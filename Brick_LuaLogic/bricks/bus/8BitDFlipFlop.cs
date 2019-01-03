datablock fxDTSBrickData(LogicGate_8BitDFlipFlop_Data)
{
	brickFile = $LuaLogic::Path @ "bricks/blb/1x8f_8i_8o_p.blb";
	category = "Logic Bricks";
	subCategory = "Bus";
	uiName = "8 Bit D FlipFlop";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "8 Bit D FlipFlop";
	logicUIDesc = "8 bit d flipflop with clock propagate";

	logic =
"if gate.ports[9]:isrising() then " @
"	for i = 1, 8 do " @
"		gate.ports[i+10]:setstate(gate.ports[i].state) " @
"	end " @
"end " @
"gate.ports[10]:setstate(gate.ports[9].state)";

	numLogicPorts = 18;

	logicPortType[0] = 1;
	logicPortPos[0] = "-7 0 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "D7";

	logicPortType[1] = 1;
	logicPortPos[1] = "-5 0 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "D6";

	logicPortType[2] = 1;
	logicPortPos[2] = "-3 0 0";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "D5";

	logicPortType[3] = 1;
	logicPortPos[3] = "-1 0 0";
	logicPortDir[3] = 3;
	logicPortUIName[3] = "D4";

	logicPortType[4] = 1;
	logicPortPos[4] = "1 0 0";
	logicPortDir[4] = 3;
	logicPortUIName[4] = "D3";

	logicPortType[5] = 1;
	logicPortPos[5] = "3 0 0";
	logicPortDir[5] = 3;
	logicPortUIName[5] = "D2";

	logicPortType[6] = 1;
	logicPortPos[6] = "5 0 0";
	logicPortDir[6] = 3;
	logicPortUIName[6] = "D1";

	logicPortType[7] = 1;
	logicPortPos[7] = "7 0 0";
	logicPortDir[7] = 3;
	logicPortUIName[7] = "D0";

	logicPortType[8] = 1;
	logicPortPos[8] = "7 0 0";
	logicPortDir[8] = 2;
	logicPortCauseUpdate[8] = true;
	logicPortUIName[8] = "ClockIn";

	logicPortType[9] = 0;
	logicPortPos[9] = "-7 0 0";
	logicPortDir[9] = 0;
	logicPortUIName[9] = "ClockOut";

	logicPortType[10] = 0;
	logicPortPos[10] = "-7 0 0";
	logicPortDir[10] = 1;
	logicPortUIName[10] = "Q7";

	logicPortType[11] = 0;
	logicPortPos[11] = "-5 0 0";
	logicPortDir[11] = 1;
	logicPortUIName[11] = "Q6";

	logicPortType[12] = 0;
	logicPortPos[12] = "-3 0 0";
	logicPortDir[12] = 1;
	logicPortUIName[12] = "Q5";

	logicPortType[13] = 0;
	logicPortPos[13] = "-1 0 0";
	logicPortDir[13] = 1;
	logicPortUIName[13] = "Q4";

	logicPortType[14] = 0;
	logicPortPos[14] = "1 0 0";
	logicPortDir[14] = 1;
	logicPortUIName[14] = "Q3";

	logicPortType[15] = 0;
	logicPortPos[15] = "3 0 0";
	logicPortDir[15] = 1;
	logicPortUIName[15] = "Q2";

	logicPortType[16] = 0;
	logicPortPos[16] = "5 0 0";
	logicPortDir[16] = 1;
	logicPortUIName[16] = "Q1";

	logicPortType[17] = 0;
	logicPortPos[17] = "7 0 0";
	logicPortDir[17] = 1;
	logicPortUIName[17] = "Q0";
};
lualogic_registergatedefinition("LogicGate_8BitDFlipFlop_Data");
