Simulation = {}

function Simulation:new()
	local o = {
		definitions = {},
		wires = {},
		gates = {},

		nwires = 0,
		ngates = 0,
		ninports = 0,
		noutports = 0,

		groupqueue = {},
		groupfxqueue = {},
		gatequeue = {},

		callbacks = {},

		currenttick = 0
	}
	setmetatable(o, self)
	self.__index = self
	return o
end

function Simulation:addtoworld(obj, x, y, z)
	if self[x] == nil then
		self[x] = {}
	end

	if self[x][y] == nil then
		self[x][y] = {}
	end

	if self[x][y][z] == nil then
		self[x][y][z] = {}
	end

	self[x][y][z][obj] = obj
end

function Simulation:getfromworld(x, y, z)
	if self[x] == nil or self[x][y] == nil or self[x][y][z] == nil then
		return {}
	else
		return self[x][y][z]
	end
end

function Simulation:getdefinitionbyref(objref)
	return self.definitions[objref]
end

function Simulation:getgatebyref(objref)
	return self.gates[objref]
end

function Simulation:getwirebyref(objref)
	return self.wires[objref]
end

function Simulation:addgatedefinition(definition)
	self.definitions[definition.objref] = definition
end

function Simulation:addwire(wire)
	self.wires[wire.objref] = wire

	for x = wire.bounds[1]+1, wire.bounds[4]-1, 2 do
		for z = wire.bounds[3]+1, wire.bounds[6]-1, 2 do
			self:addtoworld(wire, x, wire.bounds[2], z)
			self:addtoworld(wire, x, wire.bounds[5], z)
		end
	end

	for y = wire.bounds[2]+1, wire.bounds[5]-1, 2 do
		for z = wire.bounds[3]+1, wire.bounds[6]-1, 2 do
			self:addtoworld(wire, wire.bounds[1], y, z)
			self:addtoworld(wire, wire.bounds[4], y, z)
		end
	end

	for x = wire.bounds[1]+1, wire.bounds[4]-1, 2 do
		for y = wire.bounds[2]+1, wire.bounds[5]-1, 2 do
			self:addtoworld(wire, x, y, wire.bounds[3])
			self:addtoworld(wire, x, y, wire.bounds[6])
		end
	end

	self.nwires = self.nwires + 1
	self:connectwire(wire)
end

function Simulation:addgate(gate)
	self.gates[gate.objref] = gate

	for k, port in pairs(gate.ports) do
		local offset = port:getconnectionposition()
		self:addtoworld(port, offset[1], offset[2], offset[3])
		self:connectport(port)

		if port.type == PortTypes.input then
			self.ninports = self.ninports + 1
		elseif port.type == PortTypes.output then
			self.noutports = self.noutports + 1
		end
	end

	self.ngates = self.ngates + 1
end

function Simulation:removewire(objref)
	local wire = self.wires[objref]
	if wire ~= nil then
		self.wires[objref] = nil

		for x = wire.bounds[1]+1, wire.bounds[4]-1, 2 do
			for z = wire.bounds[3]+1, wire.bounds[6]-1, 2 do
				sim[x][wire.bounds[2]][z][wire] = nil
				sim[x][wire.bounds[5]][z][wire] = nil
			end
		end
	
		for y = wire.bounds[2]+1, wire.bounds[5]-1, 2 do
			for z = wire.bounds[3]+1, wire.bounds[6]-1, 2 do
				sim[wire.bounds[1]][y][z][wire] = nil
				sim[wire.bounds[4]][y][z][wire] = nil
			end
		end
	
		for x = wire.bounds[1]+1, wire.bounds[4]-1, 2 do
			for y = wire.bounds[2]+1, wire.bounds[5]-1, 2 do
				sim[x][y][wire.bounds[3]][wire] = nil
				sim[x][y][wire.bounds[6]][wire] = nil
			end
		end

		self.nwires = self.nwires - 1
		wire.group:removewire(wire)
	end
end

function Simulation:removegate(objref)
	local gate = self.gates[objref]
	if gate ~= nil then
		for k, port in pairs(gate.ports) do
			local pos = port:getconnectionposition()
			self[pos[1]][pos[2]][pos[3]][port] = nil
			port.group:removeport(port)

			if port.type == PortTypes.input then
				self.ninports = self.ninports - 1
			elseif port.type == PortTypes.output then
				self.noutports = self.noutports - 1
			end
		end
	end

	self.gates[objref] = nil
	self.ngates = self.ngates - 1
end

function Simulation:connectwireat(wire, x, y, z)
	local objs = self:getfromworld(x, y, z)
	for k, obj in pairs(objs) do
		if obj ~= wire and obj.group ~= nil then
			if obj.logictype == 0 and obj.layer == wire.layer then
				if obj.layer == wire.layer then
					obj.group:addwire(wire)
				end
			elseif obj.logictype == 1 then
				obj.group:addwire(wire)
			end
		end
	end
end

function Simulation:connectwire(wire)
	for x = wire.bounds[1]+1, wire.bounds[4]-1, 2 do
		for z = wire.bounds[3]+1, wire.bounds[6]-1, 2 do
			self:connectwireat(wire, x, wire.bounds[2], z)
			self:connectwireat(wire, x, wire.bounds[5], z)
		end
	end

	for y = wire.bounds[2]+1, wire.bounds[5]-1, 2 do
		for z = wire.bounds[3]+1, wire.bounds[6]-1, 2 do
			self:connectwireat(wire, wire.bounds[1], y, z)
			self:connectwireat(wire, wire.bounds[4], y, z)
		end
	end

	for x = wire.bounds[1]+1, wire.bounds[4]-1, 2 do
		for y = wire.bounds[2]+1, wire.bounds[5]-1, 2 do
			self:connectwireat(wire, x, y, wire.bounds[3])
			self:connectwireat(wire, x, y, wire.bounds[6])
		end
	end
	
	if wire.group == nil then
		Group:new():addwire(wire)
	end
end

function Simulation:connectport(port)
	local connpos = port:getconnectionposition()
	local objs = self:getfromworld(connpos[1], connpos[2], connpos[3])
	for k, obj in pairs(objs) do
		if obj ~= port and obj.group ~= nil then
			obj.group:addport(port)
		end
	end

	if port.group == nil then
		Group:new():addport(port)
	end
end

function Simulation:queuegate(gate)
	self.gatequeue[gate] = gate
end

function Simulation:queuegroup(group)
	self.groupqueue[group] = group
end

function Simulation:queuegroupfx(group)
	self.groupfxqueue[group] = group
end

function Simulation:queuecallback(gate, str)
	self.callbacks[gate.objref] = str
end

function Simulation:tick()
	for k, group in pairs(self.groupqueue) do
		local newstate = false
		for j, port in pairs(group.out_ports) do
			newstate = newstate or port.state
			if newstate then
				break
			end
		end
		
		group:setstate(newstate)
	end

	self.groupqueue = {}

	for k, gate in pairs(self.gatequeue) do
		gate.definition.logic(gate)
	end

	self.gatequeue = {}
	self.currenttick = self.currenttick + 1
end

function Simulation:sendfxupdate()
	for k, group in pairs(self.groupfxqueue) do
		if group.state ~= group.fxstate then
			group.fxstate = group.state

			local data = bool_to_int[group.state]

			if OPT_FX_UPDATES then
				for i, wire in pairs(group.wires) do
					data = data .. "\t" .. wire.objref
				end
			else
				for i, wire in pairs(group.wires) do
					if wire.isvisual then
						data = data .. "\t" .. wire.objref
					end
				end
			end

			client:send("WU\t" .. data .. "\n")
		end
	end

	self.groupfxqueue = {}
end

function Simulation:sendcallbacks()
	if next(self.callbacks) ~= nil then
		local data = "CB"

		for objref, cb in pairs(self.callbacks) do
			data = data .. "\t" .. objref .. "\t" .. cb
		end

		client:send(data .. "\n")
		self.callbacks = {}
	end
end
