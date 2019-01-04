datablock fxDTSBrickData(LogicGate_HorizontalPixel_Data)
{
	brickFile = $LuaLogic::Path @ "bricks/blb/pixels/HPixel.blb";
	category = "Logic Bricks";
	subCategory = "Special";
	uiName = "Horizontal Pixel";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Horizontal Pixel";
	logicUIDesc = "";

	logic = "gate:cb(\"3\t\" .. bool_to_int[gate.ports[1].state] .. \"\t\" .. bool_to_int[gate.ports[2].state] .. \"\t\" .. bool_to_int[gate.ports[3].state])";

	numLogicPorts = 3;

	logicPortType[0] = 1;
	logicPortPos[0] = "-1 1 0";
	logicPortDir[0] = 5;
	logicPortCauseUpdate[0] = true;
	logicPortUIName[0] = "R";

	logicPortType[1] = 1;
	logicPortPos[1] = "-1 -1 0";
	logicPortDir[1] = 5;
	logicPortCauseUpdate[1] = true;
	logicPortUIName[1] = "G";

	logicPortType[2] = 1;
	logicPortPos[2] = "1 -1 0";
	logicPortDir[2] = 5;
	logicPortCauseUpdate[2] = true;
	logicPortUIName[2] = "B";
};
lualogic_registergatedefinition("LogicGate_HorizontalPixel_Data");

function LogicGate_HorizontalPixel_Data::LuaLogic_Callback(%this, %obj, %data)
{
	%color = getField(%data, 0) @ getField(%data, 1) @ getField(%data, 2);
	if(lualogic_isprint("COLOR" @ %color))
		%obj.setPrint(lualogic_getprint("COLOR" @ %color));
}
