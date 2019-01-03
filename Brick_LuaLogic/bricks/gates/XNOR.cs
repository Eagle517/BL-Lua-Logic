datablock fxDTSBrickData(LogicGate_XNOR_Data : LogicGate_OR_Data)
{
	uiName = "1x2f XNOR";
	iconName = $LuaLogic::Path @ "icons/XNOR";
	logicUIName = "XNOR";
	logicUIDesc = "C is true if A and B are both true or both false";
	logic = "gate.ports[3]:setstate((gate.ports[1].state and gate.ports[2].state) or (not gate.ports[1].state and not gate.ports[2].state))";
};
lualogic_registergatedefinition("LogicGate_XNOR_Data");
