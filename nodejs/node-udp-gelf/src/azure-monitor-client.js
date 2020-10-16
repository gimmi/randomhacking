const https = require('https')
const crypto = require('crypto')
const fetch = require('node-fetch')

module.exports = class AzureMonitorClient {
    constructor() {
        this.customerId = null
        this.sharedKey = null
        this.logType = null
    }

    async send(logs) {
        const date = new Date().toUTCString()
        const data = Buffer.from(JSON.stringify(logs), 'utf8')

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

        const res = await fetch(`https://${this.customerId}.ods.opinsights.azure.com/api/logs?api-version=2016-04-01`, {
            method: 'POST',
            headers: {
                'Authorization': `SharedKey ${this.customerId}:${signature}`,
                'Content-Type': 'application/json',
                'Content-Length': data.length,
                'x-ms-date': date,
                'Log-Type': this.logType
            },
            body: data
        })

        if (!res.ok) {
            throw new Error(`HTTP ${res.status} ${res.statusText}`)
        }
    }
}
