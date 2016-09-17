// require('jasmine-core/lib/jasmine-core/jasmine.css');

// HACK to make jasmine setup for browser environment
global.jasmineRequire = require('jasmine-core/lib/jasmine-core/jasmine');

require('jasmine-core/lib/jasmine-core/jasmine-html');
require('jasmine-core/lib/jasmine-core/boot');

require('./UtilsSpecs');
