require('core-js/shim.js');
require('zone.js/dist/zone.js');
require('reflect-metadata/Reflect.js');

var platformBrowserDynamic = require('@angular/platform-browser-dynamic').platformBrowserDynamic;
var AppModule = require('./app.module').AppModule;

var platform = platformBrowserDynamic();
platform.bootstrapModule(AppModule);
