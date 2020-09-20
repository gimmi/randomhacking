const path = require('path');
const fs = require('fs').promises;
const vm = require('vm')

const commander = require('commander');

async function buildScripts(scriptDir, modules) {
    const scripts = {}
    for (const mod of modules) {
        for (const scriptName of mod.scripts) {
            if (!scripts[scriptName]) {
                const scriptPath = path.join(scriptDir, scriptName) + '.js'
                console.log('Loading script:', scriptPath)
                const scriptText = await fs.readFile(scriptPath);
                const script = new vm.Script(scriptText, {
                    filename: scriptPath,
                    lineOffset: 1,
                    columnOffset: 1
                })
                scripts[scriptName] = script
            }
        }
    }

    return scripts
}

async function main() {
    const args = commander.program
      .requiredOption('-i --input-dir <path>', 'input dir')
      .requiredOption('-o --output-dir <path>', 'output dir')
      .parse(process.argv);

    const scriptDir = args.inputDir

    const modulesPath = path.join(args.inputDir, 'modules.json')
    console.log('Loading modules:', modulesPath)
    const modulesJson = await fs.readFile(modulesPath);
    const modules = JSON.parse(modulesJson);
    console.dir(modules)

    const configPath = path.join(args.inputDir, 'config.json')
    console.log('Loading config:', configPath)
    const configJson = await fs.readFile(configPath);

    const scripts = await buildScripts(scriptDir, modules)

    for (const mod of modules) {

        const context = vm.createContext({
            INPUT: JSON.parse(configJson),
            OUTPUT: null
        });

        for (const scriptName of mod.scripts) {
            scripts[scriptName].runInContext(context)
        }

        console.log(context.OUTPUT)
    }

    return 0
}

main().catch(err => {
    console.error(err)
    return 1
}).then(code => process.exit(code));