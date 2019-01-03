datablock fxDTSBrickData(LogicGate_NotUp_Data)
{
	brickFile = $LuaLogic::Path @ "bricks/blb/1x1fU_1i_1o.blb";
	category = "Logic Bricks";
	subCategory = "Gates";
	uiName = "Not Up";
	iconName = $LuaLogic::Path @ "icons/NOTup";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Not Up";
	logicUIDesc = "B is the inverse of A";

	logic = "gate.ports[2]:setstate(not gate.ports[1].state)";

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
lualogic_registergatedefinition("LogicGate_NotUp_Data");

datablock fxDTSBrickData(LogicGate_NotDown_Data)
{
	brickFile = $LuaLogic::Path @ "bricks/blb/1x1fD_1i_1o.blb";
	category = "Logic Bricks";
	subCategory = "Gates";
	uiName = "Not Down";
	iconName = $LuaLogic::Path @ "icons/NOTdown";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Not Down";
	logicUIDesc = "B is the inverse of A";

	logic = "gate.ports[2]:setstate(not gate.ports[1].state)";

	numLogicPorts = 2;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 0 0";
	logicPortDir[0] = 4;
	logicPortCauseUpdate[0] = true;
	logicPortUIName[0] = "A";

	logicPortType[1] = 0;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = 5;
	logicPortUIName[1] = "B";
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
