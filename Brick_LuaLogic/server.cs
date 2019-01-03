$LuaLogic::Path = filePath(ExpandFilename("./server.cs")) @ "/";

if($Pref::Server::LuaLogic::OPT_TICK_ENABLED $= "")	$Pref::Server::LuaLogic::OPT_TICK_ENABLED = true;
if($Pref::Server::LuaLogic::OPT_TICK_TIME $= "")	$Pref::Server::LuaLogic::OPT_TICK_TIME = 0;
if($Pref::Server::LuaLogic::OPT_FX_UPDATES $= "")	$Pref::Server::LuaLogic::OPT_FX_UPDATES = true;
if($Pref::Server::LuaLogic::OPT_FX_TIME $= "")		$Pref::Server::LuaLogic::OPT_FX_TIME = 0.03;

exec("./utilities.cs");
exec("./tcp.cs");
exec("./bricks.cs");
exec("./brickdata.cs");
exec("./cmds.cs");

function lualogic_loadprintsandcolors()
{
	lualogic_definecolor("RED",		"0.729412 0.137255 0.137255 1.000000");
	lualogic_definecolor("GREEN",	"0.411765 0.564706 0.266667 1.000000");

	lualogic_defineprint("ARROW",		"Add-Ons/Print_Logic_Default/prints/arrow.png");
	lualogic_defineprint("UPARROW",		"Add-Ons/Print_Logic_Default/prints/uparrow.png");
	lualogic_defineprint("DOWNARROW",	"Add-Ons/Print_Logic_Default/prints/downarrow.png");

	for(%i = 0; %i < 8; %i++)
	{
		%a = (%i >> 2) & 1;
		%b = (%i >> 1) & 1;
		%c = (%i >> 0) & 1;
		lualogic_defineprint("COLOR"@%a@%b@%c,	"Add-Ons/Print_Logic_Default/prints/color_"@%a@%b@%c@".png");
	}
}
schedule(0, 0, "lualogic_loadprintsandcolors");

package LuaLogic
{
	// function onMissionLoaded()
	// {
	// 	parent::onMissionLoaded();
	// 	lualogic_loadprintsandcolors();
	// }

	function onServerDestroyed()
	{
		deleteVariables("$LuaLogic*");
		parent::onServerDestroyed();
	}
};
activatePackage("LuaLogic");
