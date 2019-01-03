datablock fxDTSBrickData(Logic1x2fNORData : Logic1x2fORData)
{
	uiName = "1x2f NOR";
	iconName = $LuaLogic::Path @ "icons/NOR";
	logicUIName = "NOR";
	logicUIDesc = "C is false if A or B are true";
	logic = "gate.ports[3]:setstate(not (gate.ports[1].state or gate.ports[2].state))";
};
lualogic_registergatedefinition("Logic1x2fNORData");
