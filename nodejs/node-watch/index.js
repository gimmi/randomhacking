const pathlib = require('path')
const chokidar = require('chokidar')
const fs = require('fs-extra')

const mappings = [
    { src: './src', dest: './dest' },
].map(mapping => {
    mapping.src = pathlib.join(__dirname, mapping.src)
    mapping.dest = pathlib.join(__dirname, mapping.dest)
    return mapping
})

const watcher = chokidar.watch(mappings.map(x => x.src), {})
    .on('add', copy)
    .on('change', copy)
    .on('unlink', del)
    .on('error', fatalError)

process.on('uncaughtException', fatalError)
process.on('unhandledRejection', fatalError)

function copy(srcPath) {
    const destPath = getDestPath(srcPath)
    const destDir = pathlib.dirname(destPath)
    fs.ensureDir(destDir)
        .then(() => fs.copy(srcPath, destPath))
        .then(() => console.log(`${srcPath} => ${destPath}`))
}

function del(srcPath) {
    const destPath = getDestPath(srcPath)
    fs.remove(destPath)
        .then(() => console.log(` => ${destPath}`))
}

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