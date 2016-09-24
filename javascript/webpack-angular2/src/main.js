var platformBrowserDynamic = require('@angular/platform-browser-dynamic').platformBrowserDynamic;
var AppModule = require('./app.module').AppModule;

var platform = platformBrowserDynamic();
platform.bootstrapModule(AppModule);
