datablock fxDTSBrickData(Logic1x2fXORData : Logic1x2fORData)
{
	uiName = "1x2f XOR";
	iconName = $LuaLogic::Path @ "icons/XOR";
	logicUIName = "XOR";
	logicUIDesc = "C is true if A or B are true but false if A and B are true";
	logic = "gate.ports[3]:setstate((gate.ports[1].state or gate.ports[2].state) and not (gate.ports[1].state and gate.ports[2].state))";
};
lualogic_registergatedefinition("Logic1x2fXORData");
