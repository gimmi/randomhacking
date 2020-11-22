import { promises as fs } from 'fs';

async function main() {
    const content = await fs.readFile('./package.json', { encoding: 'utf8' })
    console.log(content)
}

main()
