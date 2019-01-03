datablock fxDTSBrickData(Logic1x1fNOTData)
{
	category = "Logic Bricks";
	subCategory = "Gates";
	uiName = "1x1f NOT";
	iconName = $LuaLogic::Path @ "icons/NOT";
	brickFile = $LuaLogic::Path @ "bricks/blb/1x1f_1i_1o.blb";
	hasPrint = 1;
	printAspectRatio = "Logic";

	isLogic = 1;
	isLogicGate = 1;
	logicUIName = "NOT";
	logicUIDesc = "B is the opposite of A";

	logic = "gate.ports[2]:setstate(not gate.ports[1].state)";

	numLogicPorts = 2;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 0 0";
	logicPortDir[0] = "0";
	logicPortCauseUpdate[0] = true;
	logicPortUIName[0] = "A";
	logicPortUIDesc[0] = "";

	logicPortType[1] = 0;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = "2";
	logicPortUIName[1] = "B";
	logicPortUIDesc[1] = "";
};
lualogic_registergatedefinition("Logic1x1fNOTData");

function Logic1x1fNOTData::onPlant(%this, %obj)
{
	if(lualogic_iscolor("RED"))
		%obj.setColor(lualogic_getcolor("RED"));
	
	if(lualogic_isprint("ARROW"))
		%obj.setPrint(lualogic_getprint("ARROW"));
	
	parent::onPlant(%this, %obj);
}
