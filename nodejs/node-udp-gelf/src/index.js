const dgram = require('dgram')
const zlib = require('zlib')

const chunkedMessages = {}

const socket = dgram.createSocket('udp4')
    .on('listening', onListening)
    .on('message', onMessage)
    .bind(12201)

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
        const sequenceNumber = buffer.readInt8(10)
        const sequenceCount = buffer.readInt8(11)

        if (!chunkedMessages[messageId]) {
            chunkedMessages[messageId] = new Array(sequenceCount).fill(null)
            setTimeout(() => delete chunkedMessages[messageId], 5000)
        }

        chunkedMessages[messageId][sequenceNumber] = buffer.toString('utf8', 12)

        if (chunkedMessages[messageId].every(m => m !== null)) {
            const json = chunkedMessages[messageId].join('')
            console.dir(JSON.parse(json))
        }
    } else {
        const json = buffer.toString('utf8', 0)
        console.dir(JSON.parse(json))
    }
}
