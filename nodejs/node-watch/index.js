const pathlib = require('path')
const chokidar = require('chokidar')
const colors = require('colors/safe')
const fs = require('fs-extra')

const mappings = [
    { src: './src', dest: './dest' },
]

console.log('Watching paths:')
mappings.forEach(mapping => {
    mapping.src = pathlib.join(__dirname, mapping.src)
    mapping.dest = pathlib.join(__dirname, mapping.dest)
    
    console.log('    ' + mapping.src + ' => ' + mapping.dest)
})

const watcher = chokidar.watch(mappings.map(x => x.src), {})
    .on('add', srcPath => {
        const destPath = getDestPath(srcPath)
        fs.copy(srcPath, destPath)
            .then(() => console.log(`${srcPath} => ${destPath}`))
    })
    .on('change', srcPath => {
        const destPath = getDestPath(srcPath)
        fs.copy(srcPath, destPath)
            .then(() => console.log(`${srcPath} => ${destPath}`))
    })
    .on('unlink', srcPath => {
        const destPath = getDestPath(srcPath)
        fs.remove(destPath)
            .then(() => console.log(` => ${destPath}`))
    })
    .on('addDir', srcPath => {
        const destPath = getDestPath(srcPath)
        fs.remove(destPath)
            .then(() => fs.mkdir(destPath))
            .then(() => console.log(` => ${destPath}`))
    })
    .on('unlinkDir', srcPath => {
        const destPath = getDestPath(srcPath)
        fs.remove(destPath)
            .then(() => console.log(` => ${destPath}`))
    })
    .on('ready', () => {
        console.log('Initial scan complete. Ready for changes')
    })
    .on('error', fatalError)

process.on('uncaughtException', fatalError)
process.on('unhandledRejection', fatalError)

function getDestPath(srcPath) {
    const mapping = mappings.find(mapping => srcPath.startsWith(mapping.src))
    if (!mapping) {
        throw new Error('No mapping for ' + srcPath)
    }
    return pathlib.join(mapping.dest, srcPath.substring(mapping.src.length))
}

function fatalError(err) {
    console.error(err.message)
    console.error(err.stack)
    process.exit(1)
}