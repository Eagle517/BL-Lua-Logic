loadfile("simulation.lua")()
loadfile("group.lua")()
loadfile("wire.lua")()
loadfile("gatedef.lua")()
loadfile("gate.lua")()
loadfile("port.lua")()

OPT_TICK_ENABLED = true
OPT_TICK_TIME = 0
OPT_FX_UPDATES = true
OPT_FX_TIME = 0.03

bool_to_int = {[false] = 0, [true] = 1}

local lastticktime = 0
local ticks = 0
local tickrate = 0
local lastmeasuretime = 0
local lastfxtime = 0

local avgticks = {}
local totalticks = 0

sim = Simulation:new()

local units = {
	"uTz",
	"mTz",
	"Tz",
	"kTz",
	"MTz",
	"GTz",
}

local function round(x)
	return math.floor(x+0.5)
end

local function unitize(v)
	local unit = 1
	v = v*1000000

	while v >= 1000 do
		v = v/1000
		unit = unit+1
	end

	local s
	if v >= 100 then
		s = "" .. round(v/10)*10
	elseif v >= 10 then
		s = "" .. round(v)
	elseif v >= 1 then
		s = "" .. round(v*10)/10
		if #s == 1 then s = s .. ".0" end
	else
		s = 0
	end
	
	return s .. " " .. units[unit]
end

function vectotable(vec)
	local tbl = {}
	for comp in string.gmatch(vec, "([^%s]+)") do
		tbl[#tbl+1] = tonumber(comp)
	end
	return tbl
end

function tabletostring(table)
	local str = tostring(table[1])
	for i = 2, #table do
		str = str .. " " .. tostring(table[i])
	end
	return str
end

function toboolean(value)
	local num = tonumber(value)
	if num == 1 then
		return true
	else
		return false
	end
end

local function acceptclient()
	client = server:accept()
	client:settimeout(0)
	local ip, port = client:getsockname()
	print("Connection from " .. ip .. ":" .. port)
end

local socket = require("socket")
server = assert(socket.bind("*", 25000))
client = nil

local ip, port = server:getsockname()
print("Server listening on " .. ip .. ":" .. port)

acceptclient()

while 1 do
	local line, err = client:receive()

	if not err then
		local data = {}
		local i = 1
		for str in string.gmatch(line, "([^;]+)") do
			data[i] = str
			i = i + 1
		end

		local i = 1
		while i <= #data do
			if data[i] == "W" then
				local min = vectotable(data[i+3])
				local max = vectotable(data[i+4])
				local bounds = {min[1], min[2], min[3], max[1], max[2], max[3]}

				local wire = Wire:new(tonumber(data[i+1]), tonumber(data[i+2]), bounds)
				sim:addwire(wire)

				i = i + 4
			elseif data[i] == "G" then
				local objref = tonumber(data[i+1])
				local definition = sim:getdefinitionbyref(tonumber(data[i+2])) or GateDefinition
				local position = vectotable(data[i+3])
				local rotation = tonumber(data[i+4])
				local gate = definition:constructgate(objref, position, rotation)

				sim:addgate(gate)
				gate.definition.logic(gate)

				i = i + 4
			elseif data[i] == "RW" then
				sim:removewire(tonumber(data[i+1]))
				i = i + 1
			elseif data[i] == "RG" then
				sim:removegate(tonumber(data[i+1]))
				i = i + 1
			elseif data[i] == "GD" then
				local objref = tonumber(data[i+1])
				local name = data[i+2]
				local desc = data[i+3]
				local logic = data[i+4]
				local numports = tonumber(data[i+5])
				local ports = {}

				for a = i+6, numports*5+i+5, 5 do
					local port = {
						type = tonumber(data[a]),
						position = vectotable(data[a+1]),
						direction = tonumber(data[a+2]),
						causeupdate = toboolean(data[a+3]),
						name = data[a+4],
					}
					ports[#ports+1] = port
				end

				local definition = GateDefinition:new(objref, name, desc, logic, ports)
				sim:addgatedefinition(definition)

				i = i + 5 + numports*5
			elseif data[i] == "SL" then
				local wire = sim:getwirebyref(tonumber(data[i+1]))
				if wire ~= nil then
					wire:setlayer(tonumber(data[i+2]))
				end

				i = i + 2
			elseif data[i] == "SP" then
				local gate = sim:getgatebyref(tonumber(data[i+1]))
				if gate ~= nil then
					gate.ports[tonumber(data[i+2])]:setstate(toboolean(data[i+3]))
				end

				i = i + 3
			elseif data[i] == "SG" then
				local wire = sim:getwirebyref(tonumber(data[i+1]))
				if wire ~= nil then
					wire.group:setstate(toboolean(data[i+2]))
				end

				i = i + 2
			elseif data[i] == "OPT" then
				local option = data[i+1]
				local value = tonumber(data[i+2])
				
				if option == "TICK_ENABLED" then
					OPT_TICK_ENABLED = toboolean(value)
				elseif option == "TICK_TIME" then
					if value < 0 or value > 999999 then
						value = 0
					end
					OPT_TICK_TIME = value
				elseif option == "FX_UPDATES" then
					OPT_FX_UPDATES = toboolean(value)
				elseif option == "FX_TIME" then
					if value < 0 or value > 999999 then
						value = 0
					end
					OPT_FX_TIME = value
				end

				i = i + 2
			elseif data[i] == "GINFO" then
				local userid = data[i+1]
				local objref = tonumber(data[i+2])

				local obj = sim:getwirebyref(objref) or sim:getgatebyref(objref)

				if obj ~= nil then
					local info = ""

					if obj.logictype == 0 then
						info = "\\c5WIRE<br>" .. (obj.group.state and "\\c2ON" or "\\c0OFF")
					else
						info = "\\c5" .. obj.definition.name .. "<br>"
						for i = 1, #obj.ports do
							info = info .. (obj.ports[i].state and "\\c2" or "\\c0") .. obj.definition.ports[i].name .. (i ~= #obj.ports and " " or "")
						end
					end

					if info ~= "" then
						client:send("GINFO\t" .. userid .. "\t" .. info .. "\n")
					end
				end

				i = i + 2
			elseif data[i] == "SINFO" then
				client:send("SINFO\t" .. data[i+1] .. "\t" .. sim.nwires .. "\t" .. sim.ngates .. "\t" .. sim.ninports .. "\t" .. sim.noutports .. "\n")
				i = i + 1
			elseif data[i] == "TICK" then
				sim:tick()
				ticks = ticks + 1
			elseif data[i] == "TEST" then
				local gate = sim:getgatebyref(tonumber(data[i+1]))
				gate:testlogic(tonumber(data[i+2]))
				i = i + 2
			end

			i = i + 1
		end
	elseif err == "closed" then
		sim = Simulation:new()
		acceptclient()
	end

	local time = os.clock()

	if OPT_TICK_ENABLED then
		if time - lastticktime >= OPT_TICK_TIME then
			sim:tick()
			ticks = ticks + 1
			lastticktime = time
		end
	end

	if time-lastfxtime >= OPT_FX_TIME then
		sim:sendfxupdate()
		sim:sendcallbacks()
		lastfxtime = time
	end
	
	if time-lastmeasuretime >= 0.1 then
		if #avgticks >= 20 then
			totalticks = totalticks - table.remove(avgticks, 1)
		end
		
		table.insert(avgticks, ticks)
		totalticks = totalticks + ticks

		ticks = 0

		client:send("TPS\t" .. unitize((totalticks/#avgticks)/0.1) .. "\n")
		lastmeasuretime = os.clock()
	end

	socket.sleep(0)
end
