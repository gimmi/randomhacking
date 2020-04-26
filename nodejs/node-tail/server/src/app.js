const debug = require('debug')('tail:app')
const path = require('path')
const express = require('express')
const bodyParser = require('body-parser')
const expressWs = require('express-ws');
const bus = require('./bus');
const fluentd = require('./fluentd-forward');

// Debug stuff -----------------------------------------------------------------
if (debug.enabled) {
    const rndFn = (min, max) => Math.floor(Math.random() * (max - min + 1) + min);

    debug('Enabling debug stuff')

    const containerNames = [ 'container-one', 'container-two', 'container-three', 'container-four' ]

    const sampleTexts = [
        '<2>ConsoleApp.Program[10] Critical message #',
        '<3>ConsoleApp.Program[10] Error message #',
        '<4>ConsoleApp.Program[10] Warning message #',
        '<6>ConsoleApp.Program[10] Information message #',
        '<7>ConsoleApp.Program[10] Debug message #',
    ]

    let counter = 0;
    setInterval(() => {
        counter += 1;
        const containerName = containerNames[rndFn(0, 3)]
        const sampleText = sampleTexts[rndFn(0, 4)]
        bus.emit('message', { container_name: containerName, log: sampleText + counter })
    }, 1000);

    bus.on('message', msg => debug('Published:', msg));
}
// -----------------------------------------------------------------------------

const app = express()
expressWs(app)
app.use(bodyParser.json())

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
});

app.use(express.static(path.join(__dirname, 'static')))

app.listen(3000, () => console.log('Listening at http://localhost:3000'))

fluentd.createServer()
    .listen(24225, () => console.log('Listening at ws://0.0.0.0:24225'));
