const net = require('net');
const msgpack = require('@msgpack/msgpack');

module.exports.createServer = function(bus) {
    const server = net.createServer();
    server.on('connection', async socket => {
        for await (const request of msgpack.decodeStream(socket)) {
            for (const event of parseRequest(request)) {
                bus.emit('message', event)
            }
        }
    });
    return server;
}

function parseRequest(request) {
    const events = [];
    // Request ::= Message | Forward | PackedForward | nil
    if (request === null) {
        // Request ::= nil
        console.log('nil');
    } else if (Array.isArray(request[1])) {
        // Request ::= Forward
        for (let event of request[1]) {
            event = parseEvent(request[0], event[0], event[1]);
            events.push(event);
        }
    } else if ((typeof request[1] === 'number') || (request[1] instanceof msgpack.ExtData)) {
        // Request ::= Message
        const event = parseEvent(request[0], request[1], request[2])
        events.push(event);
    } else {
        // Request ::= PackedForward
        throw new Error('Request ::= PackedForward')
    }
    return events;
}

function parseEvent(tag, time, record) {
    if (typeof time === 'number') {
        time = new Date(time * 1000);
    } else {
        const buffer = Buffer.from(time.data);
        const secondFromEpoch = buffer.readUInt32BE(0);
        const nanosecond = buffer.readUInt32BE(4);
        time = new Date(secondFromEpoch * 1000 + nanosecond / 1000000);
    }
    return { tag, time, ...record };
}
