const dgram = require('dgram')
const zlib = require('zlib')
const isGzip = require('is-gzip')

module.exports.start = function (port) {
    return dgram.createSocket('udp4')
        .on('listening', onListening)
        .on('message', onMessage)
        .bind(port)
}

function onListening() {
    const address = this.address()
    console.log(`UDP Server listening on ${address.address}:${address.port}`)
}

function onMessage(buffer) {
    if (isGzip(buffer)) {
        zlib.gunzip(buffer, (err, buffer) => {
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
        console.error('Skipping chunked message, they seems to be not sent by Docker gelf plugin')
        return
    }

    const json = buffer.toString('utf8', 0)
    console.dir(JSON.parse(json))
}
