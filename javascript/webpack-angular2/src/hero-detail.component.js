var Component = require('@angular/core').Component;
var Hero = require('./hero');

module.exports = Component({
	selector: 'my-hero-detail',
	template: require('./hero-detail.component.html'),
	inputs: ['hero'],
}).Class({
	constructor: function () { }
})
