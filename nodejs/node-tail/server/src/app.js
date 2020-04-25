const debug = require('debug')('tail:app')
const path = require('path')
const express = require('express')
const bodyParser = require('body-parser')
const expressWs = require('express-ws');
const bus = require('./bus');
const fluentd = require('./fluentd-forward');

// Debug stuff -----------------------------------------------------------------
if (debug.enabled) {
    debug('Enabling debug stuff')

    const containerNames = [ 'container-one', 'container-two', 'container-three', 'container-four' ]

    let counter = 0;
    setInterval(() => {
        counter += 1;
        bus.emit('message', { container_name: containerNames[counter % 4], log: 'Log #' + counter })
    }, 1000);

    bus.on('message', msg => debug('Published:', msg));
}
// -----------------------------------------------------------------------------

const app = express()
expressWs(app)
app.use(bodyParser.json())

app.ws('/ws', (webSocket, req) => {
    debug('ws connected:', req.query.id);
    bus.on('message', messageListener);

    webSocket.on('close', function (msg) {
        debug('ws close:', req.query.id);
        bus.removeListener('message', messageListener);
    });

    webSocket.on('disconnect', function (msg) {
        debug('ws disconnect:', req.query.id);
        bus.removeListener('message', messageListener);
    });

    function messageListener(message) {
        webSocket.send(JSON.stringify(message));
    }
});

app.use(express.static(path.join(__dirname, 'static')))

app.listen(3000, () => console.log('Listening at http://localhost:3000'))

fluentd.createServer()
    .listen(24225, () => console.log('Listening at ws://0.0.0.0:24225'));
