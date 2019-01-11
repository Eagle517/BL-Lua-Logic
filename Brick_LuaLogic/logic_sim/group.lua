Group = {}

function Group:new()
	local o = {
		state = false,
		fxstate = false,
		updatetick = 0,
		wires = {},
		out_ports = {},
		in_ports = {},
	
		nwires = 0,
		nout_ports = 0,
		nin_ports = 0
	}
	setmetatable(o, self)
	self.__index = self
	return o
end

function Group:getsize()
	return self.nwires + self.nout_ports + self.nin_ports
end

function Group:addwire(wire)
	if wire.group ~= self then
		if wire.group ~= nil then
			self:mergewith(wire.group)
		else
			self.wires[wire] = wire
			self.nwires = self.nwires + 1
			
			wire.group = self
			wire:update()
			sim:queuegroup(self)
		end
	end
end

function Group:removewire(wire)
	wire.group = nil
	self.wires[wire] = nil

	for k, wire in pairs(self.wires) do
		wire.group = nil
	end

	for k, port in pairs(self.out_ports) do
		port.group = nil
	end

	for k, port in pairs(self.in_ports) do
		port.group = nil
	end

	for k, wire in pairs(self.wires) do
		sim:connectwire(wire)
	end

	for k, port in pairs(self.out_ports) do
		sim:connectport(port)
	end

	for k, port in pairs(self.in_ports) do
		sim:connectport(port)
	end

	self.wires = {}
	self.out_ports = {}
	self.in_ports = {}

	self.nwires = 0
	self.nout_ports = 0
	self.nin_ports = 0
end

function Group:addport(port)
	port.group = self

	if port.type == PortTypes.output then
		self.out_ports[port] = port
		self.nout_ports = self.nout_ports + 1
		sim:queuegroup(self)
	elseif port.type == PortTypes.input then
		self.in_ports[port] = port
		self.nin_ports = self.nin_ports + 1
		port:setinputstate(self.state)
	end
end

function Group:removeport(port)
	if port.type == PortTypes.output then
		self.out_ports[port] = nil
		self.nout_ports = self.nout_ports - 1
	elseif port.type == PortTypes.input then
		self.in_ports[port] = nil
		self.nin_ports = self.nin_ports - 1
	end

	sim:queuegroup(self)
end

function Group:mergewith(group)
	if self:getsize() >= group:getsize() then
		group:mergeinto(self)
		return self
	else
		self:mergeinto(group)
		return group
	end
end

function Group:mergeinto(group)
	for k, wire in pairs(self.wires) do
		wire.group = nil
		group:addwire(wire)
	end

	for k, port in pairs(self.out_ports) do
		group:addport(port)
	end

	for k, port in pairs(self.in_ports) do
		group:addport(port)
	end

	self.wires = {}
	self.out_ports = {}
	self.in_ports = {}

	self.nwires = 0
	self.nout_ports = 0
	self.nin_ports = 0
end

function Group:setstate(state)
	if state ~= self.state then
		self.state = state
		self.updatetick = sim.currenttick

		for k, port in pairs(self.in_ports) do
			port:setinputstate(state)
		end

		sim:queuegroupfx(self)
	end
end
