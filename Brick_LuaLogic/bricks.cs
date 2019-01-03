function lualogic_addwire(%wire)
{
	%color = %wire.getColorID();

	%box = %wire.getWorldBox();

	%minX = mFloatLength(getWord(%box, 0)*2, 0)/2;
	%minY = mFloatLength(getWord(%box, 1)*2, 0)/2;
	%minZ = mFloatLength(getWord(%box, 2)*5, 0)/5;

	%maxX = mFloatLength(getWord(%box, 3)*2, 0)/2;
	%maxY = mFloatLength(getWord(%box, 4)*2, 0)/2;
	%maxZ = mFloatLength(getWord(%box, 5)*5, 0)/5;

	%min = lualogic_pos(%minX SPC %minY SPC %minZ);
	%max = lualogic_pos(%maxX SPC %maxY SPC %maxZ);

	lualogic_send("W;" @ %wire.getID() @ ";" @ %color @ ";" @ %min @ ";" @ %max);
	%wire.logicIsAdded = true;
}

function lualogic_addgate(%gate)
{
	%db = %gate.getDataBlock();
	%pos = lualogic_pos(%gate.getPosition());
	%rot = %gate.angleId;

	%data = "G;" @ %gate.getID() @ ";" @ %db @ ";" @ %pos @ ";" @ %rot;

	lualogic_send(%data);
	%gate.logicIsAdded = true;

	if(isFunction(%db.getName(), "Logic_onAdd"))
		%db.Logic_onAdd(%gate);
}

function lualogic_removewire(%wire)
{
	if(%wire.logicIsRemoved == false)
	{
		lualogic_send("RW;" @ %wire);
		%wire.logicIsRemoved = true;
	}
}

function lualogic_removegate(%gate)
{
	if(%gate.logicIsRemoved == false)
	{
		%db = %gate.getDataBlock();
		if(isFunction(%db.getName(), "Logic_onRemove"))
			%db.Logic_onRemove(%gate);

		lualogic_send("RG;" @ %gate);
		%gate.logicIsRemoved = true;
	}
}

function lualogic_sendall()
{
	%groups = mainBrickGroup.getCount();
	for(%i = 0; %i < %groups; %i++)
	{
		%group = mainBrickGroup.getObject(%i);
		%bricks = %group.getCount();
		for(%a = 0; %a < %bricks; %a++)
		{
			%brick = %group.getObject(%a);
			%data = %brick.getDataBlock();
			if(%data.isLogic && %brick.isPlanted())
			{
				if(%data.isLogicWire)
					lualogic_addwire(%brick);
				else if(%data.isLogicGate)
					lualogic_addgate(%brick);
			}
		}
	}
}

function fxDTSBrick::Logic_SetOutput(%this, %port, %state)
{
	lualogic_send("SP;" @ %this @ ";" @ %port+1 @ ";" @ %state);
}

function fxDTSBrick::Logic_SetInputState(%this, %state, %force)
{
	%data = %this.getDataBlock();
	if(%data.isLogicInput && (%this.logicInputState != %state || %force))
	{
		%this.logicInputState = %state;
		%ports = %data.numLogicPorts;
		for(%i = 0; %i < %ports; %i++)
			%this.Logic_SetOutput(%i, %state);
		
		if(%state)
			%this.setColorFX(3);
		else
			%this.setColorFX(0);
	}
}

package LuaLogic_Bricks
{
	function fxDTSBrickData::onPlant(%this, %brick)
	{
		parent::onPlant(%this, %brick);

		if(isObject(%brick) && %this.isLogic)
		{
			if(%this.isLogicWire)
				lualogic_addwire(%brick);
			else if(%this.isLogicGate)
				lualogic_addgate(%brick);
		}
	}

	function fxDTSBrickData::onLoadPlant(%this, %brick)
	{
		parent::onLoadPlant(%this, %brick);

		if(isObject(%brick) && %this.isLogic)
		{
			if(%this.isLogicWire)
				lualogic_addwire(%brick);
			else if(%this.isLogicGate)
				lualogic_addgate(%brick);
		}
	}

	function fxDTSBrickData::onColorChange(%data, %obj)
	{
		parent::onColorChange(%data, %obj);

		if(isObject(%obj) && %obj.isPlanted() && !%obj.isDead() && %data.isLogic && %data.isLogicWire)
			lualogic_send("SL;" @ %obj @ ";" @ %obj.getColorID());
	}

	function fxDTSBrickData::onDeath(%this, %brick)
	{
		if(%this.isLogic)
		{
			if(%this.isLogicWire)
				lualogic_removewire(%brick);
			else if(%this.isLogicGate)
				lualogic_removegate(%brick);
		}

		parent::onDeath(%this, %brick);
	}

	function fxDTSBrickData::onRemove(%this, %brick)
	{
		if(%this.isLogic && %brick.logicIsAdded)
		{
			if(%this.isLogicWire)
				lualogic_removewire(%brick);
			else if(%this.isLogicGate)
				lualogic_removegate(%brick);
		}

		parent::onRemove(%this, %brick);
	}

	function Player::activateStuff(%this, %a, %b)
	{
		parent::activateStuff(%this, %a, %b);

		if(isObject(%client = %this.client))
		{
			%eye = %this.getEyePoint();
			%vec = %this.getEyeVector();
			%ray = containerRayCast(%eye, vectorAdd(%eye, vectorScale(%vec, 5*getWord(%this.getScale(), 2))), $TypeMasks::FxBrickObjectType);
			if(isObject(%hit = firstWord(%ray)))
			{
				%data = %hit.getDataBlock();
				if(%data.isLogic)
				{
					if(%data.isLogicInput)
						%data.Logic_onInput(%hit, %hitPos, %hitNorm);
					else
						lualogic_send("GINFO;" @ %client @ ";" @ %hit);
				}
			}
		}
	}
};
activatePackage("LuaLogic_Bricks");
