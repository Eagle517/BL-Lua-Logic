datablock fxDTSBrickData(LogicGate_NOT_Data : LogicGate_Diode_Data)
{
	uiName = "1x1f NOT";
	iconName = $LuaLogic::Path @ "icons/NOT";

	logicUIName = "NOT";
	logicUIDesc = "B is the opposite of A";

	logic = "gate.ports[2]:setstate(not gate.ports[1].state)";

	numLogicPorts = 2;
};
lualogic_registergatedefinition("LogicGate_NOT_Data");

function LogicGate_NOT_Data::onPlant(%this, %obj)
{
	if(lualogic_iscolor("RED"))
		%obj.setColor(lualogic_getcolor("RED"));
	
	if(lualogic_isprint("ARROW"))
		%obj.setPrint(lualogic_getprint("ARROW"));
	
	parent::onPlant(%this, %obj);
}
