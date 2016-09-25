'use strict';

var Component = require('@angular/core').Component;

module.exports = Component({
	selector: 'my-app',
	template: require('./app.component.html'),
}).Class({
	constructor: function () { 
		this.title = 'Tour of heroes'
	}
});
