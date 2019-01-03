PortTypes = {
	output = 0,
	input = 1
}

PortDirections = {
	[0] = {-1, 0, 0},
	[1] = {0, 1, 0},
	[2] = {1, 0, 0},
	[3] = {0, -1, 0},
	[4] = {0, 0, 1},
	[5] = {0, 0, -1}
}

Port = {
	logictype = 1,
}

function Port:new(type, direction, position, causeupdate)
	local o = {
		type = type,
		direction = direction,
		position = position,
		causeupdate = causeupdate,
		state = false,
		gate = nil,
		group = nil,
	}
	setmetatable(o, self)
	self.__index = self
	return o
end

function Port:setstate(state)
	if state ~= self.state then
		self.state = state
		sim:queuegroup(self.group)
	end
end

function Port:setinputstate(state)
	if state ~= self.state then
		self.state = state
		if self.causeupdate then
			sim:queuegate(self.gate)
		end
	end
end

function Port:getconnectionposition()
	local offset = PortDirections[self.direction]
	return {self.position[1]+offset[1], self.position[2]+offset[2], self.position[3]+offset[3]}
end

function Port:isrising()
	if self.group == nil then
		return false
	end
	return self.group.state and (self.group.updatetick == sim.currenttick)
end

function Port:isfalling()
	if self.group == nil then
		return false
	end
	return self.group.state == false and (self.group.updatetick == sim.currenttick)
end
