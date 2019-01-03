Wire = {
	logictype = 0
}

function Wire:new(objref, layer, bounds)
	local o = {
		objref = objref,
		layer = layer,
		group = nil,
		bounds = bounds
	}
	setmetatable(o, self)
	self.__index = self
	return o
end

function Wire:setlayer(layer)
	if self.group ~= nil then
		self.group:removewire(self)
	end
	self.layer = layer
	sim:connectwire(self)
end

function Wire:update()
	client:send("WU\t" .. bool_to_int[self.group.state] .. "\t" .. self.objref .. "\n")
end
