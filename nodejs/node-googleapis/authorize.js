const util = require('util');
const fs = require('fs');
const readline = require('readline');
const GoogleAuth = require('google-auth-library');
const path = require('path');
const os = require('os');

const SCOPES = ['https://www.googleapis.com/auth/spreadsheets.readonly'];
const TOKEN_DIR = path.join(os.homedir(), '.credentials');
const TOKEN_PATH = path.join(TOKEN_DIR, 'sheets.googleapis.com-nodejs-quickstart.json');

module.exports = authorize;

async function authorize() {
    const readFile = util.promisify(fs.readFile);
    const clientSecretFile = require('./client_secret');

    const clientSecret = clientSecretFile.installed.client_secret;
    const clientId = clientSecretFile.installed.client_id;
    const redirectUrl = clientSecretFile.installed.redirect_uris[0];
    const auth = new GoogleAuth();
    const oauth2Client = new auth.OAuth2(clientId, clientSecret, redirectUrl);

    try {
        const token = await readFile(TOKEN_PATH);
        oauth2Client.credentials = JSON.parse(token);
    } catch(err) {
        const token = await getNewToken(oauth2Client);
        oauth2Client.credentials = token;
    }
    return oauth2Client;
}

async function getNewToken(oauth2Client) {
    const authUrl = oauth2Client.generateAuthUrl({
        access_type: 'offline',
        scope: SCOPES
    });
    console.log('Authorize this app by visiting this url:');
    console.log(authUrl);

    const getToken = util.promisify(oauth2Client.getToken.bind(oauth2Client));

    const code = await question('Enter the code from that page here: ');

    const token = await getToken(code);

    await storeToken(token);

    return token;
}

async function storeToken(token) {
    const mkdir = util.promisify(fs.mkdir);
    const writeFile = util.promisify(fs.writeFile);

    try {
        await mkdir(TOKEN_DIR);
    } catch (err) {
        if (err.code !== 'EEXIST') {
            throw err;
        }
    }

    await writeFile(TOKEN_PATH, JSON.stringify(token));
    console.log('Token stored to ' + TOKEN_PATH);
}

function question(question, validationRegex) {
    return new Promise(resolve => {
        const readlineInterface = readline.createInterface({
            input: process.stdin,
            output: process.stdout
        });

        readlineInterface.question(question, processAnswer);

        function processAnswer(answer) {
            if (!validationRegex || validationRegex.test(answer)) {
                readlineInterface.close();
                resolve(answer);
            } else {
                console.error(`Invalid answer '${answer}'`);
                readlineInterface.question(question, processAnswer);
            }
        }
    });
}
