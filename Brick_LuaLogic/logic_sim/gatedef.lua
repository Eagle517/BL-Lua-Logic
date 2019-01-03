GateDefinition = {
	logic = function(gate) end
}

function GateDefinition:new(objref, name, description, logic, ports)
	local o = {
		objref = objref,
		name = name,
		description = description,
		ports = ports or {}
	}

	local logicfunc = loadstring(tostring(logic))
	if logicfunc ~= nil then
		o.logic = loadstring("return function(gate) " .. tostring(logic) .. " end")()
	end

	setmetatable(o, self)
	self.__index = self
	return o
end

function GateDefinition:constructgate(objref, position, rotation)
	local gate = Gate:new(objref, self)

	for i = 1, #self.ports do
		local port = self.ports[i]
		local type = port.type
		local pos = {port.position[1], port.position[2], port.position[3]}
		local dir = port.direction

		if dir < 4 then
			dir = (dir + rotation) % 4
		end

		local x = pos[1]

		if rotation == 1 then
			pos[1] = pos[2]
			pos[2] = -x
		elseif rotation == 2 then
			pos[1] = -pos[1]
			pos[2] = -pos[2]
		elseif rotation == 3 then
			pos[1] = -pos[2]
			pos[2] = x
		end

		gate:addport(Port:new(type, dir, {position[1]+pos[1], position[2]+pos[2], position[3]+pos[3]}, port.causeupdate))
	end

	return gate
end
