const fs = require("fs")
const readline = require('readline')
const google = require('googleapis')
const GoogleAuth = require('google-auth-library')
const promisify = require('./promisify')

const path = require('path')
const os = require('os')

const winston = require('winston')

// If modifying these scopes, delete your previously saved credentials
// at ~/.credentials/sheets.googleapis.com-nodejs-quickstart.json
var SCOPES = ['https://www.googleapis.com/auth/spreadsheets.readonly'];

var TOKEN_PATH = path.resolve(os.homedir(),  '.credentials', 'sheets.googleapis.com-nodejs-quickstart.json')
winston.info('TOKEN_PATH = %s', TOKEN_PATH)

module.exports = {
	createAuth: createAuth
}

function createAuth() {
  return promisify(fs.readFile, 'client_secret.json').then(clientSecretJson => {
    return JSON.parse(clientSecretJson)
  }).then(cs => {
    var clientId = cs.installed.client_id;
    var clientSecret = cs.installed.client_secret;
    var redirectUrl = cs.installed.redirect_uris[0];

    var googleAuth = new GoogleAuth();
    var oauth2Client = new googleAuth.OAuth2(clientId, clientSecret, redirectUrl);

    return promisify(fs.readFile, TOKEN_PATH).then(credentialsJson => {
      return JSON.parse(credentialsJson)
    }).catch(err => {
      return generateNewCredentials(oauth2Client)
    }).then(credentials => {
      oauth2Client.credentials = credentials
      return oauth2Client
    })
  })
}

function generateNewCredentials(oauth2Client) {
  var authUrl = oauth2Client.generateAuthUrl({
    access_type: 'offline',
    scope: SCOPES
  });
  console.log('Authorize this app by visiting this url: ', authUrl);
  return question('Enter the code from that page here: ').then(code => {
    return promisify(oauth2Client.getToken, code)
  }).then(token => {
    storeToken(token);
    return token;
  }).catch(err => {
      console.log('Error while trying to retrieve access token', err);
  })
}

function question(question) {
  return new Promise((resolve, reject) => {
    var rl = readline.createInterface({
      input: process.stdin,
      output: process.stdout
    });
    rl.question(question, code => {
      rl.close()
      resolve(code)
    })
  })
}

function storeToken(token) {
  try {
    fs.mkdirSync(path.dirname(TOKEN_PATH));
  } catch (err) {
    if (err.code != 'EEXIST') {
      throw err;
    }
  }
  fs.writeFile(TOKEN_PATH, JSON.stringify(token));
  console.log('Token stored to ' + TOKEN_PATH);
}
