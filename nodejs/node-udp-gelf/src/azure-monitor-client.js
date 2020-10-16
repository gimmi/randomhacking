const https = require('https')
const crypto = require('crypto')

module.exports = class AzureMonitorClient {
    constructor() {
        this.customerId = null
        this.sharedKey = null
        this.logType = null
    }

    send(data) {
        return new Promise((resolve, reject) => {
            data = Buffer.from(JSON.stringify(data), 'utf8')
            const date = new Date().toUTCString()

            let signature = [
                'POST',
                data.length,
                'application/json',
                'x-ms-date:' + date,
                '/api/logs'
            ]
            signature = signature.join('\n')
            signature = Buffer.from(signature, 'utf8')
            signature = crypto.createHmac('sha256', Buffer.from(this.sharedKey, 'base64'))
                .update(signature)
                .digest('base64')

            const options = {
                method: 'POST',
                hostname: this.customerId + '.ods.opinsights.azure.com',
                path: '/api/logs?api-version=2016-04-01',
                headers: {
                    'Authorization': `SharedKey ${this.customerId}:${signature}`,
                    'Content-Type': 'application/json',
                    'Content-Length': data.length,
                    'x-ms-date': date,
                    'Log-Type': this.logType
                }
            }

            console.dir(options)

            const req = https.request(options)

            req.on('response', res => {
                if (res.statusCode >= 200 && res.statusCode < 300) {
                    resolve()
                } else {
                    reject(new Error(`HTTP ${res.statusCode} ${res.statusMessage}`))
                }
            })

            req.on('error', error => reject(error))

            req.write(data)
            req.end()
        })
    }
}
