datablock fxDTSBrickData(LogicGate_NAND_Data : LogicGate_OR_Data)
{
	uiName = "1x2f NAND";
	iconName = $LuaLogic::Path @ "icons/NAND";
	logicUIName = "NAND";
	logicUIDesc = "C is false if A and B are true";
	logic = "gate.ports[3]:setstate(not (gate.ports[1].state and gate.ports[2].state))";
};
lualogic_registergatedefinition("LogicGate_NAND_Data");
