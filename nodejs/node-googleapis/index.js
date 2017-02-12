const googleFacade = require('./google-facade')
const google = require('googleapis')
const promisify = require('./promisify')

googleFacade.createAuth().then(auth => {
  var sheets = google.sheets({ version: 'v4', auth: auth});
  promisify(sheets.spreadsheets.values.get, {
    spreadsheetId: '1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms',
    range: 'Class Data!A2:E',
  }).then(function(response) {
    console.log('Name, Major:');
    response.values.forEach(row => {
      console.log('%s, %s', row[0], row[4]);
    })
  }).catch(err => {
    console.log('The API returned an error: ' + err);
  })
})
