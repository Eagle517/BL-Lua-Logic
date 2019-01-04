function serverCmdLT(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		$Pref::Server::LuaLogic::OPT_TICK_ENABLED = !$Pref::Server::LuaLogic::OPT_TICK_ENABLED;
		messageAll('', '\c3%1\c6 has %2 the logic tick.', %client.name, $Pref::Server::LuaLogic::OPT_TICK_ENABLED ? "enabled":"disabled");
		lualogic_sendoptions();
	}
}

function serverCmdLST(%client, %time)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		%time = mClamp(%time, 0, 999999);
		$Pref::Server::LuaLogic::OPT_TICK_TIME = %time/1000;
		messageAll('', '\c3%1\c6 has set the logic tick time to \c3%2\c6 millisecond%3.', %client.name, %time, %time == 1 ? "":"s");
		lualogic_sendoptions();
	}
}

function serverCmdLS(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		commandToAll('bottomprint', "\c3" @ %client.name @ "\c6 has forced a logic tick.", 3, 1);
		lualogic_send("TICK");
	}
}

function serverCmdLFX(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		$Pref::Server::LuaLogic::OPT_FX_UPDATES = !$Pref::Server::LuaLogic::OPT_FX_UPDATES;
		messageAll('', '\c3%1\c6 has %2 logic FX updates.', %client.name, $Pref::Server::LuaLogic::OPT_FX_UPDATES ? "enabled":"disabled");
		lualogic_sendoptions();
	}
}

function serverCmdLFXT(%client, %time)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		%time = mClamp(%time, 0, 999999);
		$Pref::Server::LuaLogic::OPT_FX_TIME = %time/1000;
		messageAll('', '\c3%1\c6 has set the logic FX time to \c3%2\c6 millisecond%3.', %client.name, %time, %time == 1 ? "":"s");
		lualogic_sendoptions();
	}
}

function serverCmdLTR(%client)
{
	%client.logicLTR = !%client.logicLTR;
	if(%client.logicLTR == false)
		commandToClient(%client, 'bottomPrint', "", 0, 1);
}

function serverCmdLI(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
		lualogic_send("SINFO;" @ %client);
}

function serverCmdLG(%client, %n)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if(isObject(%player = %client.player))
		{
			%eye = %player.getEyePoint();
			%vec = %player.getEyeVector();
			%ray = containerRayCast(%eye, vectorAdd(%eye, vectorScale(%vec, 5*getWord(%player.getScale(), 2))), $TypeMasks::FxBrickObjectType);
			if(isObject(%hit = firstWord(%ray)))
			{
				%data = %hit.getDataBlock();
				if(%data.isLogicGate)
					lualogic_send("TEST;" @ %hit @ ";" @ %n);
			}
		}
	}
}
