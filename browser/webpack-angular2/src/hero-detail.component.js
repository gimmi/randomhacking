'use strict';

var Component = require('@angular/core').Component;

module.exports = Component({
	selector: 'my-hero-detail',
	template: require('./hero-detail.component.html'),
	inputs: ['hero'],
}).Class({
	constructor: function () { }
})
