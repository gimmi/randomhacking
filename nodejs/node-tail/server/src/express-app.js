const debug = require('debug')('app:express')
const path = require('path')
const express = require('express')
const bodyParser = require('body-parser')
const expressWs = require('express-ws')
const bus = require('./bus');

module.exports.create = function () {
    const app = express()
    expressWs(app)
    app.use(bodyParser.json())
    app.use(express.static(path.join(__dirname, 'static')))

    app.ws('/ws', webSocket => {
        debug('ws connected');
        bus.on('message', messageListener);

        webSocket.on('close', function (msg) {
            debug('ws close');
            bus.removeListener('message', messageListener);
        });

        webSocket.on('disconnect', function (msg) {
            debug('ws disconnect');
            bus.removeListener('message', messageListener);
        });

        function messageListener(message) {
            webSocket.send(JSON.stringify(message));
        }
    })

    return app
}
