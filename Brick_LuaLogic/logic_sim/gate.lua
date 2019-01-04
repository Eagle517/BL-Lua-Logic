Gate = {}

function Gate:new(objref, definition)
	local o = {
		objref = objref,
		definition = definition,
		ports = {}
	}
	setmetatable(o, self)
	self.__index = self
	return o
end

function Gate:addport(port)
	self.ports[#self.ports+1] = port
	port.gate = self
end

function Gate:setportstate(index, state)
	self.ports[index]:setstate(state)
end

-- function Gate:cb(...)
-- 	local args = {...}
-- 	local str = tostring(#args)

-- 	for i, v in ipairs(args) do
-- 		v = bool_to_int[v] or tostring(v)
-- 		str = str .. "\t" .. tostring(v)
-- 	end

-- 	sim:queuecallback(self, str)
-- end

function Gate:cb(str)
	sim:queuecallback(self, str)
end

function Gate:testlogic(n)
	local time = os.clock()
	for i = 1, n do
		self.definition.logic(self)
	end
	client:send("TEST\t" .. (os.clock()-time) .. "\n")
end
