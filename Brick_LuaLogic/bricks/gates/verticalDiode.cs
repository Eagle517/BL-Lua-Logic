datablock fxDTSBrickData(LogicGate_DiodeUp_Data)
{
	brickFile = $LuaLogic::Path @ "bricks/blb/1x1fU_1i_1o.blb";
	category = "Logic Bricks";
	subCategory = "Gates";
	uiName = "Diode Up";
	iconName = $LuaLogic::Path @ "icons/diodeup";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Diode Up";
	logicUIDesc = "B is A";

	logic = "gate.ports[2]:setstate(gate.ports[1].state)";

	numLogicPorts = 2;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 0 0";
	logicPortDir[0] = 5;
	logicPortCauseUpdate[0] = true;
	logicPortUIName[0] = "A";

	logicPortType[1] = 0;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = 4;
	logicPortUIName[1] = "B";
};
lualogic_registergatedefinition("LogicGate_DiodeUp_Data");

datablock fxDTSBrickData(LogicGate_DiodeDown_Data : LogicGate_DiodeUp_Data)
{
	brickFile = $LuaLogic::Path @ "bricks/blb/1x1fD_1i_1o.blb";
	uiName = "Diode Down";
	iconName = $LuaLogic::Path @ "icons/diodedown";

	logicUIName = "Diode Down";

	logicPortDir[0] = 4;
	logicPortDir[1] = 5;
};
lualogic_registergatedefinition("LogicGate_DiodeDown_Data");

function LogicGate_DiodeUp_Data::onPlant(%this, %obj)
{
	if(lualogic_iscolor("GREEN"))
		%obj.setColor(lualogic_getcolor("GREEN"));
	
	if(lualogic_isprint("UPARROW"))
		%obj.setPrint(lualogic_getprint("UPARROW"));
	
	parent::onPlant(%this, %obj);
}

function LogicGate_DiodeDown_Data::onPlant(%this, %obj)
{
	if(lualogic_iscolor("GREEN"))
		%obj.setColor(lualogic_getcolor("GREEN"));
	
	if(lualogic_isprint("DOWNARROW"))
		%obj.setPrint(lualogic_getprint("DOWNARROW"));
	
	parent::onPlant(%this, %obj);
}
