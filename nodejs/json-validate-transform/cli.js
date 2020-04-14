const commander = require('commander');
const liquid = require('liquid');
const path = require('path');
const json5 = require('json5')
const fs = require('fs').promises;

var Ajv = require('ajv');

async function main() {
    const args = commander.program
      .requiredOption('-i --input-dir <path>', 'input dir')
      .requiredOption('-o --output-dir <path>', 'output dir')
      .parse(process.argv);

    const modelText = await fs.readFile(path.join(args.inputDir, 'model.json'));
    const model = json5.parse(modelText, 'utf8');

    const schemaText = await fs.readFile(path.resolve(args.inputDir, model['$schema']));
    const schema = json5.parse(schemaText, 'utf8');

    var ajv = new Ajv({ schemas: [], allErrors: true });
    if (!ajv.validate(schema, model)) {
        console.error(ajv.errorsText(null, { dataVar: 'model', separator: '\n' }));
        return 1;
    }

    await fs.mkdir(args.outputDir, { recursive: true });

    const liquidEng = new liquid.Engine();
    const inputFiles = await fs.readdir(args.inputDir)
    await Promise.all(inputFiles
        .filter(name => path.extname(name) === '.liquid')
        .map(name => path.join(args.inputDir, name))
        .map(async templatePath => {
            const outputPath = path.join(args.outputDir, path.parse(templatePath).name + '.json');
            console.log(templatePath, '->', outputPath);

            const templateText = await fs.readFile(templatePath, 'utf8');
            const resultText = await liquidEng.parseAndRender(templateText, model);
            const resultJson = json5.parse(resultText);
            await fs.writeFile(outputPath, JSON.stringify(resultJson, null, 2));
        }));
}

main().catch(err => {
    console.error(err)
    return 1
}).then(code => process.exit(code));