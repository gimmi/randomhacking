const gelf = require('./gelf-udp-listener')
const bus = require('./bus')
const azure = require('./azure-monitor')

async function main() {
    const config = {
        customerId: 'TODO',
        sharedKey: 'TODO',
        logType: 'TODO',
        batchMs: 5000,
        listenPort: 12201
    }

    gelf.start(config)

    await azureSendLoop(config)
}

async function azureSendLoop(config) {
    let batch = []

    bus.on('log', log => batch.push(log))

    for (; ;) {
        await timeout(config.batchMs)

        if (batch.length) {
            const data = batch
            batch = []

            await azure.send(config, data)
                .catch(console.error)
        }
    }
}

async function timeout(ms) {
    return new Promise(resolve => setTimeout(resolve, ms))
}

main().catch(err => {
    console.error(err)
    return 1
}).then(code => process.exit(code));