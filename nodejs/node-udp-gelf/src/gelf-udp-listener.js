const debug = require('debug')('app:gelf-udp-listener')
const bus = require('./bus')
const dgram = require('dgram')
const zlib = require('zlib')
const isGzip = require('is-gzip')

module.exports.start = function (config) {
    return dgram.createSocket('udp4')
        .on('listening', onListening)
        .on('message', onMessage)
        .on('error', onError)
        .bind(config.listenPort)
}

function onError(err) {
    console.error(err)
}

function onListening() {
    const address = this.address()
    console.log(`Listening for GELF logs on UDP ${address.address}:${address.port}`)
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
    const gelf = JSON.parse(json)
    const log = {
        host: gelf.host,
        ts: new Date(gelf.timestamp * 1000).toISOString(),
        log: gelf.short_message,
        container_name: gelf._container_name
    }
    debug('emit log: %o', log)
    bus.emit('log', log)
}
