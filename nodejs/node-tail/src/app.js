const path = require('path')
const express = require('express')
const bodyParser = require('body-parser')
const expressWs = require('express-ws');
const EventEmitter = require('events');

const port = 3000

const bus = new EventEmitter();

const app = express()
expressWs(app)
app.use(bodyParser.json())
app.use(bodyParser.text())

app.post('/api/send', (req, res) => {
    console.log('Publish:', req.body);
    bus.emit('message', req.body)
    res.status(200).end();
})

app.ws('/ws', webSocket => {
    const messageListener = message => {
        console.log('WS send:', message);
        webSocket.send(message);
    }

    console.log('WS client open');
    bus.addListener('message', messageListener);

    webSocket.addListener('close', function (msg) {
        console.log('WS client close');
        bus.removeListener('message', messageListener);
    });
});

app.use(express.static(path.join(__dirname, 'static')))

app.listen(port, () => console.log(`Example app listening at http://localhost:${port}`))
