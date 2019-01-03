datablock fxDTSBrickData(LogicGate_Switch_Data)
{
	category = "Logic Bricks";
	subCategory = "Inputs";
	uiName = "Switch";
	iconName = $LuaLogic::Path @ "icons/switch";
	brickFile = $LuaLogic::Path @ "bricks/blb/switch.blb";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = 1;
	isLogicGate = 1;
	isLogicInput = 1;

	numLogicPorts = 2;

	logicPortType[0] = 0;
	logicPortPos[0] = "0 1 0";
	logicPortDir[0] = "1";

	logicPortType[1] = 0;
	logicPortPos[1] = "0 -1 0";
	logicPortDir[1] = "3";
};
lualogic_registergatedefinition("LogicGate_Switch_Data");

function LogicGate_Switch_Data::Logic_onInput(%this, %obj, %pos, %norm)
{
	%obj.Logic_SetInputState(!%obj.logicInputState);
}

function LogicGate_Switch_Data::Logic_onAdd(%this, %obj)
{
	%obj.Logic_SetInputState(%obj.getColorFXID() == 3, true);
}
