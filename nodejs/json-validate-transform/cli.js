const commander = require('commander');
const jsonschema = require('jsonschema');
const liquid = require('liquid');
const path = require('path');
const json5 = require('json5')
const fs = require('fs').promises;

async function main() {
    const args = commander.program
      .requiredOption('-i --input-dir <path>', 'input dir')
      .requiredOption('-o --output-dir <path>', 'output dir')
      .parse(process.argv);

    const schema = json5.parse(await fs.readFile(path.join(args.inputDir, 'schema.json'), 'utf8'));
    const model = json5.parse(await fs.readFile(path.join(args.inputDir, 'model.json'), 'utf8'));

    const validatioResult = jsonschema.validate(model, schema)
    if (validatioResult.errors.length) {
        validatioResult.errors.forEach(console.log);
        return;
    }

    await fs.mkdir(args.outputDir, { recursive: true });

    const liquidEng = new liquid.Engine();
    const inputFiles = await fs.readdir(args.inputDir)
    await Promise.all(inputFiles
        .filter(name => path.extname(name) === '.liquid')
        .map(name => path.join(args.inputDir, name))
        .map(async templatePath => {
            const outputPath = path.join(args.outputDir, path.parse(templatePath).name + '.json');
            console.log(templatePath + ' -> ' + outputPath);

            const templateText = await fs.readFile(templatePath, 'utf8');
            const resultText = await liquidEng.parseAndRender(templateText, model);
            const resultJson = json5.parse(resultText);
            await fs.writeFile(outputPath, JSON.stringify(resultJson, null, 2));
        }));
    console.log("Done.");
}

main().catch(err => console.error(err));