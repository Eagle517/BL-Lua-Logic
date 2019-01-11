function LuaLogicTCP::sendData(%this)
{
	cancel(%this.lualogicTick);
	%this.lualogicTick = %this.schedule(31, "sendData");

	if(%this.data !$= "")
	{
		%data = %this.data;
		while(strpos(%data, ";;") != -1)
			%data = strReplace(%data, ";;", "; ;");
		
		%this.send(%data @ "\n");
		%this.data = "";
	}
}

function LuaLogicTCP::onConnected(%this)
{
	lualogic_print("tcp connected");
	
	%this.data = "";
	%this.sendData();
	%this.isConnected = true;

	lualogic_sendoptions();
	lualogic_sendgatedefinitions();
	lualogic_sendall();
}

function LuaLogicTCP::onLine(%this, %line)
{
	%cmd = getField(%line, 0);
	switch$(%cmd)
	{
		case "WU":
			%state = getField(%line, 1)|0;
			%count = getFieldCount(%line);

			if(%state)
			{
				for(%i = 2; %i < %count; %i++)
				{
					%brick = getField(%line, %i);
					if(isObject(%brick))
						%brick.setColorFX(3);
				}
			}
			else
			{
				for(%i = 2; %i < %count; %i++)
				{
					%brick = getField(%line, %i);
					if(isObject(%brick))
						%brick.setColorFX(0);
				}
			}
		case "TPS":
			%tz = getField(%line, 1);

			%count = ClientGroup.getCount();
			for(%i = 0; %i < %count; %i++)
			{
				%client = ClientGroup.getObject(%i);
				if(%client.logicLTR)
					commandToClient(%client, 'bottomPrint', "\c3Logic Tick Rate\c6: " @ %tz, 2, 1);
			}
		case "GINFO":
			%client = getField(%line, 1);
			if(isObject(%client))
			{
				%info = getField(%line, 2);
				%info = strReplace(%info, "\\c0", "\c0");
				%info = strReplace(%info, "\\c2", "\c2");
				%info = strReplace(%info, "\\c5", "\c5");
				%client.centerPrint(%info, 5);
			}
		case "SINFO":
			if(isObject(%client = getField(%line, 1)))
			{
				%wires = getField(%line, 2);
				%gates = getField(%line, 3);
				%inports = getField(%line, 4);
				%outports = getField(%line, 5);

				messageClient(%client, '', '\c3Wires\c6: %1', %wires);
				messageClient(%client, '', '\c3Gates\c6: %1', %gates);
				messageClient(%client, '', '\c3Ports\c6: %1 inputs | %2 outputs (%3 total)', %inports, %outports, %inports + %outports);
			}
		case "CB":
			%data = getFields(%line, 1, getFieldCount(%line));
			%data = nextToken(%data, brick, "\t");
			while(%brick !$= "")
			{
				%data = nextToken(%data, argc, "\t");
				if(%argc > 0)
				{
					%data = nextToken(%data, args, "\t");
					for(%i = 1; %i < %argc; %i++)
					{
						%data = nextToken(%data, arg, "\t");
						%args = %args TAB %arg;
					}
				}

				if(isObject(%brick))
					%brick.getDatablock().LuaLogic_Callback(%brick, %args);
				
				%data = nextToken(%data, brick, "\t");
			}
		case "TEST":
			talk("Time: " @ getField(%line, 1));
	}
}

function LuaLogicTCP::onConnectFailed(%this)
{
	lualogic_print("tcp failed to connect");
}

function LuaLogicTCP::onDisconnect(%this)
{
	lualogic_print("tcp disconnected");
	%this.isConnected = false;
	cancel(%this.lualogicTick);
}
