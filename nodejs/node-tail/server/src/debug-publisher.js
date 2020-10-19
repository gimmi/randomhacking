const debug = require('debug')('tail:app')
const bus = require('./bus');

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

    bus.on('message', msg => debug('Published: %o', msg));
}
