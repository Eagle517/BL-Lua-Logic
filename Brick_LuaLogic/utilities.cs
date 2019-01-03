function lualogic_registergatedefinition(%data)
{
	if(!isObject(%data))
		return;
	%id = %data.getID();

	if((%idx = $LuaLogic::GateDefinitionIDX[%id]) $= "")
	{
		%idx = $LuaLogic::NumGateDefintions+0;
		$LuaLogic::GateDefinitionIDX[%id] = %idx;
		$LuaLogic::NumGateDefintions++;
	}

	%def = %id @";"@ %data.logicUIName @";"@ %data.logicUIDesc @";"@ %data.logic @";"@ (%ports = %data.numLogicPorts);

	for(%i = 0; %i < %ports; %i++)
	{
		%def = %def @";"@ %data.logicPortType[%i] @";"@ %data.logicPortPos[%i] @";"@ %data.logicPortDir[%i]
				@";"@ (%data.logicPortCauseUpdate[%i] == true) @";"@ %data.logicPortUIName[%i];
	}

	$LuaLogic::GateDefintion[%idx] = %def;
}

function lualogic_print(%text)
{
	echo("LuaLogic -> ", %text);
}

function lualogic_roundpos(%pos)
{
	return mFloor(getWord(%pos, 0)*4)/4 SPC mFloor(getWord(%pos, 1)*4)/4 SPC mFloor(getWord(%pos, 2)*10)/10;
}

function lualogic_roundstudpos(%pos)
{
	return mFloor(getWord(%pos, 0)*2)/2 SPC mFloor(getWord(%pos, 1)*2)/2 SPC mFloor(getWord(%pos, 2)*5)/5;
}

function lualogic_pos(%pos)
{
	%pos = lualogic_roundpos(%pos);
	return getWord(%pos, 0)/0.25 SPC getWord(%pos, 1)/0.25 SPC getWord(%pos, 2)/0.1;
}

function lualogic_studpos(%pos)
{
	%pos = lualogic_roundstudpos(%pos);
	return getWord(%pos, 0)/0.5*2 + 1 SPC getWord(%pos, 1)/0.5*2 + 1 SPC getWord(%pos, 2)/0.2*2;
}

function lualogic_postobrick(%pos)
{
	return getWord(%pos, 0)*0.25 SPC getWord(%pos, 1)*0.25 SPC getWord(%pos, 2)*0.1;
}

function lualogic_connect(%port)
{
	if(isObject(LuaLogicTCP))
		LuaLogicTCP.delete();
	%tcp = new TCPObject(LuaLogicTCP);
	%tcp.connect("127.0.0.1:" @ %port);
}

function lualogic_send(%data)
{
	if(isObject(LuaLogicTCP) && LuaLogicTCP.isConnected)
	{
		while(strpos(%data, ";;") != -1)
			%data = strReplace(%data, ";;", "; ;");
		
		if(strlen(LuaLogicTCP.data) + strlen(%data) >= 1024)
			LuaLogicTCP.sendData();
		
		if(LuaLogicTCP.data $= "")
			LuaLogicTCP.data = %data;
		else
			LuaLogicTCP.data = LuaLogicTCP.data @ ";" @ %data;
	}
}

function lualogic_sendgatedefinitions()
{
	for(%i = 0; %i < $LuaLogic::NumGateDefintions; %i++)
		lualogic_send("GD;" @ $LuaLogic::GateDefintion[%i]);
}

function lualogic_sendoptions()
{
	lualogic_send("OPT;TICK_ENABLED;"	@ $Pref::Server::LuaLogic::OPT_TICK_ENABLED);
	lualogic_send("OPT;TICK_TIME;"		@ $Pref::Server::LuaLogic::OPT_TICK_TIME);
	lualogic_send("OPT;FX_UPDATES;"		@ $Pref::Server::LuaLogic::OPT_FX_UPDATES);
	lualogic_send("OPT;FX_TIME;"		@ $Pref::Server::LuaLogic::OPT_FX_TIME);
}

function lualogic_ss(%obj, %state)
{
	lualogic_send("SG;" @ %obj @ ";" @ (%state == true));
}

function lualogic_definecolor(%color, %rgb, %allowTransparency)
{
	%r = getWord(%rgb, 0);
	%g = getWord(%rgb, 1);
	%b = getWord(%rgb, 2);

	%alpha = %allowTransparency ? 0.001 : 1;

	%bestDist = 9e9;

	for(%i = 0; %i < 64; %i++)
	{
		%crgba = getColorIDTable(%i);
		if(getWord(%crgba, 3) >= %alpha)
		{
			%dr = getWord(%crgba, 0) - %r;
			%dg = getWord(%crgba, 1) - %g;
			%db = getWord(%crgba, 2) - %b;
			%dist = %dr*%dr + %dg*%dg + %db*%db;

			if(%dist < %bestDist)
			{
				%bestDist = %dist;
				%bestColor = %i;
			}
		}
	}

	$LuaLogic::Color[%color] = %bestColor;
	return %bestColor;
}

function lualogic_iscolor(%color)
{
	return $LuaLogic::Color[%color] !$= "";
}

function lualogic_getcolor(%color)
{
	if($LuaLogic::Color[%color] !$= "")
		return $LuaLogic::Color[%color];
	return 0;
}

function lualogic_defineprint(%print, %file)
{
	%count = getNumPrintTextures();
	for(%i = 0; %i < %count; %i++)
	{
		if(getPrintTexture(%i) $= %file)
		{
			$LuaLogic::Print[%print] = %i;
			return %i;
		}
	}

	return "";
}

function lualogic_isprint(%print)
{
	return $LuaLogic::Print[%print] !$= "";
}

function lualogic_getprint(%print)
{
	if($LuaLogic::Print[%print] !$= "")
		return $LuaLogic::Print[%print];
	return 0;
}
