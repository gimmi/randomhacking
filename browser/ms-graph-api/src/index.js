/* globals Msal, MicrosoftGraph */

function buildAuthProvider() {
    const msalConfig = {
        auth: {
            // This is the "Application (client) ID" of the registered application
            // Need also this setting: https://github.com/microsoftgraph/microsoft-graph-toolkit/issues/91#issuecomment-494494475
            clientId: "<GUID>",

            // Must match with a "Redirect URIs" in the registered application
            redirectUri: "http://localhost:8080"
        }
    }
    const msalApplication = new Msal.UserAgentApplication(msalConfig);

    const graphScopes = ["user.read"]
    const options = new MicrosoftGraph.MSALAuthenticationProviderOptions(graphScopes)

    return new MicrosoftGraph.ImplicitMSALAuthenticationProvider(msalApplication, options);
}

function buildClient() {
    const Client = MicrosoftGraph.Client;
    return Client.initWithMiddleware({
        authProvider: buildAuthProvider()
    });
}

async function main() {
    const client = buildClient()

    const userDetails = await client.api("/me").get();
    console.dir(userDetails);
}

main().catch(console.error)
