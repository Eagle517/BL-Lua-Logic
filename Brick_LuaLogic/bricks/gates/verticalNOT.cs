datablock fxDTSBrickData(LogicGate_NotUp_Data : LogicGate_DiodeUp_Data)
{
	uiName = "Not Up";
	iconName = $LuaLogic::Path @ "icons/NOTup";

	logicUIName = "Not Up";
	logicUIDesc = "B is the inverse of A";

	logic = "gate.ports[2]:setstate(not gate.ports[1].state)";
};
lualogic_registergatedefinition("LogicGate_NotUp_Data");

datablock fxDTSBrickData(LogicGate_NotDown_Data : LogicGate_DiodeUp_Data)
{
	uiName = "Not Down";
	iconName = $LuaLogic::Path @ "icons/NOTdown";

	logicUIName = "Not Down";
	logicUIDesc = "B is the inverse of A";

	logic = "gate.ports[2]:setstate(not gate.ports[1].state)";
};
lualogic_registergatedefinition("LogicGate_NotDown_Data");

function LogicGate_NotUp_Data::onPlant(%this, %obj)
{
	if(lualogic_iscolor("RED"))
		%obj.setColor(lualogic_getcolor("RED"));
	
	if(lualogic_isprint("UPARROW"))
		%obj.setPrint(lualogic_getprint("UPARROW"));
	
	parent::onPlant(%this, %obj);
}

function LogicGate_NotDown_Data::onPlant(%this, %obj)
{
	if(lualogic_iscolor("RED"))
		%obj.setColor(lualogic_getcolor("RED"));
	
	if(lualogic_isprint("DOWNARROW"))
		%obj.setPrint(lualogic_getprint("DOWNARROW"));
	
	parent::onPlant(%this, %obj);
}
