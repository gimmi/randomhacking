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

app.post('/api/publish', (req, res) => {
    console.log('Publish:', req.body);
    bus.emit('message', req.body)
    res.status(200).end();
})

app.ws('/ws', webSocket => {
    console.log('WS client open');
    bus.addListener('message', messageListener);

    webSocket.addListener('close', function (msg) {
        console.log('WS client close');
        bus.removeListener('message', messageListener);
    });

    function messageListener(message) {
        console.log('WS send:', message);
        webSocket.send(JSON.stringify(message));
    }
});

app.use(express.static(path.join(__dirname, 'static')))

app.listen(port, () => console.log(`Example app listening at http://localhost:${port}`))

var net = require('net');
var server = net.createServer();

server.on('connection', socket => {
    socket.setEncoding('utf8').on('data', data => {
        console.log(data)
    });
});

server.listen(1337, () => console.log(`Listening at 0.0.0.0:1337`));
