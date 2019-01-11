datablock fxDTSBrickData(LogicGate_8bitSubtractor_Data)
{
	brickFile = $LuaLogic::Path @ "bricks/blb/8bitAdder.blb";
	category = "Logic Bricks";
	subCategory = "Math";
	uiName = "8bit Subtractor";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "8bit Subtractor";
	logicUIDesc = "Subtracts B from A";

	logic =
"local c = bool_to_int[gate.ports[17].state] " @
"local a = 0 " @
"local b = 0 " @
"for i = 1, 8 do " @
	"a = bool_to_int[gate.ports[i].state] " @
	"b = bool_to_int[gate.ports[i+8].state] " @
	"gate.ports[i+17]:setstate(bit.bxor(bit.bxor(a, b), c) == 1) " @
	"c = bit.bor(bit.bor(bit.band(bool_to_int[a == 0], b), bit.band(bool_to_int[a == 0], c)), bit.band(b, c)) " @
"end " @
"gate.ports[26]:setstate(c == 1)";

	numLogicPorts = 26;

	logicPortType[0] = 1;
	logicPortPos[0] = "-1 -1 0";
	logicPortDir[0] = 3;
	logicPortCauseUpdate[0] = true;
	logicPortUIName[0] = "A0";

	logicPortType[1] = 1;
	logicPortPos[1] = "-3 -1 0";
	logicPortDir[1] = 3;
	logicPortCauseUpdate[1] = true;
	logicPortUIName[1] = "A1";

	logicPortType[2] = 1;
	logicPortPos[2] = "-5 -1 0";
	logicPortDir[2] = 3;
	logicPortCauseUpdate[2] = true;
	logicPortUIName[2] = "A2";

	logicPortType[3] = 1;
	logicPortPos[3] = "-7 -1 0";
	logicPortDir[3] = 3;
	logicPortCauseUpdate[3] = true;
	logicPortUIName[3] = "A3";

	logicPortType[4] = 1;
	logicPortPos[4] = "-9 -1 0";
	logicPortDir[4] = 3;
	logicPortCauseUpdate[4] = true;
	logicPortUIName[4] = "A4";

	logicPortType[5] = 1;
	logicPortPos[5] = "-11 -1 0";
	logicPortDir[5] = 3;
	logicPortCauseUpdate[5] = true;
	logicPortUIName[5] = "A5";

	logicPortType[6] = 1;
	logicPortPos[6] = "-13 -1 0";
	logicPortDir[6] = 3;
	logicPortCauseUpdate[6] = true;
	logicPortUIName[6] = "A6";

	logicPortType[7] = 1;
	logicPortPos[7] = "-15 -1 0";
	logicPortDir[7] = 3;
	logicPortCauseUpdate[7] = true;
	logicPortUIName[7] = "A7";

	logicPortType[8] = 1;
	logicPortPos[8] = "15 -1 0";
	logicPortDir[8] = 3;
	logicPortCauseUpdate[8] = true;
	logicPortUIName[8] = "B0";

	logicPortType[9] = 1;
	logicPortPos[9] = "13 -1 0";
	logicPortDir[9] = 3;
	logicPortCauseUpdate[9] = true;
	logicPortUIName[9] = "B1";

	logicPortType[10] = 1;
	logicPortPos[10] = "11 -1 0";
	logicPortDir[10] = 3;
	logicPortCauseUpdate[10] = true;
	logicPortUIName[10] = "B2";

	logicPortType[11] = 1;
	logicPortPos[11] = "9 -1 0";
	logicPortDir[11] = 3;
	logicPortCauseUpdate[11] = true;
	logicPortUIName[11] = "B3";

	logicPortType[12] = 1;
	logicPortPos[12] = "7 -1 0";
	logicPortDir[12] = 3;
	logicPortCauseUpdate[12] = true;
	logicPortUIName[12] = "B4";

	logicPortType[13] = 1;
	logicPortPos[13] = "5 -1 0";
	logicPortDir[13] = 3;
	logicPortCauseUpdate[13] = true;
	logicPortUIName[13] = "B5";

	logicPortType[14] = 1;
	logicPortPos[14] = "3 -1 0";
	logicPortDir[14] = 3;
	logicPortCauseUpdate[14] = true;
	logicPortUIName[14] = "B6";

	logicPortType[15] = 1;
	logicPortPos[15] = "1 -1 0";
	logicPortDir[15] = 3;
	logicPortCauseUpdate[15] = true;
	logicPortUIName[15] = "B7";

	logicPortType[16] = 1;
	logicPortPos[16] = "15 -1 0";
	logicPortDir[16] = 2;
	logicPortCauseUpdate[16] = true;
	logicPortUIName[16] = "Borrow In";

	logicPortType[17] = 0;
	logicPortPos[17] = "15 1 0";
	logicPortDir[17] = 1;
	logicPortUIName[17] = "Diff0";

	logicPortType[18] = 0;
	logicPortPos[18] = "13 1 0";
	logicPortDir[18] = 1;
	logicPortUIName[18] = "Diff1";

	logicPortType[19] = 0;
	logicPortPos[19] = "11 1 0";
	logicPortDir[19] = 1;
	logicPortUIName[19] = "Diff2";

	logicPortType[20] = 0;
	logicPortPos[20] = "9 1 0";
	logicPortDir[20] = 1;
	logicPortUIName[20] = "Diff3";

	logicPortType[21] = 0;
	logicPortPos[21] = "7 1 0";
	logicPortDir[21] = 1;
	logicPortUIName[21] = "Diff4";

	logicPortType[22] = 0;
	logicPortPos[22] = "5 1 0";
	logicPortDir[22] = 1;
	logicPortUIName[22] = "Diff5";

	logicPortType[23] = 0;
	logicPortPos[23] = "3 1 0";
	logicPortDir[23] = 1;
	logicPortUIName[23] = "Diff6";

	logicPortType[24] = 0;
	logicPortPos[24] = "1 1 0";
	logicPortDir[24] = 1;
	logicPortUIName[24] = "Diff7";

	logicPortType[25] = 0;
	logicPortPos[25] = "-15 -1 0";
	logicPortDir[25] = 0;
	logicPortUIName[25] = "Borrow Out";
};
lualogic_registergatedefinition("LogicGate_8bitSubtractor_Data");
