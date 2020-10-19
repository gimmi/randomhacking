const fluentd = require('./fluentd-forward');
const app = require('./express-app')

require('./debug-publisher')

app.create()
    .listen(3000, () => console.log('Web app listening on port 3000'))

fluentd.createServer()
    .listen(24225, () => console.log('FluentD forward listening on port 24225'))
