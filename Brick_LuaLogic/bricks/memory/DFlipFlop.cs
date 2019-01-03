datablock fxDTSBrickData(Logic1x2fDFlipFlop : Logic1x2fORData)
{
	subCategory = "Memory";
	uiName = "D FlipFlop";
	iconName = $LuaLogic::Path @ "icons/DFlipFlop";

	logicUIName = "D FlipFlop";
	logicUIDesc = "Q becomes D when C rises";

	logic = "if gate.ports[1]:isrising() then gate.ports[3]:setstate(gate.ports[2].state) end";

	logicPortUIName[0] = "C";
	logicPortCauseUpdate[1] = false;
	logicPortUIName[1] = "D";
	logicPortUIName[2] = "Q";
};
lualogic_registergatedefinition("Logic1x2fDFlipFlop");
