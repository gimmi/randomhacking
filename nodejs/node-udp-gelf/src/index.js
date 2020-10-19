const gelf = require('./gelf-udp-listener')
const bus = require('./bus')
const azure = require('./azure-monitor')
const loadConf = require('./config-loader')

async function main() {
    const config = await loadConf()

    if (config.azure) {
        azureSendLoop(config).catch(console.error)
    }

    gelf.start(config)
}

async function azureSendLoop(config) {
    console.log(`Starting Azure send loop customerId=${config.azure.customerId} logType=${config.azure.logType} batchMs=${config.azure.batchMs}`)

    let batch = []

    bus.on('log', log => batch.push(log))

    for (; ;) {
        await timeout(config.azure.batchMs)

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
    process.exit(1)
})
