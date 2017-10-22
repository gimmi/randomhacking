#### [Node.js Quickstart](https://developers.google.com/sheets/api/quickstart/nodejs)

#### Setup account for Google Sheets API Access

* Go to [Google Cloud Console](https://console.cloud.google.com)
* Create an empty project
    * Name: Google Sheets API Access
* APIs & Services > Library
    * Select Google Sheets API
    * Click ENABLE
* APIs & Services > Credentials > OAuth consent screen
    * Select email
    * Name: MyApp
* APIs & Services > Credentials > Credentials > Create credentials > OAuth client ID
    * Application type: Other
    * Name: MyApp
    * Click on "Download JSON" icon. This is your `client_secret.json`
