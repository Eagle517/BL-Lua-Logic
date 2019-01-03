datablock fxDTSBrickData(Logic1x1fDiodeData)
{
	category = "Logic Bricks";
	subCategory = "Gates";
	uiName = "1x1f Diode";
	iconName = $LuaLogic::Path @ "icons/diode";
	brickFile = $LuaLogic::Path @ "bricks/blb/1x1f_1i_1o.blb";
	hasPrint = 1;
	printAspectRatio = "Logic";

	isLogic = 1;
	isLogicGate = 1;
	logicUIName = "Diode";
	logicUIDesc = "B is A";

	logic = "gate.ports[2]:setstate(gate.ports[1].state)";

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
lualogic_registergatedefinition("Logic1x1fDiodeData");

function Logic1x1fDiodeData::onPlant(%this, %obj)
{
	if(lualogic_iscolor("GREEN"))
		%obj.setColor(lualogic_getcolor("GREEN"));
	
	if(lualogic_isprint("ARROW"))
		%obj.setPrint(lualogic_getprint("ARROW"));
	
	parent::onPlant(%this, %obj);
}
