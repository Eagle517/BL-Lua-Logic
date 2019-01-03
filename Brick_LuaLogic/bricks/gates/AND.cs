datablock fxDTSBrickData(Logic1x2fANDData : Logic1x2fORData)
{
	uiName = "1x2f AND";
	iconName = $LuaLogic::Path @ "icons/AND";
	logicUIName = "AND";
	logicUIDesc = "C is true if A and B are true";
	logic = "gate.ports[3]:setstate(gate.ports[1].state and gate.ports[2].state)";
};
lualogic_registergatedefinition("Logic1x2fANDData");
