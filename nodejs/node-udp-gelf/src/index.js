const { URL, fileURLToPath } = require('url')
const fs = require('fs').promises
const fetch = require('node-fetch')

const gelf = require('./gelf-udp-listener')
const bus = require('./bus')
const azure = require('./azure-monitor')

async function main() {
    const config = await loadConf()

    if (config.sharedKey) {
        console.log(`Starting Azure send loop to customerId: ${config.customerId} logType: ${config.logType}`)
        azureSendLoop(config).catch(console.error)
    }

    gelf.start(config)
}

async function loadConf() {
    const url = new URL(process.argv[2])

    if (url.protocol === 'file:') {
        const path = fileURLToPath(url)
        console.log('Loading config from file: ' + path)
        const json = await fs.readFile(path, 'utf8')
        return JSON.parse(json)
    }

    console.log('Loading config from url: ' + url)
    return await fetch(url, {
        headers: {
            Accept: 'application/json'
        }
    }).then(res => res.json())
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
    process.exit(1)
})
