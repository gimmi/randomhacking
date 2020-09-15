const dgram = require('dgram')
const zlib = require('zlib')

const socket = dgram.createSocket('udp4')

socket.on('listening', () => {
    const address = socket.address()
    console.log(`UDP Server listening on ${address.address}:${address.port}`)
})

const chunkedMessages = {}

socket.on('message', buffer => {
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
})

socket.bind(2222);

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