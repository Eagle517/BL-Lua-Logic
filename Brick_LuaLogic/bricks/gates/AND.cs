datablock fxDTSBrickData(LogicGate_AND_Data : LogicGate_OR_Data)
{
	uiName = "1x2f AND";
	iconName = $LuaLogic::Path @ "icons/AND";
	logicUIName = "AND";
	logicUIDesc = "C is true if A and B are true";
	logic = "gate.ports[3]:setstate(gate.ports[1].state and gate.ports[2].state)";
};
lualogic_registergatedefinition("LogicGate_AND_Data");
