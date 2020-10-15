const dgram = require('dgram')
const zlib = require('zlib')

const socket = dgram.createSocket('udp4')
    .on('listening', onListening)
    .on('message', onMessage)
    .bind(12201)

const chunkedMessages = {}

function onListening() {
    const address = socket.address()
    console.log(`UDP Server listening on ${address.address}:${address.port}`)
}

function onMessage(buffer) {
    if (buffer[0] === 0x1F && buffer[1] === 0x8B && buffer[2] === 0x08) {
        zlib.inflate(buffer, (err, buffer) => {
            if (err) {
                console.error(err)
                return
            }

            process(buffer)
        })
    } else {
        process(buffer)
    }
}

function process(buffer) {
    if (buffer[0] === 0x1E && buffer[1] === 0x0F) {
        const messageId = buffer.readBigInt64LE(2)
        const sequenceNumber = buffer.readBigInt8(10)
        const sequenceCount = buffer.readBigInt8(11)

        console.dir({ messageId, sequenceNumber, sequenceCount })
        return
    }

    const json = buffer.toString('utf8', 0)
    console.dir(JSON.parse(json))
}
