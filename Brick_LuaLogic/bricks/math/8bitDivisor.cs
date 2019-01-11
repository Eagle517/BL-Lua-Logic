datablock fxDTSBrickData(LogicGate_8bitDivisor_Data)
{
	brickFile = $LuaLogic::Path @ "bricks/blb/8bitMultiplier.blb";
	category = "Logic Bricks";
	subCategory = "Math";
	uiName = "8bit Divisor";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "8bit Divisor";
	logicUIDesc = "Divides A by B";

	logic =
"local a, b, n = 0, 0 " @
"for i = 1, 8 do " @
	"local n = 2^(i-1) " @
	"a = a + bool_to_int[gate.ports[i].state] * n " @
	"b = b + bool_to_int[gate.ports[i+8].state] * n " @
"end " @
"if b ~= 0 then " @
	"local q = math.floor(a/b) " @
	"local r = a-q*b " @
	"for i = 1, 8 do " @
		"local n = 2^(i-1) " @
		"gate.ports[i+16]:setstate(bit.band(q, n) > 0) " @
		"gate.ports[i+24]:setstate(bit.band(r, n) > 0) " @
	"end " @
"else " @
	"for i = 1, 8 do " @
		"gate.ports[i+16]:setstate(false) " @
		"gate.ports[i+24]:setstate(false) " @
	"end " @
"end";

	numLogicPorts = 32;

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

	logicPortType[16] = 0;
	logicPortPos[16] = "15 1 0";
	logicPortDir[16] = 1;
	logicPortUIName[16] = "Q0";

	logicPortType[17] = 0;
	logicPortPos[17] = "13 1 0";
	logicPortDir[17] = 1;
	logicPortUIName[17] = "Q1";

	logicPortType[18] = 0;
	logicPortPos[18] = "11 1 0";
	logicPortDir[18] = 1;
	logicPortUIName[18] = "Q2";

	logicPortType[19] = 0;
	logicPortPos[19] = "9 1 0";
	logicPortDir[19] = 1;
	logicPortUIName[19] = "Q3";

	logicPortType[20] = 0;
	logicPortPos[20] = "7 1 0";
	logicPortDir[20] = 1;
	logicPortUIName[20] = "Q4";

	logicPortType[21] = 0;
	logicPortPos[21] = "5 1 0";
	logicPortDir[21] = 1;
	logicPortUIName[21] = "Q5";

	logicPortType[22] = 0;
	logicPortPos[22] = "3 1 0";
	logicPortDir[22] = 1;
	logicPortUIName[22] = "Q6";

	logicPortType[23] = 0;
	logicPortPos[23] = "1 1 0";
	logicPortDir[23] = 1;
	logicPortUIName[23] = "Q7";

	logicPortType[24] = 0;
	logicPortPos[24] = "-1 1 0";
	logicPortDir[24] = 1;
	logicPortUIName[24] = "R0";

	logicPortType[25] = 0;
	logicPortPos[25] = "-3 1 0";
	logicPortDir[25] = 1;
	logicPortUIName[25] = "R1";

	logicPortType[26] = 0;
	logicPortPos[26] = "-5 1 0";
	logicPortDir[26] = 1;
	logicPortUIName[26] = "R2";

	logicPortType[27] = 0;
	logicPortPos[27] = "-7 1 0";
	logicPortDir[27] = 1;
	logicPortUIName[27] = "R3";

	logicPortType[28] = 0;
	logicPortPos[28] = "-9 1 0";
	logicPortDir[28] = 1;
	logicPortUIName[28] = "R4";

	logicPortType[29] = 0;
	logicPortPos[29] = "-11 1 0";
	logicPortDir[29] = 1;
	logicPortUIName[29] = "R5";

	logicPortType[30] = 0;
	logicPortPos[30] = "-13 1 0";
	logicPortDir[30] = 1;
	logicPortUIName[30] = "R6";

	logicPortType[31] = 0;
	logicPortPos[31] = "-15 1 0";
	logicPortDir[31] = 1;
	logicPortUIName[31] = "R7";
};
lualogic_registergatedefinition("LogicGate_8bitDivisor_Data");
