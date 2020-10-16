//const gelfServer = require('./gelf-server').start(12201)
const AzureMonitorClient = require('./azure-monitor-client')

const data = [
    {
        version: '1.1',
        host: 'dockerhost',
        short_message: 'xoxo',
        timestamp: 1602850875.683,
        level: 6,
        _command: 'python3',
        _container_id: '58553fa455bb0648a96e4b95a58146ec386144a59381d6b6a24c257ea654d55d',
        _container_name: 'agitated_goldberg',
        _created: '2020-10-16T12:00:21.8260828Z',
        _image_id: 'sha256:55d14c2b2fc19eaf161b672519dfddb62b9dab5e93cacdf636cf66834a9d982b',
        _image_name: '172.16.0.13:6000/python:alpine',
        _tag: '58553fa455bb'
    }
].map(x => ({
    timestamp: new Date(x.timestamp * 1000),
    message: x.short_message,
    container_name: x._container_name
}));

async function main() {
    const client = new AzureMonitorClient()
    client.customerId = 'TODO'
    client.sharedKey = 'TODO'
    client.logType = 'TODO'

    await client.send(data)

    return 0
}

main().catch(err => {
    console.error(err)
    return 1
}).then(code => process.exit(code));