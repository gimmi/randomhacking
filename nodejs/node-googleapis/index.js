const authorize = require('./authorize');
const util = require('util');
const googleapis = require('googleapis');

main().then(() => {
    console.log('Done.');
}, err => {
    console.error(err.stack);
    process.exit(1);
});

async function main() {
    const oauth2Client = await authorize();

    // See https://github.com/google/google-api-nodejs-client/#setting-global-or-service-level-auth
    googleapis.options({ auth: oauth2Client });

    await listMajors();
}

async function listMajors() {
    const sheets = googleapis.sheets('v4');
    const get = util.promisify(sheets.spreadsheets.values.get.bind(sheets.spreadsheets.values));

    const response = await get({
        spreadsheetId: '1LgBEV_wNPFfr-SjlncpPF1tCLdHXKQIhg83PidSUPfU',
        range: 'Spese!A2:E',
        valueRenderOption: 'UNFORMATTED_VALUE'
    });

    response.values.forEach(row => {
        const date = toDateObj(Math.trunc(row[0])).toDateString();
        const amount = row[1];
        const descr = row[4];

        console.log(`${amount} @ ${date}: ${descr}`);
    });
}

function toDateObj(value) {
    // See https://developers.google.com/sheets/api/reference/rest/v4/DateTimeRenderOption

    value -= 25569; // Difference in days between 30 December 1899 and 1 January 1970
    value *= 24*60; // Converted to minutes
    value += new Date().getTimezoneOffset(); // Normalized to UTC
    value *= 60*1000; // Converted to ms
    return new Date(value)
}
